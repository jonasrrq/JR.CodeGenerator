namespace JR.CodeGenerator.Models;

/// <summary>
/// InfoCampo
/// </summary>
public class InfoCampo
{
    /// <summary>
    /// Gets or sets the table catalog.
    /// </summary>
    /// <value>
    /// The table catalog.
    /// </value>
    public string Table_Catalog { get; set; }
    /// <summary>
    /// Gets or sets the table schema.
    /// </summary>
    /// <value>
    /// The table schema.
    /// </value>
    public string Table_Schema { get; set; }
    /// <summary>
    /// Gets or sets the name of the table.
    /// </summary>
    /// <value>
    /// The name of the table.
    /// </value>
    public string Table_Name { get; set; }
    /// <summary>
    /// Gets or sets the name of the column.
    /// </summary>
    /// <value>
    /// The name of the column.
    /// </value>
    public string Column_Name { get; set; }
    /// <summary>
    /// Gets or sets the ordinal position.
    /// </summary>
    /// <value>
    /// The ordinal position.
    /// </value>
    public int Ordinal_Position { get; set; }
    /// <summary>
    /// Gets or sets the column default.
    /// </summary>
    /// <value>
    /// The column default.
    /// </value>
    public string Column_Default { get; set; }
    /// <summary>
    /// Gets or sets the is nullable.
    /// </summary>
    /// <value>
    /// The is nullable.
    /// </value>
    public string Is_Nullable { get; set; }
    /// <summary>
    /// Gets or sets the type of the data.
    /// </summary>
    /// <value>
    /// The type of the data.
    /// </value>
    public string Data_Type { get; set; }
    /// <summary>
    /// Gets or sets the maximum length of the character.
    /// </summary>
    /// <value>
    /// The maximum length of the character.
    /// </value>
    public int Character_Maximum_Length { get; set; } = 0;
    /// <summary>
    /// Gets or sets the length of the character octet.
    /// </summary>
    /// <value>
    /// The length of the character octet.
    /// </value>
    public int? Character_Octet_Length { get; set; }
    /// <summary>
    /// Gets or sets the numeric precision.
    /// </summary>
    /// <value>
    /// The numeric precision.
    /// </value>
    public int? Numeric_Precision { get; set; }
    /// <summary>
    /// Gets or sets the numeric precision radix.
    /// </summary>
    /// <value>
    /// The numeric precision radix.
    /// </value>
    public int? Numeric_Precision_Radix { get; set; }
    /// <summary>
    /// Gets or sets the numeric scale.
    /// </summary>
    /// <value>
    /// The numeric scale.
    /// </value>
    public int? Numeric_Scale { get; set; }
    /// <summary>
    /// Gets or sets the datetime precision.
    /// </summary>
    /// <value>
    /// The datetime precision.
    /// </value>
    public int? Datetime_Precision { get; set; }
    /// <summary>
    /// Gets or sets the character set catalog.
    /// </summary>
    /// <value>
    /// The character set catalog.
    /// </value>
    public string Character_Set_Catalog { get; set; }
    /// <summary>
    /// Gets or sets the character set schema.
    /// </summary>
    /// <value>
    /// The character set schema.
    /// </value>
    public string Character_Set_Schema { get; set; }
    /// <summary>
    /// Gets or sets the name of the character set.
    /// </summary>
    /// <value>
    /// The name of the character set.
    /// </value>
    public string Character_Set_Name { get; set; }
    /// <summary>
    /// Gets or sets the collation catalog.
    /// </summary>
    /// <value>
    /// The collation catalog.
    /// </value>
    public string Collation_Catalog { get; set; }
    /// <summary>
    /// Gets or sets the collation schema.
    /// </summary>
    /// <value>
    /// The collation schema.
    /// </value>
    public string Collation_Schema { get; set; }
    /// <summary>
    /// Gets or sets the name of the collation.
    /// </summary>
    /// <value>
    /// The name of the collation.
    /// </value>
    public string Collation_Name { get; set; }
    /// <summary>
    /// Gets or sets the domain catalog.
    /// </summary>
    /// <value>
    /// The domain catalog.
    /// </value>
    public string Domain_Catalog { get; set; }
    /// <summary>
    /// Gets or sets the domain schema.
    /// </summary>
    /// <value>
    /// The domain schema.
    /// </value>
    public string Domain_Schema { get; set; }
    /// <summary>
    /// Gets or sets the name of the domain.
    /// </summary>
    /// <value>
    /// The name of the domain.
    /// </value>
    public string Domain_Name { get; set; }
    /// <summary>
    /// Gets or sets the length of the column.
    /// </summary>
    /// <value>
    /// The length of the column.
    /// </value>
    public int Column_Length { get; set; }
    /// <summary>
    /// Gets or sets the is computed.
    /// </summary>
    /// <value>
    /// The is computed.
    /// </value>
    public int Is_Computed { get; set; }
    /// <summary>
    /// Gets or sets the is identity.
    /// </summary>
    /// <value>
    /// The is identity.
    /// </value>
    public int Is_Identity { get; set; }
    /// <summary>
    /// Gets or sets the is rowguidcol.
    /// </summary>
    /// <value>
    /// The is rowguidcol.
    /// </value>
    public int Is_Rowguidcol { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether [clave primaria].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [clave primaria]; otherwise, <c>false</c>.
    /// </value>
    public bool Clave_Primaria { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether [clave foranea].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [clave foranea]; otherwise, <c>false</c>.
    /// </value>
    public bool Clave_Foranea { get; set; }
    /// <summary>
    /// Gets or sets the campo descripcion.
    /// </summary>
    /// <value>
    /// The campo descripcion.
    /// </value>
    public string Campo_Descripcion { get; set; }


}
