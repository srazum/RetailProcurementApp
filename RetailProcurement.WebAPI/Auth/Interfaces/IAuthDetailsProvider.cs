namespace RetailProcurement.WebAPI.Auth.Interfaces
{
    public interface IAuthDetailsProvider
    {
        string GetUserName();
        string GetPassword();
        string GetJwtIssuer();
        string GetJwtAudience();
        string GetJwtKey();
    }
}
