using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;
using ScreenRecorder.Services.ScreenRecorder;
using ScreenRecorder.ViewModels.Base;
using ScreenRecorder.Views;

namespace ScreenRecorder.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //Consts
        private const string VideoName = "video";

        // Variables
        private bool _recording;
        private bool _result;

        // Services
        private readonly IScreenRecorderService _screenRecorderService;

        // Commands
        private ICommand _recordCommand;
        private ICommand _resultCommand;

        public MainViewModel(IScreenRecorderService screenRecorderService)
        {
            _screenRecorderService = screenRecorderService;
        }

        public bool Recording
        {
            get { return _recording; }
            set
            {
                _recording = value;
                RaisePropertyChanged("Recording");
            }
        }

        public bool Result
        {
            get { return _result; }
            set
            {
                _result = value;
                RaisePropertyChanged("Result");
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

        public ICommand RecordCommand
        {
            get { return _recordCommand = _recordCommand ?? new DelegateCommandAsync(RecordCommandDelegate); }
        }
        public ICommand ResultCommand
        {
            get { return _resultCommand = _resultCommand ?? new DelegateCommand(ResultCommandDelegate); }
        }

        public async Task RecordCommandDelegate()
        {
            if (_recording)
            {
                Recording = false;
                _screenRecorderService.Stop();

                Result = true;
            }
            else
            {
                Recording = true;
                await _screenRecorderService.Start(VideoName);
            }
        }

        public void ResultCommandDelegate()
        {
            AppFrame.Navigate(typeof(ResultView), VideoName);
        }
    }
}
