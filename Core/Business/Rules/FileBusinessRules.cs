using Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails.Types;
using Core.Messages;

namespace Core.Business.Rules;

public class FileBusinessRules : BaseBusinessRules
{
    public async Task CheckFileExist(string filePath)
    {
        var result = File.Exists(filePath);

        if (!result)
        {
            throw new BusinessException(CoreMessages.FileNotFound);
        }

    }


    public async Task IsExistDestinationFolder(string destinationFolderPath)
    {
        if (!Directory.Exists(destinationFolderPath))
        {
            CreateDestinationFolder(destinationFolderPath);
        }
    }


    public void CreateDestinationFolder(string destinationFolderPath)
    {
        Directory.CreateDirectory(destinationFolderPath);
    }

    public async Task CheckFileExtension(string fileName)
    {
        if (!fileName.Contains("png") || !fileName.Contains("pdf") || !fileName.Contains("jpg") || !fileName.Contains("jpeg"))
        {
            throw new BusinessException(CoreMessages.InvalidExtension);

        }
    }
}
