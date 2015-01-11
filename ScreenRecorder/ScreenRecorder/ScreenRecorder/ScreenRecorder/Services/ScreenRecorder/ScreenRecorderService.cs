using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.UI.Xaml;

namespace ScreenRecorder.Services.ScreenRecorder
{
    public class ScreenRecorderService : IScreenRecorderService
    {
        public enum RecordingStatus
        {
            Stopped,
            Recording,
            Failed,
            Sucessfull
        };

        private MediaCapture _mediaCapture;
        private RecordingStatus _recordingStatus;

        public RecordingStatus Status
        {
            get { return _recordingStatus; }
        }

        public async Task Start(string recordName)
        {
            try
            {
                //Inicializamos ScreenCapture.
                ScreenCapture screenCapture = ScreenCapture.GetForCurrentView();

                // Establecemos MediaCaptureInitializationSettings para que ScreenCapture capture audio y video.
                var mediaCaptureInitializationSettings = new MediaCaptureInitializationSettings
                {
                    VideoSource = screenCapture.VideoSource,
                    AudioSource = screenCapture.AudioSource,
                    StreamingCaptureMode = StreamingCaptureMode.AudioAndVideo
                };

                // Inicializamos MediaCapture con los settings anteriores.
                _mediaCapture = new MediaCapture();
                await _mediaCapture.InitializeAsync(mediaCaptureInitializationSettings);

                _mediaCapture.Failed += (s, e) => { _recordingStatus = RecordingStatus.Failed; };
                _mediaCapture.RecordLimitationExceeded += s => { _recordingStatus = RecordingStatus.Failed; };

                screenCapture.SourceSuspensionChanged += (s, e) => Debug.WriteLine("IsAudioSuspended:" +
                                                                                   e.IsAudioSuspended +
                                                                                   " IsVideoSuspended:" +
                                                                                   e.IsVideoSuspended);

                // Establecemos el MediaCapture de App.xaml.cs para gestionar la suspensión.
                var app = Application.Current as App;
                if (app != null) app.MediaCapture = _mediaCapture;

                // Creamos un encondig a utilizar. Por defecto, MP4 1080P.                  
                MediaEncodingProfile mediaEncodingProfile = MediaEncodingProfile.CreateMp4(VideoEncodingQuality.HD1080p);

                // Creamos el fichero resultante de la grabación.            
                StorageFile video =
                    await
                        ApplicationData.Current.LocalFolder.CreateFileAsync(string.Format("{0}.mp4", recordName),
                            CreationCollisionOption.ReplaceExisting);

                // Con el formato, calidad y archivo destino definidos, comenzamos a grabar.
                IAsyncAction startAction = _mediaCapture.StartRecordToStorageFileAsync(mediaEncodingProfile, video);
                startAction.Completed += (info, status) =>
                {
                    if (status == AsyncStatus.Completed)
                        _recordingStatus = RecordingStatus.Recording;
                    if (status == AsyncStatus.Error)
                    {
                        _recordingStatus = RecordingStatus.Failed;
                        Debug.WriteLine(info.ErrorCode.Message);
                    }
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                _recordingStatus = RecordingStatus.Failed;
            }
        }

        public void Stop()
        {
            //Detenemos la grabación.           
            IAsyncAction stopAction = _mediaCapture.StopRecordAsync();
            stopAction.Completed += (info, status) =>
            {
                if (status == AsyncStatus.Completed)
                    if (_recordingStatus == RecordingStatus.Recording)
                        _recordingStatus = RecordingStatus.Sucessfull;
            };
        }
    }
}
