namespace PlaylistCleaner.Infrastructure.Exceptions;

internal sealed class SpotifyApiHttpException : Exception
{
    public SpotifyApiHttpException(string message) : base(message)
    {
    }
}