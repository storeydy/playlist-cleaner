import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginService } from '../auth/services/login/login.service';
import { HttpClientModule } from '@angular/common/http';
import { ApiService } from '../shared/api/src';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'playlist-cleaner-welcome',
  standalone: true,
  imports: [CommonModule, HttpClientModule],
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss'],
})

export class WelcomeComponent implements OnInit {

  private readonly loginService = inject(LoginService)
  private readonly apiService = inject(ApiService)
  private subscription = new Subscription();

  profile$: Observable<any> = this.getProfile();
  profileData: any;


  async ngOnInit(){
    await this.loginService.loginAsync();
    this.subscription = this.getProfile().subscribe((response) => {
      this.profileData = response;
    })
  }

  getProfile(): Observable<any> { 
    return this.apiService.get('/api/v1/users/me');
  }

  ngOnDestroy() {
		this.subscription.unsubscribe();
	}
}
