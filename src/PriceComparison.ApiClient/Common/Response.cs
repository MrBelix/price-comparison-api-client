namespace PriceComparison.ApiClient.Common;

public class Response<TResult>
{
    private TResult? Data { get; init; }

    private ResponseError? Error { get; init; }

    private bool IsSuccess { get; init; }

    public Response(TResult data)
    {
        Data = data;
        IsSuccess = true;
    }

    public Response(ResponseError error)
    {
        Error = error;
        IsSuccess = false;
    }

    public void Do(Action<TResult> successAction, Action<ResponseError> errorAction)
    {
        if (IsSuccess)
        {
            successAction(Data ?? throw new NullReferenceException(nameof(Data)));
        }
        else
        {
            errorAction(Error ?? throw new NullReferenceException(nameof(Error)));
        }
    }

    public T Match<T>(Func<TResult, T> successAction, Func<ResponseError, T> errorAction)
    {
        return IsSuccess
            ? successAction(Data ?? throw new NullReferenceException(nameof(Data)))
            : errorAction(Error ?? throw new NullReferenceException(nameof(Error)));
    }

    public Task DoAsync(Func<TResult, Task> successAction, Func<ResponseError, Task> errorAction)
    {
        return IsSuccess
            ? successAction(Data ?? throw new NullReferenceException(nameof(Data)))
            : errorAction(Error ?? throw new NullReferenceException(nameof(Error)));
    }
}