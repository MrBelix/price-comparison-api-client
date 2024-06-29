using PriceComparison.ApiClient.Common;
using PriceComparison.Contracts.Authentication;

namespace PriceComparison.ApiClient.Rest.Interfaces;

public interface IAuthModule
{
    public Task<Response<AccessTokenResponse>> LoginAsync(LoginRequest request);

    public Task<Response<AccessTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request);
}