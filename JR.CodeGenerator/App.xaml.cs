﻿using JR.CodeGenerator.Services;
using JR.CodeGenerator.ViewModels;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System.Windows;

namespace JR.CodeGenerator;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Gets the service provider.
    /// </summary>
    /// <value>
    /// The service provider.
    /// </value>
    /// <autogeneratedoc />
    public static IServiceProvider ServiceProvider { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    /// <autogeneratedoc />
    public App()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(ConfigureServices)
            .Build();
        ServiceProvider = host.Services;

    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Application.Startup" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
    /// <autogeneratedoc />
    protected override void OnStartup(StartupEventArgs e)
    {
        var windows = ServiceProvider.GetRequiredService<MainViwsModwl>();
        windows.Show();
        base.OnStartup(e);
    }

    /// <summary>
    /// Configures the services.
    /// </summary>
    /// <param name="hostBuilderContext">The host builder context.</param>
    /// <param name="services">The services.</param>
    /// <autogeneratedoc />
    private void ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection services)
    {
        services.AddSingleton<ISQLServerService, SQLServerService>();

        services.AddSingleton<MainViewModel>();

        services.AddSingleton<MainViwsModwl>();
    }
}

