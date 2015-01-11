using System.Threading.Tasks;

namespace ScreenRecorder.Services.Dialog
{
    public interface IDialogService
    {
        Task ShowAsync(string title, string message);
    }
}
