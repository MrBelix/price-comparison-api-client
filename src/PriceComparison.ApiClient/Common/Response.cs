namespace PriceComparison.ApiClient.Common;

public class Response<T>
{
    private T? Data { get; init; }

    private ResponseError? Error { get; init; }

    private bool IsSuccess { get; init; }

    public Response(T data)
    {
        Data = data;
        IsSuccess = true;
    }

    public Response(ResponseError error)
    {
        Error = error;
        IsSuccess = false;
    }

    public void Do(Action<T> successAction, Action<ResponseError> errorAction)
    {
        if (IsSuccess)
        {
            if (Data is null)
            {
                throw new NullReferenceException(nameof(Data));
            }

            successAction(Data);
        }
        else
        {
            if (Error is null)
            {
                throw new NullReferenceException(nameof(Error));
            }
            
            errorAction(Error);
        }
    }
}