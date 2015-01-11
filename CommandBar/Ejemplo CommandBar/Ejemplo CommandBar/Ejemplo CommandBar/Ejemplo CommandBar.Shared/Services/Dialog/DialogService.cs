namespace Ejemplo_CommandBar.Services.Dialog
{
    using System;
    using System.Threading.Tasks;
    using Windows.UI.Popups;

    public class DialogService : IDialogService
    {
        public async Task Show(string message)
        {
            await new MessageDialog(message).ShowAsync();
        }
    }
}
