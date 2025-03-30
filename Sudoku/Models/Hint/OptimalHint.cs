using System.Collections.ObjectModel;
using Sudoku.Models.GameElements;

namespace Sudoku.Models.Hint
{
    public class OptimalHint : Hint
    {
        private Hint _pairHint;
        private Hint _wingHint;

        public OptimalHint(string name, List<int>[,] gameboard, ObservableCollection<SudokuTrainingCell> gameCells) : base(name, gameboard, gameCells)
        {
            _pairHint = new PairHint(name, gameboard, gameCells, false);
            _wingHint = new WingHint(name, gameboard, gameCells, false);
        }

        private bool TryFindSingleCandidate()
        {
            for (int i = 0; i < GAMEBOARD_SIZE; ++i)
            {
                for (int j = 0; j < GAMEBOARD_SIZE; ++j)
                {
                    if (_gameBoard[i,j].Count == 1 && IsNewHint(i, j))
                    {
                        MarkCells(i, j);

                        _usedHints.Add(new Pair(i, j));

                        return true;
                    }
                }
            }

            return false;
        }

        public override string Message()
        {
            return "This cell has only single candidate";
        }

        public override string? GetHint()
        {
            while (true)
            {
                if (TryFindSingleCandidate())
                {
                    return Message();
                }

                string? hint = null;

                hint = _pairHint.GetHint();
                if (hint != null)
                {
                    return hint;
                }

                hint = _wingHint.GetHint();
                if (hint != null)
                {
                    return hint;
                }

                if (_usedHints.Count == 0 && _pairHint.UsedHints == 0 && _wingHint.UsedHints == 0)
                {
                    break;
                }

                _usedHints.Clear();
            }

            return null;
        }
    }
}
