import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development'; //Local environment variables file - in gitignore 
import { Buffer } from 'buffer';


const params = new URLSearchParams(window.location.search);
const code: any = params.get("code");
const clientId = environment.clientId;

@Injectable({
  providedIn: 'root',
})
export class LoginService {

  async loginAsync(){
    if (!code) {
      this.redirectToAuthCodeFlow(clientId);
    }
    else {
      const accessToken = await this.getAccessToken(clientId, code);
      localStorage.setItem('access_token', accessToken);
    }
  }

  async redirectToAuthCodeFlow(clientId: string) {    //GET request to /authorize with permission list 
    const verifier = this.generateCodeVerifier(128);
    const challenge = await this.generateCodeChallenge(verifier);
    const state = this.generateCodeVerifier(16);

    localStorage.setItem("verifier", verifier);

    const params = new URLSearchParams();
    params.append("client_id", clientId);
    params.append("response_type", "code");
    params.append("state", state);
    params.append("redirect_uri", "http://localhost:4200/callback");
    params.append("scope", "user-read-private user-read-email playlist-read-private playlist-read-collaborative");
    params.append("code_challenge_method", "S256");
    params.append("code_challenge", challenge);

    document.location = `https://accounts.spotify.com/authorize?${params.toString()}`;
  }

  generateCodeVerifier(length: number) {     //PKCE Cryptographic String -> input between 43-128
    let text = '';
    let possible = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';

    for (let i = 0; i < length; i++) {
        text += possible.charAt(Math.floor(Math.random() * possible.length));
    }
    return text;
  }

  async generateCodeChallenge(codeVerifier: string) {    //SHA Hash of random string
    const data = new TextEncoder().encode(codeVerifier);
    const digest = await window.crypto.subtle.digest('SHA-256', data);
    const base64Digest = Buffer.from(new Uint8Array(digest)).toString('base64');
    return base64Digest
        .replace(/\+/g, '-')
        .replace(/\//g, '_')
        .replace(/=+$/, '');
  }

  async getAccessToken(clientId: string, code: string): Promise<string> { //Gets token using auth code
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
          "Content-Type": "application/x-www-form-urlencoded" },
        body: params
    });
    
    const { access_token } = await result.json();
    
    return access_token;
  }
}
