using System.Net.Http.Headers;
using PriceComparison.Contracts.Authentication;

namespace PriceComparison.ApiClient.Rest.Handlers;

public class ApiTokenHandler(RestClient client) : DelegatingHandler(new HttpClientHandler())
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (client.AccessToken is not null)
        {
            if (client.AccessToken.ExpiresAt >= DateTime.UtcNow)
            {
                await client.Authentication.RefreshTokenAsync(new RefreshTokenRequest(client.AccessToken.RefreshToken));
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", client.AccessToken.AccessToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}