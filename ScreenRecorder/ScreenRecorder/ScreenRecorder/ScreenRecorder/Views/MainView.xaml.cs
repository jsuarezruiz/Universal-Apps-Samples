using Windows.UI.Xaml.Navigation;
using ScreenRecorder.Views.Base;

namespace ScreenRecorder.Views
{
    /// <summary>
    ///     MainView
    /// </summary>
    public sealed partial class MainView : PageBase
    {
        public MainView()
        {
            InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;
        }
    }
}