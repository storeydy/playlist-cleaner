import { clientId, redirectToAuthCodeFlow } from "./auth/login";
import { getAccessToken } from "./auth/token";
import { fetchProfile, getPlaylists, populateUI } from "./homepage/homepage";
import * as dotenv from 'dotenv';


const params = new URLSearchParams(window.location.search);
const code = params.get("code");

if (!code) {
    redirectToAuthCodeFlow(clientId);
} else {
    const accessToken = await getAccessToken(clientId, code);
    const profile = await fetchProfile(accessToken);
    const playlists = await getPlaylists(accessToken, profile.id);
    console.log(profile);
    console.log(playlists);
    populateUI(profile);
}


