import { Injectable, inject } from '@angular/core';
import { ApiService } from '../../api/src';
import { Observable, Subject, switchMap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserProfileService {

  private readonly apiService = inject(ApiService);
  
  profile$ = this.apiService.get('/api/v1/users/me') as Observable<any>;
  private readonly getProfileAction = new Subject<void>();

  private readonly getProfile$ = this.getProfileAction.asObservable().pipe(switchMap(() => this.profile$));

  readonly profileObject$ = this.getProfile$;

  getUserProfile(){
    this.getProfileAction.next();
  }
}
