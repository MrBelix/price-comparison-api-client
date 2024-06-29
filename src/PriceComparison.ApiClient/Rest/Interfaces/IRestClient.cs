using PriceComparison.Contracts.Authentication;

namespace PriceComparison.ApiClient.Rest.Interfaces;

public interface IRestClient
{

    public AccessTokenResponse? AccessToken { get; }

    public event EventHandler<AccessTokenResponse?> AccessTokenChanged;

    public IAuthModule Authentication { get; }

    public void SetAccessToken(AccessTokenResponse? accessToken);
}