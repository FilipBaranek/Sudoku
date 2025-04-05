using Sudoku.Models.GameElements;

namespace Sudoku.Models.Hint
{
    public abstract class Hint
    {
        protected const int GAMEBOARD_SIZE = 9;
        protected List<int>[,] _gameBoard;
        protected List<Cell> _usedHints;

        public string Name { get; private set; }
        public List<Cell> UsedHints { get => _usedHints; }
        public List<Cell> MarkedHint { get; set; }

        public Hint(string name, List<int>[,] gameBoard)
        {
            Name = name;
            _gameBoard = gameBoard;
            _usedHints = new List<Cell>();
            MarkedHint = new List<Cell>();
        }

        protected bool IsNewHint(int row, int column)
        {
            foreach (var pair in _usedHints)
            {
                if (pair.Row == row && pair.Column == column)
                {
                    return false;
                }
            }

            return true;
        }

        protected void UpdateHints(List<Cell> hintCells)
        {
            MarkedHint.Clear();

            foreach (Cell hintCell in hintCells)
            {
                MarkedHint.Add(hintCell);
                _usedHints.Add(hintCell);
            }
        }

        public void ClearPotentialHint(int row, int column)
        {
            _gameBoard[row, column].Clear();
        }

        public void ChangeCandidates(List<int>[,] newGameBoard)
        {
            _gameBoard = newGameBoard;
        }

        public abstract string Message();

        public abstract string? GetHint();
    }
}
