using System.Collections.ObjectModel;
using System.Windows;
using Sudoku.Commands;
using Sudoku.Models.GameElements;

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

        public static ObservableCollection<GameCell> GenerateCells(Action<GameCell> command, int[,] sudokuElements)
        {
            var sudokuGameBoard = new ObservableCollection<GameCell>();

            for (int i = 0; i < ROW_COUNT; ++i)
            {
                for (int j  = 0; j < COLUMN_COUNT; ++j)
                {
                    string content = sudokuElements[i, j] == 0 ? "" : sudokuElements[i, j].ToString();

                    sudokuGameBoard.Add(new GameCell(i, j, content, Borders(i, j), ThemeManager.GameButtonColor(), new RelayCommand<GameCell>(command)));
                }
            }

            return sudokuGameBoard;
        }

        public static ObservableCollection<SudokuTrainingCell> GenerateTrainingCells(Action<SudokuTrainingCell> command, Action<SudokuTrainingCell> rightClickCommand, int[,] trainingElements)
        {
            var trainingGameBoard = new ObservableCollection<SudokuTrainingCell>();

            for (int i = 0; i < ROW_COUNT; ++i)
            {
                for (int j = 0; j < COLUMN_COUNT; ++j)
                {
                    string content = trainingElements[i, j] == 0 ? "" : trainingElements[i, j].ToString();

                    trainingGameBoard.Add(new SudokuTrainingCell(i, j, content, Borders(i, j), ThemeManager.GameButtonColor(),
                                                                ThemeManager.GameButtonTextColor(), new RelayCommand<SudokuTrainingCell>(command),
                                                                new RelayCommand<SudokuTrainingCell>(rightClickCommand)));
                }
            }

            return trainingGameBoard;
        }

        public static ObservableCollection<SudokuPivot> GeneratePivotCells(Action<int> command)
        {
            var pivotElements = new ObservableCollection<SudokuPivot>();

            for (int i = 1; i <= ROW_COUNT; ++i)
            {
                pivotElements.Add(new SudokuPivot(i, i.ToString(), new RelayCommand<int>(command)));
            }

            return pivotElements;
        }

    }
}
