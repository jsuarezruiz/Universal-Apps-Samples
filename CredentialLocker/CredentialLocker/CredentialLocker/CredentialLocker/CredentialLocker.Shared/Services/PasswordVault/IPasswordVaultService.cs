using System.Collections.Generic;
using Windows.Security.Credentials;

namespace CredentialLocker.Services.PasswordVaultService
{
    public interface IPasswordVaultService
    {
        /// <summary>
        /// Agrega una credencial a la Caja de seguridad de credenciales.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        void Save(string resource, string userName, string password);

        /// <summary>
        /// Lee una credencial de la Caja de seguridad de credenciales.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        PasswordCredential Read(string resource, string userName);

        /// <summary>
        /// Recupera todas las credenciales almacenadas en la Caja de seguridad de credenciales.
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<PasswordCredential> GetAll();

        /// <summary>
        /// Quita una credencial de la Caja de seguridad de credenciales.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="userName"></param>
        void Delete(string resource, string userName);
    }
}
