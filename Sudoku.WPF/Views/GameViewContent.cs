using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace Sudoku.WPF.Views
{
    public class GameViewContent
    {
        private StackPanel[] _gameBoardRows;
        private Button[,] _sudokuElementButtons;
        private Grid _grid;
        public GameViewContent(Grid grid)
        {
            _grid = grid;

            DrawGameBoard();
        }

        private Thickness Borders(int row, int column)
        {
            row = row % 3;
            column = column % 3;

            int left, top, right, bottom;

            top = row == 0 ? 6 : 1;
            bottom = row == 2 ? 6 : 1;
            left = column == 0 ? 6 : 1;
            right = column == 2 ? 6 : 1;

            return new Thickness(left, top, right, bottom);
        }

        private void DrawGameBoard()
        {
            _gameBoardRows = new StackPanel[9];
            _sudokuElementButtons = new Button[9, 9];

            for (int i = 0; i < 9; ++i)
            {
                StackPanel newStackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                Grid.SetRow(newStackPanel, i + 1);
                Grid.SetColumn(newStackPanel, 0);

                _gameBoardRows[i] = newStackPanel;
                _grid.Children.Add(newStackPanel);

                for (int j = 0; j < 9; ++j)
                {
                    Button newButton = new Button
                    {
                        Tag = i.ToString() + j.ToString(),
                        Width = 80,
                        Height = 80,
                        FontSize = 20,
                        Background = Brushes.White,
                        BorderThickness = Borders(i, j),
                        BorderBrush = Brushes.Black
                        //HERE ADD COMMAND = ?
                    };

                    _sudokuElementButtons[i, j] = newButton;
                    newStackPanel.Children.Add(newButton);
                }
            }
        }
    }
}
