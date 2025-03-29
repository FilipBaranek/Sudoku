namespace Sudoku.Models.Hint
{
    public class PairHint : Hint
    {
        public PairHint(string name, List<int>[,] gameboard) : base(name, gameboard) { }

        public override string Message()
        {
            throw new NotImplementedException();
        }

        public override string? GetHint(ref int? row, ref int? column)
        {
            throw new NotImplementedException();
        }

    }
}
