using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using ScreenRecorder.ViewModels.Base;

namespace ScreenRecorder.ViewModels
{
    public class ResultViewModel : ViewModelBase
    {
        private readonly MediaElement _video;

        public ResultViewModel()
        {
            _video = new MediaElement
            {
                AreTransportControlsEnabled = true,
                IsFullWindow = true,
                AutoPlay = true
            }; 
        }

        public override Task OnNavigatedFrom(NavigationEventArgs args)
        {
            return null;
        }

        public override async Task OnNavigatedTo(NavigationEventArgs args)
        {
            if (args.Parameter == null)
                return;

            var videoName = args.Parameter.ToString();
            StorageFile file =
                await ApplicationData.Current.LocalFolder.GetFileAsync(string.Format("{0}.mp4", videoName));
            var stream = await file.OpenAsync(FileAccessMode.Read);
            Video.SetSource(stream, file.FileType); 
        }

        public MediaElement Video
        {
            get { return _video; }
        }
    }
}
