using PriceComparison.Contracts.Authentication;

namespace PriceComparison.ApiClient.Rest.TokenManagers;

public class VariableTokenManager : BaseTokenManager
{
    private AccessTokenResponse? _tokenResponse;

    public override Task<AccessTokenResponse?> GetTokenAsync()
    {
        return Task.FromResult(_tokenResponse);
    }

    protected override Task UpdateTokenAsync(AccessTokenResponse? token)
    {
        _tokenResponse = token;
        return Task.CompletedTask;
    }
}