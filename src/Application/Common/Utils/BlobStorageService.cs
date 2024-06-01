using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.File;

namespace Application.Common.Utils;

public class BlobStorageService(IConfiguration configuration)
{
    public string ConnectionString { get; set; } = configuration.GetSection("AzureStorage:ConnectionString").Value ?? string.Empty;
    public string StorageAccountName { get; set; } = configuration.GetSection("AzureStorage:StorageAccountName").Value ?? string.Empty;
    public string ContainerName { get; set; } = configuration.GetSection("AzureStorage:ContainerName").Value ?? string.Empty;

    public async Task UploadFile(FileStream file)
    {
        BlobServiceClient blobService = new BlobServiceClient(ConnectionString);
        BlobContainerClient containerClient = blobService.GetBlobContainerClient(ContainerName);
        var blobClient = containerClient.GetBlobClient("Prueba.csv");
        await blobClient.UploadAsync(file, true);
    }

    //public async Task<List<string>> GetNameList()
    //{
    //    var blobs = new List<string>();
    //    await foreach (var blobItem in _containerClient.GetBlobsAsync())
    //    {
    //        blobs.Add(blobItem.Name);
    //    }
    //    return blobs;
    //}

    public async Task DownloadFile()
    {
        try
        {
            BlobServiceClient blobService= new BlobServiceClient(ConnectionString);
            BlobContainerClient containerClient = blobService.GetBlobContainerClient(ContainerName);
            BlobClient blobClient = containerClient.GetBlobClient("BATCO_price_20240530.csv");

            await blobClient.DownloadToAsync("C:\\Users\\Mainsoft\\Desktop\\Azure\\Valor.csv");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }
}
