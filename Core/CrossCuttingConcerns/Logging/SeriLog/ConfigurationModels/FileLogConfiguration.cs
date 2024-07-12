namespace Core.CrossCuttingConcerns.Logging.SeriLog.ConfigurationModels;

public class FileLogConfiguration
{
    public string FolderPath { get; set; }

    public FileLogConfiguration()
    {
        FolderPath = string.Empty;
    }

    public FileLogConfiguration(string folderPath)
    {
        FolderPath = folderPath;
    }
}
