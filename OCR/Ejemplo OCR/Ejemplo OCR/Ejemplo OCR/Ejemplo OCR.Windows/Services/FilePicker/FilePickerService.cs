using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace Ejemplo_OCR.Services.FilePicker
{
    public class FilePickerService : IFilePickerService
    {
        public void Initialise()
        {
        }

        public async Task<StorageFile> ShowPickerAsync(FileOpenPicker picker)
        {
            StorageFile file = await picker.PickSingleFileAsync();
            return (file);
        }
    }
}
