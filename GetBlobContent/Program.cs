

using Azure.Identity;
using Azure.Storage.Blobs;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


//string key = "DefaultEndpointsProtocol=https;AccountName=ericgignite2024;AccountKey=KEY;EndpointSuffix=core.windows.net";

//endpoint for Storage account
Uri containerUri = new Uri("https://ericgignite2024.blob.core.windows.net/container");


app.MapGet("/writeblob", async () =>
    {
        try
        {
            //create storage account client
            //var blobServiceClient = new BlobServiceClient(key);

            //create container client
            //var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            //create the container client with integrated security
            BlobContainerClient containerClient = new BlobContainerClient(containerUri, new DefaultAzureCredential());

            //generate content
            string message = DateTime.Now.ToString();
            byte[] byteArray = Encoding.ASCII.GetBytes(message);

            //generate blobname
            string blobName = Guid.NewGuid().ToString();

            //upload blob     
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                await containerClient.UploadBlobAsync(blobName, stream);
            }

            return blobName;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    });


app.Run();


