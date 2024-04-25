export * from './playlists.service';
import { PlaylistsService } from './playlists.service';
export * from './playlists.serviceInterface';
export * from './users.service';
import { UsersService } from './users.service';
export * from './users.serviceInterface';
export const APIS = [PlaylistsService, UsersService];
