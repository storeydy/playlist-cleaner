import { Injectable, inject } from '@angular/core';
import { ApiService } from '../../api/src';
import { BehaviorSubject, Observable, Subject, combineLatest, of, switchMap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PlaylistsService {
  private readonly apiService = inject(ApiService);
  private readonly userId = localStorage.getItem('user_id');
  private selectedPlaylistId = new BehaviorSubject<number | null>(null);
  playlists$ = this.apiService.get('/api/v1/playlists/' + this.userId + '/playlists') as Observable<any>;


  selectedPlaylist$ = combineLatest([this.playlists$, this.selectedPlaylistId]).pipe(
    switchMap(([playlists, selectedId]) => {
      if (selectedId !== null && playlists.playlist_ids.includes(selectedId)) {
        return this.apiService.get('/api/v1/playlists/' + selectedId) as Observable<any>;
      } else {
        return of(null);
      }
    })
  );

  private readonly getPlaylistsAction = new Subject<void>();
  private readonly getSelectedPlaylistAction = new Subject<number>();
  private readonly getPlaylists$ = this.getPlaylistsAction
    .asObservable()
    .pipe(switchMap(() => this.playlists$));

  readonly playlistsList$ = this.getPlaylists$;

  readonly getSelectedPlaylist$ = this.getSelectedPlaylistAction
    .asObservable()
    .pipe(switchMap(() => this.selectedPlaylist$));

  getUserPlaylists() {
    this.getPlaylistsAction.next();
  }

  getPlaylistById(playlistId: number) {
    this.getSelectedPlaylistAction.next(playlistId);
  }

  updateSelectedPlaylistId(id: number) {
    this.selectedPlaylistId.next(id);
  }

  getSelectedPlaylistId(): number {
    return this.selectedPlaylistId.value!;
  }
}
