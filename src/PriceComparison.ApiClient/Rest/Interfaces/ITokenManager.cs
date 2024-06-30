using PriceComparison.Contracts.Authentication;

namespace PriceComparison.ApiClient.Rest.Interfaces;

public interface ITokenManager
{
    event EventHandler<AccessTokenResponse?> TokenChanged;
    
    Task<AccessTokenResponse?> GetTokenAsync();

    Task SetTokenAsync(AccessTokenResponse? token);
}