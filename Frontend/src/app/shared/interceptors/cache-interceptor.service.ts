import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CacheInterceptorService implements HttpInterceptor {

  private cache = new Map<string, { response: HttpResponse<any>, expiration: number }>();

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.method !== 'GET'){
      return next.handle(req);
    }

    const cached = this.cache.get(req.urlWithParams);
    if(cached) {
      const now = Date.now();
      if (cached.expiration > now){
        return of(cached.response);
      } else {
        this.cache.delete(req.urlWithParams);
      }
    }

    return next.handle(req).pipe(
      tap(event => {
        if (event instanceof HttpResponse) {
          this.cache.set(req.urlWithParams, { response: event, expiration: Date.now() + 300000 });
        }
      })
    );
  }
}
