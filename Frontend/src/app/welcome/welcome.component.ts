import { Component, OnInit, ViewChild, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginService } from '../auth/services/login/login.service';
import { HttpClientModule } from '@angular/common/http';
import { Subscription } from 'rxjs';
import { UserProfileService } from '../shared/data-access/user-profile/user-profile.service';
import { PlaylistsService } from '../shared/data-access/playlists/playlists.service';
import { GetCurrentUsersProfileResponse, GetPlaylistResponse, GetUsersPlaylistsResponse } from '../shared/types/openapi';
import { SpeedDialModule } from 'primeng/speeddial';
import { MenuItem, MessageService } from 'primeng/api';

@Component({
  selector: 'playlist-cleaner-welcome',
  standalone: true,
  imports: [CommonModule, HttpClientModule, SpeedDialModule ],
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss'],
  providers: [MessageService],
})

export class WelcomeComponent implements OnInit {

  private readonly loginService = inject(LoginService)
  private readonly userProfileService = inject(UserProfileService);
  private readonly playlistsService = inject(PlaylistsService);
  profilePictureUrl : string = "";

  private subscription = new Subscription();

  profileData: GetCurrentUsersProfileResponse | null = null;
  
  menuItems: MenuItem[] = [
    {
      label: '<p>Spotify Webpage<p>',
      icon: 'pi pi-external-link',
      url: 'http://angular.io',
      title: 'Spotify Webpage'
    },
    {
      title: 'User Information',
      icon: 'pi pi-id-card',
      routerLink: 'playlist-cleaner-user-profile',
      label: 'User Information'
    },
    {
      title: 'Playlist View',
      icon: 'pi pi-list',
      routerLink: 'playlist-cleaner-playlist-list',
      label: 'Playlist View'
    }
  ]



  async ngOnInit() {
    await this.loginService.loginAsync();
    this.initialiseSubscriptions();
    this.userProfileService.getUserProfile();
    this.playlistsService.getUserPlaylists();
  }

  private initialiseSubscriptions() {
    this.subscription.add(
      this.userProfileService.profileObject$.subscribe((res) => {
        this.profileData = res;
        console.log(res);
        this.setUserId(this.profileData.id!);
        this.profilePictureUrl = this.profileData.images![0].url ?? "";
      })
    );

  }

  setUserId(userId: string) {
    localStorage.setItem('user_id', userId);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
