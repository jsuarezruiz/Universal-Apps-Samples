using System.Threading.Tasks;

namespace ScreenRecorder.Services.ScreenRecorder
{
    public interface IScreenRecorderService
    {
        ScreenRecorderService.RecordingStatus Status { get; }
        Task Start(string recordName);
        void Stop();
    }
}
