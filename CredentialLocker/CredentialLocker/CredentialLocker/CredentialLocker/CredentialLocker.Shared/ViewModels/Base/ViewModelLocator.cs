using CredentialLocker.Services.PasswordVaultService;
using Microsoft.Practices.Unity;

namespace CredentialLocker.ViewModels.Base
{
    public class ViewModelLocator
    {
        readonly IUnityContainer _container;

        public ViewModelLocator()
        {
            _container = new UnityContainer();

            _container.RegisterType<MainViewModel>();

            _container.RegisterType<IPasswordVaultService, PasswordVaultService>(new ContainerControlledLifetimeManager());
        }

        public MainViewModel MainViewModel
        {
            get { return _container.Resolve<MainViewModel>(); }
        }
    }
}
