import { clientId, redirectToAuthCodeFlow } from "./auth/login";
import { getAccessToken } from "./auth/token";
import { getRecentlyPlayed } from "./history/recently-played";
import { fetchProfile, getPlaylists, getTracksInPlaylist, fillUserProfileSummary, fillUserPlaylistsDisplay, fillHistoryDisplay } from "./homepage/homepage";
import { Playlist } from "./types";

const params = new URLSearchParams(window.location.search);
const code = params.get("code");

if (!code) {
    redirectToAuthCodeFlow(clientId);
} else {

    const accessToken = await getAccessToken(clientId, code);
    const profile = await fetchProfile(accessToken);
    let playlists : Playlist[] =  await getPlaylists(accessToken, profile.id);
    let tracks : any[] = await getTracksInPlaylist(accessToken, playlists[0].id);
    let history : any[] = await getRecentlyPlayed(accessToken);
    
    fillUserProfileSummary(profile);
    fillUserPlaylistsDisplay(playlists);
    fillHistoryDisplay(history);
}

