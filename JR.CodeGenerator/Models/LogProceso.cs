using CommunityToolkit.Mvvm.ComponentModel;

namespace JR.CodeGenerator.Models
{
    /// <summary>
    /// LogProceso
    /// </summary>
    /// <seealso cref="CommunityToolkit.Mvvm.ComponentModel.ObservableObject" />
    public partial class LogProceso : ObservableObject
    {
        [ObservableProperty]
        string tableVista;

        [ObservableProperty]
        string tableName;

        [ObservableProperty]
        string status;

        [ObservableProperty]
        string errorMessage;

    }
}
