﻿using Dapper;

using JR.CodeGenerator.Extensions;
using JR.CodeGenerator.Models;

using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.SymbolStore;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace JR.CodeGenerator.Services;

/// <summary>
/// 
/// </summary>
/// <autogeneratedoc />
public class ClaseMetodos
{
    private readonly string _connectionString;

    /// <summary>
    /// Initializes a new instance of the <see cref="ClaseMetodos"/> class.
    /// </summary>
    /// <param name="connectionString">The connection string.</param>
    /// <autogeneratedoc />
    public ClaseMetodos(string connectionString)
    {
        _connectionString = connectionString;
    }

    /// <summary>
    /// Gets the campos.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns></returns>
    /// <autogeneratedoc />
    public async Task<string> GetFields(string query, bool toTitleCase)
    {
        string result = string.Empty;
        DataTable dt = new DataTable();
        var into = Environment.NewLine;
        using (var db = new SqlConnection(_connectionString))
        {
            var resultData = await db.QueryAsync<InfoCampo>(query);

            foreach (var item in resultData)
            {
                result += "\t\t ///<summary> "; //+ into;
                //result += "\t\t /// ";
                string s = item.Column_Name;
                string p = Regex.Replace(s, "([a-z](?=[A-Z0-9])|[A-Z](?=[A-Z][a-z]))", "$1 ").ToString();
                result += "Gets or sets the " + p;

                if (!string.IsNullOrEmpty(item.Campo_Descripcion))
                {
                    result += into;
                    result += "\t\t /// ";
                    result += item.Campo_Descripcion.Trim().Replace("\r\n", "\r\n \t\t  ///");

                }
                //result += into;
                // result += "\t\t ///</summary> " + into;
                result += " </summary> " + into;

                string campo = toTitleCase ? item.Column_Name.ToLower().ToTitleCase() : item.Column_Name.UpperFirstChar();

                result += $"\t\tpublic {clsSQLToCsharp.SQLToCsharp(item.Data_Type)} {campo} ";

                result += "{get;set;}" + into;
                //result += clsSQLToCsharp.DefaultValue(item.Data_Type);//TODO: Validar si colocarlo
            }

        }

        return result;
    }

    /// <summary>
    /// Gets the validations.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <param name="toTitleCase">if set to <c>true</c> [to title case].</param>
    /// <returns></returns>
    public async Task<string> GetValidations(string query, bool toTitleCase)
    {
        string result = string.Empty;
        DataTable dt = new DataTable();
        var into = Environment.NewLine;

        using (var db = new SqlConnection(_connectionString))
        {
            var resultData = await db.QueryAsync<InfoCampo>(query);
            int i = 0;
            foreach (var item in resultData)
            {
                i++;
                if (item.Is_Identity == 1)
                    continue;

                string campo = toTitleCase ? item.Column_Name.ToLower().ToTitleCase() : item.Column_Name.UpperFirstChar();
                if (campo != "AuditKey")
                {
                    if (item.Character_Maximum_Length != 0)
                    {
                        result += i == 1 ? $"if (string.IsNullOrWhiteSpace(entity.{campo})){into}" : $"        if (string.IsNullOrWhiteSpace(entity.{campo})){into}";
                        result += $"        list.Add(Error.Create(\"{campo}\" ,\"{campo} no puede esta vacio\"));{into}{into}";

                        if (item.Character_Maximum_Length != -1)
                        {
                            result += $"        if (entity.{campo}?.Length > {item.Character_Maximum_Length}){into}";
                            result += $"        list.Add(Error.Create(\"{campo}\" ,\"La longitud de {campo} es invalida, Intente reduciendo el número de caracteres\"));{into}{into}";
                        }
                    }

                    if (item.Clave_Foranea)
                    {
                        result += $"        if (entity.{campo} == 0){into}";
                        result += $"        list.Add(Error.Create(\"{campo}\" ,\"{campo} no puede ser cero\"));{into}{into}";
                    }
                }
            }
        }

        return result;
    }


    /// <summary>
    /// Gets the fields insert.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <param name="toTitleCase">if set to <c>true</c> [to title case].</param>
    /// <returns></returns>
    public async Task<string> GetFieldsInsert(string query, bool toTitleCase)
    {
        string _filds = string.Empty;
        string _parameters = string.Empty;
        using (var db = new SqlConnection(_connectionString))
        {
            var resultData = await db.QueryAsync<InfoCampo>(query);
            string _fieldName = string.Empty;
            foreach (var item in resultData)
            {
                if (item.Is_Identity == 1)
                    continue;

                _fieldName = toTitleCase ? item.Column_Name.ToLower().ToTitleCase() : item.Column_Name.UpperFirstChar();


                if (string.IsNullOrEmpty(_filds))
                {
                    _filds = $"                            {_fieldName}";
                    _parameters = $"                            @{_fieldName}";
                }
                else
                {
                    _filds += $", {Environment.NewLine}                            {_fieldName}";
                    _parameters += $", {Environment.NewLine}                            @{_fieldName}";
                }
            }
        }

        return $"{Environment.NewLine}                            ({_filds}) {Environment.NewLine}Values {Environment.NewLine}                            ({_parameters})";
    }

    /// <summary>
    /// Gets the fields update.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <param name="toTitleCase">if set to <c>true</c> [to title case].</param>
    /// <returns></returns>
    public async Task<string> GetFieldsUpdate(string query, bool toTitleCase)
    {
        string result = string.Empty;
        string _where = string.Empty;

        using (var db = new SqlConnection(_connectionString))
        {
            var resultData = await db.QueryAsync<InfoCampo>(query);
            string _fieldName = string.Empty;

            foreach (var item in resultData)
            {
                _fieldName = toTitleCase ? item.Column_Name.ToLower().ToTitleCase() : item.Column_Name.UpperFirstChar();

                if (item.Clave_Primaria)
                {
                    if (string.IsNullOrEmpty(_where))
                        _where = $"                            WHERE {_fieldName} = @{_fieldName}";
                    else
                        _where += $" AND {_fieldName} = @{_fieldName}";

                    continue;
                }

                if (item.Is_Identity == 1)
                    continue;

                if (string.IsNullOrEmpty(result))
                    result += $"                                {_fieldName}=@{_fieldName},";
                else
                    result += $"{Environment.NewLine}                                {_fieldName}=@{_fieldName},";
            }
        }

        return $"{result.TrimEnd(',')}{Environment.NewLine}{_where}";
    }

    /// <summary>
    /// Gets the fields update.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns></returns>
    public async Task<string> GetFieldsPrimaryKey(string query)
    {
        string result = string.Empty;

        using (var db = new SqlConnection(_connectionString))
        {
            var resultData = await db.QueryAsync<InfoCampo>(query);
            string _fieldName = string.Empty;

            foreach (var item in resultData)
            {
                if (item.Clave_Primaria)
                {
                    result = item.Column_Name.ToLower().ToTitleCase();
                    break;
                }
            }
        }

        return result;
    }



}
