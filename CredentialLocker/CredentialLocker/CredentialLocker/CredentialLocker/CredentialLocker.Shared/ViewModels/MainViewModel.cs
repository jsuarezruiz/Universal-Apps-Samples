using System.Threading.Tasks;
using CredentialLocker.ViewModels.Base;
using Windows.UI.Xaml.Navigation;
using System.Windows.Input;
using CredentialLocker.Services.PasswordVaultService;
using System;

namespace CredentialLocker.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        // Variables
        private string _source;
        private string _user;
        private string _password;
        private string _info;

        // Services
        private IPasswordVaultService _passwordVaultService;

        // Commands
        private ICommand _saveCommand;
        private ICommand _readCommand;
        private ICommand _deleteCommand;

        public MainViewModel(IPasswordVaultService passwordVaultService)
        {
            _passwordVaultService = passwordVaultService;
        }

        public override Task OnNavigatedFrom(NavigationEventArgs args)
        {
            return null;
        }

        public override Task OnNavigatedTo(NavigationEventArgs args)
        {
            return null;
        }

        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }

        public string User
        {
            get { return _user; }
            set { _user = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Info
        {
            get { return _info; }
            set
            {
                _info = value;
                RaisePropertyChanged("Info");
            }
        }

        public ICommand SaveCommand
        {
            get { return _saveCommand = _saveCommand ?? new DelegateCommand(SaveCommandDelegate); }
        }

        public ICommand ReadCommand
        {
            get { return _readCommand = _readCommand ?? new DelegateCommand(ReadCommandDelegate); }
        }

        public ICommand DeleteCommand
        {
            get { return _deleteCommand = _deleteCommand ?? new DelegateCommand(DeleteCommandDelegate); }
        }

        public void SaveCommandDelegate()
        {
            if (string.IsNullOrEmpty(Source) || string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
            {
                Info += "La fuente, el usuario y la contraseña son obligatorios." + "\r\n";
                return;
            }

            try
            {
                _passwordVaultService.Save(Source, User, Password);
                Info += string.Format("Datos guardatos. Resource: {0}, User: {1}, Password: {2}",
                    Source, User, Password) + "\r\n";
            }
            catch(Exception ex)
            {
                Info += ex.Message + "\r\n";
            }
        }

        public void ReadCommandDelegate()
        {
            if (string.IsNullOrEmpty(Source) || string.IsNullOrEmpty(User))
            {
                Info += "La fuente y el usuario son obligatorios." + "\r\n";
                return;
            }

            try
            {
                var cred = _passwordVaultService.Read(Source, User);
                Info += string.Format("Datos obtenidos con éxito. Resource: {0}, User: {1}, Password: {2}",
                      cred.Resource, cred.UserName, cred.Password) + "\r\n";
            }
            catch (Exception ex)
            {
                Info += ex.Message + "\r\n";
            }
        }

        public void DeleteCommandDelegate()
        {
            if (string.IsNullOrEmpty(Source) || string.IsNullOrEmpty(User))
            {
                Info += "La fuente y el usuario son obligatorios." + "\r\n";
                return;
            }

            try
            {
                _passwordVaultService.Delete(Source, User);
                Info += string.Format("Datos eliminados con éxito. Resource: {0}, User: {1}, Password: {2}",
                    Source, User, Password) + "\r\n";
            }
            catch (Exception ex)
            {
                Info += ex.Message + "\r\n";
            }
        }
    }
}
