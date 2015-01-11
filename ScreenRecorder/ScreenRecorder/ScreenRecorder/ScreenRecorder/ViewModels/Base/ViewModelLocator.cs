using Microsoft.Practices.Unity;
using ScreenRecorder.Services.Dialog;
using ScreenRecorder.Services.ScreenRecorder;

namespace ScreenRecorder.ViewModels.Base
{
    public class ViewModelLocator
    {
        readonly IUnityContainer _container;

        public ViewModelLocator()
        {
            _container = new UnityContainer();

            _container.RegisterType<MainViewModel>();
            _container.RegisterType<ResultViewModel>();

            _container.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IScreenRecorderService, ScreenRecorderService>(new ContainerControlledLifetimeManager());
        }

        public MainViewModel MainViewModel
        {
            get { return _container.Resolve<MainViewModel>(); }
        }

        public ResultViewModel ResultViewModel
        {
            get { return _container.Resolve<ResultViewModel>(); }
        }
    }
}
