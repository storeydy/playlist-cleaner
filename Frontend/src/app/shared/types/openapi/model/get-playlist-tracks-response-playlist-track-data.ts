/**
 * PlaylistCleaner.Api
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */
import { GetPlaylistTracksResponsePlaylistTrackArtist } from './get-playlist-tracks-response-playlist-track-artist';
import { GetPlaylistTracksResponsePlaylistTrackAlbum } from './get-playlist-tracks-response-playlist-track-album';


export interface GetPlaylistTracksResponsePlaylistTrackData { 
    album?: GetPlaylistTracksResponsePlaylistTrackAlbum;
    artists?: Array<GetPlaylistTracksResponsePlaylistTrackArtist> | null;
    duration_ms?: number;
    explicit?: boolean;
    id?: string | null;
    name?: string | null;
    type?: string | null;
}

