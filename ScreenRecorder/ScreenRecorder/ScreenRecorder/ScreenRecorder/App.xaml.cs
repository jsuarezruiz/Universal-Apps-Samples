using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Media.Capture;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using ScreenRecorder.Views;

namespace ScreenRecorder
{
    /// <summary>
    ///     Proporciona un comportamiento específico de la aplicación para complementar la clase Application predeterminada.
    /// </summary>
    public sealed partial class App : Application
    {
        private TransitionCollection _transitions;

        public MediaCapture MediaCapture { get; set; }
        public bool IsRecording { get; set; }

        public async Task CleanupCaptureResources()
        {
            if (IsRecording && MediaCapture != null)
            {
                await MediaCapture.StopRecordAsync();
                IsRecording = false;
            }

            if (MediaCapture != null)
                MediaCapture.Dispose();
        }

        /// <summary>
        ///     Inicializa el objeto de aplicación Singleton.  Esta es la primera línea de código creado
        ///     ejecutado y, como tal, es el equivalente lógico de main() o WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        /// <summary>
        ///     Se invoca cuando la aplicación la inicia normalmente el usuario final. Se usarán otros puntos
        ///     de entrada cuando la aplicación se inicie para abrir un archivo específico, para mostrar
        ///     resultados de la búsqueda, etc.
        /// </summary>
        /// <param name="e">Información detallada acerca de la solicitud y el proceso de inicio.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (Debugger.IsAttached)
            {
                DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            var rootFrame = Window.Current.Content as Frame;

            // No repetir la inicialización de la aplicación si la ventana tiene contenido todavía,
            // solo asegurarse de que la ventana está activa.
            if (rootFrame == null)
            {
                // Crear un marco para que actúe como contexto de navegación y navegar a la primera página.
                rootFrame = new Frame();

                // TODO: Cambiar este valor a un tamaño de caché adecuado para la aplicación
                rootFrame.CacheSize = 1;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Cargar el estado de la aplicación suspendida previamente
                }

                // Poner el marco en la ventana actual.
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // Quita la navegación de transición en el inicio.
                if (rootFrame.ContentTransitions != null)
                {
                    _transitions = new TransitionCollection();
                    foreach (Transition c in rootFrame.ContentTransitions)
                    {
                        _transitions.Add(c);
                    }
                }

                rootFrame.ContentTransitions = null;
                rootFrame.Navigated += RootFrame_FirstNavigated;

                // Cuando no se restaura la pila de navegación para navegar a la primera página,
                // configurar la nueva página al pasar la información requerida como parámetro
                // de navegación
                if (!rootFrame.Navigate(typeof(MainView), e.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            // Asegurarse de que la ventana actual está activa.
            Window.Current.Activate();
        }

        /// <summary>
        ///     Restaura las transiciones de contenido después de iniciar la aplicación.
        /// </summary>
        /// <param name="sender">Objeto al que se asocia el controlador.</param>
        /// <param name="e">Detalles sobre el evento de navegación.</param>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = _transitions ?? new TransitionCollection { new NavigationThemeTransition() };
            rootFrame.Navigated -= RootFrame_FirstNavigated;
        }

        /// <summary>
        ///     Se invoca al suspender la ejecución de la aplicación.  El estado de la aplicación se guarda
        ///     sin saber si la aplicación se terminará o se reanudará con el contenido
        ///     de la memoria aún intacto.
        /// </summary>
        /// <param name="sender">Origen de la solicitud de suspensión.</param>
        /// <param name="e">Detalles sobre la solicitud de suspensión.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();

            await CleanupCaptureResources();

            deferral.Complete();
        }
    }
}