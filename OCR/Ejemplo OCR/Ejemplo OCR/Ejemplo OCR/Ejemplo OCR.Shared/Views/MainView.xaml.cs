using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Ejemplo_OCR.Views
{
    /// <summary>
    ///     MainView
    /// </summary>
    public sealed partial class MainView : Page
    {
        public MainView()
        {
            InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;
        }
    }
}