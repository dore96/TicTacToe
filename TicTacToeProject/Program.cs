using System;
using System.Linq;
using GameLogic;

namespace TicTacToeProject
{
    class Program
    {
        const int User = 1;
        const int Computer = -1;
        static void Main(string[] args)
        {
            Board game = new Board(); 
            
            while ((!game.IsWinner(game.board, User) && !game.IsWinner(game.board, Computer)) || 
                   !game.board.Contains(0))
                Turn(game);

            if (game.IsWinner(game.board, Computer))
                Console.WriteLine("The computer won ! haha ");
            else
                Console.WriteLine("No one won the game");

        }

        private static void Turn(Board game)
        {
            game.PickSquare(game.board, User);
            if (game.IsWinner(game.board, User))
            {
                Console.WriteLine("You wins !");
                game.PrintBoard(game.board);
                return;
            }
            game.PickSquare(game.board, Computer);
            game.PrintBoard(game.board);
        }
    }
}
