using PriceComparison.Contracts.Users;

namespace PriceComparison.ApiClient.Rest.Interfaces;

public interface IUserModule
{
    public Task Create(CreateUserRequest request);
}