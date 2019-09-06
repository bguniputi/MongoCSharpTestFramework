using MongoDB.Driver;
using System;

namespace MongoCSharpLibrary.DataAccessLayer
{
    public static class GetClientSettings
    {
        public static string ApplicationName { get; set; }
        public static MongoServerAddress ServerAddress { get; set; }
        public static ConnectionMode ConnectionType { get; set; } = ConnectionMode.Automatic;
        public static MongoCredential Credentails { get; set; }
        public static TimeSpan TimeOut { get; set; }       
        public static bool UseSSL { get; set; } = false;
        public static SslSettings SSLSettings { get; set; }
        public static bool VerifySSLCertificate { get; set; } = false;
        /// <summary>
        /// Get mongo client configuration
        /// </summary>
        /// <returns></returns>
        internal static MongoClientSettings GetClientConfiguration()
        {

            MongoClientSettings settings = new MongoClientSettings();

            if (ApplicationName != null)
                settings.ApplicationName = ApplicationName;
            else if (ServerAddress != null)
                settings.Server = ServerAddress;
            else if (Credentails != null)
                settings.Credential = Credentails;
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            else if (ConnectionType != null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                settings.ConnectionMode = ConnectionType;
            else if (TimeOut != null)
                settings.ConnectTimeout = TimeOut;
            else if (UseSSL)
            {
                settings.AllowInsecureTls = VerifySSLCertificate;
                settings.SslSettings = SSLSettings;
            }

            return settings;
        }
    }
}
