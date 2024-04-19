import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  constructor() { }

  retrieveTokenFromLocalStorage(): string | null {
    var accessToken = localStorage.getItem('access_token');
    return accessToken !== null && accessToken !== 'undefined' ? accessToken : null;
  }


}
