using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.IO;
using Coombu.Utils;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.ServiceRuntime;

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
            accountStorage = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));
            queueClient = accountStorage.CreateCloudQueueClient();

            queue = queueClient.GetQueueReference(RoleEnvironment.GetConfigurationSettingValue("QueueName"));
            queue.CreateIfNotExists();

            blobClient = accountStorage.CreateCloudBlobClient();
            blobContainer = blobClient.GetContainerReference(RoleEnvironment.GetConfigurationSettingValue("ContainerName"));

            blobContainer.CreateIfNotExists();   

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
                CloudBlockBlob blockBlob = blobClient.GetContainerReference(RoleEnvironment.GetConfigurationSettingValue("ContainerName")).GetBlockBlobReference(image);
                blockBlob.DeleteIfExists();
            }
        }
    }
}
