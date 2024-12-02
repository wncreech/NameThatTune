using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NameThatTune
{
    public static class UI
    {
        public static void DisplayBanner() {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("                                    Welcome to William Creech's                            ");
            Console.WriteLine("███╗   ██╗ █████╗ ███╗   ███╗███████╗    ████████╗██╗  ██╗ █████╗ ████████╗    ████████╗██╗   ██╗███╗   ██╗███████╗");
            Console.WriteLine("████╗  ██║██╔══██╗████╗ ████║██╔════╝    ╚══██╔══╝██║  ██║██╔══██╗╚══██╔══╝    ╚══██╔══╝██║   ██║████╗  ██║██╔══");
            Console.WriteLine("██╔██╗ ██║███████║██╔████╔██║█████╗         ██║   ███████║███████║   ██║          ██║   ██║   ██║██╔██╗ ██║█████╗  ");
            Console.WriteLine("██║╚██╗██║██╔══██║██║╚██╔╝██║██╔══╝         ██║   ██╔══██║██╔══██║   ██║          ██║   ██║   ██║██║╚██╗██║██╔══╝  ");
            Console.WriteLine("██║ ╚████║██║  ██║██║ ╚═╝ ██║███████╗       ██║   ██║  ██║██║  ██║   ██║          ██║   ╚██████╔╝██║ ╚████║███████");
            Console.WriteLine("╚═╝  ╚═══╝╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝       ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝          ╚═╝    ╚═════╝ ╚═╝  ╚═══╝╚══════╝)");
            ResetColor();
            Console.WriteLine("\n\n");
            Thread.Sleep(3000);
            ResetColor();
        }

        public static void WaitKey() {
            Console.WriteLine("\n-Press any key to continue-");
            Thread.Sleep(500);
            Console.ReadKey(true);
        }

        public static int GetMenuChoice(int first, int last) {
            while (true)
            {
                Thread.Sleep(250);
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Escape) return -1;
        
                if (char.IsDigit(keyInfo.KeyChar))
                {
                    int option = int.Parse(keyInfo.KeyChar.ToString());
                    if (option >= first && option <= last)
                    {
                        return option;
                    }
                }

                Console.WriteLine("\nInvalid input. Please choose from the above options.");
                Thread.Sleep(250);
            }
        }


        public static void IncorrectPulse() {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("X");
            Thread.Sleep(2000);
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void CorrectGreen() {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
        }

        public static void ResetColor() {
            Console.BackgroundColor = ConsoleColor.Black;
        }
        }
    }
