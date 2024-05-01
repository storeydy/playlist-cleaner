import { Component, OnInit, ViewChild, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlaylistsService } from '../shared/data-access/playlists/playlists.service';
import { Subscription } from 'rxjs';
import { GetCurrentUsersProfileResponse, GetPlaylistResponse, GetUsersPlaylistsResponse } from '../shared/types/openapi';
import { ButtonModule } from 'primeng/button';
import { Table, TableModule } from 'primeng/table';
import { Router } from '@angular/router';
import { UserProfileService } from '../shared/data-access/user-profile/user-profile.service';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'playlist-cleaner-playlist-list',
  standalone: true,
  imports: [CommonModule, ButtonModule, TableModule, ProgressSpinnerModule, InputTextModule],
  templateUrl: './playlist-list.component.html',
  styleUrl: './playlist-list.component.scss',
})
export class PlaylistListComponent implements OnInit {

  private readonly playlistService = inject(PlaylistsService);
  private readonly userProfileService = inject(UserProfileService);

  private subscription = new Subscription();
  @ViewChild('dt') dt!: Table;
  
  playlistsList: GetPlaylistResponse[] | null = null;
  unsortedPlaylistsList: GetPlaylistResponse[] | null = null;
  profileData: GetCurrentUsersProfileResponse | null = null;

  isSorted: boolean | null = null;
  constructor(private router: Router) {}

  async ngOnInit() {
    this.initialiseSubscriptions();
    this.userProfileService.getUserProfile();
  }

  private initialiseSubscriptions() {
    this.subscription.add(
      this.playlistService.playlists$.subscribe((res) => {
        this.playlistsList = res;
        this.unsortedPlaylistsList = [...res]
      })
    );
    this.subscription.add(
      this.userProfileService.profileObject$.subscribe((res) => {
        this.profileData = res;
      })
    );
  }

  resetTableSort(){
    this.dt.reset();
    this.playlistsList = [...this.unsortedPlaylistsList!];
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  navigateToWelcome(){
    this.router.navigate(['']);
  }
}
