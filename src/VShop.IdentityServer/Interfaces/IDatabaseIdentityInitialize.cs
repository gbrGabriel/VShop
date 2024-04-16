namespace VShopIdentityServer.Interfaces;

public interface IDatabaseIdentityInitialize
{
    Task InitializeRoles();
    Task InitializeUser();
}
