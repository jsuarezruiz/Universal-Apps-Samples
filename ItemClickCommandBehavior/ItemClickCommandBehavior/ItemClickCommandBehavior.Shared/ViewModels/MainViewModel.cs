using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;
using ItemClickCommandBehavior.Services.Dialog;
using ItemClickCommandBehavior.ViewModels.Base;

namespace ItemClickCommandBehavior.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        // Variables
        private ObservableCollection<string> _items;

        // Commands
        private ICommand _clickCommand;

        // Services
        private readonly IDialogService _dialogService;

        public MainViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public ObservableCollection<string> Items
        {
            get
            {
                if (_items == null)
                    LoadData();
                return _items;
            }
            set { _items = value; }
        }

        public ICommand ClickCommand
        {
            get { return _clickCommand = _clickCommand ?? new DelegateCommandAsync<string>(ClickCommandExecute); }
        }

        public override Task OnNavigatedFrom(NavigationEventArgs args)
        {
            return null;
        }

        public override Task OnNavigatedTo(NavigationEventArgs args)
        {
            return null;
        }

        private void LoadData()
        {
            _items = new ObservableCollection<string>();
            for (int i = 0; i < 100; i++)
            {
                _items.Add((i + 1).ToString());
            }
        }

        private async Task ClickCommandExecute(string parameter)
        {
            await _dialogService.ShowAsync(parameter);
        }
    }
}
