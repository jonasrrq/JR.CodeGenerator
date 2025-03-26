using Microsoft.Extensions.DependencyInjection;

namespace JR.CodeGenerator.ViewModels;

/// <summary>
/// View Model Locator
/// </summary>
public class ViewModelLocator
{
    /// <summary>
    /// Gets the main view model.
    /// </summary>
    /// <value>
    /// The main view model.
    /// </value>
    public MainViewModel MainViewModel => App.ServiceProvider.GetRequiredService<MainViewModel>();
}