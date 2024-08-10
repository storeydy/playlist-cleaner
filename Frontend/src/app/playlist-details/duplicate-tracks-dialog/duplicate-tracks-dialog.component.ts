import { Component, inject } from '@angular/core';
import { DynamicDialogRef, DynamicDialogConfig, DynamicDialogModule, DialogService } from 'primeng/dynamicdialog';
import { GetPlaylistDuplicateSongs } from '../../shared/types/playlists/GetPlaylistDuplicateSongs';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { PlaylistsService } from '../../shared/data-access/playlists/playlists.service';
import { GetPlaylistTracksResponsePlaylistTrack } from '../../shared/types/openapi';
import { firstValueFrom, tap } from 'rxjs';
import { ConfirmationService, Message } from 'primeng/api';
import { MessagesModule } from 'primeng/messages';
import { HttpResponse } from '@angular/common/http';
import { ConfirmDialogModule } from 'primeng/confirmdialog';

@Component({
  selector: 'playlist-cleaner-duplicate-tracks-dialog',
  standalone: true,
  imports: [DynamicDialogModule, CardModule, CommonModule, ButtonModule, MessagesModule, ConfirmDialogModule],
  templateUrl: './duplicate-tracks-dialog.component.html',
  styleUrl: './duplicate-tracks-dialog.component.scss',
  providers: [ConfirmationService]
})
export class DuplicateTracksDialogComponent {

  private readonly playlistService = inject(PlaylistsService);

  messages: Message[] = [];
  duplicateSetIndex: number = 0;
  duplicateTracks: GetPlaylistDuplicateSongs
  duplicateTracksPlaylistContext: Map<number, GetPlaylistTracksResponsePlaylistTrack[]> = new Map();

  constructor(public ref: DynamicDialogRef, public config: DynamicDialogConfig, private confirmationService: ConfirmationService) {
    this.duplicateTracks = this.config.data;
  }

  async ngOnInit() {
    await this.getDuplicatesContextWithinPlaylist();
  }

  addMessages() {
    this.messages = [
      { severity: 'success', summary: 'Duplicate Removed Successfully' },
      { severity: 'error', summary: 'Error Removing Duplicate' },
    ]
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

  openConfirmationDialog(songId: string, duplicateIndex: number) {
    if (this.getTrackContext(duplicateIndex)){
      var trackPositionInPlaylist = this.getTrackContext(duplicateIndex)!.position!;
      
      this.confirmationService.confirm({
        message: this.getConfirmDialogMessage(songId),
        header: 'Delete Confirmation',
        icon: 'pi pi-exclamation-triangle',
        acceptButtonStyleClass: 'p-button-danger p-button-text',
        rejectButtonStyleClass: 'p-button-text p-button-text',
        acceptIcon: 'none',
        rejectIcon: 'none',

        accept: () => {
          this.playlistService.removeSongFromPlaylist(songId, trackPositionInPlaylist).pipe(
            tap((res: HttpResponse<void>) => {
              if (res.status === 204) {
                const duplicateTracksPlaylistContextEntry = this.duplicateTracksPlaylistContext.get(this.duplicateSetIndex);
                if (duplicateTracksPlaylistContextEntry) {
                  duplicateTracksPlaylistContextEntry.splice(duplicateIndex, 1);
                } 
                this.duplicateTracks.duplicateTrackSets[this.duplicateSetIndex].songs.splice(duplicateIndex, 1)
                this.messages = [{ severity: 'success', summary: 'Success', detail: 'Track deleted successfully' }];

                // this.playlistService.triggerPlaylistTracksUpdate();
                this.getDuplicatesContextWithinPlaylist()
              }
            })
          ).subscribe({
            error: (err) => {
              this.messages = [{ severity: 'error', summary: 'Error', detail: 'There was an error deleting the track.' }];
            }
          })
        },
        reject: () => {
          this.confirmationService.close();
        }
      });
    }
    else{
      this.messages = [{severity: 'error', summary: 'Error finding track context', detail: 'There was an error finding the position of this track within the playlist.'}]
    }
  }

  getTrackContext(duplicateIndex: number) {
    if (this.duplicateTracksPlaylistContext.has(this.duplicateSetIndex)){
      return this.duplicateTracksPlaylistContext.get(this.duplicateSetIndex)![duplicateIndex];
    }
    else return null
  }

  getConfirmDialogMessage(songToDeleteId: string): string {
    var message = 'Are you sure you want to remove this song from your playlist?'
    if (this.duplicateTracks.duplicateTrackSets[this.duplicateSetIndex].songs.filter(s => s.id == songToDeleteId).length > 1){
      message = 'There are more than one instance of this Song ID in the playlist. The Spotify API currently does not support deleting individual instances of a song, so the other instances will be removed and re-added at their original position as part of this operation. This means that their "Date Added" value will be updated to the current time. Are you sure you want to continue?'
    }

    return message;
  }
}
