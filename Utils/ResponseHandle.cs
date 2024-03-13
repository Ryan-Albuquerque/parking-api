namespace Parking.Utils
{
    public class ResponseHandler<T>(T? result, string? error)
    {
        public T? Result { get; } = result;
        public string? Error { get; } = error;
    }
}
