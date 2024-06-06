import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development'; //Local environment variables file - in gitignore 

const clientId = environment.clientId;

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  retrieveAccessTokenFromLocalStorage(): string | null {
    var accessToken = localStorage.getItem('access_token');
    return accessToken !== null && accessToken !== 'undefined' ? accessToken : null;
  }

  retrieveRefreshTokenFromLocalStorage(): string | null {
    var refreshToken = localStorage.getItem('refresh_token');
    return refreshToken !== null && refreshToken !== 'undefined' ? refreshToken : null;
  }

  retrieveTokenExpiryFromLocalStorage(): number | null {
    var expiryString = localStorage.getItem('token_expiry');    
    return expiryString !== null ? parseInt(expiryString!) : null;
  }

  async getAccessAndRefreshTokens(clientId: string, code: string) { //Gets token using auth code
    const verifier = localStorage.getItem("verifier");
    const params = new URLSearchParams();
    params.append("client_id", clientId);
    params.append("grant_type", "authorization_code");
    params.append("code", code);
    params.append("redirect_uri", "http://localhost:4200/callback");
    params.append("code_verifier", verifier!);

    const result = await fetch("https://accounts.spotify.com/api/token", {
      method: "POST",
      headers: {
        "Content-Type": "application/x-www-form-urlencoded"
      },
      body: params
    });
    const response = await result.json();

    var tokenExpiry = Date.now() + response.expires_in*1000;      
    
    localStorage.setItem('access_token', response.access_token);
    localStorage.setItem('refresh_token', response.refresh_token);
    localStorage.setItem('token_expiry', tokenExpiry.toString());
  }

  async getAccessTokenUsingRefreshToken() : Promise<string> {
    const params = new URLSearchParams();
    let refreshToken = this.retrieveRefreshTokenFromLocalStorage();
    params.append("grant_type", "refresh_token");
    params.append("refresh_token", refreshToken!);
    params.append("client_id", clientId);
    
    const result = await fetch("https://accounts.spotify.com/api/token", {
      method: "POST",
      headers: {
        "Content-Type": "application/x-www-form-urlencoded"
      },
      body: params
    });
    const response = await result.json();

    var tokenExpiry = Date.now() + response.expires_in*1000;

    
    localStorage.setItem('access_token', response.access_token);
    localStorage.setItem('refresh_token', response.refresh_token);
    localStorage.setItem('token_expiry', tokenExpiry.toString());    

    return response.access_token
  }

}
