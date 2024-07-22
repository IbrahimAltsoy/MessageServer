namespace Core.CrossCuttingConcerns.Exceptions.Types;

public class FileException : Exception
{
    public FileException(string message) : base(message) { }
}
