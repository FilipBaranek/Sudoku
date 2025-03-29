namespace Sudoku.Models.Hint
{
    public abstract class Hint
    {
        protected const int ROWS = 9;
        protected const int COLUMNS = 9;
        protected List<int>[,] _gameBoard;
        public string Name { get; private set; }

        public Hint(string name, List<int>[,] gameBoard)
        {
            Name = name;
            _gameBoard = gameBoard;
        }

        public abstract string Message();

        public abstract string? GetHint(ref int? row, ref int? column);
    }
}
