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
import { GetSongResponseAlbumArtist } from './get-song-response-album-artist';
import { GetSongResponseAlbumImage } from './get-song-response-album-image';


export interface GetSongResponseAlbum { 
    album_type?: string | null;
    available_markets?: Array<string> | null;
    id?: string | null;
    images?: Array<GetSongResponseAlbumImage> | null;
    name?: string | null;
    release_date?: string | null;
    restrictions?: Array<string> | null;
    artists?: Array<GetSongResponseAlbumArtist> | null;
}

