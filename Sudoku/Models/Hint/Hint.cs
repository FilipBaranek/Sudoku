using Sudoku.Models.GameElements;
using System.Collections.ObjectModel;

namespace Sudoku.Models.Hint
{
    public abstract class Hint
    {
        protected const int GAMEBOARD_SIZE = 9;
        protected List<int>[,] _gameBoard;
        protected List<Pair> _usedHints;
        protected ObservableCollection<SudokuTrainingCell> _gameCells;
        public string Name { get; private set; }
        public int UsedHints { get => _usedHints.Count; }

        public Hint(string name, List<int>[,] gameBoard, ObservableCollection<SudokuTrainingCell> gameCells)
        {
            Name = name;
            _gameBoard = gameBoard;
            _gameCells = gameCells;
            _usedHints = new List<Pair>();
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

        protected void MarkCells(int row, int column)
        {
            foreach (var cell in _gameCells)
            {
                if (cell.Row == row && cell.Column == column)
                {
                    cell.SetHintBackground();
                }
            }
        }

        public abstract string Message();

        public abstract string? GetHint();
    }
}
