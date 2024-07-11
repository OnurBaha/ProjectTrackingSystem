using Microsoft.AspNetCore.Http;

namespace Core.Utilities.FileUpload;

public interface IFileUploadAdapter
{
    Task<string> Upload(IFormFile file);
    Task Delete(string imageUrl);
    Task<string> Update(IFormFile formFile, string imageUrl);
}