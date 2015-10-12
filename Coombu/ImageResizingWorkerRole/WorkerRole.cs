using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Coombu.Utils;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace ImageResizingWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        CloudStorageAccount accountStorage;
        CloudQueueClient queueClient;
        CloudQueue queue;
        CloudBlobClient blobClient;
        CloudBlobContainer blobContainer;


        public override void Run()
        {
            // Ceci est un exemple d'implémentation de travail. Remplacez-le par votre logique.
            Trace.WriteLine("ImageResizingWorkerRole entry point called", "Information");

            while (true)
            {
                Trace.WriteLine("Image Resizing Worker Role Working", "Information");
                CloudQueueMessage message = queue.GetMessage(new TimeSpan(0, 5, 0));
                
                string[] blobInfo;
                
                if(message != null) 
                { 
                    blobInfo = message.AsString.Split(','); 
                }
                else
                { 
                    blobInfo = null; 
                }
                          
                if (blobInfo != null)
	            {
                    if (blobInfo.Length == 2)
                    {
                        ResizeImage(blobInfo[0], blobInfo[1]);
                        queue.DeleteMessage(message);
                    }
                    else
                    {
                        queue.DeleteMessage(message);
                    }
	            }              
            }
        }

        public override bool OnStart()
        {
            // Définissez le nombre maximal de connexions simultanées 
            ServicePointManager.DefaultConnectionLimit = 12;

            // Pour plus d'informations sur la gestion des modifications de configuration
            // consultez la rubrique MSDN à l'adresse http://go.microsoft.com/fwlink/?LinkId=166357.
            accountStorage = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));
            queueClient = accountStorage.CreateCloudQueueClient();

            queue = queueClient.GetQueueReference(RoleEnvironment.GetConfigurationSettingValue("QueueName"));
            queue.CreateIfNotExists();

            blobClient = accountStorage.CreateCloudBlobClient();
            blobContainer = blobClient.GetContainerReference(RoleEnvironment.GetConfigurationSettingValue("ContainerName"));

            blobContainer.CreateIfNotExists();            

            return base.OnStart();
        }

        // blobDefinition -> "<postId>:<blobUrl>" 
        // will generate /post-<postId>/image_<Small|Medium|Large>_<guid>.<ext>
        private void ResizeImage(string postId, string blobUrl)
        {                                                
            string ext = Path.GetExtension(blobUrl);
            string guid = GuidFinder.find(blobUrl);

            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(blobUrl);
            blob.FetchAttributes();
            string imageMimeType = blob.Properties.ContentType;

            Trace.WriteLine("Resiszing " + blob.Name);

            Stream inputStream;
            //MemoryStream outputStream = new MemoryStream();

            inputStream = blob.OpenRead();

            Image originalImage = Image.FromStream(inputStream);
            Image thumbSmall;
            Image thumbMedium;
            Image thumbLarge;

            Trace.WriteLine("Resize to Small");
            thumbSmall = _resize(originalImage, new Size(64, 64), false);
            Trace.WriteLine("Resize to Medium");
            thumbMedium = _resize(originalImage, new Size(128, 128), true);
            Trace.WriteLine("Resize to Large");
            thumbLarge = _resize(originalImage, new Size(256, 256), true);

            // Encoder parameter for image quality
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);

            // Get image codec
            ImageCodecInfo codec = getEncoderInfo(imageMimeType);

            if (codec == null)
            {
                Trace.WriteLine("No codec found for " + imageMimeType);
                thumbSmall.Dispose();
                thumbMedium.Dispose();
                thumbLarge.Dispose();
                inputStream.Close();
                return;
            }

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            try
            {
                var thumbBlob = blobContainer.GetBlockBlobReference(string.Format("/post-{0}/image_Small_{1}{2}",postId, guid, ext));
                thumbBlob.Properties.ContentType = imageMimeType;
                using (var stream = thumbBlob.OpenWrite())
                {
                    thumbSmall.Save(stream, codec, encoderParams);
                }
                Trace.WriteLine("Small image " + thumbBlob.Name);

                thumbBlob = blobContainer.GetBlockBlobReference(string.Format("/post-{0}/image_Medium_{1}{2}", postId, guid, ext));
                thumbBlob.Properties.ContentType = imageMimeType;
                using (var stream = thumbBlob.OpenWrite())
                {
                    thumbMedium.Save(stream, codec, encoderParams);
                }
                Trace.WriteLine("Medium image " + thumbBlob.Name);

                thumbBlob = blobContainer.GetBlockBlobReference(string.Format("/post-{0}/image_Large_{1}{2}", postId, guid, ext));
                thumbBlob.Properties.ContentType = imageMimeType;
                using (var stream = thumbBlob.OpenWrite())
                {
                    thumbLarge.Save(stream, codec, encoderParams);
                }
                Trace.WriteLine("Large image " + thumbBlob.Name);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
            }

            // Release all resources when they no longer use
            thumbSmall.Dispose();
            thumbMedium.Dispose();
            thumbLarge.Dispose();
            inputStream.Close();            
        }

        private ImageCodecInfo getEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }

        private Image _resize(Image imgToResize, Size size, bool keepAspect = true)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            int destWidth;
            int destHeight;

            if (keepAspect)
            {
                nPercentW = ((float)size.Width / (float)sourceWidth);
                nPercentH = ((float)size.Height / (float)sourceHeight);

                if (nPercentH < nPercentW)
                    nPercent = nPercentH;
                else
                    nPercent = nPercentW;

                destWidth = (int)(sourceWidth * nPercent);
                destHeight = (int)(sourceHeight * nPercent);
            }
            else
            {
                destWidth = size.Width;
                destHeight = size.Height;
            }

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }

    }
}
