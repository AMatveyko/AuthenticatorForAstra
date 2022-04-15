namespace UserServiceInterface.DTO;

public sealed class Answer<T>
{
    public bool IsError { get; set; }
    public string Message { get; set; } = "Ok";
    public T Result { get; set; }
}

public sealed class Answer
{
    public bool IsError { get; set; }
    public string Message { get; set; } = "Ok";
}