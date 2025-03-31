using Sudoku.Models.GameElements;

namespace Sudoku.Models.Hint
{
    public class OptimalHint : Hint
    {
        private Hint _pairHint;
        private Hint _wingHint;

        public OptimalHint(string name, List<int>[,] gameboard) : base(name, gameboard)
        {
            _pairHint = new PairHint(name, gameboard, false);
            _wingHint = new WingHint(name, gameboard, false);
        }

        private bool TryFindSingleCandidate()
        {
            for (int i = 0; i < GAMEBOARD_SIZE; ++i)
            {
                for (int j = 0; j < GAMEBOARD_SIZE; ++j)
                {
                    if (_gameBoard[i,j].Count == 1 && IsNewHint(i, j))
                    {
                        var cell = new Cell(i, j);

                        MarkedHint.Clear();
                        MarkedHint.Add(cell);
                        _usedHints.Add(cell);

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

                if (_usedHints.Count == 0 && _pairHint.UsedHints.Count == 0 && _wingHint.UsedHints.Count == 0)
                {
                    break;
                }

                _usedHints.Clear();
                _pairHint.UsedHints.Clear();
                _wingHint.UsedHints.Clear();
            }

            return null;
        }
    }
}
