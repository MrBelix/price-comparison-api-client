using System.Collections.Immutable;

namespace PriceComparison.ApiClient.Common;

public record ResponseError
{
    public required string Message { get; init; }
    public required string Code { get; init; }
    public IImmutableDictionary<string, IImmutableList<string>> Errors { get; init; } = ImmutableDictionary<string, IImmutableList<string>>.Empty;
}