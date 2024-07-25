import { Component, inject } from '@angular/core';
import { DynamicDialogRef, DynamicDialogConfig, DynamicDialogModule } from 'primeng/dynamicdialog';
import { GetPlaylistDuplicateSongs } from '../../shared/types/playlists/GetPlaylistDuplicateSongs';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { PlaylistsService } from '../../shared/data-access/playlists/playlists.service';
import { GetPlaylistTracksResponsePlaylistTrack } from '../../shared/types/openapi';
import { firstValueFrom } from 'rxjs';

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
  duplicateTracks: GetPlaylistDuplicateSongs
  duplicateTracksPlaylistContext: Map<number, GetPlaylistTracksResponsePlaylistTrack[]> = new Map();

  constructor(public ref: DynamicDialogRef, public config: DynamicDialogConfig) {
    this.duplicateTracks = this.config.data;
  }

  async ngOnInit() {
    await this.getDuplicatesContextWithinPlaylist();
  }

  private async getDuplicatesContextWithinPlaylist() {
    for (const [duplicateSetIndex, duplicateSet] of this.duplicateTracks.duplicateTrackSets.entries()) {
      let duplicateSearchIndex = 0;

      for (const song of duplicateSet.songs) {
        if (song.id) {
          try {
            const res = await firstValueFrom(this.playlistService.getTrackById(song.id, duplicateSearchIndex));

            if (res) {
              let duplicatesContext = this.duplicateTracksPlaylistContext.get(duplicateSetIndex) || [];
              duplicatesContext.push(res);
              this.duplicateTracksPlaylistContext.set(duplicateSetIndex, duplicatesContext);

              duplicateSearchIndex = res.position! + 1;
            }
          } catch (error) {
            console.error(`Error fetching track for song ID ${song.id} at index ${duplicateSetIndex}:`, error);
          }
        }
      }
    }
  }

  onClose() {
    this.duplicateSetIndex = 0;
    this.duplicateTracksPlaylistContext.clear()
    this.ref.close();
  }

  removeSongFromPlaylist(songId: string, duplicateIndex: number) {
    var trackPositionInPlaylist = this.getTrackContext(duplicateIndex).position!;

    this.playlistService.removeSongFromPlaylist(songId, trackPositionInPlaylist).subscribe((res) => {
      console.log(res);
      //TODO - Remove deleted track upon success
    });
  }

  getTrackContext(duplicateIndex: number) {
    return this.duplicateTracksPlaylistContext.get(this.duplicateSetIndex)![duplicateIndex];
  }
}
