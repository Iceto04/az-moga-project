using System;
using System.Collections.Generic;
using System.Text;

namespace Queens.ConsoleUI
{
    public class Board
    {
        private static char[,] board;
        private const int minBoardSize = 3;
        private const int maxBoardSize = 100;
        public Board()
        {
            while (!InitBoard())
            {
                var isBoardCorrect = InitBoard();
                if (isBoardCorrect)
                {
                    break;
                }
            }
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

        public bool IsBoardFull()
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

        public List<Tuple<int, int>> GetAllFreeSpots()
        {
            var spots = new List<Tuple<int, int>>();
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] == '\0')
                    {
                        var tuple = Tuple.Create(row, col);
                        spots.Add(tuple);
                    }
                }
            }

            return spots;
        }

        public bool IsPositionValid(int row, int col)
        {
            return row < 0 || row > board.GetLength(0) - 1 ||
                row >= board.GetLength(0) ||
                col < 0 || col > board.GetLength(1) - 1 ||
                col >= board.GetLength(1);
        }

        public bool IsPositionFree(int row, int col)
        {
            return board[row, col] == '1' || board[row, col] == '2';
        }

        public void PlaceQueen(int row, int col, char userNumber)
        {
            board[row, col] = userNumber;
            PrintPositionsUnderAttack(board, row, col);
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

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] == '\0')
                    {
                        sb.Append($"  ");
                    }
                    else
                    {
                        sb.Append($"{board[row, col]} ");
                    }
                }
                sb.AppendLine();
            }
            sb.AppendLine();

            return sb.ToString();
        }
    }
}