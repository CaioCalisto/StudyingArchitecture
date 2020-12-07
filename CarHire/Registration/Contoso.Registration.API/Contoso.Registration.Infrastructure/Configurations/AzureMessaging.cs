namespace Contoso.Registration.Infrastructure.Configurations
{
    /// <summary>
    /// Azure Messaging Config.
    /// </summary>
    public class AzureMessaging
    {
        /// <summary>
        /// Gets or Sets ConnectionString.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets Topic name.
        /// </summary>
        public string Topic { get; set; }
    }
}
