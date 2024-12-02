using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NAudio.Wave.SampleProviders;
using SpotifyAPI.Web;

namespace NameThatTune
{
    //TO DO - prevent duplicates
    public class Game
    {
        const int NUM_ROUNDS = 10;

        private Playlist playlist;
        public Round[] rounds;

        public int score = 0;

        public int scoreToBeat = 0;

        public bool playable = false;

        public Game(Playlist playlist)
        {
            this.playlist = playlist;
            rounds = new Round[NUM_ROUNDS];
            scoreToBeat = playlist.highScore;
        }

        public async Task LoadGame()
        {
            Console.WriteLine("Loading game...");
            //selection algorithm gets 10 random songs, ensures valid samples, removes from candidates to avoid repeats
            List<int> songIndices = new List<int>();
            for (int i = 0; i < playlist.songs.Count; i++)
            {
                songIndices.Add(i);
            }

            List<Task> tasks = new List<Task>();
            int numLoadedRounds = 0;
            int numFailedAttempts = 0;
            Random rnd = new Random();



            while (numLoadedRounds < NUM_ROUNDS && songIndices.Count > 0 && numFailedAttempts < 10)
            { //until 10 is reached or out of songs
                int nextPosition = rnd.Next(songIndices.Count);
                int nextIndex = songIndices[nextPosition];
                string sampleURL = await DeezerTools.GetPreviewUrlAsync(playlist.songs[nextIndex].title, playlist.songs[nextIndex].artist);
                if (sampleURL != null)
                {
                    rounds[numLoadedRounds] = new Round(numLoadedRounds + 1, playlist.songs[nextIndex], sampleURL);
                    numLoadedRounds++;
                }
                else {
                    numFailedAttempts++;
                }
                songIndices.RemoveAt(nextPosition);
            }

            if (numLoadedRounds < NUM_ROUNDS)
            { //failed to generate 10 good songs
                Console.WriteLine("The playlist failed to load.");
                Console.WriteLine("This could be because of a bad link, the playlist having too few songs, or because of artist-specific licensing restrictions.");
                Console.WriteLine("Please try another playlist.");
                return;
            }

            else
            {
                Console.WriteLine("Game loaded successfully.");
                playable = true;
                return;
            }
        }




        public void Play()
        {
            if (playable == false) return;
            Console.Clear();
            if (scoreToBeat != 0) Console.WriteLine(playlist.name + " High Score: " + scoreToBeat);
            UI.WaitKey();
            for (int i = 0; i < NUM_ROUNDS; i++)
            {
                score += rounds[i].PlayRound();
                Console.WriteLine("Current total score: " + score);
                UI.WaitKey();
            }
            Console.Clear();
            Console.WriteLine("Final score: " + score);
            if (score > scoreToBeat)
            {
                Console.WriteLine("New high score!");
                playlist.highScore = score;
                playlist.SaveToFile();
                Thread.Sleep(1000);
                UI.WaitKey();
            }
        }
    }
}