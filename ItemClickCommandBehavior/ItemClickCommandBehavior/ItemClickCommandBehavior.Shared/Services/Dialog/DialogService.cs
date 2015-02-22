using System;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace ItemClickCommandBehavior.Services.Dialog
{
    public class DialogService : IDialogService
    {
        public async Task ShowAsync(string message)
        {
            var dialog = new MessageDialog(message);

            await dialog.ShowAsync();
        }
    }
}
