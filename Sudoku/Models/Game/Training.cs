using Sudoku.Models.GameElements;

namespace Sudoku.Models.GameLib
{
    public class Training : Game
    {
        private Dictionary<int, int> _placedNumbersCount;
        
        public Training(Difficulty difficulty) : base(difficulty)
        {
            _placedNumbersCount = new Dictionary<int, int>();
            LoadGeneratedNumbersCount();
        }

        public Training(int[,] solutionGameBoard, int[,] sudokuGameBoard, int correct) : base(solutionGameBoard, sudokuGameBoard, correct)
        {
            _placedNumbersCount = new Dictionary<int, int>();
            LoadGeneratedNumbersCount();
        }

        public override void PlaceNumber(GameCell cell)
        {
            if (SelectedNumber == 0 || _sudokuGameBoard[cell.Row, cell.Column] != 0)
            {
                return;
            }
            else if (_solutionGameBoard[cell.Row, cell.Column] == SelectedNumber)
            {
                _sudokuGameBoard[cell.Row, cell.Column] = SelectedNumber;
                ++_placedNumbersCount[SelectedNumber];
                ++_correct;

                _isUpdateNeeded = true;

                if (_correct == TOTAL_CORRECT)
                {
                    Win = true;
                }
            }
            else
            {
                _isWrongMove = true;
            }
        }

        private void LoadGeneratedNumbersCount()
        {
            for (int i = 1; i <= 9; ++i)
            {
                _placedNumbersCount.Add(i, 0);
            }

            for (int i = 0; i < GAME_BOARD_SIZE; ++i)
            {
                for (int j = 0; j < GAME_BOARD_SIZE; ++j)
                {
                    if (_sudokuGameBoard[i, j] != 0)
                    {
                        ++_placedNumbersCount[_sudokuGameBoard[i, j]];
                    }
                }
            }
        }

        public bool IsMarkedNumber(int row, int column)
        {
            return SelectedNumber != 0 && _sudokuGameBoard[row, column] == SelectedNumber;
        }

        public bool IsFullyFilled(int number)
        {
            if (_placedNumbersCount[number] == 9)
            {
                return true;
            }

            return false;
        }

        public void SwitchCandidatesGameBoard()
        {
            ActualCandidates = ActualCandidates == _automaticCandidates ? _userCandidates : _automaticCandidates;
        }

        public void UpdateRowAndColumn(int row, int column)
        {
            for (int i = 0; i < 9; ++i)
            {
                if (ActualCandidates[row, i].Contains(SelectedNumber))
                {
                    ActualCandidates[row, i].Remove(SelectedNumber);
                }

                if (ActualCandidates[i, column].Contains(SelectedNumber))
                {
                    ActualCandidates[i, column].Remove(SelectedNumber);
                }
            }
        }

        public void UpdateSectorCandidates(int row, int column)
        {
            int sectorRow = _gameBoard.SectorIndex(row);
            int sectorColumn = _gameBoard.SectorIndex(column);

            for (int i = sectorRow; i < sectorRow + 3; ++i)
            {
                for (int j = sectorColumn; j < sectorColumn + 3; ++j)
                {
                    if (ActualCandidates[i, j].Contains(SelectedNumber))
                    {
                        ActualCandidates[i, j].Remove(SelectedNumber);
                    }
                }
            }
        }

    }
}
