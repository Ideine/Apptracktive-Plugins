namespace Aptk.Plugins.AzureForMobile.Identity
{
    public interface IAzureForMobileCredentialsCacheService
    {
        /// <summary>
        /// Try to load previously saved credentials
        /// </summary>
        /// <param name="credentials">Saved credentials</param>
        /// <returns>Load success</returns>
        bool TryLoadCredentials(out IAzureForMobileCredentials credentials);

        /// <summary>
        /// Save actual credentials
        /// </summary>
        /// <param name="credentials">Credentials to save</param>
        void SaveCredentials(IAzureForMobileCredentials credentials);

        /// <summary>
        /// Clear saved credentials
        /// </summary>
        void ClearCredentials();
    }
}
