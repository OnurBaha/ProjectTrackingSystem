using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Helpers;

public interface IFileHelper
{
    Task<string> Add(IFormFile file,string destinationFolderPath);
    Task Update(IFormFile file, string oldFilePath);
    Task Delete(string deletedFilePath);
}
