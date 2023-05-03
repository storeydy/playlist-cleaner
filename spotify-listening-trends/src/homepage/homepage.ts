import { Playlist, UserProfile } from "../types";

export async function fetchProfile(token: string): Promise<UserProfile> {
    const result = await fetch("https://api.spotify.com/v1/me", {
        method: "GET", headers: { Authorization: `Bearer ${token}` }
    });

    return await result.json();
}

export async function getPlaylists(token: string, userId: string): Promise<any> {
    const result = await fetch("https://api.spotify.com/v1/users/" + userId + "/playlists", {
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

export function populateUI(profile: UserProfile) {
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