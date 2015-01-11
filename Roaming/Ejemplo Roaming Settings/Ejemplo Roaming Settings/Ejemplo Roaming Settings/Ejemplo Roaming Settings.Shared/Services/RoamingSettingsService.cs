namespace Ejemplo_Roaming_Settings.Services
{
    using Windows.Storage;

    /// <summary>
    ///  Almacenar y recuperar configuraciones y archivos desde el almacén de datos móviles de aplicaciones. 
    /// 
    /// Más info: http://msdn.microsoft.com/es-es/library/windows/apps/xaml/hh700362.aspx
    /// </summary>
    public class RoamingSettingsService : IRoamingSettingsService
    {
        internal ApplicationDataContainer RoamingSettings
        {
            get { return ApplicationData.Current.RoamingSettings; }
        }

        /// <summary>
        /// Escribir datos en una configuración.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SaveData(string key, string value)
        {
            RoamingSettings.Values[key] = value;
        }

        /// <summary>
        /// Escribir datos en una configuración.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SaveData(string container, string key, string value)
        {
            RoamingSettings.Containers[container].Values[key] = value;
        }

        /// <summary>
        /// Escribir datos en una configuración.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SaveData(string key, ApplicationDataCompositeValue value)
        {
            RoamingSettings.Values[key] = value;
        }

        /// <summary>
        /// Verifica si existe algun dato relacionado con la clave facilitada.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            return RoamingSettings.Values.ContainsKey(key);
        }

        /// <summary>
        /// Leer datos desde una configuración.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetData(string key)
        {
            return RoamingSettings.Values[key];
        }

        /// <summary>
        /// Leer datos desde una configuración de un contenedor de configuración especificado.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetData(string container, string key)
        {
            return RoamingSettings.Containers[container].Values[key];
        }

        /// <summary>
        /// Leer datos desde una configuración.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ApplicationDataCompositeValue GetDataComposite(string key)
        {
            return (ApplicationDataCompositeValue)RoamingSettings.Values[key];
        }

        /// <summary>
        /// Eliminar configuraciones.
        /// </summary>
        /// <param name="key"></param>
        public void RemoveData(string key)
        {
            RoamingSettings.Values.Remove(key);
        }

        /// <summary>
        /// Crea o abre el contenedor de configuración especificado.
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public ApplicationDataContainer CreateContainer(string container)
        {
            return RoamingSettings.CreateContainer(container, ApplicationDataCreateDisposition.Always);
        }

        /// <summary>
        /// Elimina el contenedor de configuración especificado.
        /// </summary>
        /// <param name="container"></param>
        public void RemoveContainer(string container)
        {
            RoamingSettings.DeleteContainer(container);
        }
    }
}
