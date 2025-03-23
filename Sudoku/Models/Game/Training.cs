using System.ComponentModel;
using Sudoku.Models.GameElements;

namespace Sudoku.Models.Game
{
    public class Training : Game
    {
        private bool _isUpdateNeeded;
        private List<int>[,] _trainingElements;

        public Training(Difficulty difficulty) : base(difficulty)
        {
            _trainingElements = _gameBoard.TrainingGameBoard(_sudokuGameBoard);
        }

        public override void PlaceNumber(SudokuCell cell)
        {
            if (cell is not SudokuTrainingCell trainingCell)
            {
                return;
            }

            if (_selectedNumber == 0 || _sudokuGameBoard[trainingCell.Row, trainingCell.Column] != 0)
            {
                return;
            }
            else if (_solutionGameBoard[trainingCell.Row, trainingCell.Column] == _selectedNumber)
            {
                _sudokuGameBoard[trainingCell.Row, trainingCell.Column] = _selectedNumber;
                ++_correct;

                _isUpdateNeeded = true;

                UpdateCell(trainingCell);

                if (_correct == TOTAL_CORRECT)
                {
                    Win = true;
                }
            }
            else
            {
                WrongMove(trainingCell);
            }
        }

        private void UpdateCell(SudokuTrainingCell trainingCell)
        {
            trainingCell.Content = _selectedNumber.ToString();
            trainingCell.Background = trainingCell.DefaultBackground;
            trainingCell.SetDefaultFontSize();
            trainingCell.SetDefaultForeground();
            trainingCell.SetDefaultAlignment();
        }

        public bool HandleCandidate(int row, int column)
        {
            if (_selectedNumber != 0)
            {
                if (_trainingElements[row, column].Contains(_selectedNumber))
                {
                    _trainingElements[row, column].Remove(_selectedNumber);

                    return true;
                }
                else
                {
                    _trainingElements[row, column].Add(_selectedNumber);
                }
            }

            return false;
        }

        public void UpdateRowAndColumn(int row, int column)
        {
            for (int i = 0; i < 9; ++i)
            {
                if (_trainingElements[row, i].Contains(_selectedNumber))
                {
                    _trainingElements[row, i].Remove(_selectedNumber);
                }

                if (_trainingElements[i, column].Contains(_selectedNumber))
                {
                    _trainingElements[i, column].Remove(_selectedNumber);
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
                    if (_trainingElements[i, j].Contains(_selectedNumber))
                    {
                        _trainingElements[i, j].Remove(_selectedNumber);
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
                return _trainingElements[row, column];
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
            return _selectedNumber != 0 && _sudokuGameBoard[row, column] == _selectedNumber;
        }

    }
}
