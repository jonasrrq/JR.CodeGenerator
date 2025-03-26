﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using JR.CodeGenerator.Extensions;
using JR.CodeGenerator.Models;
using JR.CodeGenerator.Services;

using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;

namespace JR.CodeGenerator.ViewModels;

/// <summary>
/// MainViewModel
/// </summary>
/// <autogeneratedoc />
public partial class MainViewModel : ObservableObject
{
    private readonly ISQLServerService _SQLServerService;


    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ConctDBCommand))]
    [NotifyCanExecuteChangedFor(nameof(GenerateCommand))]
    string serverName;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEnableUderPass))]
    [NotifyCanExecuteChangedFor(nameof(GenerateCommand))]
    bool isIntegrateSecurity = true;
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GenerateCommand))]
    string userName;
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GenerateCommand))]
    string password;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GenerateCommand))]
    string empresa;


    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GenerateCommand))]
    string autor;


    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GenerateCommand))]
    string nameSpace;


    [ObservableProperty]
    bool toTitleCase = false;

    [ObservableProperty]
    string pathCode;

    [ObservableProperty]
    bool isDapper;

    [ObservableProperty]
    bool isCreateTrigger;

    [ObservableProperty]
    bool isOneFileScript;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DataTables))]
    string textBuscar;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DataTables))]
    [NotifyPropertyChangedFor(nameof(IsEnableBuscar))]
    List<TableView> dataTablesOld;

    /// <summary>
    /// Gets or sets the data tables.
    /// </summary>
    /// <value>
    /// The data tables.
    /// </value>
    public List<TableView> DataTables
    {
        get
        {
            if (string.IsNullOrWhiteSpace(TextBuscar))
                return dataTablesOld;
            else
            {
                var list2 = new List<TableView>();

                foreach (var item in dataTablesOld)
                {
                    list2.Add(new TableView()
                    {
                        Name = item.Name,
                        ImageUri = item.ImageUri,
                        Schema = item.Schema,
                        Children = item.Children.Where(x => x.Schema.ToLower().Contains(TextBuscar.ToLower()) || x.Name.ToLower().Contains(TextBuscar.ToLower())).ToList()
                    });
                }

                return list2;
            }

        }

    }

    /// <summary>
    /// Gets a value indicating whether this instance is visible buscar.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is visible buscar; otherwise, <c>false</c>.
    /// </value>
    public bool IsEnableBuscar
    {
        get => DataTables?.Count > 0;
    }


    [ObservableProperty]
    ObservableCollection<LogProceso> logs;

    [ObservableProperty]
    List<DataBase> dataBases;

    DataBase selectdataBases;
    /// <summary>
    /// Gets or sets the selectdata bases.
    /// </summary>
    /// <value>
    /// The selectdata bases.
    /// </value>
    /// <autogeneratedoc />
    public DataBase SelectdataBases
    {
        get { return selectdataBases; }
        set
        {
            DataTablesOld = new List<TableView>();
            SetProperty(ref selectdataBases, value);
            if (selectdataBases?.Id != 0)
            {
                _ = GetTablesViewsAsync();
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is enable uder pass.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is enable uder pass; otherwise, <c>false</c>.
    /// </value>
    /// <autogeneratedoc />
    public bool IsEnableUderPass
    {
        get => !IsIntegrateSecurity;
    }

    private AsyncRelayCommand conctDBCommand;
    private AsyncRelayCommand generateCommand;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainViewModel"/> class.
    /// </summary>
    /// <autogeneratedoc />
    public MainViewModel(ISQLServerService sQLServerService)
    {
        _SQLServerService = sQLServerService;
        PathCode = $"{System.IO.Path.Combine(Environment.CurrentDirectory, "Resultados")}";
        LoadSettin();
    }

    /// <summary>
    /// Loads the settin.
    /// </summary>
    private async void LoadSettin()
    {
        var setting = await _SQLServerService.GetSetting();


        if (setting != null)
        {
            Empresa = setting.DataGeneral.Empresa;
            Autor = setting.DataGeneral.Autor;
            NameSpace = setting.DataGeneral.NameSpace;
            PathCode = setting.DataGeneral.Path;
            ToTitleCase = setting.DataGeneral.ToTitleCase;
            IsDapper = setting.DataGeneral.IsDapper;
            IsCreateTrigger = setting.DataGeneral.IsCreateTrigger;
            IsOneFileScript = setting.DataGeneral.IsOneFileScript;

            ServerName = setting.DataConnection.ServerName;
            IsIntegrateSecurity = setting.DataConnection.IsIntegrateSecurity;
            UserName = setting.DataConnection.UserName;
            Password = setting.DataConnection.Password;
        }



    }

    /// <summary>
    /// Gets the conct database command.
    /// </summary>
    /// <value>
    /// The conct database command.
    /// </value>
    /// <autogeneratedoc />Hola 
    public IAsyncRelayCommand ConctDBCommand => conctDBCommand ??= new AsyncRelayCommand(ConctDBsAsync, () => ValidedConnect());
    /// <summary>
    /// Gets the select all command.
    /// </summary>
    /// <value>
    /// The select all command.
    /// </value>
    /// <autogeneratedoc />
    public IAsyncRelayCommand GenerateCommand => generateCommand ??= new AsyncRelayCommand(GenerateAsync, () => ValidedGenerar());

    /// <summary>
    /// Connects the dadabase asynchronous.
    /// </summary>
    /// <autogeneratedoc />
    private async Task ConctDBsAsync()
    {
        try
        {
            DataBases = await _SQLServerService.GetDataBaseseAsync(GetConnet);
            if (DataBases.Count != 0)
                SelectdataBases = DataBases[0];

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
            MessageBox.Show(ex.Message);
        }

    }

    /// <summary>
    /// Gets the t ables views asynchronous.
    /// </summary>
    /// <autogeneratedoc />
    private async Task GetTablesViewsAsync()
    {
        try
        {
            var _connt = GetConnet;
            _connt.DataBase = SelectdataBases?.Name;
            DataTablesOld = await _SQLServerService.GetDatblesVistasAsync(_connt);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
            MessageBox.Show(ex.Message);
        }

    }

    /// <summary>
    /// Generates the asynchronous.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <autogeneratedoc />
    private async Task GenerateAsync()
    {
        try
        {
            Logs = new ObservableCollection<LogProceso>();
            var _genral = GetDataGen;

            var logBase = new LogProceso()
            {
                TableVista = "GenerateCodeBase",
                TableName = "GenerateCodeBase",
                Status = "LightYellow"
            };
            Logs.Add(logBase);

            await _SQLServerService.GenerateCodeBase(_genral);

            logBase.Status = "LightGreen";


            foreach (var item in DataTables)
            {
                foreach (var items in item.Children.Where(x => x.IsSelected))
                {
                    var log = new LogProceso()
                    {
                        TableVista = item.Name,
                        TableName = items.Name,
                        Status = "LightYellow"
                    };

                    Logs.Add(log);
                    await Task.Delay(50);

                    var _connet = GetConnet;
                    _connet.DataBase = SelectdataBases.Name;

                    _genral.TableName = items.Name.UpperFirstChar();
                    _genral.TableVista = item.Name;
                    _genral.SchemaName = items.Schema;

                    try
                    {
                        await _SQLServerService.GenerateCode(_connet, _genral);

                        log.Status = "LightGreen";
                    }
                    catch (Exception ex)
                    {
                        log.ErrorMessage = ex.Message;
                        log.Status = "Red";
                    }
                }
            }
            System.Diagnostics.Process.Start("explorer", PathCode);

            var setting = new Setting()
            {
                DataConnection = GetConnet,
                DataGeneral = GetDataGen
            };

            await _SQLServerService.SetSetting(setting);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
            MessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// Connecs this instance.
    /// </summary>
    /// <returns></returns>
    /// <autogeneratedoc />
    private DataConnection GetConnet
    {
        get
        {
            if (!ValidedConnect())
            {
                throw new Exception("ServerName no puesde estas vacio /nUserName y Password no pueden estar vacios");
            }

            return new DataConnection()
            {
                ServerName = ServerName,
                IsIntegrateSecurity = IsIntegrateSecurity,
                UserName = UserName,
                Password = Password
            };
        }

    }

    /// <summary>
    /// Gets the data gen.
    /// </summary>
    /// <returns></returns>
    /// <autogeneratedoc />
    private DataGeneral GetDataGen
    {
        get => new DataGeneral()
        {
            Empresa = Empresa,
            Autor = Autor,
            NameSpace = NameSpace,
            Path = PathCode,
            ToTitleCase = ToTitleCase,
            IsDapper = IsDapper,
            IsCreateTrigger = IsCreateTrigger,
            IsOneFileScript = IsOneFileScript
        };
    }



    /// <summary>
    /// Valideds the connect.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="System.Exception">ServerName no puesde estas vacio
    /// or
    /// UserName y Password no pueden estar vacios</exception>
    private bool ValidedConnect()
    {
        if (string.IsNullOrWhiteSpace(ServerName))
            return false;//throw new Exception("ServerName no puesde estas vacio");

        if (!IsIntegrateSecurity && (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password)))
            return false; //throw new Exception("UserName y Password no pueden estar vacios");

        return true;
    }

    /// <summary>
    /// Valideds the generar.
    /// </summary>
    /// <returns></returns>
    /// <autogeneratedoc />
    private bool ValidedGenerar()
    {
        bool ret = string.IsNullOrWhiteSpace(Empresa) || string.IsNullOrWhiteSpace(Autor) || string.IsNullOrWhiteSpace(NameSpace);
        if (string.IsNullOrWhiteSpace(ServerName) || ret)
            return false;

        if (!IsIntegrateSecurity && (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password)) || ret)
            return false;

        return true;
    }
}

