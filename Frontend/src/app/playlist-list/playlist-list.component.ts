import { Component, OnInit, ViewChild, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlaylistsService } from '../shared/data-access/playlists/playlists.service';
import { Subscription } from 'rxjs';
import { GetPlaylistResponse } from '../shared/types/openapi';
import { ButtonModule } from 'primeng/button';
import { Table, TableModule } from 'primeng/table';
import { InputTextModule } from 'primeng/inputtext';
import { HeaderComponent } from '../shared/components/header/header.component';
import { Router } from '@angular/router';
import { ProgressBarModule } from 'primeng/progressbar';
import { ToastModule } from 'primeng/toast';

@Component({
  selector: 'playlist-cleaner-playlist-list',
  standalone: true,
  imports: [CommonModule, ButtonModule, TableModule, ProgressBarModule, InputTextModule, HeaderComponent, ToastModule],
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
  playlistsLoadingProgress: number = 0;
  totalPlaylistsCount: number = 0;

  async ngOnInit() {    
    this.initialiseSubscriptions();
  }

  private initialiseSubscriptions() {
    this.subscription.add(
      this.playlistService.playlists$.subscribe((res) => {
        this.playlistsList = res;
        this.unsortedPlaylistsList = [...res]
        this.totalPlaylistsCount = this.playlistsList.length;
      })
    );

    this.subscription.add(
      this.playlistService.playlistFetchingProgress$.subscribe(progress => {
        this.playlistsLoadingProgress = progress;
      })
    );

    this.subscription.add(
      this.playlistService.playlistsCount$.subscribe(total => {
        this.totalPlaylistsCount = total;
      })
    )
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
