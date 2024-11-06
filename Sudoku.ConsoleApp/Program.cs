using Sudoku.GameLibrary;

namespace Sudoku.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameBoard game = new GameBoard(Difficulty.Hard);

            int[,] solutionGameBoard = game.Solution();

            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    Console.Write(solutionGameBoard[i, j] + "  ");
                    if (j % 3 == 2)
                    {
                        Console.Write("|| ");
                    }
                }
                Console.WriteLine();
                if (i % 3 == 2)
                {
                    Console.WriteLine("--------------------------------");
                }
            }


            Console.WriteLine();
            Console.WriteLine();
            
            int[,] unsolvedGameBoard = game.GetGameBoard();

            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    Console.Write(unsolvedGameBoard[i, j] + "  ");
                    if (j % 3 == 2)
                    {
                        Console.Write("|| ");
                    }
                }
                Console.WriteLine();
                if (i % 3 == 2)
                {
                    Console.WriteLine("--------------------------------");
                }
            }
            
        }
    }
}
