namespace UserServiceInterface.DTO;

public sealed class Result<T> where T : class
{
    public Result(bool isError, string message, T data) => (IsError, Message, Data) = (isError, message, data);
    public bool IsError { get; }
    public string Message { get; }
    public T Data { get; }
    public static Result<T> Ok(T data) => new(false, "Ok", data);
    public static Result<T> Fail(string message) => new(true, message, null);
    public override string ToString() => $"IsError={IsError}, Message={Message}";
}

public sealed class Result
{
    public Result(bool isError, string message) => (IsError, Message) = (isError, message);
    public bool IsError { get; }
    public string Message { get; }
    public static Result Ok() => new(false, "Ok");
    public static Result Fail(string message) => new(true, message);
    public override string ToString() => $"IsError={IsError}, Message={Message}";
}