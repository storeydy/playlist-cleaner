namespace PlaylistCleaner.Infrastructure.Exceptions;

internal sealed class TokenNotFoundException : Exception
{
    public TokenNotFoundException(string message) : base(message)
    {
    }
}
