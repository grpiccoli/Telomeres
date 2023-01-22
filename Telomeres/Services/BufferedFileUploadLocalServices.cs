using Telomeres.Interfaces;

namespace Telomeres.Services
{
    public class BufferedFileUploadLocalServices : IBufferedFileUploadService
    {
        public async Task<bool> UploadFile(IFormFile file)
        {

            string path = "";

            if (file.Length == 0)
            {
                return false;
            }

            try
            {
                path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFile"));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
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
