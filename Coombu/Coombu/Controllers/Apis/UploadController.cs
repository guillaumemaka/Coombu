using Coombu.Models;
using Coombu.Models.Repositories;
using Coombu.Models.ViewModels;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Coombu.Controllers.Apis
{
    public class UploadController : ApiController
    {
        private CloudBlobContainer _container;
        private CloudStorageAccount _storageAccount;
        private CloudBlobClient _blobClient;
        private CloudQueueClient _queueClient;
        private CloudQueue _queue;
        private readonly IPostRepository _repository;

        public UploadController(IPostRepository repo)
        {
            _repository = repo;
            _storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            _blobClient = _storageAccount.CreateCloudBlobClient();
            _container = _blobClient.GetContainerReference(CloudConfigurationManager.GetSetting("ContainerName"));
            _queueClient = _storageAccount.CreateCloudQueueClient();
            _queue = _queueClient.GetQueueReference(CloudConfigurationManager.GetSetting("QueueName"));
        }

        public async Task<HttpResponseMessage> Post()
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "contenu non supporté");
            }
            else
            {
                var provider = new MultipartFormDataStreamProvider(Path.GetTempPath());
                var response = await Request.Content.ReadAsMultipartAsync(provider)
                    .ContinueWith<HttpResponseMessage>(t =>
                    {
                        if (t.IsFaulted)
                            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,t.Exception);

                        var fileData = provider.FileData[0];
                        string postContent = provider.FormData["Content"];
                        Post newPost = _repository.Add(new Post { Content = postContent });

                        string fileName = Path.GetFileName(fileData.Headers.ContentDisposition.FileName.Trim('"'));

                        string uniqueBlobName = string.Format("/posts-{0}/image_Original_{1}{2}",newPost.PostId, Guid.NewGuid().ToString(), Path.GetExtension(fileName));
                        CloudBlockBlob blockBlob = _container.GetBlockBlobReference(uniqueBlobName);
                        blockBlob.Properties.ContentType = fileData.Headers.ContentType.MediaType;

                        using (var fs = new FileStream(fileData.LocalFileName, FileMode.Open, FileAccess.Read, FileShare.Read, 1024, true))
                        {
                            blockBlob.UploadFromStream(fs);
                        }

                        File.Delete(fileData.LocalFileName);

                        newPost.Picture = blockBlob.Uri.ToString();
                        _repository.Update(newPost);

                        // Add the uploaded image to the queue for resizing process 
                        CloudQueueMessage message = new CloudQueueMessage(string.Format("{0},{1}",newPost.PostId,newPost.Picture));
                        _queue.AddMessage(message);

                        ApiResponse<Post> responseMessage = new ApiResponse<Post>(newPost);
                        responseMessage.AddMetatData("status", HttpStatusCode.Created);

                        return Request.CreateResponse<ApiResponse<Post>>(HttpStatusCode.Created, responseMessage);
                    });

                return response;
            }                                        
        }    
    }
}
