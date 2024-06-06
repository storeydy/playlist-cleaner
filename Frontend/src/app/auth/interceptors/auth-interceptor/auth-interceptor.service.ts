import { HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { from, lastValueFrom } from 'rxjs';
import { TokenService } from '../../services/token/token.service';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorService implements HttpInterceptor{

  private tokenService = inject(TokenService)

  intercept(request: HttpRequest<any>, next: HttpHandler) {
    return from(this.handle(request, next))
  }

  async handle(req: HttpRequest<any>, next: HttpHandler) {
    var token = this.tokenService.retrieveAccessTokenFromLocalStorage();
    const expiry = this.tokenService.retrieveTokenExpiryFromLocalStorage();
        
    if (token && expiry) {
      if (Date.now() + 180000 > expiry - 3300000){
        token = await this.tokenService.getAccessTokenUsingRefreshToken();
      }

      req = req.clone({ 
        headers: req.headers.set('Authorization', 'Bearer ' + token)
      });
    }

    return lastValueFrom(next.handle(req));
  }
}
