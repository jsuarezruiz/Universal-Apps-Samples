using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using WindowsPreview.Media.Ocr;

namespace Ejemplo_OCR.Services.Ocr
{
    public class OcrService : IOcrService
    {
        private OcrEngine _ocrEngine;

        public async Task<string> GetText(WriteableBitmap bitmap)
        {
            string result = string.Empty;

            if (_ocrEngine == null)
                _ocrEngine = new OcrEngine(OcrLanguage.English);

            // Sintetizamos la imagen para extraer el texto (RecognizeAsync)
            var ocrResult = await _ocrEngine.RecognizeAsync((uint)bitmap.PixelHeight, (uint)bitmap.PixelWidth, bitmap.PixelBuffer.ToArray());

            // Si el resultado no contiene líneas no hacemos nada
            if (ocrResult.Lines != null)
                // Si hay líneas, las vamos añadiendo al resultado final
                result = ocrResult.Lines.Aggregate(result, (current1, line) => line.Words.Aggregate(current1, (current, word) => current + word.Text + " "));

            return result;
        }
    }
}
