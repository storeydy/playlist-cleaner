import { Injectable, inject } from '@angular/core';
import { ApiService } from '../../api/src';
import { Observable, combineLatest, of, retry, switchMap, tap, timer } from 'rxjs';
import { GetPlaylistResponse, GetUsersPlaylistsResponse } from '../../types/openapi';

@Injectable({
  providedIn: 'root'
})
export class PlaylistsService {
  private readonly apiService = inject(ApiService);
  private readonly userId = localStorage.getItem('user_id');

  playlists$: Observable<GetPlaylistResponse[]> = this.apiService.get<GetUsersPlaylistsResponse>('/api/v1/playlists/' + this.userId + '/playlists')
    .pipe(
      switchMap((response: GetUsersPlaylistsResponse) =>
        response.playlist_ids ?
          this.fetchPlaylists(response.playlist_ids.slice(0, 200)).pipe(
            this.backoff(3, 2000)
          ) as Observable<GetPlaylistResponse[]> :
          of([])
      )
    );

  private fetchPlaylists(playlistIds: string[]): Observable<GetPlaylistResponse[]> {
    return combineLatest(
      playlistIds.map(id =>
        this.apiService.get<GetPlaylistResponse>('/api/v1/playlists/' + id)
      )
    );
  }

  private backoff(maxTries: number, initialDelay: number) {
    return retry({
      count: maxTries,
      delay: (error, retryCount) => timer(initialDelay * retryCount ** 2),
    });
  }
}
