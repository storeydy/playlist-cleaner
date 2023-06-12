
export async function getRecentlyPlayed(token : string){
    let recents: any = [];
    const startTime = Date.now()
    let historyPeriod = Date.now();
    let url = "https://api.spotify.com/v1/me/player/recently-played?limit=50&before=" + historyPeriod   //*fix request
    while(historyPeriod > startTime - 3*3600000)   //3 hours history
    {
        const result = await fetch(url, {
            method: "GET", headers: { Authorization: `Bearer ${token}` }
        })
        let newPage : any = await result.json();

        recents.push(...newPage.items)
        historyPeriod -= 3600000 // - hour
    }
    return recents;
}