import { Component, OnInit, ViewChild, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginService } from '../auth/services/login/login.service';
import { HttpClientModule } from '@angular/common/http';
import { Subscription } from 'rxjs';
import { UserProfileService } from '../shared/data-access/user-profile/user-profile.service';
import { GetCurrentUsersProfileResponse } from '../shared/types/openapi';
import { SpeedDialModule } from 'primeng/speeddial';
import { MenuItem, MessageService } from 'primeng/api';
import { TokenService } from '../auth/services/token/token.service';
import { CardModule } from 'primeng/card';

@Component({
  selector: 'playlist-cleaner-welcome',
  standalone: true,
  imports: [CommonModule, HttpClientModule, SpeedDialModule, CardModule ],
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss'],
  providers: [MessageService],
})

export class WelcomeComponent implements OnInit {

  private readonly loginService = inject(LoginService)
  private readonly userProfileService = inject(UserProfileService);
  private readonly tokenService = inject(TokenService);
  profilePictureUrl : string = "";

  private subscription = new Subscription();

  profileData: GetCurrentUsersProfileResponse | null = null;
  
  menuItems: MenuItem[] = [
    {
      title: 'User Information',
      icon: 'pi pi-id-card',
      routerLink: 'user-profile',
      label: 'User Information'
    },
    {
      title: 'Playlist View',
      icon: 'pi pi-list',
      routerLink: 'playlist-list',
      label: 'Playlist View'
    }
  ]



  async ngOnInit() {
    if(!this.tokenService.retrieveAccessTokenFromLocalStorage()){
      await this.loginService.loginAsync();
    }

    this.initialiseSubscriptions();
    this.userProfileService.getUserProfile();    
  }

  private initialiseSubscriptions() {
    this.subscription.add(
      this.userProfileService.profileObject$.subscribe((res) => {
        this.profileData = res;
        this.setUserId(this.profileData.id!);
        this.pushUserProfileMenuItem(this.profileData.spotify_external_url!);
        this.profilePictureUrl = this.profileData.images![0].url ?? "";
      })
    );

  }

  setUserId(userId: string) {
    localStorage.setItem('user_id', userId);
  }

  pushUserProfileMenuItem(spotifUrl: string) { 
    var item = {     
      label: '<p>Spotify Webpage<p>',
      icon: 'pi pi-external-link',
      url: spotifUrl,
      title: 'Spotify Webpage'
    } as MenuItem
    this.menuItems.push(item);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
