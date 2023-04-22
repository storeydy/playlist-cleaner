interface UserProfile {
    country: string;
    display_name: string;
    email: string;
    explicit_content: {
        filter_enabled: boolean,
        filter_locked: boolean
    },
    external_urls: { spotify: string; };
    followers: { href: string; total: number; };
    href: string;
    id: string;
    images: Image[];
    product: string;
    type: string;
    uri: string;
}

interface Image {
    url: string;
    height: number;
    width: number;
}

interface PlaylistOwner{
    displayName : string;
    externalUrls: {},
    href: string;
    id: string;
    type: string;
    uri: string;
}

interface PlaylistTracks{
    href: string;
    total: number;
}

export interface Playlist {
    collaborative: boolean;
    description: string;
    externalUrls: {};
    href: string;
    id: string;
    images: Image[];
    name: string;
    owner: PlaylistOwner;
    primaryColor: string;
    public: boolean;
    snapshotId: string;
    tracks : PlaylistTracks;
    type: string;
    uri: string;
}