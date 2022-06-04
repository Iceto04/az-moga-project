using System;
using System.Collections.Generic;
using System.Linq;

namespace Queens.ConsoleUI
{
    public class QueensGame
    {
        private static HashSet<int> attackedRows = new HashSet<int>();
        private static HashSet<int> attackedCols = new HashSet<int>();
        private static HashSet<int> attackedLeftDiagonals = new HashSet<int>();
        private static HashSet<int> attackedRightDiagonals = new HashSet<int>();

        private static int userNumber = 1;

        public QueensGame()
        {
        }

        public void Play()
        {
            Board board = new Board();

            ResetHashSets();
            while (true)
            {
                if (IsGameOver(board))
                {
                    PrintWinner();
                    break;
                }

                var isInputValid = ChoosePosition(userNumber, board);

                if (!isInputValid)
                {
                    continue;
                }

                if (!IsGameOver(board))
                {
                    if (userNumber == 1)
                    {
                        userNumber++;
                    }
                    else
                    {
                        userNumber--;
                    }
                }
            }
        }

        private static bool ChoosePosition(int userNumber, Board board)
        {
            Console.Write("Put queen on (0,0 (col, row) pattern): ");
            int row;
            int col;
            try
            {
                var positions = Console.ReadLine()
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

                row = positions[0];
                col = positions[1];
                char userNumberAsChar = userNumber.ToString().ToCharArray()[0];

                return PutQueens(board, col, row, userNumberAsChar);
            }
            catch (Exception)
            {
                Console.WriteLine("Input in not in the correct format. Please use (0,0 pattern)");

                return false;
            }
        }

        private static bool PutQueens(Board board, int row, int col, char userNumber)
        {
            if (board.IsPositionValid(row, col))
            {
                Console.WriteLine("Invalid position.");
                return false;
            }

            if (board.IsPositionFree(row, col))
            {
                Console.WriteLine("This position is occupied.");
                return false;
            }

            if (IsAttacked(row, col))
            {
                Console.WriteLine("This position is under attack.");
                return false;
            }

            Mark(board, row, col, userNumber);
            Console.WriteLine(board.ToString());

            return true;
        }

        private static void PrintWinner()
        {
            Console.WriteLine($"The winner is player {userNumber}!");
        }

        private static bool IsGameOver(Board board)
        {
            return board.IsBoardFull();
        }

        private static void Mark(Board board, int row, int col, char userNumber)
        {
            board.PlaceQueen(row, col, userNumber);
            attackedRows.Add(row);
            attackedCols.Add(col);
            attackedLeftDiagonals.Add(row - col);
            attackedRightDiagonals.Add(row + col);
        }

        private static bool IsAttacked(int row, int col)
        {
            return attackedRows.Contains(row) ||
                   attackedCols.Contains(col) ||
                   attackedLeftDiagonals.Contains(row - col) ||
                   attackedRightDiagonals.Contains(row + col);
        }

        private static void ResetHashSets()
        {
            attackedRows = new HashSet<int>();
            attackedCols = new HashSet<int>();
            attackedLeftDiagonals = new HashSet<int>();
            attackedRightDiagonals = new HashSet<int>();
        }
    }
}
