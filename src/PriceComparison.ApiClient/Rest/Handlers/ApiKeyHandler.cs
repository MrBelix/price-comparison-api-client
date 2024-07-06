using System.Net;
using System.Net.Http.Headers;
using PriceComparison.ApiClient.Rest.Interfaces;
using PriceComparison.Contracts.Authentication;

namespace PriceComparison.ApiClient.Rest.Handlers;

public class ApiKeyHandler(IRestClient client) : DelegatingHandler(new HttpClientHandler())
{

    private static readonly SemaphoreSlim Sem = new(1);

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await client.TokenManager.GetTokenAsync();

        if (token != null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
        }

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized && token?.ExpiresAt >= DateTime.UtcNow)
        {
            await Sem.WaitAsync(cancellationToken);
            await client.Authentication.RefreshTokenAsync(new RefreshTokenRequest(token.RefreshToken));
        }

        return await base.SendAsync(request, cancellationToken);
    }
}