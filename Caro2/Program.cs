using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Caro2
{
    class Program
    {
        const int BOARD_SIZE = 15;
        const int WIN_COUNT = 5;

        static int[,] board = new int[BOARD_SIZE, BOARD_SIZE];

        public static void Main()
        {
            while (true)
            {
                PrintBoard();
                PlayerMove();
                PrintBoard();
                if (CheckWin(1))
                {
                    Console.WriteLine("Player wins!");
                    break;
                }
                AIMove();
                if (CheckWin(2))
                {
                    Console.WriteLine("AI wins!");
                    break;
                }
            }
        }

        private static void PrintBoard()
        {
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    switch (board[i, j])
                    {
                        case 0: Console.Write(". "); break;
                        case 1: Console.Write("O "); break;
                        case 2: Console.Write("X "); break;
                    }
                }
                Console.WriteLine();
            }
        }

        private static void PlayerMove()
        {
            int row, col;
            while (true)
            {
                Console.WriteLine("Enter your move (row and column): ");
                row = int.Parse(Console.ReadLine());
                col = int.Parse(Console.ReadLine());
                if (row >= 0 && row < BOARD_SIZE && col >= 0 && col < BOARD_SIZE && board[row, col] == 0)
                {
                    board[row, col] = 1;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid move, try again.");
                }
            }
        }

        private static void AIMove()
        {
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    if (board[i, j] == 0)
                    {
                        board[i, j] = 2;
                        return;
                    }
                }
            }
        }

        private static bool CheckWin(int player)
        {
            int size = board.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i, j] == player)
                    {
                        if (j <= size - WIN_COUNT && CheckDirection(player, i, j, 0, 1))
                            return true;
                        if (i <= size - WIN_COUNT && CheckDirection(player, i, j, 1, 0))
                            return true;
                        if (i <= size - WIN_COUNT && j <= size - WIN_COUNT && CheckDirection(player, i, j, 1, 1))
                            return true;
                        if (i <= size - WIN_COUNT && j >= WIN_COUNT - 1 && CheckDirection(player, i, j, 1, -1))
                            return true;
                    }
                }
            }
            return false;
        }

        private static bool CheckDirection(int player, int startRow, int startCol, int deltaRow, int deltaCol)
        {
            for (int k = 0; k < WIN_COUNT; k++)
            {
                if (board[startRow + k * deltaRow, startCol + k * deltaCol] != player)
                    return false;
            }
            return true;
        }
    }

}
