import { Component } from '@angular/core';
import { DynamicDialogRef, DynamicDialogConfig, DynamicDialogModule } from 'primeng/dynamicdialog';
import { GetPlaylistDuplicateSongs } from '../../shared/types/playlists/GetPlaylistDuplicateSongs';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';

@Component({
  selector: 'playlist-cleaner-duplicate-tracks-dialog',
  standalone: true,
  imports: [DynamicDialogModule, CardModule, CommonModule],
  templateUrl: './duplicate-tracks-dialog.component.html',
  styleUrl: './duplicate-tracks-dialog.component.scss'
})
export class DuplicateTracksDialogComponent {

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
}
