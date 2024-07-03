import { Injectable, inject } from '@angular/core';
import { ApiService } from '../../api/src';
import { Observable } from 'rxjs';
import { GetSongResponse } from '../../types/openapi';

@Injectable({
  providedIn: 'root'
})
export class SongsService {

  private readonly apiService = inject(ApiService);
  private readonly userId = localStorage.getItem('user_id');

  fetchSong(songId: string) : Observable<GetSongResponse> {
    return this.apiService.get('/api/v1/songs/' + songId)
  }
}
