using Sudoku.Models.GameElements;

namespace Sudoku.Models.Tools
{
    public class Crosshair
    {
        private bool _isToggled;
        private List<Cell> _cellsInCrossHair;

        public Crosshair()
        {
            _cellsInCrossHair = new List<Cell>();
        }

        public void MarkCrossHairCells(SudokuTrainingCell cell)
        {
            if (_isToggled)
            {
                _cellsInCrossHair.Clear();

                int blockRowStart = (cell.Row / 3) * 3;
                int blockColumnStart = (cell.Column / 3) * 3;

                for (int i = 0; i < 9; ++i)
                {
                    _cellsInCrossHair.Add(new Cell(cell.Row, i));
                    _cellsInCrossHair.Add(new Cell(i, cell.Column));
                }

                for (int i = 0; i < 3; ++i)
                {
                    for (int j = 0; j < 3; ++j)
                    {
                        _cellsInCrossHair.Add(new Cell(blockRowStart + i, blockColumnStart + j));
                    }
                }
            }
        }

        public bool IsMarkedAsCrosshair(SudokuTrainingCell cell)
        {
            foreach (var crosshairCell in _cellsInCrossHair)
            {
                if (cell.Row == crosshairCell.Row && cell.Column == crosshairCell.Column)
                {
                    return true;
                }
            }

            return false;
        }

        public void ToggleCrosshair()
        {
            _isToggled = !_isToggled;
        }

        public void ClearCrossHair()
        {
            _cellsInCrossHair.Clear();
        }

    }
}
