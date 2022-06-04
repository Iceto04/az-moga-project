using System;
using System.Collections.Generic;
using System.Linq;

namespace Queens.ConsoleUI
{
    public class Program
    {
        private const string player = "Player";
        private const string random = "Random";

        public static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Player vs Player");
                Console.WriteLine("2. Player vs Random");
                Console.WriteLine("3. (Simulate) Random vs Random");
                Console.WriteLine("0. Exit");

                while (true)
                {
                    Console.Write("Please enter number [0-9]: ");
                    var line = Console.ReadLine();
                    if (line == "0")
                    {
                        return;
                    }
                    if (line == "1")
                    {
                        PlayGame(player, player);
                        break;
                    }
                    if (line == "2")
                    {
                        PlayGame(player, random);
                        break;
                    }
                    if (line == "3")
                    {
                        PlayGame(random, random);
                        break;
                    }
                }

                Console.WriteLine($"Do you want to play again?");
                Console.WriteLine("Press [Enter] to continue.");
                var key = Console.ReadKey();
            }
        }

        private static void PlayGame(string playerOne, string playerTwo)
        {
            var game = new QueensGame(playerOne, playerTwo);
            game.Play();
        }
    }
}
