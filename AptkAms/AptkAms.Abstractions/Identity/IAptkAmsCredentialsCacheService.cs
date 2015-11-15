namespace Aptk.Plugins.AzureMobileServices.Identity
{
    public interface IAptkAmsCredentialsCacheService
    {
        /// <summary>
        /// Try to load previously saved credentials
        /// </summary>
        /// <param name="credentials">Saved credentials</param>
        /// <returns>Load success</returns>
        bool TryLoadCredentials(out IAptkAmsCredentials credentials);

        /// <summary>
        /// Save actual credentials
        /// </summary>
        /// <param name="credentials">Credentials to save</param>
        void SaveCredentials(IAptkAmsCredentials credentials);

        /// <summary>
        /// Clear saved credentials
        /// </summary>
        void ClearCredentials();
    }
}
