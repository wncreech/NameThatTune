using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SpotifyAPI.Web;

namespace NameThatTune
{
    public class MainMenu
    {
        public static async Task Main(SpotifyClient spotify) {
            while (true) {
                UI.DisplayBanner();
                DisplayMainMenu();
                int choice = UI.GetMenuChoice(1, 2);
                if (choice == -1) break;
                else if (choice == 1) await GameMenu.MainGameMenu(spotify);
                else if (choice == 2) await PlaylistMenu.MainPlaylistMenu(spotify);
            }
        }

        public static void DisplayMainMenu() {
            Console.WriteLine("Please enter the key of a choice below or press escape to exit.\n");
            Console.WriteLine("1. Play");
            Console.WriteLine("2. View playlists");
        }

        public static async Task<SpotifyClient> VerifyConnection() {
            SpotifyClient spotify = await Authenticator.Run();
            if (!Authenticator.isActive) {
                Console.WriteLine("Internet connection could not be established. Please check connection and restart.");
                return null;
            }
            return spotify;
        }
}}