using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Board
    {
        const int User = 1;
        const int Computer = -1;
        public int[] board;
        int choice = 0;

        public Board()
        {
            board = new int[9];
        }

        public bool IsWinner(int[] board, int player)
        {//method peruse is to check if there is a winner yet. 
            // check rows
            if ((board[0] == player && board[1] == player && board[2] == player) ||
                (board[3] == player && board[4] == player && board[5] == player) ||
                (board[6] == player && board[7] == player && board[8] == player) ||
                // check columns
                (board[0] == player && board[3] == player && board[6] == player) ||
                (board[1] == player && board[4] == player && board[7] == player) ||
                (board[2] == player && board[5] == player && board[8] == player) ||
                // check diags
                (board[0] == player && board[4] == player && board[8] == player) ||
                (board[2] == player && board[4] == player && board[6] == player))
            { return true; }

            return false;
        }

        public void PrintBoard(int[] board)
        {//method peruse is to print the board 
            for (int i = 0; i < 9; i++)
            {
                if (i % 3 == 0 && i != 0)
                    Console.WriteLine();
                switch (board[i])
                {
                    case 0:
                        Console.Write(".");
                        break;
                    case 1:
                        Console.Write("X");
                        break;
                    case -1:
                        Console.Write("O");
                        break;
                }
            }
            Console.WriteLine();
        }

        public void PickSquare(int[] board, int player)
        {
            bool validChoice = false;
            int computerTurn;
            while (!validChoice)
            {
                if (player == User)
                {
                    Console.WriteLine("Please enter a picked square (for 0 - 8): ");
                    int userTurn = int.Parse(Console.ReadLine());
                    validChoice = ValidSquare(board, userTurn, User);
                }
                else
                {
                    Random rand = new Random();
                    computerTurn = rand.Next(8);
                    //MiniMax(board, player);
                    validChoice = ValidSquare(board, computerTurn, Computer);
                }
            }

        }

        private static bool ValidSquare(int[] board, int userPick, int player)
        {
            if (board[userPick] != 0)
            {
                if (player == User)
                    Console.Write("Not a valid square, ");
                return false;
            }
            else
            {
                board[userPick] = player;
                return true;
            }
        }

        public int MiniMax(int[] board, int player)
        {
            int[] Grid = cloneBoard(board);

            if (checkScore(Grid, Computer) != 0)
                return checkScore(Grid, Computer);
            else if (!Grid.Contains(0)) 
                return 0;

            List<int> scores = new List<int>();
            List<int> moves = new List<int>();

            for (int i = 0; i < 9; i++)
            {
                if (Grid[i] == 0)
                {
                    scores.Add(MiniMax(makeGridMove(Grid, player, i), SwitchPlayer(player)));
                    moves.Add(i);
                }
            }

            if (player == Computer)
            {
                int MaxScoreIndex = scores.IndexOf(scores.Max());
                choice = moves[MaxScoreIndex];
                return scores.Max();
            }
            else
            {
                int MinScoreIndex = scores.IndexOf(scores.Min());
                choice = moves[MinScoreIndex];
                return scores.Min();
            }
        }

        private static int[] cloneBoard(int[] inputGrid)
        {
            int[] Clone = new int[9];
            for (int i = 0; i < 9; i++) 
                Clone[i] = inputGrid[i];
            return Clone;
        }

        private static int checkScore(int[] board, int Player)
        {
            if (checkGameWin(board, Player)) return 10;

            else if (checkGameWin(board, SwitchPlayer(Player))) return -10;

            else return 0;
        }

        static bool checkGameWin(int[] board, int player)
        {
            //method peruse is to check if there is a winner yet. 
            // check rows
            if ((board[0] == player && board[1] == player && board[2] == player) ||
                (board[3] == player && board[4] == player && board[5] == player) ||
                (board[6] == player && board[7] == player && board[8] == player) ||
                // check columns
                (board[0] == player && board[3] == player && board[6] == player) ||
                (board[1] == player && board[4] == player && board[7] == player) ||
                (board[2] == player && board[5] == player && board[8] == player) ||
                // check diags
                (board[0] == player && board[4] == player && board[8] == player) ||
                (board[2] == player && board[4] == player && board[6] == player))
            {
                return true;
            }

            return false;
        }

        private static int SwitchPlayer(int player)
        {
            if (player == User) 
                return Computer;
            else 
                return User;
        }

        private static int[] makeGridMove(int[] Grid, int Move, int Position)
        {
            int[] newGrid = cloneBoard(Grid);
            newGrid[Position] = Move;
            return newGrid;
        }
    }

}
