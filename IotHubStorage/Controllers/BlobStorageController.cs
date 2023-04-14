using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Blobs.Models;
using IotHubStorage.Repository;

namespace IotHubStorage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobStorageController : ControllerBase
    {

        [HttpPost("AddBlob")]
        public async Task<string>AddBlob(string blobName)
        {
            await BlobStorage.CreateBlob(blobName);
            return null;
        }

        [HttpPut("UpdateBlobContent")]
        public async Task UpdateBlobContent(string blobName,IFormFile file)
        {
            await BlobStorage.UpdateBlobContent(blobName, file);
            
        }

        [HttpGet("GetBlobContent")]
        public async Task<BlobProperties> GetBlobContent(string blobName, string file)
        {
           var data= await BlobStorage.GetBlobContent(blobName, file);
            return data;
        }

        [HttpGet("GetBlob")]
        public async Task<List<string>> GetBlob(string blobName, string file)
        {
            var data=await BlobStorage.GetBlob(blobName, file);
            return data ;
        }

        [HttpGet("DownloadBlobContent")]
        public async Task<BlobProperties> DownloadBlob(string blobName, string file)
        {
            var data=await BlobStorage.DownloadBlob(blobName, file);
            return data;
        }

        [HttpDelete("DeleteBlobContent")]
        public async Task<string> DeleteBlobContent(string blobName, string file)
        {
            await BlobStorage.DeleteBlobContent(blobName, file);
            return null;
           

        }
        [HttpDelete("DeleteBlob")]
        public async Task<string> DeleteBlob(string blobName)
        {
            await BlobStorage.DeleteBlob(blobName);
            return null;


        }


    }
}
