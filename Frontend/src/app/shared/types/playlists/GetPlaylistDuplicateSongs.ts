import { GetSongResponse } from "../openapi";

export interface GetPlaylistDuplicateSongs {
    duplicateTrackSets: Array<GetPlaylistDuplicateSongsResponseSet>;
  }
  
  export interface GetPlaylistDuplicateSongsResponseSet {
    songs: Array<GetSongResponse>;
  }