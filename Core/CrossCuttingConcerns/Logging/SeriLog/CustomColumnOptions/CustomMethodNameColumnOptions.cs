using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

namespace Core.CrossCuttingConcerns.Logging.SeriLog.CustomColumnOptions;

public static class CustomMethodNameColumnOptions
{
    public static ColumnOptions CreateMethodNameColumnOptions()
    {
        var methodNameColumnOptions = new ColumnOptions
        {
            AdditionalColumns = new Collection<SqlColumn>
            {
                new SqlColumn
                {
                    ColumnName = "MethodName",
                    DataType = SqlDbType.NVarChar,
                    DataLength = 4000,
                    AllowNull = true,
                }
            }
        };
        return methodNameColumnOptions;
    }
}
