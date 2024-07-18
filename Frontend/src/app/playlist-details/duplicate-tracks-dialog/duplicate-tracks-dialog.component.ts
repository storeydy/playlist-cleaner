import { Component } from '@angular/core';
import { DynamicDialogRef, DynamicDialogConfig, DynamicDialogModule } from 'primeng/dynamicdialog';
import { GetPlaylistDuplicateSongs } from '../../shared/types/playlists/GetPlaylistDuplicateSongs';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'playlist-cleaner-duplicate-tracks-dialog',
  standalone: true,
  imports: [DynamicDialogModule, CardModule, CommonModule, ButtonModule],
  templateUrl: './duplicate-tracks-dialog.component.html',
  styleUrl: './duplicate-tracks-dialog.component.scss'
})
export class DuplicateTracksDialogComponent {

  duplicateSetIndex: number = 0;
  visible: boolean = true;
  duplicateTracks: GetPlaylistDuplicateSongs

  constructor(public ref: DynamicDialogRef, public config: DynamicDialogConfig) 
  {
    this.duplicateTracks = this.config.data;
  }

  ngOnInit() {
    console.log(this.config);
  }

  onClose() {
    this.ref.close();
  }

  removeSongFromPlaylist(songId: string){
    console.log("removing " + songId );
  }
}
