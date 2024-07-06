using System.Net.Http.Headers;
using PriceComparison.ApiClient.Rest.Interfaces;
using PriceComparison.Contracts.Authentication;

namespace PriceComparison.ApiClient.Rest.Handlers;

public class ApiKeyHandler(IRestClient client) : DelegatingHandler(new HttpClientHandler())
{

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = client.TokenManager.GetToken();

        if (token?.ExpiresAt <= DateTime.UtcNow)
        {
            await client.Authentication.RefreshTokenAsync(new RefreshTokenRequest(token.RefreshToken));
            token = client.TokenManager.GetToken();
        }

        if (token != null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}