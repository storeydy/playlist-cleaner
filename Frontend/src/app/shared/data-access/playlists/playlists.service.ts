import { Injectable, inject } from '@angular/core';
import { ApiService } from '../../api/src';
import { BehaviorSubject, Observable, Subject, combineLatest, of, retry, switchMap, take, timer } from 'rxjs';
import { GetPlaylistResponse, GetUsersPlaylistsResponse } from '../../types/openapi';

@Injectable({
  providedIn: 'root'
})
export class PlaylistsService {
  private readonly apiService = inject(ApiService);
  private readonly userId = localStorage.getItem('user_id');
  private selectedPlaylistId = new BehaviorSubject<string | null>(null);

  //TODO: Update to use proper type & address server throttling
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

  private readonly getPlaylistsAction = new Subject<void>();
  private readonly getSelectedPlaylistAction = new Subject<string>();
  private readonly getPlaylists$ = this.getPlaylistsAction
    .asObservable()
    .pipe(switchMap(() => this.playlists$));

  readonly playlistsList$ = this.getPlaylists$;

  backoff(maxTries: number, initialDelay: number) {
    return retry({
        count: maxTries,
        delay: (error, retryCount) => timer(initialDelay * retryCount ** 2),
      });
  }

  private fetchPlaylists(playlistIds: string[]): Observable<GetPlaylistResponse[]> {
    return combineLatest(
      playlistIds.map(id =>
        this.apiService.get<GetPlaylistResponse>('/api/v1/playlists/' + id)
      )
    ) as Observable<GetPlaylistResponse[]>;
  }

  getUserPlaylists() {
    this.getPlaylistsAction.next();
  }

  getPlaylistById(playlistId: string) {
    this.getSelectedPlaylistAction.next(playlistId);
  }

  updateSelectedPlaylistId(id: string) {
    this.selectedPlaylistId.next(id);
  }

  getSelectedPlaylistId(): string {
    return this.selectedPlaylistId.value!;
  }
}
