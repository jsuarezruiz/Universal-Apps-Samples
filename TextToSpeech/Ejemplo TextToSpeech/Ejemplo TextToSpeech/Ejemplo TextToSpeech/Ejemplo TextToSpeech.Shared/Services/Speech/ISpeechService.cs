using System.Threading.Tasks;

namespace Ejemplo_TextToSpeech.Services.Speech
{
    /// <summary>
    /// SpeechService Contract.
    /// </summary>
    public interface ISpeechService
    {
        /// <summary>
        /// Synthetize a text into a speech and pronounces it.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        Task TextToSpeech(string text);

        /// <summary>
        /// Stop the current speech running.
        /// </summary>
        void StopSpeech();
    }
}
