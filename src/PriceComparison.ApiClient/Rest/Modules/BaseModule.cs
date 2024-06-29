namespace PriceComparison.ApiClient.Rest.Modules;

public class BaseModule(RestClient client)
{
    protected readonly RestClient Client = client;
}