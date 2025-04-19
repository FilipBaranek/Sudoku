namespace Sudoku.Models.GameLib
{
    public class GameBoard
    {
        private const int TOTAL_POINTS = 81;
        private const int GAMEBOARD_SIZE = 9;
        private const int SECTOR_SIZE = 3;
        private Random _random;

        public GameBoard()
        {
            _random = new Random();
        }

        public int SectorIndex(int index)
        {
            return index - index % SECTOR_SIZE;
        }

        private bool CheckPossibility(int row, int column, int number, int[,] sudokuElements)
        {
            for (int i = SectorIndex(row); i < SectorIndex(row) + SECTOR_SIZE; ++i)
            {
                for (int j = SectorIndex(column); j < SectorIndex(column) + SECTOR_SIZE; ++j)
                {
                    if (sudokuElements[i, j] == number)
                    {
                        return false;
                    }
                }
            }

            for (int i = 0; i < GAMEBOARD_SIZE; ++i)
            {
                if (sudokuElements[i, column] == number || sudokuElements[row, i] == number)
                {
                    return false;
                }
            }

            return true;
        }

        private bool GenerateSolutionGameBoard(ref int[,] solutionGameBoard)
        {
            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    if (solutionGameBoard[i, j] == 0)
                    {
                        HashSet<int> usedOptions = new HashSet<int>();
                        int maxAttempts = 100;
                        int attempts = 0;

                        while (usedOptions.Count < 9 && attempts < maxAttempts)
                        {
                            int randNumber = _random.Next(1, 10);
                            ++attempts;

                            if (!usedOptions.Contains(randNumber))
                            {
                                usedOptions.Add(randNumber);

                                if (CheckPossibility(i, j, randNumber, solutionGameBoard))
                                {
                                    solutionGameBoard[i, j] = randNumber;

                                    if (GenerateSolutionGameBoard(ref solutionGameBoard))
                                    {
                                        return true;
                                    }

                                    solutionGameBoard[i, j] = 0;
                                }
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        private void CountSolutions(int[,] gameBoard, ref int solutionCount)
        {
            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    if (gameBoard[i, j] == 0)
                    {
                        for (int number = 1; number <= 9; ++number)
                        {
                            if (CheckPossibility(i, j, number, gameBoard))
                            {
                                gameBoard[i, j] = number;

                                CountSolutions(gameBoard, ref solutionCount);

                                if (solutionCount > 1)
                                {
                                    return;
                                }

                                gameBoard[i, j] = 0;
                            }
                        }
                        return;
                    }
                }
            }

            ++solutionCount;
        }

        private bool HasSingleSolution(int[,] gameBoard)
        {
            int[,] gameBoardCopy = (int[,])gameBoard.Clone();
            int solutionCount = 0;

            CountSolutions(gameBoardCopy, ref solutionCount);

            return solutionCount == 1;
        }

        public void GenerateGameBoard(int[,] gameBoard, int numbersToHide)
        {
            for (int i = 0; i < numbersToHide; ++i)
            {
                int row = _random.Next(0, 9);
                int column = _random.Next(0, 9);

                while (gameBoard[row, column] == 0)
                {
                    row = _random.Next(0, 9);
                    column = _random.Next(0, 9);
                }

                int hiddenValue = gameBoard[row, column];
                gameBoard[row, column] = 0;

                if (!HasSingleSolution(gameBoard))
                {
                    gameBoard[row, column] = hiddenValue;
                    --i;
                }
            }
        }

        public int[,] SolutionGameBoard()
        {
            int[,] solutionGameBoard = new int[GAMEBOARD_SIZE, GAMEBOARD_SIZE];

            GenerateSolutionGameBoard(ref solutionGameBoard);

            return solutionGameBoard;
        }

        public int[,] SudokuGameBoard(int[,] solutionGameBoard, Difficulty difficulty)
        {
            int[,] gameBoard = (int[,])solutionGameBoard.Clone();

            GenerateGameBoard(gameBoard, TOTAL_POINTS - (int)difficulty);

            return gameBoard;
        }

        public List<int>[,] TrainingGameBoard(int[,] gameBoard)
        {
            var trainingElements = new List<int>[GAMEBOARD_SIZE, GAMEBOARD_SIZE];

            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    trainingElements[i, j] = new List<int>();
                    if (gameBoard[i, j] == 0)
                    {
                        for (int k = 1; k <= 9; ++k)
                        {
                            if (CheckPossibility(i, j, k, gameBoard))
                            {
                                trainingElements[i, j].Add(k);
                            }
                        }
                    }
                }
            }

            return trainingElements;
        }

    }
}