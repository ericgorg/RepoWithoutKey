

using Azure.Identity;
using Azure.Storage.Blobs;


var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


//string key = "DefaultEndpointsProtocol=https;AccountName=ericgignite2024;AccountKey=KEY;EndpointSuffix=core.windows.net";
string containerName = "container1";
string blobName = "Ignite2024.txt";
//endpoint for Storage account
Uri containerUri = new Uri("https://ericgignite2024.blob.core.windows.net/container");


app.MapGet("/readBlobContent", async () =>
    {
        try
        {
            //create storage account client
            //var blobServiceClient = new BlobServiceClient(key);

            //create container client
            //var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            //create the container client with integrated security
            BlobContainerClient containerClient = new BlobContainerClient(containerUri, new DefaultAzureCredential());

            //create blob client
            var blobClient = containerClient.GetBlobClient(blobName);

            //download blob content
            var response = await blobClient.DownloadAsync();

            //read content
            var content = await new StreamReader(response.Value.Content).ReadToEndAsync();
            return content;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    });


app.Run();


