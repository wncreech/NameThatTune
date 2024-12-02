using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NAudio.Wave.Asio;
using SpotifyAPI.Web;

namespace NameThatTune
{
    public static class GameMenu
    {
        public static async Task MainGameMenu(SpotifyClient spotify) {
            while (true) {
                Console.Clear();
                DisplayGameOptions();
                int choice = UI.GetMenuChoice(1, 2);
                if (choice == -1) break;
                else if (choice == 1) {
                    Playlist playlist = await GameBoot(spotify);
                    if (playlist != null)
                    await GameUI(playlist);
                }
                else if (choice == 2) DisplayExplanation();
                else break;
            }
        }


        public static void DisplayGameOptions() {
            Console.Clear();
            Console.WriteLine("Please enter the key of a choice below or press escape to exit.\n");
            Console.WriteLine("1. Play");
            Console.WriteLine("2. View Explanation");
        }

        public static void DisplayExplanation() {
            Console.WriteLine("Placeholder!");
            UI.WaitKey();
        }

        public static async Task<Playlist> GameBoot(SpotifyClient spotify) {
            while (true) {
                Console.Clear();
                Console.WriteLine("Enter the File Name of a playlist below or '-1' to exit.\n\n");
                Thread.Sleep(200);
                PlaylistMenu.DisplayAllPlaylists();
                string input = Console.ReadLine()?.Trim(); //learned this syntax in CS!//
                if (input == "-1") return null;

                string[] inputName = input.Split('.');
                input = inputName[0] + ".json";
                Console.WriteLine("input");


                if (File.Exists(input)) {
                    Playlist selectedPlaylist = new Playlist(input, false);
                    Console.WriteLine(selectedPlaylist.songs[0].title);
                    return selectedPlaylist;
                }

                else {
                    Console.WriteLine("\nInvalid input. Please try again.");
                    Thread.Sleep(600);
                    continue;
                }
            }
        }

        public static async Task GameUI(Playlist playlist) {
            Game game = new Game(playlist);
            await game.LoadGame();
            game.Play();
        }
    }
}