namespace PriceComparison.ApiClient.Rest.Interfaces;

public interface IRestClient
{
    public IAuthModule Authentication { get; }
}