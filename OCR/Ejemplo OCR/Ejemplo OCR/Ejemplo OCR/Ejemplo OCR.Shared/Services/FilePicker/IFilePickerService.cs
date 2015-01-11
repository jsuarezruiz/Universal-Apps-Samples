using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace Ejemplo_OCR.Services.FilePicker
{
    public interface IFilePickerService
    {
        void Initialise();
        Task<StorageFile> ShowPickerAsync(FileOpenPicker picker);
    }
}
