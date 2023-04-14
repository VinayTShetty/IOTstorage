using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IotHubStorage.Repository;

namespace IottHubStorage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileStorageController : ControllerBase
    {
        [HttpPost("AddFile")]
        public async Task AddFile(string fileName)
        {
            await FileStorage.CreateFile(fileName);

        }

        [HttpPost("CreateDirectory")]
        public async Task CreateDirectory(string directoryName,string fileName)
        {
            await FileStorage.CreateDirectory(directoryName,fileName);

        }

        [HttpPut("UploadFile")]
        public async Task UploadFile(IFormFile file,string directoryName, string fileShareName)
        {
            await FileStorage.UploadFile(file,directoryName, fileShareName);

        }

        

       

        [HttpGet("GetAllFiles")]
        public async Task<List<string>>GetAllFiles(string directoryName, string fileShareName)
        {
            var data=await FileStorage.GetAllFile(directoryName, fileShareName);
            return data;

        }

        [HttpPost("DownloadFile")]
        public async Task DownloadFile(string directoryName, string fileShareName,string fileName)
        {
             await FileStorage.DownloadFile(directoryName, fileShareName,fileName);
            

        }

        [HttpDelete("DeleteFile")]
        public async Task DeleteFile(string directoryName, string fileShareName, string fileName)
        {
            await FileStorage.DeleteFile(directoryName, fileShareName, fileName);

        }

        [HttpDelete("DeleteDirectory")]
        public async Task DeleteDirectory(string directoryName, string fileShareName)
        {
            await FileStorage.DeleteDirectory(directoryName, fileShareName);

        }

        [HttpDelete("DeletFileFolder")]
        public async Task DeleteFileFolder(string fileName)
        {
            await FileStorage.DeleteFileFolder(fileName);

        }





    }
}
