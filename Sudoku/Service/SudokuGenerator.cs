using System.Collections.ObjectModel;
using System.Windows;
using Sudoku.Commands;
using Sudoku.Model;

namespace Sudoku.Service
{
    public static class SudokuGenerator
    {
        private const int ROW_COUNT = 9;
        private const int COLUMN_COUNT = 9;

        private static Thickness Borders(int row, int column)
        {
            row = row % 3;
            column = column % 3;

            int left, top, right, bottom;

            top = row == 0 ? 3 : 1;
            bottom = row == 2 ? 3 : 1;
            left = column == 0 ? 3 : 1;
            right = column == 2 ? 3 : 1;

            return new Thickness(left, top, right, bottom);
        }

        public static ObservableCollection<SudokuCell> GenerateCells(Action<SudokuCell> command, int[,] sudokuElements)
        {
            var sudokuGameBoard = new ObservableCollection<SudokuCell>();

            for (int i = 0; i < ROW_COUNT; ++i)
            {
                for (int j  = 0; j < COLUMN_COUNT; ++j)
                {
                    sudokuGameBoard.Add(new SudokuCell(i, j, sudokuElements[i, j].ToString(), Borders(i, j), new RelayCommand<SudokuCell>(command)));
                }
            }

            return sudokuGameBoard;
        }
    }
}
