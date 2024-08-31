using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace PriceComparison.ApiClient.Common;

public record ResponseError
{
    [JsonPropertyName("title")]
    public required string Title { get; init; }

    [JsonPropertyName("status")]
    public required int Status { get; init; }

    [JsonPropertyName("detail")]
    public required string Detail { get; init; }

    [JsonPropertyName("errors")]
    public IImmutableDictionary<string, IImmutableList<string>> Errors { get; init; } = ImmutableDictionary<string, IImmutableList<string>>.Empty;
}