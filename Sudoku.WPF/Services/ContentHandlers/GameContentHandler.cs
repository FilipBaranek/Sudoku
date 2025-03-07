using System.Windows.Media;
using Sudoku.WPF.Models;
using Sudoku.WPF.Commands;
using System.Collections.ObjectModel;
using System.Windows;

namespace Sudoku.WPF.Services.ContentHandlers
{
    public class GameContentHandler : ContentHandler
    {
        private readonly int BUTTONS_COUNT = 2;
        private readonly int GAMEBOARD_BUTTON_ROWS_COUNT = 9;
        private readonly int GAMEBOARD_BUTTON_COLUMNS_COUNT = 9;
        private readonly int PIVOT_BUTTONS_COUNT = 9;
        private Theme _theme;
        public GameContentHandler()
        {
            _theme = new Theme();
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

        public ObservableCollection<ButtonTemplate[]> CreateButtons(int[,] gameBoardElements, Action<ButtonTemplate> command)
        {
            Brush buttonColor = _theme.ButtonsColor();

            var buttons = new ObservableCollection<ButtonTemplate[]>();

            for (int i = 0; i < GAMEBOARD_BUTTON_ROWS_COUNT; ++i)
            {
                var row = new ButtonTemplate[GAMEBOARD_BUTTON_ROWS_COUNT];

                for (int j = 0; j < GAMEBOARD_BUTTON_COLUMNS_COUNT; ++j)
                {
                    string buttonContent = gameBoardElements[i, j] == 0 ? "" : gameBoardElements[i, j].ToString();

                    ButtonTemplate button = new ButtonTemplate(buttonColor, new RelayCommand<ButtonTemplate>(command), true,
                                                               buttonContent, i.ToString() + j.ToString(), Borders(i, j));
                    row[j] = button;
                }

                buttons.Add(row);
            }

            return buttons;
        }

        public ButtonTemplate[] CreateButtons(Action<ButtonTemplate> command)
        {
            Brush buttonColor = _theme.ButtonsColor();

            var buttons = new ButtonTemplate[PIVOT_BUTTONS_COUNT];

            for (int i  = 0; i < PIVOT_BUTTONS_COUNT; ++i)
            {
                buttons[i] = new ButtonTemplate(buttonColor, new RelayCommand<ButtonTemplate>(command), true, (i + 1).ToString());
            }

            return buttons;
        }

        public override ButtonTemplate[] CreateButtons(Dictionary<string, Action> commands)
        {
            Brush buttonColor = _theme.ButtonsColor();

            var buttons = new ButtonTemplate[BUTTONS_COUNT];

            buttons[0] = new ButtonTemplate(buttonColor, new RelayCommand(commands.ElementAt(0).Value), false, commands.ElementAt(0).Key);
            buttons[1] = new ButtonTemplate(buttonColor, new RelayCommand(commands.ElementAt(1).Value), false, commands.ElementAt(1).Key);

            return buttons;
        }
    }
}
