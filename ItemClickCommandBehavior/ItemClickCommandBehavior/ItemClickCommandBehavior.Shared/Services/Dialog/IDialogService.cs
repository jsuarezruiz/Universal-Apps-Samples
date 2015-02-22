using System.Threading.Tasks;

namespace ItemClickCommandBehavior.Services.Dialog
{
    public interface IDialogService
    {
        Task ShowAsync(string message);
    }
}
