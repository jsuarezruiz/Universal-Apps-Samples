using Ejemplo_OCR.Services.Dialog;
using Ejemplo_OCR.Services.FilePicker;
using Ejemplo_OCR.Services.Ocr;
using Microsoft.Practices.Unity;

namespace Ejemplo_OCR.ViewModels.Base
{
    public class ViewModelLocator
    {
        readonly IUnityContainer _container;

        public ViewModelLocator()
        {
            _container = new UnityContainer();

            _container.RegisterType<MainViewModel>();

            _container.RegisterType<IFilePickerService, FilePickerService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IOcrService, OcrService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());
        }

        public MainViewModel MainViewModel
        {
            get { return _container.Resolve<MainViewModel>(); }
        }
    }
}
