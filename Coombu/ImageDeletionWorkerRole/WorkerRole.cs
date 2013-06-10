using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;
using System.IO;
using Coombu.Utils;

namespace ImageDeletionWorkerRole
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
            Trace.WriteLine("ImageDeletionWorkerRole entry point called", "Information");

            while (true)
            {
                Thread.Sleep(5000);
                Trace.WriteLine("Image Deletion Worker Role Working", "Information");
                
                string[] blobInfo;

                CloudQueueMessage message 
                    = queue.GetMessage(new TimeSpan(0,5,0));
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
                        deleteImages(blobInfo[0], blobInfo[1]);
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
            accountStorage = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            queueClient = accountStorage.CreateCloudQueueClient();

            queue = queueClient.GetQueueReference(CloudConfigurationManager.GetSetting("QueueName"));
            queue.CreateIfNotExist();

            blobClient = accountStorage.CreateCloudBlobClient();
            blobContainer = blobClient.GetContainerReference(CloudConfigurationManager.GetSetting("ContainerName"));

            blobContainer.CreateIfNotExist();   

            return base.OnStart();
        }

        private void deleteImages(string postId, string blobUrl)
        {
            string ext = Path.GetExtension(blobUrl);
            string guid = GuidFinder.find(blobUrl);

            List<string> images = new List<string>
            {
                string.Format("/post-{0}/image_Original_{1}{2}",postId,guid,ext),
                string.Format("/post-{0}/image_Small_{1}{2}",postId,guid,ext),
                string.Format("/post-{0}/image_Medium_{1}{2}",postId,guid,ext),
                string.Format("/post-{0}/image_Large_{1}{2}",postId,guid,ext)
            };

            foreach (var image in images)
            {
                CloudBlockBlob blockBlob = blobClient.GetBlockBlobReference(image);
                blockBlob.DeleteIfExists();
            }
        }
    }
}
