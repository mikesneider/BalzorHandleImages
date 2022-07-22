using System;
using System.Threading.Tasks;
using MediatR;
using System.Threading;
using BalzorHandleImages.Services;
using Azure.Storage.Blobs.Models;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;

namespace BalzorHandleImages.Commands
{
    public record DownloadImage(string ImgName) : INotification;
    public class DownloadImages : INotificationHandler<DownloadImage>
    {
        private readonly IBlobFactory _blobFactory;

        public DownloadImages(IBlobFactory blobFactory)
        {
            _blobFactory = blobFactory ?? throw new ArgumentNullException(nameof(blobFactory));
        }

        
        public async Task Handle(DownloadImage notification, CancellationToken cancellationToken)
        {
            //no existe método para traer Contenedor, lo crea o lo trae
            var blobContainer = await _blobFactory.GetContainer("uploaded-images", cancellationToken);
            var cloudFile = blobContainer.GetBlobClient(notification.ImgName);
            Stream fileStream = new MemoryStream();
            cloudFile.DownloadTo(fileStream);
            //return fileStream;
            //Puedo transformat filestream a Base64?
            Console.WriteLine(cloudFile.Uri);


            var blobitem = blobContainer.GetBlobs(
                Azure.Storage.Blobs.Models.BlobTraits.None,
                Azure.Storage.Blobs.Models.BlobStates.None,
                notification.ImgName,
                cancellationToken: cancellationToken);
            foreach(var item in blobitem)
            {
                Console.WriteLine(item.Name);
                //Console.WriteLine(item.
            }
            Console.WriteLine("Mi item especifico");
            Console.WriteLine(blobitem.GetType());

            //var account = new CloudStorageAccount(new StorageCredentials(accountName, accountKey), true);
            //var cloudBlobClient = account.CreateCloudBlobClient();
            //var container = cloudBlobClient.GetContainerReference("container-name");
            //var blob = blobContainer.GetBlockBlobReference("image.png");
            //blob.UploadFromFile("File Path ....");//Upload file....

            //var blobUrl = blob.Uri.AbsoluteUri;
            //BlobItem vs BlobInfo?
            //A storage account have: An Account, a Client, a Container an a Blob
            // Get a reference to the public blob at https://aka.ms/bloburl

            // Download the blob
            //Response<BlobDownloadInfo> download = blob.Download();
            //using (FileStream file = File.OpenWrite("hello.jpg"))
            //{
            //    download.Value.Content.CopyTo(file);
            //}


            foreach (var item in blobContainer.GetBlobs()) //but I do not want to read all the images I 
            {
                Console.WriteLine(item.Name);
                //Console.WriteLine(item.GetType());
            }


            

        }

    }
}
