import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { WelcomeComponent } from './welcome/welcome.component';
import { LoginService } from './auth/services/login/login.service';
import { ApiService } from './shared/api/src';

@Component({
  standalone: true,
  imports: [
    RouterModule,
    HttpClientModule,
    WelcomeComponent,
  ],
  providers : 
  [ 
    LoginService,
    ApiService,
  ],
  selector: 'playlist-cleaner-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'playlist-cleaner';
}
