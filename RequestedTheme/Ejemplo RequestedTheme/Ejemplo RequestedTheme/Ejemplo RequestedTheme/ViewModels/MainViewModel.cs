namespace Ejemplo_RequestedTheme.ViewModels
{
    using Windows.UI.Xaml;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Windows.UI.Xaml.Navigation;
    using Base;

    public class MainViewModel  : ViewModelBase
    {
        // Commands
        private ICommand _lightCommand;
        private ICommand _darkCommand;

        public override Task OnNavigatedFrom(NavigationEventArgs args)
        {
            return null;
        }

        public override Task OnNavigatedTo(NavigationEventArgs args)
        {
            return null;
        }

        public ICommand LightCommand
        {
            get { return _lightCommand = _lightCommand ?? new DelegateCommand(LightCommandExecute); }
        }

        public ICommand DarkCommand
        {
            get { return _darkCommand = _darkCommand ?? new DelegateCommand(DarkCommandExecute); }
        }

        private void LightCommandExecute()
        {
            AppFrame.RequestedTheme = ElementTheme.Light;
        }

        private void DarkCommandExecute()
        {
            AppFrame.RequestedTheme = ElementTheme.Dark;
        }
    }
}
