using Ejemplo_TextToSpeech.Services.Speech;
using Microsoft.Practices.Unity;

namespace Ejemplo_TextToSpeech.ViewModels.Base
{
    public class ViewModelLocator
    {
        readonly IUnityContainer _container;

        public ViewModelLocator()
        {
            _container = new UnityContainer();

            _container.RegisterType<MainViewModel>();

            _container.RegisterType<ISpeechService, SpeechService>(new ContainerControlledLifetimeManager());
        }

        public MainViewModel MainViewModel
        {
            get { return _container.Resolve<MainViewModel>(); }
        }
    }
}
