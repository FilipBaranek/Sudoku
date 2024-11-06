
namespace Sudoku.GameLibrary
{
    public class GameBoard
    {
        private int[,] gameBoard;
        private int[,] solutionGameBoard;
        private Random random;

        public GameBoard(Difficulty difficulty)
        {
            random = new Random();

            solutionGameBoard = new int[9, 9];
            GenerateSolutionGameBoard();

            gameBoard = (int[,])solutionGameBoard.Clone();
            GenerateGameBoard(difficulty);
        }

        public int SectorIndex(int index)
        {
            return (index / 3) * 3;
        }

        public void GenerateGameBoard(Difficulty difficulty)
        {
            int difficultyLayout = 81 - (int)difficulty;

            while (difficultyLayout != 0)
            {
                int randRowIndex = random.Next(0, 9);
                int randColumnIndex = random.Next(0, 9);
                
                while (gameBoard[randRowIndex, randColumnIndex] == 0)
                {
                    randRowIndex = random.Next(0, 9);
                    randColumnIndex = random.Next(0, 9);
                }

                gameBoard[randRowIndex, randColumnIndex] = 0;

                --difficultyLayout;
            }
        }

        public bool GenerateSolutionGameBoard()
        {
            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    if (solutionGameBoard[i, j] == 0)
                    {
                        for (int k = 0; k < 9; ++k)
                        {
                            int randNumber = random.Next(1, 10);

                            if (CheckPossibility(i, j , randNumber, GameBoardType.Solution))
                            {
                                solutionGameBoard[i, j] = randNumber;

                                if (GenerateSolutionGameBoard())
                                {
                                    return true;
                                }

                                solutionGameBoard[i, j] = 0;
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        public bool CheckPossibility(int row, int column, int number, GameBoardType gameBoardType)
        {
            //////////////////////////
            //Toto pravdepodobne prerobiť na kontrolovanie podla solution konkretných elementov - ak gameboardtype = game tak len zoberie indexy a pozrie či su v matici ok
            int[,] sudokuElements = new int[9, 9];
            if (gameBoardType == GameBoardType.Solution)
            {
                sudokuElements = solutionGameBoard;
            }
            else if (gameBoardType == GameBoardType.Game)
            {
                sudokuElements = gameBoard;
            }
            /////////////////////////////

            for (int i = SectorIndex(row); i < SectorIndex(row) + 3; ++i)
            {
                for (int j = SectorIndex(column); j < SectorIndex(column) + 3; ++j)
                {
                    if (sudokuElements[i, j] == number)
                    {
                        return false;
                    }
                }
            }

            for (int i = 0; i < 9; ++i)
            {
                if (sudokuElements[i, column] == number || sudokuElements[row, i] == number)
                {
                    return false;
                }
            }

            return true;
        }


        public int[,] Solution()
        {
            return solutionGameBoard;
        }

        public int[,] GetGameBoard()
        {
            return gameBoard;
        }

    }
}
