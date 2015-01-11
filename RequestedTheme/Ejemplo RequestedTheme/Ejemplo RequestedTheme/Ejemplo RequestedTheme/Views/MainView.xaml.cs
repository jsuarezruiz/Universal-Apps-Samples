namespace Ejemplo_RequestedTheme.Views
{
    using Windows.UI.Xaml.Navigation;
    using Base;

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