using Ejemplo_TextToSpeech.Services.Speech;
using Ejemplo_TextToSpeech.ViewModels.Base;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ejemplo_TextToSpeech.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ISpeechService _speechService;
        private ICommand _speechCommand;

        public MainViewModel(ISpeechService speechService)
        {
            _speechService = speechService;
        }

        public override System.Threading.Tasks.Task OnNavigatedFrom(Windows.UI.Xaml.Navigation.NavigationEventArgs args)
        {
            return null;
        }

        public override System.Threading.Tasks.Task OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs args)
        {
            return null;
        }

        public string Message
        {
            get { return "Hola!. Este ejemplo muestra como crear un servicio para poder realizar Text to Speech en aplicaciones universales utilizando el patrón MVVM."; }
        }

        public ICommand SpeechCommand
        {
            get { return _speechCommand = _speechCommand ?? new DelegateCommandAsync<string>(SpeechCommandDelegate); }
        }

        public async Task SpeechCommandDelegate(string message)
        {
            await _speechService.TextToSpeech(message);
        }
    }
}
