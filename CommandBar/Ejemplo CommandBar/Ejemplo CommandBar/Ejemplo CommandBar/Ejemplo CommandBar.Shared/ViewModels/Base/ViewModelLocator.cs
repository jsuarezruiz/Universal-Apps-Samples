namespace Ejemplo_CommandBar.Base
{
    using Microsoft.Practices.Unity;
    using Ejemplo_CommandBar.ViewModels;
    using Ejemplo_CommandBar.Services.Dialog;

    public class ViewModelLocator
    {
        IUnityContainer _container;

        public ViewModelLocator()
        {
            _container = new UnityContainer();

            _container.RegisterType<MainViewModel>();

            _container.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());
        }

        public MainViewModel MainViewModel
        {
            get { return _container.Resolve<MainViewModel>(); }
        }
    }
}
