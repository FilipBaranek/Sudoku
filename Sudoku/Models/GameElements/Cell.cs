namespace Sudoku.Models.GameElements
{
    public class Cell
    {
        public int Row { get; private set; }
        public int Column { get; private set; }

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
        }

    }
}
