import { Component, OnInit, ViewChild, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { PlaylistsService } from '../shared/data-access/playlists/playlists.service';
import { Subscription } from 'rxjs';
import { Table, TableModule } from 'primeng/table';
import { GetPlaylistTracksResponsePlaylistTrack } from '../shared/types/openapi';
import { ButtonModule } from 'primeng/button';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { InputTextModule } from 'primeng/inputtext';
import { HeaderComponent } from '../shared/components/header/header.component';

@Component({
  selector: 'playlist-cleaner-playlist-details',
  standalone: true,
  imports: [CommonModule, ButtonModule, TableModule, ProgressSpinnerModule, InputTextModule, HeaderComponent],
  templateUrl: './playlist-details.component.html',
  styleUrl: './playlist-details.component.scss',
})
export class PlaylistDetailsComponent implements OnInit {

  constructor(private route: ActivatedRoute) {}

  private readonly playlistService = inject(PlaylistsService);
  private subscription = new Subscription();
  @ViewChild('dt') dt!: Table;

  playlistId: string = "";
  playlistTracks: GetPlaylistTracksResponsePlaylistTrack[] | null = null;
  unsortedPlaylistTracks: GetPlaylistTracksResponsePlaylistTrack[] | null = null;
  playlistDetailsHeaderText: string = "";

  async ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.playlistId = params.get('id')!;
      this.playlistDetailsHeaderText = "Playlist Tracks for " + this.playlistId
    })

    this.initialiseSubscriptions();
  }

  private initialiseSubscriptions() {
    this.subscription.add(
      this.playlistService.selectedPlaylistTracks$.subscribe((res) => {
        this.playlistTracks = res?.items!;
        this.unsortedPlaylistTracks = res?.items!;
      })
    )
  }

  resetTableSort(){
    this.dt.reset();
    this.playlistTracks = [...this.unsortedPlaylistTracks!]
  }

  ngOnDestroy(){
    this.subscription.unsubscribe();
  }

  cleanPlaylist(){
    console.log('Cleaning...')
  }
}
