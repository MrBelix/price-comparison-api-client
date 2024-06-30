using PriceComparison.ApiClient.Common;
using PriceComparison.ApiClient.Rest.Interfaces;
using PriceComparison.Contracts.Authentication;

namespace PriceComparison.ApiClient.Rest.Modules;

public sealed class AuthenticationModule(RestClient client) : BaseModule(client), IAuthModule
{
    public async Task<Response<AccessTokenResponse>> LoginAsync(LoginRequest request)
    {
        var response = await Client.SendAsync<LoginRequest, AccessTokenResponse>("login", HttpMethod.Post, request);

        response.Do(
            r => Client.TokenManager.SetTokenAsync(r),
            _ => Client.TokenManager.SetTokenAsync(null));

        return response;
    }

    public async Task<Response<AccessTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var response = await Client.SendAsync<RefreshTokenRequest, AccessTokenResponse>("refresh", HttpMethod.Post, request);

        response.Do(
            r => Client.TokenManager.SetTokenAsync(r),
            _ => Client.TokenManager.SetTokenAsync(null));

        return response;
    }
}