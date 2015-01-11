namespace Ejemplo_Roaming_Settings.Services
{
    using Windows.Storage;

    /// <summary>
    /// Almacenar y recuperar configuraciones y archivos desde el almacén de datos móviles de aplicaciones. 
    /// </summary>
    public interface IRoamingSettingsService
    {
        void SaveData(string key, string value);

        void SaveData(string container, string key, string value);

        void SaveData(string key, ApplicationDataCompositeValue value);

        bool ContainsKey(string key);

        object GetData(string key);

        object GetData(string container, string key);

        ApplicationDataCompositeValue GetDataComposite(string key);

        void RemoveData(string key);

        ApplicationDataContainer CreateContainer(string container);

        void RemoveContainer(string container);
    }
}
