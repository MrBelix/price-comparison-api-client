using PriceComparison.ApiClient.Rest.Interfaces;
using PriceComparison.Contracts.Authentication;

namespace PriceComparison.ApiClient.Rest.TokenManagers;

public class VariableTokenManager : ITokenManager
{
    private AccessTokenResponse? _tokenResponse;

    public event EventHandler<AccessTokenResponse?>? TokenChanged;

    public Task<AccessTokenResponse?> GetTokenAsync()
    {
        return Task.FromResult(_tokenResponse);
    }

    public Task SetTokenAsync(AccessTokenResponse? token)
    {
        _tokenResponse = token;
        TokenChanged?.Invoke(this, token);
        return Task.CompletedTask;
    }
}