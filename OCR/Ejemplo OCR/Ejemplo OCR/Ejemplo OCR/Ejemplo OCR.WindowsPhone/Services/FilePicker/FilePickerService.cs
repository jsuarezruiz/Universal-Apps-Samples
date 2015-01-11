using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace Ejemplo_OCR.Services.FilePicker
{
    public class FilePickerService : IFilePickerService
    {
        private TaskCompletionSource<StorageFile> _completionSource;
        private StorageFile _selectedFile;

        public void Initialise()
        {
            CoreApplication.GetCurrentView().Activated += OnApplicationActivated;
        }

        private void OnApplicationActivated(CoreApplicationView sender,
            IActivatedEventArgs args)
        {
            var continueArgs =
                args as FileOpenPickerContinuationEventArgs;

            if (continueArgs != null)
            {
                _selectedFile = continueArgs.Files[0];

                if (_completionSource != null)
                {
                    _completionSource.SetResult(_selectedFile);
                    _completionSource = null;
                }
            }
        }

        public async Task<StorageFile> ShowPickerAsync(FileOpenPicker picker)
        {
            _completionSource = new TaskCompletionSource<StorageFile>();

            picker.PickSingleFileAndContinue();

            StorageFile file = await _completionSource.Task;

            return (file);
        }
    }
}