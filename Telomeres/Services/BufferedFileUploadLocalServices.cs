using Telomeres.Interfaces;

namespace Telomeres.Services
{
    public class BufferedFileUploadLocalServices : IBufferedFileUploadService
    {
        private const string DOWNLOAD_PATH = "UploadRepository";
        private const string FILE_EXTENSION = ".amxd";

        public async Task<bool> UploadFile(IFormFile file, string temporal_filename)
        {


            string path = "";

            /*check directory exists*/
            path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, DOWNLOAD_PATH));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            /*check file length is valid*/
            if (file.Length == 0)
            {
                return false;
            }


            /*move uploaded file to repository folder*/
            try
            {
                using (var fileStream = new FileStream(Path.Combine(path, temporal_filename + FILE_EXTENSION), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("File upload failed", ex);
            }

        }
    }
}
