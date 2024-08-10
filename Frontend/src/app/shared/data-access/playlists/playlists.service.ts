import { Injectable, inject } from '@angular/core';
import { ApiService } from '../../api/src';
import { BehaviorSubject, Observable, Subject, catchError, combineLatest, filter, forkJoin, from, map, mergeMap, of, startWith, switchMap, tap, toArray } from 'rxjs';
import { GetPlaylistDuplicatesResponse, GetPlaylistResponse, GetPlaylistTracksResponse, GetPlaylistTracksResponsePlaylistTrack, GetSongResponse, GetUsersPlaylistsResponse, PlaylistsServiceApi } from '../../types/openapi';
import { SongsService } from '../songs/songs.service';
import { GetPlaylistDuplicateSongs, GetPlaylistDuplicateSongsResponseSet } from '../../types/playlists/GetPlaylistDuplicateSongs';
import { HttpResponse } from '@angular/common/http';
import { CacheInterceptorService } from '../../interceptors/cache-interceptor.service';

@Injectable({
  providedIn: 'root'
})

export class PlaylistsService {
  private readonly apiService = inject(ApiService);
  private readonly playlistsApiService = inject(PlaylistsServiceApi);
  private readonly userId = localStorage.getItem('user_id');
  private readonly songsService = inject(SongsService);
  private readonly cacheInterceptorService = inject(CacheInterceptorService);

  private selectedPlaylistId$ = new BehaviorSubject<string | null>(null);
  private playlistTracksUpdate$ = new Subject<void>();

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

  selectedPlaylistTracks$: Observable<GetPlaylistTracksResponse | null> = combineLatest([
    this.selectedPlaylistId$,
    this.playlistTracksUpdate$.pipe(startWith(null))
  ])
    .pipe(
      switchMap(([playlistId]) => {
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

  triggerPlaylistTracksUpdate() {
    this.playlistTracksUpdate$.next();
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

  removeSongFromPlaylist(songId: string, songIndex: number): Observable<HttpResponse<void>> {
    var playlistId = this.selectedPlaylistId$.getValue()
    var url = `https://localhost:7204/api/v1/playlists/${playlistId}/tracks`;
    return this.playlistsApiService.apiV1PlaylistsPlaylistIdTracksTrackIdDelete(playlistId!, songId, songIndex, 'response').pipe(
      tap(res => {
        if (res.status === 204){
          this.cacheInterceptorService.updateCache<GetPlaylistTracksResponse>(
            url,
            (playlistData) => {
              return {
                ...playlistData,
                tracks: playlistData.items?.filter(track => track.track?.id !== songId)
              }
            }
          )
        }
      })
    );
  }

  private fetchPlaylists(playlistIds: string[]): Observable<GetPlaylistResponse[]> {
    let retrievedPlaylistsCount = 0;

    const concurrentRequestNumber = 7;

    return from(playlistIds).pipe(
      mergeMap(id => this.apiService.get<GetPlaylistResponse>('/api/v1/playlists/' + id).pipe(
        tap(() => {
          retrievedPlaylistsCount++;
          this.playlistFetchingProgress$.next(retrievedPlaylistsCount);
        }),
        catchError(error => {
          console.error(`Failed to retrieve playlist with id ${id}`, error)
          return of(null as unknown as GetPlaylistResponse);
        })
      ), concurrentRequestNumber),
      filter(playlist => playlist !== null),
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
