import { Injectable } from '@angular/core';
import { AuthenticationTokens } from 'src/app/shared/types/auth/authentication-tokens';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  constructor() { }

  retrieveAccessTokenFromLocalStorage(): string | null {
    var accessToken = localStorage.getItem('access_token');
    return accessToken !== null && accessToken !== 'undefined' ? accessToken : null;
  }

  retrieveRefreshTokenFromLocalStorage(): string | null {
    var refreshToken = localStorage.getItem('refresh_token');
    return refreshToken !== null && refreshToken !== 'undefined' ? refreshToken : null;
  }

  async getAccessAndRefreshTokens(clientId: string, code: string): Promise<AuthenticationTokens> { //Gets token using auth code
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

    setTimeout(() => {
      this.getAccessTokenUsingRefreshToken();
    }, 3500000);
    
    localStorage.setItem('access_token', response.access_token);
    localStorage.setItem('refresh_token', response.refresh_token);

    return {
      accessToken: response.access_token,
      refreshToken: response.refresh_token,
    };
  }

  private async getAccessTokenUsingRefreshToken() {
    const params = new URLSearchParams();
    let refreshToken = this.retrieveRefreshTokenFromLocalStorage();
    params.append("grant_type", "refresh_token");
    params.append("refresh_token", refreshToken!)

    const result = await fetch("https://accounts.spotify.com/api/token", {
      method: "POST",
      headers: {
        "Content-Type": "application/x-www-form-urlencoded"
      },
      body: params
    });
    const response = await result.json();

    localStorage.setItem('access_token', response.access_token);
    localStorage.setItem('refresh_token', response.refresh_token);

    setTimeout(() => {
      this.getAccessTokenUsingRefreshToken();
    }, 3500000);

  }

}
