namespace Ejemplo_CommandBar.Base
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Windows.UI.Xaml.Controls;

    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private Frame appFrame;
        private bool isBusy;

        public ViewModelBase()
        {
        }

        public Frame AppFrame
        {
            get { return appFrame; }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set 
            {
                isBusy = value;
                RaisePropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            var Handler = PropertyChanged;
            if (Handler != null)
                Handler(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void SetAppFrame(Frame viewFrame)
        {
            appFrame = viewFrame;
        }
    }
}
