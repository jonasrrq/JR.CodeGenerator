namespace JR.CodeGenerator.Services;

/// <summary>
/// clsSQLToCsharp
/// </summary>
/// TODO Edit XML Comment Template for clsSQLToCsharp
public class clsSQLToCsharp
{
    /// <summary>
    /// SQLs to csharp.
    /// </summary>
    /// <param name="TipoDato">The tipo dato.</param>
    /// <returns></returns>
    /// <exception cref="System.Exception">Invalido el tipo de Dato en SQL Server: " + TipoDato</exception>
    public static string SQLToCsharp(string TipoDato)
    {
        switch (TipoDato.ToLower())
        {
            case "binary":
                return "byte[]";
            case "bigint":
                return "long";
            case "bit":
                return "bool";
            case "char":
                return "string";
            case "datetime2":
                return "DateTime";
            case "datetime":
                return "DateTime";
            case "date":
                return "DateTime";
            case "time":
                return "TimeSpan";
            case "decimal":
                return "decimal";
            case "float":
                return "float";
            case "image":
                return "byte[]";
            case "int":
                return "int";
            case "money":
                return "decimal";
            case "nchar":
                return "string";
            case "ntext":
                return "string";
            case "nvarchar":
                return "string";
            case "numeric":
                return "decimal";
            case "real":
                return "decimal";
            case "smalldatetime":
                return "DateTime";
            case "smallint":
                return "short";
            case "smallmoney":
                return "float";
            case "sql_variant":
                return "byte[]";
            case "sysname":
                return "string";
            case "text":
                return "string";
            case "timestamp":
                return "DateTime";
            case "tinyint":
                return "byte";
            case "varbinary":
                return "byte[]";
            case "varchar":
                return "string";
            case "uniqueidentifier":
                return "Guid";
            default:
                throw (new Exception("Invalido el tipo de Dato en SQL Server: " + TipoDato));

        }
    }

    /// <summary>
    /// Defaults the value.
    /// </summary>
    /// <param name="TipoDato">The tipo dato.</param>
    /// <returns></returns>
    /// <exception cref="System.Exception">Invalido el tipo de Dato en SQL Server: " + TipoDato</exception>
    public static string DefaultValue(string TipoDato)
    {
        switch (TipoDato.ToLower())
        {
            case "binary":
                return "new byte[0]";
            case "bigint":
                return "0";
            case "bit":
                return "false";
            case "char":
                return "String.Empty";
            case "datetime":
                return "DateTime.Now";
            case "date":
                return "DateTime.Now";
            case "time":
                return "DateTime.Now";
            case "decimal":
                return "Decimal.Zero";
            case "float":
                return "0.0F";
            case "image":
                return "new byte[0]";
            case "int":
                return "0";
            case "money":
                return "Decimal.Zero";
            case "nchar":
                return "String.Empty";
            case "ntext":
                return "String.Empty";
            case "nvarchar":
                return "String.Empty";
            case "numeric":
                return "Decimal.Zero";
            case "real":
                return "Decimal.Zero";
            case "smalldatetime":
                return "DateTime.Now";
            case "smallint":
                return "0";
            case "smallmoney":
                return "0.0F";
            case "sql_variant":
                return "new byte[0]";
            case "sysname":
                return "String.Empty";
            case "text":
                return "String.Empty";
            case "timestamp":
                return "DateTime.Now";
            case "tinyint":
                return "0x00";
            case "varbinary":
                return "new byte[0]";
            case "varchar":
                return "String.Empty";
            case "uniqueidentifier":
                return "Guid.Empty";
            default:
                throw (new Exception("Invalido el tipo de Dato en SQL Server: " + TipoDato));
        }
    }
}
