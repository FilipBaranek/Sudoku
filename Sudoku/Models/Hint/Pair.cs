using Sudoku.Models.GameElements;

namespace Sudoku.Models.Hint
{
    public class Pair
    {
        public int Row { get; private set; }
        public int Column { get; private set; }
    
        public Pair(int row, int column)
        {
            Row = row;
            Column = column;
        }
    
    }
}
