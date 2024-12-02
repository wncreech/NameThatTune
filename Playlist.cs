using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Text.Json;
using SpotifyAPI.Web;
using System.Runtime.CompilerServices;
using NAudio.MediaFoundation;

namespace NameThatTune
{
    public class Playlist
    {

        public int id { get; set; }
        public string name { get; set; }

        public int highScore { get; set; }
        public List<Song> songs { get; set; }
        private string fileName;
        private static int count = 0;

        public bool successfullyInitialized = true;



        public Playlist()
        {
            successfullyInitialized = false;
        }

        public Playlist(string fileName, bool forceOverwrite)
        {
            this.fileName = fileName;
            songs = new List<Song>();

            if (File.Exists(fileName) && forceOverwrite == false)
            {
                LoadFromFile();
                successfullyInitialized = true;
            }
            else
            {
                id = count;
                name = "Untitled Playlist";
                highScore = 0;
            }
            count++;
        }

        //interacts with SpotifyAPI to populate a public spotify playlist. it does NOT store sample data - that is loaded dynamically per game
        public static async Task<Playlist> GeneratePlaylist(SpotifyClient spotify, string PlaylistURL, string fileName, bool forceOverwrite)
        {
            var newPlaylist = new Playlist(fileName, forceOverwrite);
            if (File.Exists(fileName) && forceOverwrite == false)
            {
                return newPlaylist;
            }
            bool check = await newPlaylist.FetchNewPlaylist(spotify, PlaylistURL, fileName);
            if (check == true) newPlaylist.SaveToFile();
            if (check == true) newPlaylist.successfullyInitialized = true;
            return newPlaylist;

        }


        public void SaveToFile()
        {
            string jsonString = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileName, jsonString);
        }

        private void LoadFromFile()
        {
            if (File.Exists(fileName))
            {

                string jsonString = File.ReadAllText(fileName);


                Playlist loadedPlaylist = JsonSerializer.Deserialize<Playlist>(jsonString);

                if (loadedPlaylist != null)
                {
                    id = loadedPlaylist.id;
                    name = loadedPlaylist.name;
                    songs = loadedPlaylist.songs;
                    highScore = loadedPlaylist.highScore;
                }
            }
        }

        public async Task<bool> FetchNewPlaylist(SpotifyClient spotify, string PlaylistURL, string filePath)
        {
            FullPlaylist spotifyPlaylist = null;
            try {
                 spotifyPlaylist = await spotify.Playlists.Get(PlaylistURL);
            }
            catch
            {
                Console.WriteLine("error getting playlist - check URL");
                return false;
            }
            id = spotifyPlaylist.Id.GetHashCode();
            name = spotifyPlaylist.Name;
            highScore = 0;
            await PopulateNewPlaylist(spotify, spotifyPlaylist.Tracks);
            return true;
        }

        private async Task PopulateNewPlaylist(SpotifyClient spotify, Paging<PlaylistTrack<IPlayableItem>> tracks)
        {
            if (tracks == null) {
                Console.WriteLine("its over");
            }
            while (true)
            {
                foreach (var item in tracks.Items)
                {

                    if (item.Track is FullTrack track)
                    {
                        Song newSong = new Song(songs.Count, track.Id, track.Name, track.Artists[0].Name, track.Album.Images[0].Url);
                        songs.Add(newSong);
                    }
                }
                if (tracks.Next != null)
                {
                    tracks = await spotify.NextPage(tracks);
                }
                else break;
            }

        }

    }
}

