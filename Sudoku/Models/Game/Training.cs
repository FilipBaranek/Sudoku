using Sudoku.Models.GameElements;

namespace Sudoku.Models.Game
{
    public class Training : Game
    {
        private const int GAME_BOARD_SIZE = 9;
        private bool _isUpdateNeeded;
        public List<int>[,] _userCandidates;
        public List<int>[,] _automaticCandidates;

        private List<int>[,] _actualCandidates;
        public List<int>[,] ActualCandidates
        {
            get => _actualCandidates;
            private set => _actualCandidates = value;
        }

        public Training(Difficulty difficulty) : base(difficulty)
        {
            _automaticCandidates = _gameBoard.TrainingGameBoard(_sudokuGameBoard);
            _userCandidates = new List<int>[GAME_BOARD_SIZE, GAME_BOARD_SIZE];
            UserCandidatesInit();

            _actualCandidates = _userCandidates;
        }

        public override void PlaceNumber(GameCell cell)
        {
            if (cell is not SudokuTrainingCell trainingCell)
            {
                return;
            }

            if (SelectedNumber == 0 || _sudokuGameBoard[trainingCell.Row, trainingCell.Column] != 0)
            {
                return;
            }
            else if (_solutionGameBoard[trainingCell.Row, trainingCell.Column] == SelectedNumber)
            {
                _sudokuGameBoard[trainingCell.Row, trainingCell.Column] = SelectedNumber;
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

        private void UserCandidatesInit()
        {
            for (int i = 0; i < GAME_BOARD_SIZE; ++i)
            {
                for (int j = 0; j < GAME_BOARD_SIZE; ++j)
                {
                    _userCandidates[i, j] = new List<int>();
                }
            }
        }

        public void SwitchCandidatesGameBoard()
        {
            ActualCandidates = ActualCandidates == _automaticCandidates ? _userCandidates : _automaticCandidates;
        }

        public bool HandleCandidate(int row, int column)
        {
            if (SelectedNumber != 0)
            {
                if (ActualCandidates[row, column].Contains(SelectedNumber))
                {
                    ActualCandidates[row, column].Remove(SelectedNumber);

                    return true;
                }
                else
                {
                    ActualCandidates[row, column].Add(SelectedNumber);
                }
            }

            return false;
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

        public bool IsCandidateCell(int row, int column)
        {
            return _sudokuGameBoard[row, column] == 0;
        }

        public List<int>? Candidates(int row, int column)
        {
            if (IsCandidateCell(row, column))
            {
                return ActualCandidates[row, column];
            }

            return null;
        }

        public bool IsUpdateNeeded()
        {
            bool needToUpdate = _isUpdateNeeded;
            
            _isUpdateNeeded = false;

            return needToUpdate;
        }

        public bool IsMarkedNumber(int row, int column)
        {
            return SelectedNumber != 0 && _sudokuGameBoard[row, column] == SelectedNumber;
        }

    }
}
