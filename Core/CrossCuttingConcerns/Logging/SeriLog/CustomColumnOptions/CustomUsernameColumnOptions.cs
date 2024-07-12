using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

namespace Core.CrossCuttingConcerns.Logging.SeriLog.CustomColumnOptions;

public static class CustomUsernameColumnOptions
{
    public static ColumnOptions CreateUsernameColumnOptions()
    {
        var customColumnOptions = new ColumnOptions
        {
            AdditionalColumns = new Collection<SqlColumn>
            {
                 new SqlColumn
                 {
                    ColumnName = "Username",
                    DataType = SqlDbType.NVarChar,
                    DataLength = 255,
                    AllowNull = true,
                 }
            }
        };
        return customColumnOptions;
    }
}

