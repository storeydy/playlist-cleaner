import { Component, OnInit, ViewChild, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlaylistsService } from '../shared/data-access/playlists/playlists.service';
import { Subscription } from 'rxjs';
import { GetPlaylistResponse, GetPlaylistTracksResponse } from '../shared/types/openapi';
import { ButtonModule } from 'primeng/button';
import { Table, TableModule } from 'primeng/table';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { InputTextModule } from 'primeng/inputtext';
import { HeaderComponent } from '../shared/components/header/header.component';
import { Router } from '@angular/router';

@Component({
  selector: 'playlist-cleaner-playlist-list',
  standalone: true,
  imports: [CommonModule, ButtonModule, TableModule, ProgressSpinnerModule, InputTextModule, HeaderComponent],
  templateUrl: './playlist-list.component.html',
  styleUrl: './playlist-list.component.scss',
})
export class PlaylistListComponent implements OnInit {

  constructor(private router: Router) { }

  private readonly playlistService = inject(PlaylistsService);

  private subscription = new Subscription();
  @ViewChild('dt') dt!: Table;

  playlistsList: GetPlaylistResponse[] | null = null;
  unsortedPlaylistsList: GetPlaylistResponse[] | null = null;
  playlistListHeaderText: string = "User's Playlists";

  async ngOnInit() {    
    this.initialiseSubscriptions();
  }

  private initialiseSubscriptions() {
    this.subscription.add(
      this.playlistService.playlists$.subscribe((res) => {
        this.playlistsList = res;
        this.unsortedPlaylistsList = [...res]
      })
    );
  }

  resetTableSort() {
    this.dt.reset();
    this.playlistsList = [...this.unsortedPlaylistsList!];
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  onRowSelect(event: any) {
    if (event.data.id) {
      this.playlistService.setSelectedPlaylistId(event.data.id!);
    }
  }

  onRowDblClick(event: any) {
    if (event.id) {
      this.router.navigate(['/playlists', event.id])
    }
  }
}
