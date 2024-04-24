import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlaylistsService } from '../shared/data-access/playlists/playlists.service';
import { Subscription } from 'rxjs';
import { GetCurrentUsersProfileResponse, GetUsersPlaylistsResponse } from '../shared/types/openapi';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { Router } from '@angular/router';
import { UserProfileService } from '../shared/data-access/user-profile/user-profile.service';

@Component({
  selector: 'playlist-cleaner-playlist-list',
  standalone: true,
  imports: [CommonModule, ButtonModule, TableModule],
  templateUrl: './playlist-list.component.html',
  styleUrl: './playlist-list.component.scss',
})
export class PlaylistListComponent implements OnInit {

  private readonly playlistService = inject(PlaylistsService);
  private readonly userProfileService = inject(UserProfileService);

  private subscription = new Subscription();

  playlistsList: GetUsersPlaylistsResponse | null = null;
  profileData: GetCurrentUsersProfileResponse | null = null;

  constructor(private router: Router) {}

  async ngOnInit() {
    this.initialiseSubscriptions();
    this.playlistService.getUserPlaylists();
    this.userProfileService.getUserProfile();
  }

  private initialiseSubscriptions() {
    this.subscription.add(
      this.playlistService.playlistsList$.subscribe((res) => {
        this.playlistsList = res;
      })
    );
    this.subscription.add(
      this.userProfileService.profileObject$.subscribe((res) => {
        this.profileData = res;
      })
    );
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  navigateToWelcome(){
    this.router.navigate(['']);
  }
}
