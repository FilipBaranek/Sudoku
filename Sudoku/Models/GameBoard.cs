namespace Sudoku.Models
{
    public class GameBoard
    {
        private int[,] _gameBoard;
        private int[,] _solutionGameBoard;
        private List<int>[,] _trainingGameBoard;
        private Random _random;

        public GameBoard(Difficulty difficulty)
        {
            _random = new Random();
            _solutionGameBoard = new int[9, 9];

            GenerateSolutionGameBoard();
            GenerateGameBoard(difficulty);
        }

        public int SectorIndex(int index)
        {
            return (index / 3) * 3;
        }

        public bool DeadEnd(string usedOptions)
        {
            for (int i = 1; i <= 9; ++i)
            {
                if (!usedOptions.Contains(i.ToString()))
                {
                    return false;
                }
            }

            return true;
        }

        public void GenerateGameBoard(Difficulty difficulty)
        {
            _gameBoard = (int[,])_solutionGameBoard.Clone();

            int difficultyLayout = 81 - (int)difficulty;

            while (difficultyLayout != 0)
            {
                int randRowIndex = _random.Next(0, 9);
                int randColumnIndex = _random.Next(0, 9);

                while (_gameBoard[randRowIndex, randColumnIndex] == 0)
                {
                    randRowIndex = _random.Next(0, 9);
                    randColumnIndex = _random.Next(0, 9);
                }

                _gameBoard[randRowIndex, randColumnIndex] = 0;

                --difficultyLayout;
            }
        }

        public bool GenerateSolutionGameBoard()
        {
            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    if (_solutionGameBoard[i, j] == 0)
                    {
                        int randNumber;
                        string usedOptions = "";

                        while (true)
                        {
                            randNumber = _random.Next(1, 10);

                            if (CheckPossibility(i, j, randNumber, _solutionGameBoard))
                            {
                                _solutionGameBoard[i, j] = randNumber;

                                if (GenerateSolutionGameBoard())
                                {
                                    return true;
                                }

                                _solutionGameBoard[i, j] = 0;
                            }

                            usedOptions += randNumber.ToString();
                            if (DeadEnd(usedOptions))
                            {
                                break;
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        public void GenerateAvalaibleElements()
        {
            _trainingGameBoard = new List<int>[9, 9];

            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    _trainingGameBoard[i, j] = new List<int>();
                    if (_gameBoard[i, j] == 0)
                    {
                        for (int k = 1; k <= 9; ++k)
                        {
                            if (CheckPossibility(i, j, k, _gameBoard))
                            {
                                _trainingGameBoard[i, j].Add(k);
                            }
                        }
                    }
                }
            }
        }

        public bool CheckPossibility(int row, int column, int number, int[,] sudokuElements)
        {
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

        public int InitialTime(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    return 220;
                case Difficulty.Medium:
                    return 160;
                default:
                    return 100;
            }
        }

        public int[,] Solution()
        {
            return _solutionGameBoard;
        }

        public int[,] GetGameBoard()
        {
            return _gameBoard;
        }

        public List<int>[,] GetTrainingGameBoard()
        {
            return _trainingGameBoard;
        }
    }
}