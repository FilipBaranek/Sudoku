using System.Windows.Media;
using Sudoku.WPF.Commands;
using Sudoku.WPF.Models;
using Sudoku.WPF.Models.Templates;

namespace Sudoku.WPF.Services.ContentHandlers
{
    public class GameEndContentHandler : ContentHandler
    {
        private Theme _theme;
        public GameEndContentHandler()
        {
            _theme = new Theme();
        }

        public EndMessageTemplate CreateEndMessage(bool win)
        {
            string message = win ? "You won" : "You lose";
            var messageColor = win ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red);

            return new EndMessageTemplate(message, messageColor);
        }

        public override ButtonTemplate[] CreateButtons(Dictionary<string, Action> commands)
        {
            var button = new ButtonTemplate[1];

            button[0] = new ButtonTemplate(_theme.ButtonsColor(), new RelayCommand(commands.ElementAt(0).Value), false);

            return button;
        }
    }
}
