using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;


namespace CSDDataMorpher
{
    public class BlobConnector
    {
        private readonly string _connectionString;
        CloudBlobClient cloudBlobClient = null;
        CloudBlobContainer cloudBlobContainer = null;
        CloudBlockBlob cloudBlockBlob = null;
        public BlobConnector(string connectionString,string containerName)
        {
            _connectionString = connectionString;
            GetConnection(connectionString, containerName);
        }


        public void GetConnection(string connectionString,string containerName)
        {
            CloudStorageAccount storageAccount;
            if (CloudStorageAccount.TryParse(connectionString, out storageAccount))
            {
                // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.
                cloudBlobClient = storageAccount.CreateCloudBlobClient();

                // Create a container called 'quickstartblobs' and append a GUID value to it to make the name unique. 
                cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
            }
            else
            {
                // error message
            }
        }

        public string Download(string filePath,string relativeContainerPath = "")
        {
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                var fileName = filePath.Substring(filePath.LastIndexOf("/") + 1);
                // Get a reference to the blob address, then upload the file to the blob.
                // Use the value of localFileName for the blob name.
                if (relativeContainerPath != "") fileName = relativeContainerPath;
                cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
                var stream = new MemoryStream();
                cloudBlockBlob.DownloadToStream(stream);
                var data = UTF8Encoding.UTF8.GetString(stream.ToArray());
                return data;

            }
            else return "";
        }
    }
}
