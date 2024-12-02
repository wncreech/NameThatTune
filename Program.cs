using SpotifyAPI.Web;
using System;
using System.Collections.Immutable;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using NAudio.Wave;
using System.Runtime.CompilerServices;
using System.Text;
using System.Net.Security;

namespace NameThatTune
{
    class Program
    {
        static async Task Main(string[] args)
        {
        UI.DisplayBanner();
        Console.Clear();
        SpotifyClient spotify = await Authenticator.Run();
        Playlist myPlaylist = await PlaylistMenu.DemoRun(spotify);
        Game myGame = new Game(myPlaylist);
        await myGame.LoadGame();
        myGame.Play();
        UI.ResetColor();
        }
    }
}