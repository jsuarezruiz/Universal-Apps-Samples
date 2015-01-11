using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Ejemplo_OCR.Services.Dialog;
using Ejemplo_OCR.Services.FilePicker;
using Ejemplo_OCR.Services.Ocr;
using Ejemplo_OCR.ViewModels.Base;

namespace Ejemplo_OCR.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //Variables
        private WriteableBitmap _bitmap;
        private string _text;

        //Cons
        public const int MinWidth = 40;
        public const int MaxWidth = 2600;
        public const int MinHeight = 40;
        public const int MaxHeight = 2600;

        //Services
        private readonly IFilePickerService _fileoPickerService;
        private readonly IOcrService _ocrService;
        private readonly IDialogService _dialogService;

        //Commands
        private ICommand _fileOpenPickerCommand;
        private ICommand _getTextCommand;

        public MainViewModel(IFilePickerService fileoPickerService, IOcrService ocrService, IDialogService dialogService)
        {
            _fileoPickerService = fileoPickerService;
            _ocrService = ocrService;
            _dialogService = dialogService;

            _fileoPickerService.Initialise();
        }

        public WriteableBitmap Bitmap
        {
            get { return _bitmap; }
            set
            {
                _bitmap = value;
                RaisePropertyChanged("Bitmap");
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged("Text");
            }
        }

        public ICommand FileOpenPickerCommand
        {
            get { return _fileOpenPickerCommand = _fileOpenPickerCommand ?? new DelegateCommandAsync(FileOpenPickerCommandDelegate); }
        }

        public ICommand GetTextCommand
        {
            get { return _getTextCommand = _getTextCommand ?? new DelegateCommandAsync(GetTextCommandDelegate); }
        }

        public override Task OnNavigatedFrom(NavigationEventArgs args)
        {
            return null;
        }

        public override Task OnNavigatedTo(NavigationEventArgs args)
        {
            return null;
        }

        public async Task FileOpenPickerCommandDelegate()
        {
            var picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");

            var file =
            await _fileoPickerService.ShowPickerAsync(picker);
            Bitmap = await LoadImage(file);
        }

        public async Task GetTextCommandDelegate()
        {
            // Verificamos si la imagen cumple con las características necesarias para ser procesada.
            // Las dimensiones soportadas son desde 40 a 2600 pixels.
            if (_bitmap.PixelHeight < MinHeight ||
                _bitmap.PixelHeight > MaxHeight ||
                _bitmap.PixelWidth < MinWidth ||
                _bitmap.PixelWidth > MaxHeight)
                await _dialogService.ShowAsync("Imagen inválida", "La imagen no esta dentro del tamaño soportado.");
            else
                Text = await _ocrService.GetText(_bitmap);
        }

        internal async Task<WriteableBitmap> LoadImage(StorageFile file)
        {
            var imgProp = await file.Properties.GetImagePropertiesAsync();
            WriteableBitmap bitmap;
            using (var imgStream = await file.OpenAsync(FileAccessMode.Read))
            {
                bitmap = new WriteableBitmap((int)imgProp.Width, (int)imgProp.Height);
                bitmap.SetSource(imgStream);
            }

            return bitmap;
        }
    }
}
