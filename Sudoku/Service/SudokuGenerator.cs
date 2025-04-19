using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
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

            double left, top, right, bottom;

            top = row == 0 ? 2 : 0.5;
            bottom = row == 2 ? 2 : 0.5;
            left = column == 0 ? 2 : 0.5;
            right = column == 2 ? 2 : 0.5;

            return new Thickness(left, top, right, bottom);
        }

        public static ObservableCollection<GameCell> GenerateCells(Action<GameCell> command, Action<GameCell> rightClickCommand, int[,] sudokuElements)
        {
            var sudokuGameBoard = new ObservableCollection<GameCell>();

            var theme = ThemeManager.ThemeType();
            var buttonBackground = ThemeManager.GameButtonColor();
            var leftClick = new RelayCommand<GameCell>(command);
            var rightClick = new RelayCommand<GameCell>(rightClickCommand);
            var foreground = ThemeManager.GameButtonTextColor();
            var candidateForeground = theme.Equals("dark") ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Gray);

            for (int i = 0; i < ROW_COUNT; ++i)
            {
                for (int j  = 0; j < COLUMN_COUNT; ++j)
                {
                    string content = sudokuElements[i, j] == 0 ? "" : sudokuElements[i, j].ToString();
                    
                    sudokuGameBoard.Add(new GameCell(i, j, content, Borders(i, j), buttonBackground, foreground, candidateForeground, leftClick, rightClick));
                }
            }

            return sudokuGameBoard;
        }

        public static ObservableCollection<SudokuTrainingCell> GenerateTrainingCells(Action<SudokuTrainingCell> command, Action<SudokuTrainingCell> rightClickCommand, Action<SudokuTrainingCell> mouseOverCommand, int[,] trainingElements)
        {
            var trainingGameBoard = new ObservableCollection<SudokuTrainingCell>();

            var theme = ThemeManager.ThemeType();
            var background = ThemeManager.GameButtonColor();
            var foreground = ThemeManager.GameButtonTextColor();
            var candidateForeground = theme.Equals("dark") ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Gray);
            var crosshairBackground = theme.Equals("dark") ? new SolidColorBrush(Color.FromRgb(25, 25, 25)) : new SolidColorBrush(Colors.LightGoldenrodYellow);
            var selectedNumberBackground = theme.Equals("dark") ? new SolidColorBrush(Colors.Blue) : new SolidColorBrush(Colors.LightBlue);
            var leftClick = new RelayCommand<SudokuTrainingCell>(command);
            var rightClick = new RelayCommand<SudokuTrainingCell>(rightClickCommand);
            var mouseOver = new RelayCommand<SudokuTrainingCell>(mouseOverCommand);

            for (int i = 0; i < ROW_COUNT; ++i)
            {
                for (int j = 0; j < COLUMN_COUNT; ++j)
                {
                    string content = trainingElements[i, j] == 0 ? "" : trainingElements[i, j].ToString();
                
                    trainingGameBoard.Add(new SudokuTrainingCell(i, j, content, Borders(i, j), background, foreground, candidateForeground, crosshairBackground, selectedNumberBackground, leftClick, rightClick, mouseOver));
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
