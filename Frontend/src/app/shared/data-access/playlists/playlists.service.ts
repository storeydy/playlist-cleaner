import { Injectable, inject } from '@angular/core';
import { ApiService } from '../../api/src';
import { BehaviorSubject, Observable, combineLatest, forkJoin, map, of, retry, switchMap, tap, timer } from 'rxjs';
import { GetPlaylistDuplicatesResponse, GetPlaylistResponse, GetPlaylistTracksResponse, GetPlaylistTracksResponsePlaylistTrack, GetSongResponse, GetUsersPlaylistsResponse } from '../../types/openapi';
import { SongsService } from '../songs/songs.service';
import { GetPlaylistDuplicateSongs, GetPlaylistDuplicateSongsResponseSet } from '../../types/playlists/GetPlaylistDuplicateSongs';

@Injectable({
  providedIn: 'root'
})

export class PlaylistsService {
  private readonly apiService = inject(ApiService);
  private readonly userId = localStorage.getItem('user_id');
  private readonly songsService = inject(SongsService);

  private selectedPlaylistId$ = new BehaviorSubject<string | null>(null);

  playlists$: Observable<GetPlaylistResponse[]> = this.apiService.get<GetUsersPlaylistsResponse>('/api/v1/users/' + this.userId + '/playlists')
    .pipe(
      switchMap((response: GetUsersPlaylistsResponse) =>
        response.playlist_ids ?
          this.fetchPlaylists(response.playlist_ids) as Observable<GetPlaylistResponse[]> :
          of([])
      )
    );

  selectedPlaylistTracks$: Observable<GetPlaylistTracksResponse | null> = this.selectedPlaylistId$
    .pipe(
      switchMap(playlistId => {
        if (playlistId) {
          return this.fetchPlaylistTracks(playlistId) as Observable<GetPlaylistTracksResponse>;
        } else {
          return of(null);
        }
      })
    );

  setSelectedPlaylistId(playlistId: string) {
    if (playlistId) {
      this.selectedPlaylistId$.next(playlistId);
    }
  }

  fetchDuplicateTracks(playlistId: string): Observable<GetPlaylistDuplicateSongs> {
    return this.apiService.get<GetPlaylistDuplicatesResponse>('/api/v1/playlists/' + playlistId + '/duplicates')
      .pipe(
        switchMap((response: GetPlaylistDuplicatesResponse) => {
          if (!response.duplicateTrackSets) {
            return of({ duplicateTrackSets: [] });
          }
          
          const duplicateSetsWithSongs$: Observable<GetPlaylistDuplicateSongsResponseSet>[] = response.duplicateTrackSets.map(duplicateSet => {
            if (!duplicateSet.duplicateTrackIds) {
              return of({ songs: [] });
            }
  
            const songObservables: Observable<GetSongResponse>[] = duplicateSet.duplicateTrackIds.map(songId => 
              this.songsService.fetchSong(songId)
            );
  
            return forkJoin(songObservables).pipe(
              map(songs => ({ songs }))
            );
          });
  
          return forkJoin(duplicateSetsWithSongs$).pipe(
            map(duplicateTrackSets => ({ duplicateTrackSets }))
          );
        })
      );
  }

  getTrackById(trackId: string): Observable<GetPlaylistTracksResponsePlaylistTrack | undefined>{
    return this.selectedPlaylistTracks$.pipe(
      map(response => {
        if (response?.items) {
          return response.items.find(track => track.track?.id === trackId);
        }
        return undefined
      })
    )
  }

  removeSongFromPlaylist(songId: string): Observable<void> {
    var playlistId = this.selectedPlaylistId$.getValue()
    return this.apiService.delete('/api/v1/playlists/' + playlistId + '/tracks/' + songId)
  }


  private fetchPlaylists(playlistIds: string[]): Observable < GetPlaylistResponse[] > {
  return combineLatest(
    playlistIds.map(id =>
      this.apiService.get<GetPlaylistResponse>('/api/v1/playlists/' + id)
    )
  );
}

  private fetchPlaylistTracks(playlistId: string): Observable < GetPlaylistTracksResponse > {
  return this.apiService.get<GetPlaylistTracksResponse>('/api/v1/playlists/' + playlistId + '/tracks')
}
}
