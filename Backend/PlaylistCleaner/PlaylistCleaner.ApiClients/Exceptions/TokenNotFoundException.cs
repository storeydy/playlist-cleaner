﻿namespace PlaylistCleaner.ApiClients.Exceptions;

public class TokenNotFoundException : Exception
{
    public TokenNotFoundException(string message) : base(message)
    {
    }
}
