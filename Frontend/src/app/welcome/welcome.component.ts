import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginService } from '../auth/services/login/login.service';
import { HttpClientModule } from '@angular/common/http';
import { Subscription } from 'rxjs';
import { UserProfileService } from '../shared/data-access/user-profile/user-profile.service';
import { PlaylistsService } from '../shared/data-access/playlists/playlists.service';
import { GetCurrentUsersProfileResponse, GetPlaylistResponse, GetUsersPlaylistsResponse } from '../shared/types/openapi';

@Component({
  selector: 'playlist-cleaner-welcome',
  standalone: true,
  imports: [CommonModule, HttpClientModule],
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss'],
})

export class WelcomeComponent implements OnInit {

  private readonly loginService = inject(LoginService)
  private readonly userProfileService = inject(UserProfileService);
  private readonly playlistsService = inject(PlaylistsService);

  private subscription = new Subscription();

  profileData: GetCurrentUsersProfileResponse | null = null;
  playlistsData: GetUsersPlaylistsResponse | null = null;
  selectedPlaylistData: GetPlaylistResponse | null = null;


  async ngOnInit(){
    await this.loginService.loginAsync();
    this.initialiseSubscriptions();
    this.userProfileService.getUserProfile();
    this.playlistsService.getUserPlaylists();
  }

  private initialiseSubscriptions() {
    this.subscription.add(
      this.userProfileService.profileObject$.subscribe((res) => {
        this.profileData = res;
        this.setUserId(this.profileData.id!);
      })
    );

    this.subscription.add(
      this.playlistsService.playlistsList$.subscribe((res) => {
        this.playlistsData = res;
        this.playlistsService.updateSelectedPlaylistId(res.playlist_ids![0]);
        this.playlistsService.getPlaylistById(this.playlistsService.getSelectedPlaylistId());
      })
    );

    this.subscription.add(
      this.playlistsService.getSelectedPlaylist$.subscribe((res) => {
        this.selectedPlaylistData = res;
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
