using System;
using System.Collections.Generic;
using System.Linq;

namespace Queens.ConsoleUI
{
    public class Program
    {
        private static HashSet<int> attackedRows = new HashSet<int>();
        private static HashSet<int> attackedCols = new HashSet<int>();
        private static HashSet<int> attackedLeftDiagonals = new HashSet<int>();
        private static HashSet<int> attackedRightDiagonals = new HashSet<int>();

        private static char[,] board;
        private static int userNumber = 1;
        private const int minBoardSize = 3;
        private const int maxBoardSize = 100;

        public static void Main(string[] args)
        {
            while (!InitBoard())
            {
                var isBoardCorrect = InitBoard();
                if (isBoardCorrect)
                {
                    break;
                }
            }

            while (true)
            {
                if (IsGameOver(board))
                {
                    PrintWinner();

                    Console.WriteLine($"Do you want to play again?");
                    Console.WriteLine("Press Enter button to continue.");
                    var key = Console.ReadKey();
                    if (key.Key != ConsoleKey.Enter)
                    {
                        Console.WriteLine("Thanks for playing.");
                        break;
                    }
                    ResetGame();
                }

                var isInputValid = ChoosePosition(userNumber);

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

        private static void PrintWinner()
        {
            Console.WriteLine($"The winner is player {userNumber}!");
        }

        private static bool InitBoard()
        {
            try
            {
                Console.Write("Rows = ");
                int rows = int.Parse(Console.ReadLine());
                Console.Write("Cols = ");
                int cols = int.Parse(Console.ReadLine());

                if (rows < 0 || cols < 0)
                {
                    Console.WriteLine("Board size must be a positive number.");
                    return false;
                }

                if (rows < minBoardSize || rows > maxBoardSize
                    || cols < minBoardSize || cols > maxBoardSize)
                {
                    Console.WriteLine("Board size must be with minimum size of (3x3) and maximum size of (100x100))");

                    return false;
                }

                board = new char[rows, cols];
            }
            catch (Exception)
            {
                Console.WriteLine("Board size must be a number.");
                return false;
            }

            return true;
        }

        private static void ResetGame()
        {
            while (!InitBoard())
            {
                InitBoard();
            }
            ResetHashSets();
        }

        private static void ResetHashSets()
        {
            attackedRows = new HashSet<int>();
            attackedCols = new HashSet<int>();
            attackedLeftDiagonals = new HashSet<int>();
            attackedRightDiagonals = new HashSet<int>();
        }

        private static bool ChoosePosition(int userNumber)
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

        private static bool IsGameOver(char[,] board)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] == '\0')
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool PutQueens(char[,] board, int row, int col, char userNumber)
        {
            if (row < 0 || row > board.GetLength(0) - 1 ||
                row >= board.GetLength(0) ||
                col < 0 || col > board.GetLength(1) - 1 ||
                col >= board.GetLength(1))
            {
                Console.WriteLine("Invalid position.");
                return false;
            }

            if (board[row, col] == '1' || board[row, col] == '2')
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
            PrintBoard(board);

            return true;
        }

        private static void Mark(char[,] board, int row, int col, char userNumber)
        {
            board[row, col] = userNumber;
            PrintPositionsUnderAttack(board, row, col);
            attackedRows.Add(row);
            attackedCols.Add(col);
            attackedLeftDiagonals.Add(row - col);
            attackedRightDiagonals.Add(row + col);
        }

        private static void PrintPositionsUnderAttack(char[,] board, int row, int col)
        {
            // Left and Rigth

            for (int i = 0; i < board.GetLength(1); i++)
            {
                if (i == col)
                {
                    continue;
                }

                board[row, i] = '*';
            }

            // Up and Down

            for (int i = 0; i < board.GetLength(0); i++)
            {
                if (i == row)
                {
                    continue;
                }

                board[i, col] = '*';
            }

            int j;
            // LeftDiagonal Up
            j = 1;
            while (row - j >= 0 && col - j >= 0)
            {
                board[row - j, col - j] = '*';
                j++;
            }

            // LeftDiagonal Down
            j = 1;
            while (row + j < board.GetLength(0) &&
                col + j < board.GetLength(1))
            {
                board[row + j, col + j] = '*';
                j++;
            }

            // RigthDiagonal Up
            j = 1;
            while (row - j >= 0 && col + j < board.GetLength(1))
            {
                board[row - j, col + j] = '*';
                j++;
            }

            // LeftDiagonal Down
            j = 1;
            while (row + j < board.GetLength(0) && col - j >= 0)
            {
                board[row + j, col - j] = '*';
                j++;
            }
        }

        private static bool IsAttacked(int row, int col)
        {
            return attackedRows.Contains(row) ||
                   attackedCols.Contains(col) ||
                   attackedLeftDiagonals.Contains(row - col) ||
                   attackedRightDiagonals.Contains(row + col);
        }

        private static void PrintBoard(char[,] board)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] == '\0')
                    {
                        Console.Write($"  ");
                    }
                    else
                    {
                        Console.Write($"{board[row, col]} ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
