namespace Ejemplo_Sensor_Luz.ViewModels
{
    using System;
    using Windows.UI.Xaml;
    using System.Threading.Tasks;
    using Windows.UI.Xaml.Navigation;
    using Base;
    using Windows.Devices.Sensors;
    using System.Diagnostics;
    using Windows.UI.Core;

    public class MainViewModel : ViewModelBase
    {
        // Variables
        private CoreDispatcher _dispatcher;
        private LightSensor _lightSensor;
        private float _lux;

        public float Lux
        {
            get { return _lux; }
            set
            {
                _lux = value;
                RaisePropertyChanged();
                RaisePropertyChanged("State");
            }
        }

        public string Info
        {
            get
            {
                return
                    "En un lugar de la Mancha, de cuyo nombre no quiero acordarme, no ha mucho tiempo que vivía un hidalgo de los de lanza en astillero, adarga antigua, rocín flaco y galgo corredor. Una olla de algo más vaca que carnero, salpicón las más noches, duelos y quebrantos los sábados, lentejas los viernes, algún palomino de añadidura los domingos, consumían las tres partes de su hacienda. El resto della concluían sayo de velarte, calzas de velludo para las fiestas con sus pantuflos de lo mismo, los días de entre semana se honraba con su vellori de lo más fino. Tenía en su casa una ama que pasaba de los cuarenta, y una sobrina que no llegaba a los veinte, y un mozo de campo y plaza, que así ensillaba el rocín como tomaba la podadera. Frisaba la edad de nuestro hidalgo con los cincuenta años, era de complexión recia, seco de carnes, enjuto de rostro; gran madrugador y amigo de la caza. Quieren decir que tenía el sobrenombre de Quijada o Quesada (que en esto hay alguna diferencia en los autores que deste caso escriben), aunque por conjeturas verosímiles se deja entender que se llama Quijana; pero esto importa poco a nuestro cuento; basta que en la narración dél no se salga un punto de la verdad. ";
            }
        }

        public string State
        {
            get { return Lux < 3000 ? "Dark" : "Light"; }
        }

        public override Task OnNavigatedFrom(NavigationEventArgs args)
        {
            if (_lightSensor != null)
            {
                _lightSensor.ReportInterval = 0;
                _lightSensor.ReadingChanged -= _lightSensor_ReadingChanged;
            }

            return null;
        }

        public override async Task OnNavigatedTo(NavigationEventArgs args)
        {
            _dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            _lightSensor = LightSensor.GetDefault();

            if (_lightSensor != null)
            {
                uint minReportInterval = _lightSensor.MinimumReportInterval;
                _lightSensor.ReportInterval = minReportInterval > 1000 ? minReportInterval : 1000; 
                _lightSensor.ReadingChanged += _lightSensor_ReadingChanged;
            }
            else
            {
                Debug.WriteLine("El dispositivo no cuenta con el sensor de luz");
            }
        }

        void _lightSensor_ReadingChanged(LightSensor sender, LightSensorReadingChangedEventArgs args)
        {
            _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Lux = args.Reading.IlluminanceInLux;
                AppFrame.RequestedTheme =
                    State.Equals("Dark", StringComparison.CurrentCultureIgnoreCase) ?
                     ElementTheme.Dark : ElementTheme.Light;
            }); 

            Debug.WriteLine("Lux: {0}", Lux);
        }
    }
}
