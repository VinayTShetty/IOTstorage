using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;


namespace IotHubStorage.Repository
{
    public class FileStorage
    {
        public static string connectionString = "DefaultEndpointsProtocol=https;AccountName=vtshubstor;AccountKey=kAHuGYDMmyceoMOiokHBTKdzcYKR4HvnusqhRRov7kgXe7s6HKyFYb7IqRjGTxk7pZ0Rgcf8wrUd+ASt+0LTYQ==;EndpointSuffix=core.windows.net";
        public static ShareServiceClient serviceClient = null;

        public static async Task CreateFile(string filename)
        {
            try
            {
                serviceClient =  new ShareServiceClient(connectionString);
                var sharedService = serviceClient.GetShareClient(filename);
                await sharedService.CreateIfNotExistsAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public static async Task CreateDirectory(string directoryName,string filename)
        {
            try
            {
                serviceClient = new ShareServiceClient(connectionString);
                var sharedService = serviceClient.GetShareClient(filename);
                ShareDirectoryClient rootDirectory = sharedService.GetRootDirectoryClient();
                ShareDirectoryClient fileDirectory = rootDirectory.GetSubdirectoryClient(directoryName);
                await fileDirectory.CreateIfNotExistsAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static async Task UploadFile(IFormFile file,string directoryName, string fileSharedName)
        {
            string fileName = file.FileName;
            serviceClient = new ShareServiceClient(connectionString);
            var sharedService = serviceClient.GetShareClient(fileSharedName);
            var directory = sharedService.GetDirectoryClient(directoryName);
            var fileStorage = directory.GetFileClient(fileName);
            await using(var data=file.OpenReadStream())
            {
                await fileStorage.CreateAsync(data.Length);
                await fileStorage.UploadAsync(data);
            }
        }

        public static async Task DeleteDirectory(string directoryName,string fileSharedName)
        {
            serviceClient = new ShareServiceClient(connectionString);
            var sharedService = serviceClient.GetShareClient(fileSharedName);
            var directory = sharedService.GetDirectoryClient(directoryName);
            await directory.DeleteAsync();
        }

        public static async Task DeleteFile(string directoryName, string fileSharedName,string fileName)
        {
            serviceClient = new ShareServiceClient(connectionString);
            var sharedService = serviceClient.GetShareClient(fileSharedName);
            var directory = sharedService.GetDirectoryClient(directoryName);
            var file = directory.GetFileClient(fileName);
            await file.DeleteAsync();
        }
        public static async Task DeleteFileFolder( string fileName)
        {
           serviceClient = new ShareServiceClient(connectionString);
            var ServiceClient =serviceClient.GetShareClient(fileName);
            
            await ServiceClient.DeleteIfExistsAsync();
        }


        public static async Task<List<string>> GetAllFile(string directoryName,string fileSharedName)
        {
            serviceClient = new ShareServiceClient(connectionString);
             var sharedService = serviceClient.GetShareClient(fileSharedName);
            var file = sharedService.GetRootDirectoryClient();
            var Directory = sharedService.GetDirectoryClient(directoryName);
            List<string> name = new List<string>();
            await foreach(ShareFileItem item in Directory.GetFilesAndDirectoriesAsync())
            {
                name.Add(item.Name);
            }
            return name;
            

        }

        public static async Task DownloadFile(string directoryName,string fileShareName,string fileName)
        {
            string path = @"C:\Users\vmadmin\Desktop\Azure IOT 301\IotHubStorage\Downloads\" + fileName;
            serviceClient = new ShareServiceClient(connectionString);
            var sharedService = serviceClient.GetShareClient(fileShareName);
            var directory = sharedService.GetDirectoryClient(directoryName);
            var file = directory.GetFileClient(fileName);
            ShareFileDownloadInfo dwld =  await file.DownloadAsync();
            using(FileStream stream= File.OpenWrite(path))
            {
                await dwld.Content.CopyToAsync(stream);
            }
        }
    }
}
