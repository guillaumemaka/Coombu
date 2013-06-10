using Coombu.Models;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;

namespace Coombu.Formatters
{
    public class PngMediaTypeFormatter : BufferedMediaTypeFormatter
    {
        private CloudStorageAccount accountStorage;
        private CloudBlobClient blobClient;
        private CloudBlobContainer container;

        public PngMediaTypeFormatter() 
        {
            accountStorage = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            blobClient = accountStorage.CreateCloudBlobClient();
            container = blobClient.GetContainerReference(CloudConfigurationManager.GetSetting("ContainerName"));
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("image/png"));
        }

        public override void WriteToStream(Type type, object value, System.IO.Stream writeStream, System.Net.Http.HttpContent content)
        {
            var post = value as Post;
            if (post != null)
            {
                CloudBlockBlob blobBlock = container.GetBlockBlobReference(post.Picture);
                blobBlock.DownloadToStream(writeStream);
            }         
        }

        public override bool CanReadType(Type type)
        {
            return typeof(Post).Equals(type);            
        }

        public override bool CanWriteType(Type type)
        {
            return false;
        }
    }
}