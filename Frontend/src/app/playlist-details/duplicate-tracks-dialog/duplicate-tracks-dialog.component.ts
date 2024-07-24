import { Component, inject } from '@angular/core';
import { DynamicDialogRef, DynamicDialogConfig, DynamicDialogModule } from 'primeng/dynamicdialog';
import { GetPlaylistDuplicateSongs } from '../../shared/types/playlists/GetPlaylistDuplicateSongs';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { PlaylistsService } from '../../shared/data-access/playlists/playlists.service';
import { GetPlaylistTracksResponsePlaylistTrack } from '../../shared/types/openapi';

@Component({
  selector: 'playlist-cleaner-duplicate-tracks-dialog',
  standalone: true,
  imports: [DynamicDialogModule, CardModule, CommonModule, ButtonModule],
  templateUrl: './duplicate-tracks-dialog.component.html',
  styleUrl: './duplicate-tracks-dialog.component.scss'
})
export class DuplicateTracksDialogComponent {

  private readonly playlistService = inject(PlaylistsService);

  duplicateSetIndex: number = 0;
  visible: boolean = true;
  duplicateTracks: GetPlaylistDuplicateSongs
  duplicateTracksPlaylistContext: GetPlaylistTracksResponsePlaylistTrack[] = [];

  constructor(public ref: DynamicDialogRef, public config: DynamicDialogConfig) 
  {
    this.duplicateTracks = this.config.data;
  }

  ngOnInit() {
    this.duplicateTracks.duplicateTrackSets.forEach(duplicateSet => {
      duplicateSet.songs.forEach(song => {
        if (song.id){
          this.playlistService.getTrackById(song.id).subscribe((res) => {
            if (res){
              this.duplicateTracksPlaylistContext.push(res);
            }
          })
        }
      })
    });
    console.log(this.duplicateTracksPlaylistContext);
    
  }

  onClose() {
    this.ref.close();
  }

  removeSongFromPlaylist(songId: string){
    this.playlistService.removeSongFromPlaylist(songId).subscribe((res) => {
      console.log(res);
    });
  }

  getTrackContext(songId: string){
    return this.duplicateTracksPlaylistContext.find(i => i.track?.id === songId);
  }
}
