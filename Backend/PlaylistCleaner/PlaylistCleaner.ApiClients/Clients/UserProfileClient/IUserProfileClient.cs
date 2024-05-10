﻿using Microsoft.AspNetCore.Http;
using PlaylistCleaner.ApiClients.Responses.SpotifyApiClientResults.GetCurrentUsersProfile;

namespace PlaylistCleaner.ApiClients.Clients.UserProfileClient;

public interface IUserProfileClient
{
    Task<GetCurrentUsersProfileResult> GetCurrentUsersProfileAsync(CancellationToken cancellationToken = default);
}