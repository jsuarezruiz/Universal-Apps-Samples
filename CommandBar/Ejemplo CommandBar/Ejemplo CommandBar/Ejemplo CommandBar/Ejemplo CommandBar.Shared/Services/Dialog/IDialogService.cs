namespace Ejemplo_CommandBar.Services.Dialog
{
    using System.Threading.Tasks;

    public interface IDialogService
    {
        Task Show(string message);
    }
}
