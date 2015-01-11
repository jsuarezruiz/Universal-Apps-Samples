using System.Collections.Generic;
using Windows.Security.Credentials;

namespace CredentialLocker.Services.PasswordVaultService
{
    /// <summary>
    /// Representa una Caja de seguridad de credenciales. El contenido de la caja de seguridad es específico de la aplicación o servicio. 
    /// Las aplicaciones y los servicios no tienen acceso a las credenciales asociadas a otras aplicaciones o servicios.
    /// 
    /// Más información: http://msdn.microsoft.com/library/windows/apps/windows.security.credentials.passwordvault.aspx
    /// </summary>
    public class PasswordVaultService : IPasswordVaultService
    {
        /// <summary>
        /// Agrega una credencial a la Caja de seguridad de credenciales.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public void Save(string resource, string userName, string password)
        {
            PasswordVault vault = new PasswordVault();
            PasswordCredential cred = new PasswordCredential(resource, userName, password);
            vault.Add(cred);
        }

        /// <summary>
        /// Lee una credencial de la Caja de seguridad de credenciales.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public PasswordCredential Read(string resource, string userName)
        {
            PasswordVault vault = new PasswordVault();

            return vault.Retrieve(resource, userName);
        }

        /// <summary>
        /// Recupera todas las credenciales almacenadas en la Caja de seguridad de credenciales.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<PasswordCredential> GetAll()
        {
            PasswordVault vault = new PasswordVault();

            return vault.RetrieveAll();
        }

        /// <summary>
        /// Quita una credencial de la Caja de seguridad de credenciales.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="userName"></param>
        public void Delete(string resource, string userName)
        {
            PasswordVault vault = new PasswordVault();
            PasswordCredential cred = vault.Retrieve(resource, userName);
            vault.Remove(cred);
        }
    }
}
