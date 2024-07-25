import { ApplicationConfig } from '@angular/core';
import {
  provideRouter,
  withEnabledBlockingInitialNavigation,
} from '@angular/router';
import { appRoutes } from './app.routes';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { AuthInterceptorService } from './auth/interceptors/auth-interceptor/auth-interceptor.service';
import { provideAnimations } from '@angular/platform-browser/animations';
import { CacheInterceptorService } from './shared/interceptors/cache-interceptor.service';

export const appConfig: ApplicationConfig = {
  providers:
    [
      provideRouter(appRoutes, withEnabledBlockingInitialNavigation()),
      provideAnimations(),
      provideHttpClient(
        withInterceptorsFromDi(),
      ),
      {
        provide: HTTP_INTERCEPTORS,
        useClass: CacheInterceptorService,
        multi: true,
      },  
      {
        provide: HTTP_INTERCEPTORS,
        useClass: AuthInterceptorService,
        multi: true,
      }
    ],
};
