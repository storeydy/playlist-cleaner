import { Injectable, inject } from '@angular/core';
import { ApiService } from '../../api/src';
import { BehaviorSubject, Observable, Subject, combineLatest, forkJoin, from, map, mergeMap, of, retry, switchMap, tap, timer, toArray } from 'rxjs';
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

  playlistFetchingProgress$: Subject<number> = new Subject<number>();
  playlistsCount$: Subject<number> = new Subject<number>();

  playlists$: Observable<GetPlaylistResponse[]> = this.apiService.get<GetUsersPlaylistsResponse>('/api/v1/users/' + this.userId + '/playlists')
    .pipe(
      switchMap((response: GetUsersPlaylistsResponse) => {
        if (response.playlist_ids) {
          this.playlistsCount$.next(response.playlist_ids.length);
          return this.fetchPlaylists(response.playlist_ids) as Observable<GetPlaylistResponse[]>
        }
        else {
          this.playlistsCount$.next(0);
          return of([])
        }
      }
      )
    );

  selectedPlaylistTracks$: Observable<GetPlaylistTracksResponse | null> = this.selectedPlaylistId$
    .pipe(
      switchMap(playlistId => {
        if (playlistId) {
          return this.fetchPlaylistTracks(playlistId)
            .pipe(
              map(response => {
                return this.addPositionToPlaylistTracks(response)
              })
            );
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

  getTrackById(trackId: string, searchIndex: number): Observable<GetPlaylistTracksResponsePlaylistTrack | undefined> {
    return this.selectedPlaylistTracks$.pipe(
      map(response => {
        if (response?.items) {
          if (searchIndex > 0) {
            for (var i = searchIndex; i < response.items.length; i++) {
              if (response.items[i].track?.id == trackId) {
                return response.items[i];
              }
            }
          }
          return response.items.find(track => track.track?.id === trackId);
        }
        return undefined
      })
    )
  }

  getTrackByIndex(trackIndex: number): Observable<GetPlaylistTracksResponsePlaylistTrack | undefined> {
    return this.selectedPlaylistTracks$.pipe(
      map(response => {
        if (response?.items) {
          return response.items[trackIndex];
        }
        return undefined
      })
    )
  }

  removeSongFromPlaylist(songId: string, songIndex: number): Observable<void> {
    var playlistId = this.selectedPlaylistId$.getValue()
    return this.apiService.delete('/api/v1/playlists/' + playlistId + '/tracks/' + songId + '?trackIndex=' + songIndex)
  }

  private fetchPlaylists(playlistIds: string[]): Observable<GetPlaylistResponse[]> {
    let retrievedPlaylistsCount = 0;

    const concurrentRequestNumber = 7;

    return from(playlistIds).pipe(
      mergeMap(id => this.apiService.get<GetPlaylistResponse>('/api/v1/playlists/' + id).pipe(
        tap(() => {
          retrievedPlaylistsCount++;
          this.playlistFetchingProgress$.next(retrievedPlaylistsCount);
        })
      ), concurrentRequestNumber),
      toArray()
    );
  }

  private fetchPlaylistTracks(playlistId: string): Observable<GetPlaylistTracksResponse> {
    return this.apiService.get<GetPlaylistTracksResponse>('/api/v1/playlists/' + playlistId + '/tracks')
  }

  private addPositionToPlaylistTracks(response: GetPlaylistTracksResponse): GetPlaylistTracksResponse {
    return {
      ...response,
      items: response.items!.map((item, index) => ({
        ...item,
        position: index + 1
      }))
    };
  }
}
