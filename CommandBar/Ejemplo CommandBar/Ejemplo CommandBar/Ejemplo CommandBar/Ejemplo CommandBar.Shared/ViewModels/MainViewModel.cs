namespace Ejemplo_CommandBar.ViewModels
{
    using Ejemplo_CommandBar.Base;
    using Ejemplo_CommandBar.Services.Dialog;
    using Ejemplo_CommandBar.ViewModels.Base;
    using System.Windows.Input;

    public class MainViewModel : ViewModelBase
    {
        //Services
        private IDialogService _dialogService;

        //Commands
        private DelegateCommand _primaryCommand;
        private DelegateCommand _secondaryCommand;
        private DelegateCommand _menuFlyoutCommand;

        public MainViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public ICommand PrimaryCommand
        {
            get { return _primaryCommand = _primaryCommand ?? new DelegateCommand(PrimaryCommandDelegate); }
        }

        public ICommand SecondaryCommand
        {
            get { return _secondaryCommand = _secondaryCommand ?? new DelegateCommand(SecondaryCommandDelegate); }
        }

        public ICommand MenuFlyoutCommand
        {
            get { return _menuFlyoutCommand = _menuFlyoutCommand ?? new DelegateCommand(MenuFlyoutCommandDelegate); }
        }

        private void PrimaryCommandDelegate()
        {
            _dialogService.Show("PrimaryCommand");
        }

        private void SecondaryCommandDelegate()
        {
            _dialogService.Show("SecondaryCommand");
        }

        private void MenuFlyoutCommandDelegate()
        {
            _dialogService.Show("MenuFlyoutCommand");
        }
    }
}
