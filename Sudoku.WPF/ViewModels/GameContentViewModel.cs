using System.Windows.Media;
using System.Windows;
using Sudoku.WPF.Models;
using Sudoku.WPF.Commands;
using System.Collections.ObjectModel;

namespace Sudoku.WPF.ViewModels
{
    public class GameContentViewModel
    {
        private Theme _themeHandler;
        public GameContentViewModel()
        {
            _themeHandler = new Theme();
        }

        public Brush? ButtonsColor()
        {
            return _themeHandler.ButtonsColor();
        }

        public Brush? GameBoardButtonsColor()
        {
            return ((SolidColorBrush)_themeHandler.ButtonsColor()).Color == Colors.Gray ? new SolidColorBrush(Colors.LightGray) : new SolidColorBrush(Colors.White);
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

        public ObservableCollection<GameViewButton[]> DrawGameBoard(int[,] gameBoardElements, Action<GameViewButton> command)
        {
            ObservableCollection<GameViewButton[]> gameButtons = new ObservableCollection<GameViewButton[]>();
            Brush buttonsColor = GameBoardButtonsColor();

            for (int i = 0; i < 9; ++i)
            {
                GameViewButton[] row = new GameViewButton[9];
                for (int j = 0; j < 9; ++j)
                {
                    string buttonContent = gameBoardElements[i, j] == 0 ? "" : gameBoardElements[i, j].ToString();

                    GameViewButton button = new GameViewButton(80, 80, buttonsColor, Borders(i, j), command, buttonContent, i.ToString() + j.ToString());
                    row[j] = button;
                }
                gameButtons.Add(row);
            }

            return gameButtons;
        }

        public GameViewButton[] DrawButtons(Action<GameViewButton> command)
        {
            GameViewButton[] buttons = new GameViewButton[9];
            Brush buttonColor = ButtonsColor();

            for (int i = 0; i < 9; ++i)
            {
                buttons[i] = new GameViewButton(60, 60, buttonColor, new Thickness(4), command, (i + 1).ToString());
            }

            return buttons;
        }
    }
}
