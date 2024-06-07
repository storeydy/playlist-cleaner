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
import { MillisecondPipe } from '../shared/pipes/millisecond.pipe';

@Component({
  selector: 'playlist-cleaner-playlist-details',
  standalone: true,
  imports: [CommonModule, ButtonModule, TableModule, ProgressSpinnerModule, InputTextModule, HeaderComponent, MillisecondPipe],
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

    this.subscription.add(
      this.playlistService.playlists$.subscribe((res) => {
        var selectedPlaylist = res?.find(playlist => playlist.id == this.playlistId)
        this.playlistDetailsHeaderText =  selectedPlaylist?.name + " by " + selectedPlaylist?.owner?.display_name 
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

  customSort(event: any) {
    event.data.sort((data1: any, data2: any) => {
      let value1 = this.getFieldValue(data1, event.field);
      let value2 = this.getFieldValue(data2, event.field);
      let result = null;

      if (value1 == null && value2 != null)
        result = -1;
      else if (value1 != null && value2 == null)
        result = 1;
      else if (value1 == null && value2 == null)
        result = 0;
      else if (typeof value1 === 'string' && typeof value2 === 'string')
        result = value1.localeCompare(value2);
      else
        result = (value1 < value2) ? -1 : (value1 > value2) ? 1 : 0;

      return (event.order * result);
    });
  }

  getFieldValue(data: any, field: string): any {
    if (field === 'track.artists[0].name') {
      return data.track.artists[0]?.name;
    }
    return field.split('.').reduce((acc, part) => acc && acc[part], data);
  }

}
