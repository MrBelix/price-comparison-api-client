using System.Collections.Immutable;

namespace PriceComparison.ApiClient.Common;

public record ResponseError
{
    public string? Message { get; init; }
    public string? Code { get; init; }
    public IImmutableDictionary<string, IImmutableList<string>>? Errors { get; init; }
}