using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using PriceComparison.ApiClient.Rest.Interfaces;
using PriceComparison.Contracts.Authentication;

namespace PriceComparison.ApiClient.Rest.Handlers;

public class ApiKeyHandler(IRestClient client) : DelegatingHandler(new HttpClientHandler())
{

    private static readonly SemaphoreSlim Sem = new(1);

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {

        await Sem.WaitAsync(cancellationToken);
        try
        {
            await TryRefreshToken(cancellationToken);
        }
        finally
        {
            Sem.Release();
        }

        var token = await client.TokenManager.GetTokenAsync();

        if (token != null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }

    private async Task TryRefreshToken(CancellationToken cancellationToken = default)
    {
        var token = await client.TokenManager.GetTokenAsync();

        if (token is null)
        {
            return;
        }

        if (token.ExpiresAt <= DateTimeOffset.Now.AddMinutes(-1))
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "refresh")
            {
                Content = JsonContent.Create(new RefreshTokenRequest(token.RefreshToken))
            };

            var response = await base.SendAsync(request, cancellationToken);

            AccessTokenResponse? newToken = null;
            if (response.IsSuccessStatusCode)
            {
                newToken = await response.Content.ReadFromJsonAsync<AccessTokenResponse>(cancellationToken);
            }

            await client.TokenManager.SetTokenAsync(newToken);
        }
    }
}