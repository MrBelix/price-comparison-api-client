using PriceComparison.Contracts.Authentication;

namespace PriceComparison.ApiClient.Rest.Interfaces;

public interface ITokenManager
{
    event EventHandler<AccessTokenResponse?> TokenChanged;

    AccessTokenResponse? GetToken();

    void SetToken(AccessTokenResponse? token);
}