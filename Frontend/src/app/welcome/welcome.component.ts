import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginService } from '../auth/services/login/login.service';
import { HttpClientModule } from '@angular/common/http';
import { ApiService } from '../shared/api/src';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'playlist-cleaner-welcome',
  standalone: true,
  imports: [CommonModule, HttpClientModule],
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss'],
})

export class WelcomeComponent implements OnInit {

  private readonly loginService = inject(LoginService)
  private readonly apiService = inject(ApiService)
  private subscription = new Subscription();

  profile$: Observable<any> = this.getProfile();
  profileData: any;
  playlistsData: any;
  playlistData: any;


  async ngOnInit(){   // tidy up with RxJS map operators
    await this.loginService.loginAsync();
    this.subscription = this.getProfile().subscribe((response) => {
      this.profileData = response;
      this.getPlaylists(response.id).subscribe((playlistsResponse) => {
        this.playlistsData = playlistsResponse;        
        this.getPlaylist(this.playlistsData.playlist_ids[0]).subscribe((playlistDataResponse)=> {
          this.playlistData = playlistDataResponse;
        })
      })
    })
  }

  getProfile(): Observable<any> { 
    return this.apiService.get('/api/v1/users/me');
  }

  getPlaylists(userId: string): Observable<any> { 
    return this.apiService.get('/api/v1/playlists/' + userId + '/playlists')
  }

  getPlaylist(playlistId: string): Observable<any> { 
    return this.apiService.get('/api/v1/playlists/' + playlistId)
  }

  ngOnDestroy() {
		this.subscription.unsubscribe();
	}
}
