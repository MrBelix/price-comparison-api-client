using System.Net.Http.Headers;
using System.Net.Http.Json;
using PriceComparison.ApiClient.Common;
using PriceComparison.ApiClient.Rest.Handlers;
using PriceComparison.ApiClient.Rest.Interfaces;
using PriceComparison.ApiClient.Rest.Modules;
using PriceComparison.ApiClient.Rest.TokenManagers;

namespace PriceComparison.ApiClient.Rest;

public sealed class RestClient : IRestClient
{
    private readonly HttpClient _httpClient;
    private readonly Lazy<IAuthModule> _authenticationModule;

    public ITokenManager TokenManager { get; init; }

    public IAuthModule Authentication => _authenticationModule.Value;

    public RestClient(Uri baseUri, ITokenManager? tokenManager = null)
    {
        TokenManager = tokenManager ?? new VariableTokenManager();

        _httpClient = new HttpClient(new ApiKeyHandler(this))
        {
            BaseAddress = baseUri,
            DefaultRequestHeaders =
            {
                Accept =
                {
                    new MediaTypeWithQualityHeaderValue("application/json")
                }
            }
        };

        _authenticationModule = new Lazy<IAuthModule>(() => new AuthenticationModule(this));
    }

    internal async Task<Response<TResponse>> SendAsync<TResponse>(string uri, HttpMethod method, CancellationToken cancellationToken = default)
    {
        var message = new HttpRequestMessage(method, uri);

        var response = await _httpClient.SendAsync(message, cancellationToken);

        return await ParseResponseAsync<TResponse>(response);
    }

    internal async Task<Response<TResponse>> SendAsync<TRequest, TResponse>(string uri, HttpMethod method, TRequest request, CancellationToken cancellationToken = default)
    {
        var message = new HttpRequestMessage(method, uri)
        {
            Content = JsonContent.Create(request)
        };

        var response = await _httpClient.SendAsync(message, cancellationToken);

        return await ParseResponseAsync<TResponse>(response);
    }

    private static async Task<Response<T>> ParseResponseAsync<T>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<T>();
            if (content is null)
            {
                throw new InvalidCastException("Failed to parse response content");
            }
            return new Response<T>(content);
        }

        var error = await response.Content.ReadFromJsonAsync<ResponseError>();
        if (error is null)
        {
            throw new InvalidCastException("Failed to parse response content");
        }
        return new Response<T>(error);
    }
}