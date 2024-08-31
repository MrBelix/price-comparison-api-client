using PriceComparison.Contracts.Authentication;

namespace PriceComparison.ApiClient.Rest.TokenManagers;

public abstract class BaseTokenManager
{
    public event EventHandler<AccessTokenResponse?>? TokenChanged;

    public abstract Task<AccessTokenResponse?> GetTokenAsync();

    public async Task SetTokenAsync(AccessTokenResponse? token)
    {
        if (token != await GetTokenAsync())
        {
            await UpdateTokenAsync(token);
            TokenChanged?.Invoke(this, token);
        }
    }

    protected abstract Task UpdateTokenAsync(AccessTokenResponse? token);
}