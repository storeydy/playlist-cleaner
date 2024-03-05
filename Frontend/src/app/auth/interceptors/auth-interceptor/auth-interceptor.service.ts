import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { TokenService } from '../../services/token/token.service';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorService implements HttpInterceptor{

  private tokenService = inject(TokenService)

  intercept(request: HttpRequest<any>, next: HttpHandler) {

    const token = this.tokenService.retrieveTokenFromLocalStorage();
    
    if (token) { 
      request = request.clone({ 
        headers: request.headers.set('Authorization', 'Bearer ' + token)
      });
    }

    return next.handle(request);
  }
}
