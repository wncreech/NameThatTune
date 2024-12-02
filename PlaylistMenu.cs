using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using SpotifyAPI.Web;
using System.Reflection.Metadata;
using System.Net.NetworkInformation;
using System.Security.Cryptography;

namespace NameThatTune
{
    public static class PlaylistMenu
    {

        public static async Task MainPlaylistMenu(SpotifyClient spotify) {
            while (true) {
                Console.Clear();
                DisplayPlaylistMenu();
                int choice = UI.GetMenuChoice(1, 2);
                if (choice == -1) break;
                else if (choice == 1) ViewOnlyPlaylists();
                else if (choice == 2) await AddPlaylistInterface(spotify);
                else break;
            }
        }
        public static async Task<Playlist> DemoRun(SpotifyClient spotify) //fast testing purposes only
        //nevermind this is going to be the main interface for now
        //real frontend menus next
        {
            DisplayAllPlaylists();
            Thread.Sleep(1000);
            Console.WriteLine("\n\nEnter file name of playlist, or another name to generate a playlist");
            //jsonify
            string fileName = Console.ReadLine();
            string[] fileSplit = fileName.Split('.');
            fileName = fileSplit[0] + ".json";
            if (File.Exists(fileName))
            {
                Console.WriteLine(fileName + " exists. Loading...");
                return new Playlist(fileName, false);
            }
            else
            {
                while (true) {
                    Console.WriteLine("Enter link of public Spotify playlist to add: ");
                    string ID = ExtractIDFromURL(Console.ReadLine());
                    Playlist myPlaylist = await Playlist.GeneratePlaylist(spotify, ID, fileName, false);
                    if (myPlaylist.songs.Count != 0) { 
                        Console.WriteLine("Playlist generated successfully");
                        return myPlaylist;
                    }
                    else Console.WriteLine("Invalid link. Please try again.");
            }
        }
        }

        public static async Task AddPlaylistInterface(SpotifyClient spotify) {
            Console.Clear();
            Console.WriteLine("Enter a file name for new playlist:");
            string fileName = Console.ReadLine() + ".json";
            if (File.Exists(fileName))
            {
                Console.WriteLine(fileName + " already exists.");
                UI.WaitKey();
                return;
            }
            Console.WriteLine("(Note that due to Spotify API changes, some tracks are unavailable. Check the readme for more information.)");
            Console.WriteLine("Paste the URL for a public Spotify playlist:");
            string ID = ExtractIDFromURL(Console.ReadLine());
            Playlist myPlaylist = await Playlist.GeneratePlaylist(spotify, ID, fileName, true);
            Console.WriteLine(myPlaylist.name);
                if (myPlaylist.successfullyInitialized == true) Console.WriteLine("Playlist generated successfully.");
                else Console.WriteLine("Error initializing.");
                UI.WaitKey();
                return;
            }

        

        public static string ExtractIDFromURL(string URL)
        {
            string[] parts = URL.Split(new[] { "playlist/" }, StringSplitOptions.None);
            if (parts.Length > 1)
            {
                return parts[1].Split('?')[0];
            }
            return string.Empty;
        }

        public static void DisplayPlaylistMenu() {
            Console.WriteLine("Playlist Manager\n\n");
            Console.WriteLine("Please enter the key of a choice below or press escape to exit.\n");
            Console.WriteLine("1. View all playlists");
            Console.WriteLine("2. Generate new playlist");
        }
        public static void DisplayAllPlaylists() {
            Console.WriteLine("File name\tPlaylist Name\tHigh score");
            Console.WriteLine("=========\t=============\t==========");
            string[] playlistFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.json");
            for (int i = 0; i < playlistFiles.Length; i++) {
                string jsonContent = File.ReadAllText(playlistFiles[i]);
                Playlist currentFile = JsonSerializer.Deserialize<Playlist>(jsonContent);
                if (currentFile != null){
                    string currentFileName = Path.GetFileName(playlistFiles[i]);
                    Console.WriteLine($"{currentFileName}\t{currentFile.name}\t{currentFile.highScore}");
                }
            }
        }

        public static void ViewOnlyPlaylists() {
            Thread.Sleep(100);
            Console.Clear();
            DisplayAllPlaylists();
            UI.WaitKey();
        }

        
    }
}