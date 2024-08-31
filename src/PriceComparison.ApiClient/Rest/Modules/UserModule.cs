using PriceComparison.ApiClient.Common;
using PriceComparison.ApiClient.Rest.Interfaces;
using PriceComparison.Contracts.Users;

namespace PriceComparison.ApiClient.Rest.Modules;

public class UserModule(RestClient client) : BaseModule(client), IUserModule
{
    public Task Create(CreateUserRequest request)
    {
        return Client.SendAsync<CreateUserRequest, EmptyResponse>("register", HttpMethod.Post, request);
    }
}