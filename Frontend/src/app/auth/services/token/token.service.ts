import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  constructor() { }

    retrieveTokenFromLocalStorage(): string {
    
    return localStorage.getItem('access_token')!;
  }
}
