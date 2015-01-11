namespace Ejemplo_Roaming_Settings.ViewModels.Base
{
    using Services;
    using Microsoft.Practices.Unity;

    public class ViewModelLocator
    {
        readonly IUnityContainer _container;

        public ViewModelLocator()
        {
            _container = new UnityContainer();

            _container.RegisterType<SettingsViewModel>();

            _container.RegisterType<IRoamingSettingsService, RoamingSettingsService>(new ContainerControlledLifetimeManager());
        }

        public SettingsViewModel SettingsViewModel
        {
            get { return _container.Resolve<SettingsViewModel>(); }
        }
    }
}
