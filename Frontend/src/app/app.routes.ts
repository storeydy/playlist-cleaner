import { Route } from '@angular/router';
import { PlaylistListComponent } from './playlist-list/playlist-list.component';
import { UserProfileComponent } from './user-profile/user-profile.component';

export const appRoutes: Route[] = [
    {
        path: 'playlist-cleaner-playlist-list', component: PlaylistListComponent,
    },
    {
        path: 'playlist-cleaner-user-profile', component: UserProfileComponent,
    }
];
