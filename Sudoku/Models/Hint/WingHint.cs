using Sudoku.Models.GameElements;
using System.Collections.ObjectModel;

namespace Sudoku.Models.Hint
{
    public class WingHint : Hint
    {
        public WingHint(string name, List<int>[,] gameboard, ObservableCollection<SudokuTrainingCell> gameCells, bool isIndependend = true) : base(name, gameboard, gameCells) { }

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
