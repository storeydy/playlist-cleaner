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
import { DynamicDialogModule, DialogService } from 'primeng/dynamicdialog';
import { DuplicateTracksDialogComponent } from './duplicate-tracks-dialog/duplicate-tracks-dialog.component';
import { TooltipModule } from 'primeng/tooltip';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'playlist-cleaner-playlist-details',
  standalone: true,
  imports: [CommonModule, ButtonModule, TableModule, ProgressSpinnerModule, InputTextModule, HeaderComponent, MillisecondPipe, DynamicDialogModule, TooltipModule, ToastModule],
  templateUrl: './playlist-details.component.html',
  styleUrl: './playlist-details.component.scss',
  providers: [DialogService, MessageService]
})
export class PlaylistDetailsComponent implements OnInit {

  constructor(private route: ActivatedRoute, private dialogService: DialogService, private messagesService: MessageService) {}

  public readonly playlistService = inject(PlaylistsService);
  private subscription = new Subscription();
  @ViewChild('dt') dt!: Table;

  playlistId: string = "";
  playlistTracks: GetPlaylistTracksResponsePlaylistTrack[] | null = null;
  unsortedPlaylistTracks: GetPlaylistTracksResponsePlaylistTrack[] | null = null;
  playlistDetailsHeaderText: string = "";

  async ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.playlistId = params.get('id')!;
      this.playlistService.setSelectedPlaylistId(this.playlistId)
    })

    this.initialiseSubscriptions();
  }

  private initialiseSubscriptions() {
    // this.subscription.add(
    //   this.playlistService.selectedPlaylistTracks$.subscribe((res) => {
    //     this.playlistTracks = res?.items!;
    //     this.unsortedPlaylistTracks = res?.items!;
    //   })
    // )

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
    this.playlistService.fetchDuplicateTracks(this.playlistId).subscribe((res) => {
      this.dialogService.open(DuplicateTracksDialogComponent, {
        data: res,
        header: 'Duplicate Tracks in ' + this.playlistDetailsHeaderText,
        width: '50%'
      });
    })
  }

  customSort(event: any) {
    event.data.sort((data1: any, data2: any) => {
      let value1 = this.getFieldValue(data1, event.field);
      let value2 = this.getFieldValue(data2, event.field);
      let result : number | null = null;

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

  displayWipMessage(){
    this.messagesService.add({ severity: 'warn', summary: 'Not yet available', detail: 'This feature is not available yet.' });
  }

}
