import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NxWelcomeComponent } from './nx-welcome.component';
import { AuthComponentComponent } from './auth-component/auth-component.component';

@Component({
  standalone: true,
  imports: [NxWelcomeComponent, RouterModule, AuthComponentComponent],
  selector: 'playlist-cleaner-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'playlist-cleaner';
}
