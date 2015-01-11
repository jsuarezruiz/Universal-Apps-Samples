using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Ejemplo_OCR.Services.Ocr
{
    public interface IOcrService
    {
        Task<string> GetText(WriteableBitmap bitmap);
    }
}
