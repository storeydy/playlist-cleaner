import { Route } from '@angular/router';
import { PlaylistListComponent } from './playlist-list/playlist-list.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { CallbackComponent } from './auth/callback.component';

export const appRoutes: Route[] = [
    {
        path: '', component: WelcomeComponent,
    },
    {
        path: 'callback', component: CallbackComponent,
    },
    {
        path: 'playlist-list', component: PlaylistListComponent,
    },
    {
        path: 'user-profile', component: UserProfileComponent,
    }
];
