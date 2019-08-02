namespace UserAuthenticaton.Web.Services
{
    public interface ICacheService
    {
        string GetToken();
        void SetToken(string userToken);
    }
}