namespace PriceComparison.ApiClient.Rest.Interfaces;

public interface IRestClient
{
    public ITokenManager TokenManager { get; }
    public IAuthModule Authentication { get; }
}