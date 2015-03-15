namespace GesturesBehavior.ViewModels
{
    using System.Windows.Input;
    using System.Threading.Tasks;
    using Windows.UI.Xaml.Navigation;
    using Behaviors;
    using Base;

    public class MainViewModel : ViewModelBase
    {
        // Variables
        private string _info;

        // Commands
        private ICommand _swipeCommand;

        public MainViewModel()
        {
            // Init
            Info = "Haz un gesto de Swipe hacia arriba, abajo, izquierda o derecha.";
        }

        public string Info
        {
            get { return _info; }
            set
            {
                _info = value;
                RaisePropertyChanged();
            }
        }

        public override Task OnNavigatedFrom(NavigationEventArgs args)
        {
            return null;
        }

        public override Task OnNavigatedTo(NavigationEventArgs args)
        {
            return null;
        }

        public ICommand SwipeCommand
        {
            get { return _swipeCommand = _swipeCommand ?? new DelegateCommand<SwipeDirection>(SwipeCommandExecute); }
        }

        private void SwipeCommandExecute(SwipeDirection direction)
        {
            switch (direction)
            {
                case SwipeDirection.Up:
                    Info = "Arriba";
                    break;
                case SwipeDirection.Down:
                    Info = "Abajo";
                    break;
                case SwipeDirection.Left:
                    Info = "Izquierda";
                    break;
                case SwipeDirection.Right:
                    Info = "Derecha";
                    break;
            }
        }
    }
}
