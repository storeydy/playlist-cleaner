import { Injectable, inject } from '@angular/core';
import { ApiService } from '../../api/src';
import { BehaviorSubject, Observable, combineLatest, of, retry, switchMap, tap, timer } from 'rxjs';
import { GetPlaylistResponse, GetPlaylistTracksResponse, GetUsersPlaylistsResponse } from '../../types/openapi';

@Injectable({
  providedIn: 'root'
})
export class PlaylistsService {
  private readonly apiService = inject(ApiService);
  private readonly userId = localStorage.getItem('user_id');

  private selectedPlaylistId$ = new BehaviorSubject<string | null>(null);

  playlists$: Observable<GetPlaylistResponse[]> = this.apiService.get<GetUsersPlaylistsResponse>('/api/v1/users/' + this.userId + '/playlists')
    .pipe(
      switchMap((response: GetUsersPlaylistsResponse) =>
        response.playlist_ids ?
          this.fetchPlaylists(response.playlist_ids.slice(0, 10)) as Observable<GetPlaylistResponse[]> :
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


  private fetchPlaylists(playlistIds: string[]): Observable<GetPlaylistResponse[]> {
    return combineLatest(
      playlistIds.map(id =>
        this.apiService.get<GetPlaylistResponse>('/api/v1/playlists/' + id)
      )
    );
  }

  private fetchPlaylistTracks(playlistId: string): Observable<GetPlaylistTracksResponse> {
    return this.apiService.get<GetPlaylistTracksResponse>('/api/v1/playlists/' + playlistId + '/tracks')
  }
}
