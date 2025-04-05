using Sudoku.Models.GameElements;

namespace Sudoku.Models.Hint
{
    public class OptimalHint : Hint
    {
        public Hint PairHints;
        public Hint WingHints;

        public OptimalHint(string name, List<int>[,] gameboard) : base(name, gameboard)
        {
            PairHints = new PairHint(name, gameboard, MarkedHint);
            WingHints = new WingHint(name, gameboard, MarkedHint);
        }

        private bool TryFindSingleCandidate()
        {
            for (int i = 0; i < GAMEBOARD_SIZE; ++i)
            {
                for (int j = 0; j < GAMEBOARD_SIZE; ++j)
                {
                    if (_gameBoard[i,j].Count == 1 && IsNewHint(i, j))
                    {
                        var hints = new List<Cell>
                        {
                            new Cell(i, j)
                        };

                        UpdateHints(hints);
                        
                        return true;
                    }
                }
            }

            return false;
        }

        public override string Message()
        {
            return "This cell has naked single candidate";
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

                hint = PairHints.GetHint();
                if (hint != null)
                {
                    return hint;
                }

                hint = WingHints.GetHint();
                if (hint != null)
                {
                    return hint;
                }

                if (_usedHints.Count == 0 && PairHints.UsedHints.Count == 0 && WingHints.UsedHints.Count == 0)
                {
                    break;
                }

                _usedHints.Clear();
                PairHints.UsedHints.Clear();
                WingHints.UsedHints.Clear();
            }

            return null;
        }
    }
}
