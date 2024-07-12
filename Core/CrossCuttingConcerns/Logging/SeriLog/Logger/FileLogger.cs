using Core.CrossCuttingConcerns.Logging.SeriLog.ConfigurationModels;
using Core.CrossCuttingConcerns.Logging.SeriLog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging.SeriLog.Logger;

public class FileLogger : LoggerServiceBase
{
    public FileLogger(IConfiguration configuration)
    {
        FileLogConfiguration logConfig =
            configuration.GetSection("SeriLogConfigurations:FileLogConfiguration").Get<FileLogConfiguration>()
            ?? throw new Exception(SerilogMessages.NullOptionsMessage);

        string logFilePath = string.Format(format: "{0}{1}", arg0: Directory.GetCurrentDirectory() + logConfig.FolderPath, arg1: ".txt");

        Logger = new LoggerConfiguration().WriteTo.File(
             logFilePath, 
             rollingInterval: RollingInterval.Day,
             retainedFileCountLimit: null,
             fileSizeLimitBytes: 5000000,
             shared: true,
             encoding: Encoding.UTF8,
             outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {Message}{NewLine}{Exception}"
             ).CreateLogger();
    }
}