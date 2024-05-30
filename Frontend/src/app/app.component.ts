import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { WelcomeComponent } from './welcome/welcome.component';
import { LoginService } from './auth/services/login/login.service';
import { ApiService } from './shared/api/src';
import { HeaderComponent } from './shared/components/header/header.component';
import { FooterComponent } from './shared/components/footer/footer.component';

@Component({
  standalone: true,
  imports: [
    RouterModule,
    HttpClientModule,
    HeaderComponent,
    FooterComponent,
    WelcomeComponent
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
