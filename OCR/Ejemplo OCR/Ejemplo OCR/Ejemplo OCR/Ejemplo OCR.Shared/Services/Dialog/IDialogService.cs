using System.Threading.Tasks;

namespace Ejemplo_OCR.Services.Dialog
{
    public interface IDialogService
    {
        Task ShowAsync(string title, string message);
    }
}
