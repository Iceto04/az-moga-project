using System;
using System.Collections.Generic;
using System.Linq;

namespace Queens.ConsoleUI
{
    public class Program
    {

        public static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Player vs Player");
                Console.WriteLine("2. Player vs Random");
                Console.WriteLine("3. Random vs Player");
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
                        PlayGame();
                        break;
                    }
                    if (line == "2")
                    {
                        PlayGame();
                        break;
                    }
                    if (line == "3")
                    {
                        PlayGame();
                        break;
                    }
                }

                Console.WriteLine($"Do you want to play again?");
                Console.WriteLine("Press [Enter] to continue.");
                var key = Console.ReadKey();
            }
        }

        private static void PlayGame()
        {
            var game = new QueensGame();
            game.Play();
        }
    }
}
