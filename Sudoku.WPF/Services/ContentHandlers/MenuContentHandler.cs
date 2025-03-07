using System.Windows.Media;
using Sudoku.WPF.Commands;
using Sudoku.WPF.Models;

namespace Sudoku.WPF.Services.ContentHandlers
{
    public class MenuContentHandler : ContentHandler
    {
        private readonly int BUTTONS_COUNT = 5;
        private Theme _themeHandler;
        public MenuContentHandler()
        {
            _themeHandler = new Theme();
        }
        public override ButtonTemplate[] CreateButtons(Dictionary<string, Action> commands)
        {
            var buttons = new ButtonTemplate[BUTTONS_COUNT];
            Brush buttonColor = _themeHandler.ButtonsColor();

            for (int i = 0; i < BUTTONS_COUNT; ++i)
            {
                buttons[i] = (new ButtonTemplate(buttonColor, new RelayCommand(commands.ElementAt(i).Value), false, commands.ElementAt(i).Key));
            }

            return buttons;
        }
    }
}
