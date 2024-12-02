using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Security;

namespace NameThatTune
{
    public class Round
    {
        public Song roundSong { get; set; }
        public Sample roundSample { get; set; }

        public int score = 0;

        public int roundNumber;

        public Round(int roundNumber, Song addSong, string sampleURL)
        {
            this.roundNumber = roundNumber;
            roundSong = addSong;
            roundSample = new Sample(sampleURL);
        }


        //because Deezer previews expire rather quickly, samples must be loaded dynamically for each game.
        //original design stored sample URL with the playlist file pre spotify API change

        public int PlayRound()
        {
            UI.ResetColor();
            Console.Clear();
            Console.WriteLine("ROUND " + roundNumber + "\n\n\n");
            Thread.Sleep(2000);
            Console.WriteLine("-Press any key to start round-");
            Console.ReadKey();
            Console.Clear();

            Stopwatch scorekeeper = new Stopwatch();
            string guess = "";
            Console.WriteLine("-Type your guess, or enter to skip-");
            roundSample.Play();
            scorekeeper.Start();
            bool successful = false;

            while (true)
            {
                if (roundSample.isStopped())
                {
                    scorekeeper.Stop();
                    break;
                }
                guess = Console.ReadLine();
                if (guess.Length < 1)
                {
                    scorekeeper.Stop();
                    roundSample.Stop();
                    break;
                }
                if (ValidateGuess(roundSong.title, guess) && (int)scorekeeper.ElapsedMilliseconds < 30000)
                {
                    scorekeeper.Stop();
                    successful = true;
                    roundSample.Stop();
                    break;
                }
                else {
                    UI.IncorrectPulse();
                    Console.Clear();
                    UI.ResetColor();
                    Console.WriteLine("-Type your guess, or enter to skip-");
                }
            }

            if (successful == true)
            {
                UI.CorrectGreen();
                Console.WriteLine("\n\nCorrect!");
                score = (30000 - (int)scorekeeper.ElapsedMilliseconds) / 10;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("No! Missed it!");
                score = 0;
            }

            Console.WriteLine($"The song is {roundSong.title} by {roundSong.artist}.");
            Console.WriteLine($"Round score: {score}");
            Thread.Sleep(1000);
            return score;
        }




        public static bool ValidateGuess(string title, string guess)
        {
            return Plaintext(title) == Plaintext(guess);
        }

        private static string Plaintext(string text)
        { //excludes parentheses, dashes, punctuation, accents, makes case-insensitive
            string[] parts;
            if (text[0] != '(') parts = text.Split('(');
            else parts = text.Split('-');
            parts = parts[0].Split('-');
            text.Normalize(NormalizationForm.FormD);
            text = parts[0].Trim().ToLower();
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsPunctuation(text[i]))
                {
                    text = text.Replace(text[i].ToString(), "");
                }
            }
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsWhiteSpace(text[i]))
                {
                    text = text.Replace(text[i].ToString(), "");
                }
            }
            return text;

        }
    }
}
