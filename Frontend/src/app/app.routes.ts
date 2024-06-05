import { Route } from '@angular/router';
import { PlaylistListComponent } from './playlist-list/playlist-list.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { CallbackComponent } from './auth/callback.component';
import { PlaylistDetailsComponent } from './playlist-details/playlist-details.component';

export const appRoutes: Route[] = [
    {
        path: '', component: WelcomeComponent,
    },
    {
        path: 'callback', component: CallbackComponent,
    },
    {
        path: 'playlists', component: PlaylistListComponent,
    },
    {
        path: 'user-profile', component: UserProfileComponent,
    },
    {
        path: 'playlist/:id', component: PlaylistDetailsComponent,
    }
];
