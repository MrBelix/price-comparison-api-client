using PriceComparison.Contracts.Authentication;

namespace PriceComparison.ApiClient.Rest.TokenManagers;

public abstract class BaseTokenManager
{
    public event EventHandler<AccessTokenResponse?>? TokenChanged;

    public abstract Task<AccessTokenResponse?> GetTokenAsync();

    public abstract Task UpdateTokenAsync(AccessTokenResponse? token);

    public async Task SetTokenAsync(AccessTokenResponse? token)
    {
        if (token != await GetTokenAsync())
        {
            TokenChanged?.Invoke(this, token);
        }
    }
}