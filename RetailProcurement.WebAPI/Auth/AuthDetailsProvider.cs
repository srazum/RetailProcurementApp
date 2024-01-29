using RetailProcurement.WebAPI.Auth.Interfaces;
using System.Configuration;

namespace RetailProcurement.WebAPI.Auth
{
    public class AuthDetailsProvider : IAuthDetailsProvider
    {
        ConfigurationManager _configurationManager;
        public AuthDetailsProvider(ConfigurationManager configurationManager)
        {
            _configurationManager = configurationManager;
        }

        public string GetUserName()
        {
            return _configurationManager.GetValue<string>("USER")!;
        }
        public string GetPassword()
        {
            return _configurationManager.GetValue<string>("PASSWORD")!;
        }
        public string GetJwtIssuer()
        {
            return _configurationManager.GetValue<string>("JWT_ISSUER")!;
        }
        public string GetJwtKey()
        {
            return _configurationManager.GetValue<string>("JWT_KEY")!;
        }
        public string GetJwtAudience()
        {
            return _configurationManager.GetValue<string>("JWT_AUDIENCE")!;
        }
    }
}
