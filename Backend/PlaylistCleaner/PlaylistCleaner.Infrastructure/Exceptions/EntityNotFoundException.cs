namespace PlaylistCleaner.Infrastructure.Exceptions;

internal sealed class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string message) : base(message)
    {
    } 
}
