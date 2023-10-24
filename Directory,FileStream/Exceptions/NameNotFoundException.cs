namespace Directory_FileStream.Exceptions;

public class NameNotFoundException:Exception
{
    public string Message { get; set; }
    public NameNotFoundException(string message="Name is not found!404."):base(message)
    {
        Message= message;
    }
}
