using PriceComparison.ApiClient.Rest.TokenManagers;

namespace PriceComparison.ApiClient.Rest.Interfaces;

public interface IRestClient
{
    public BaseTokenManager TokenManager { get; }

    public IAuthModule Authentication { get; }

    public IUserModule User { get; }
}