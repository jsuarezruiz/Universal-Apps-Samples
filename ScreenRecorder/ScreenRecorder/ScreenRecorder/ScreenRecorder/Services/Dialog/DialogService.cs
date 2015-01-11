using System;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace ScreenRecorder.Services.Dialog
{
    public class DialogService : IDialogService
    {
        public async Task ShowAsync(string title, string message)
        {
            var messageDialog = new MessageDialog(message, title);

            await messageDialog.ShowAsync();
        }
    }
}
