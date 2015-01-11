namespace Ejemplo_Roaming_Settings.ViewModels
{
    using System.Linq;
    using System.Threading.Tasks;
    using Base;
    using Windows.UI.Xaml.Navigation;
    using System.Collections.ObjectModel;
    using Models;
    using Services;
    using Windows.Storage;

    public class SettingsViewModel : ViewModelBase
    {
        // Constantes
        private const string Key = "accent";

        // Servicios
        private readonly IRoamingSettingsService _roamingSettingsService;

        // Variables
        private ObservableCollection<Accent> _accents;
        private Accent _current;

        public SettingsViewModel(IRoamingSettingsService roamingSettingsService)
        {
            _roamingSettingsService = roamingSettingsService;
        }

        public ObservableCollection<Accent> Accents
        {
            get { return _accents; }
            set { _accents = value; }
        }

        public Accent Current
        {
            get { return _current; }
            set
            {
                _current = value;
                _roamingSettingsService.SaveData(Key, _current.Hex);

                RaisePropertyChanged();
            }
        }

        public override Task OnNavigatedFrom(NavigationEventArgs args)
        {
            ApplicationData.Current.DataChanged -=
                DataChangeHandler;

            return null;
        }

        public override Task OnNavigatedTo(NavigationEventArgs args)
        {
            ApplicationData.Current.DataChanged +=
                 DataChangeHandler;

            LoadAccents();

            return null;
        }

        void DataChangeHandler(ApplicationData appData, object args)
        {
            if (_roamingSettingsService.ContainsKey(Key))
            {
                var hex = _roamingSettingsService.GetData(Key).ToString();
                Current = Accents.First(c => c.Hex.Equals(hex));
            }
        }

        private void LoadAccents()
        {
            _accents = new ObservableCollection<Accent>
            {
                new Accent { Name = "Blue", Hex= "#1BA1E2" },
                new Accent { Name = "Brown", Hex= "#A05000" },
                new Accent { Name = "Green", Hex= "#339933" },
                new Accent { Name = "Pink", Hex= "#E671B8" },
                new Accent { Name = "Purple", Hex= "#A200FF" },
                new Accent { Name = "Red", Hex= "#E51400" },
                new Accent { Name = "Teal", Hex= "#00ABA9" },
                new Accent { Name = "Lime", Hex= "#A2C139 " },
                new Accent { Name = "Magenta", Hex= "#D80073 " }
            };

            if (_roamingSettingsService.ContainsKey(Key))
            {
                var hex = _roamingSettingsService.GetData(Key).ToString();
                Current = Accents.First(c => c.Hex.Equals(hex));
            }
            else
                Current = Accents.First();
        }
    }
}
