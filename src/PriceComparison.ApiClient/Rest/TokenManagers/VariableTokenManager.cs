using PriceComparison.ApiClient.Rest.Interfaces;
using PriceComparison.Contracts.Authentication;

namespace PriceComparison.ApiClient.Rest.TokenManagers;

public class VariableTokenManager : ITokenManager
{
    private AccessTokenResponse? _tokenResponse;

    public event EventHandler<AccessTokenResponse?>? TokenChanged;

    public AccessTokenResponse? GetToken()
    {
        return _tokenResponse;
    }

    public void SetToken(AccessTokenResponse? token)
    {
        _tokenResponse = token;
        TokenChanged?.Invoke(this, token);
    }
}