import { Playlist, UserProfile } from "../types";

export async function fetchProfile(token: string): Promise<UserProfile> {
    const result = await fetch("https://api.spotify.com/v1/me", {
        method: "GET", headers: { Authorization: `Bearer ${token}` }
    });

    return await result.json();
}

export async function getPlaylists(token: string, userId: string): Promise<any> {
    const result = await fetch("https://api.spotify.com/v1/users/" + userId + "/playlists?limit=50", {
        method: "GET", headers: { Authorization: `Bearer ${token}` }
    })

    let data = await result.json();

    let typedPlaylists: Playlist[] = [];

    data.items.forEach((element: any) => {
        let p = convertPlaylist(element);
        typedPlaylists.push(p);
    });
    return typedPlaylists;
}

export async function getTracksInPlaylist(token: string, playlistId: string): Promise<any> {
    let tracks: any[] = []  //Add type
    let url = "https://api.spotify.com/v1/playlists/" + playlistId + "/tracks";
    const result = await fetch(url, {
        method: "GET", headers: { Authorization: `Bearer ${token}` }
    })
    let newData : any = await result.json();
    tracks.push(...newData.items)    
    while(newData.next != null){
        const urlForNextPage : string = newData.next;
        const result = await fetch( urlForNextPage, {
            method: "GET", headers: { Authorization: `Bearer ${token}` }
        })
        newData = await result.json();
        tracks.push(...newData.items)
    }

    return tracks;
}

function convertPlaylist(externalPlaylist: any): Playlist { //ToDo: add function for more effective mapping
    return {
        name: externalPlaylist.name,
        collaborative: externalPlaylist.collaborative,
        description: externalPlaylist.description,
        externalUrls: externalPlaylist.external_urls,
        href: externalPlaylist.href,
        id: externalPlaylist.id,
        images: externalPlaylist.images,
        owner: externalPlaylist.owner,
        tracks: externalPlaylist.tracks,
        primaryColor: externalPlaylist.primary_color,
        public: externalPlaylist.public,
        snapshotId: externalPlaylist.shapshop_id,
        uri: externalPlaylist.uri,
        type: externalPlaylist.type
    }
}

export function fillUserProfileSummary(profile: UserProfile) {
    document.getElementById("displayName")!.innerText = profile.display_name;
    if (profile.images[0]) {
        const profileImage = new Image(200, 200);
        profileImage.src = profile.images[0].url;
        document.getElementById("avatar")!.appendChild(profileImage);
    }
    document.getElementById("id")!.innerText = profile.id;
    document.getElementById("email")!.innerText = profile.email;
    document.getElementById("uri")!.innerText = profile.uri;
    document.getElementById("uri")!.setAttribute("href", profile.external_urls.spotify);
    document.getElementById("url")!.innerText = profile.href;
    document.getElementById("url")!.setAttribute("href", profile.href);
    document.getElementById("followers")!.innerText = profile.followers.total.toString();
    document.getElementById("imgUrl")!.innerText = profile.images[0]?.url ?? '(no profile image)';
}

export function fillUserPlaylistsDisplay(playlists: Playlist[]) {
    var table = document.getElementById("playlist-table-body");
    for(var i = 0; i < playlists.length; i++)
    {
        var playlistRow = document.createElement("tr");
        var playlistName = document.createElement("td");
        playlistName.innerHTML = playlists[i].name;
        var playlistUri = document.createElement("td");
        playlistUri.innerHTML = playlists[i].href;
        var playlistOwner = document.createElement("td");
        playlistOwner.innerHTML = playlists[i].owner.href;
        var playlistDescription = document.createElement("td");
        playlistDescription.innerHTML = playlists[i].description;
        var playlistType = document.createElement("td");
        playlistType.innerHTML = playlists[i].type;
        var playlistTrackCount = document.createElement("td");
        playlistTrackCount.innerHTML = playlists[i].tracks.total.toString();
        playlistRow.append(playlistName, playlistUri, playlistOwner, playlistDescription, playlistType, playlistTrackCount);
        table?.appendChild(playlistRow);
    }
}


export function fillHistoryDisplay(history: any){
    var section = document.getElementById("history");
    var div = document.createElement("ol");

    for(var i = 0; i < history.length; i++)
    {
        console.log(history[i])
        var object = document.createElement("li");
        if(history[i].context != null) {//add back after
        object.innerHTML = history[i].context.type + " - " + history[i].context.href + " - " + history[i].track.name ;
        div.appendChild(object);
            }
    }
    section?.appendChild(div);
}