namespace Sudoku.Models.Hint
{
    public class WingHint : Hint
    {
        public WingHint(string name, List<int>[,] gameboard) : base(name, gameboard) { }

        public override string Message()
        {
            throw new NotImplementedException();
        }

        public override string? GetHint()
        {
            throw new NotImplementedException();
        }

    }
}
