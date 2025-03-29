namespace Sudoku.Models.Hint
{
    public class OptimalHint : Hint
    {
        private Hint? _complexHint;

        public OptimalHint(string name, List<int>[,] gameboard) : base(name, gameboard) { }

        private bool TryFindSingleCandidate(ref int? row, ref int? column)
        {
            for (int i = 0; i < ROWS; ++i)
            {
                for (int j = 0; j < COLUMNS; ++j)
                {
                    if (_gameBoard[i,j].Count == 1)
                    {
                        row = i;
                        column = j;

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

        public override string? GetHint(ref int? row, ref int? column)
        {
            if (TryFindSingleCandidate(ref row, ref column))
            {
                return Message();
            }

            string? hint = null;

            _complexHint = new PairHint("Naked / hidden pair", _gameBoard);
            hint = _complexHint.GetHint(ref row, ref column);

            if (hint != null)
            {
                return hint;
            }

            _complexHint = new WingHint("Wings", _gameBoard);
            hint = _complexHint.GetHint(ref row, ref column);

            if (hint != null)
            {
                return hint;
            }

            return "No avalaible hints";
        }
    }
}
