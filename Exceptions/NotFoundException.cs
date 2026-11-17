namespace FitTrack.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }

    public NotFoundException(string entityName, int key)
        : base($"{entityName} with id '{key}' was not found") { }
}
