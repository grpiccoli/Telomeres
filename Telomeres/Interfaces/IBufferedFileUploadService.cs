namespace Telomeres.Interfaces
{
    public interface IBufferedFileUploadService
    {
        Task<bool> UploadFile(IFormFile file);
    }
}
