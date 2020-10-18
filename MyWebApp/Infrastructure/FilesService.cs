using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApp.Infrastructure
{
    public interface IFilesService
    {
        Task UploadFile(string fileName, Stream file);

        Task<IEnumerable<string>> GetList();

        Task<Stream> DownloadFile(string fileName);
    }

    public class FilesService: IFilesService
    {
        private readonly string _connectionString = @"DefaultEndpointsProtocol=https;AccountName=storageaccount999991;AccountKey=Tbo0MJJMJ6V9cXI+D2C8IWo0UL+wzUtaDUglQD0I4H/gdef0CJHos5uTbaRKS52HiWxzmZClm7x+vymiKMBkSQ==";

        private readonly string _contanerName = "blobdata";

        public async Task UploadFile(string fileName, Stream file)
        {         
            var container = new BlobContainerClient(_connectionString, _contanerName);

            await container.UploadBlobAsync(fileName, file);
        }

        public async Task<IEnumerable<string>> GetList()
        {
            var container = new BlobContainerClient(_connectionString, _contanerName);

            var blobs = container.GetBlobs();

            return blobs.Select(x => x.Name);
        }

        public async Task<Stream> DownloadFile(string fileName)
        {
            var container = new BlobContainerClient(_connectionString, _contanerName);

            var output = new MemoryStream();

            var res = await container.GetBlobClient(fileName).DownloadToAsync(output);

            return output;
        }
    }
}
