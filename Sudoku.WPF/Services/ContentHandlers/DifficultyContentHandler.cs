using System.Windows.Media;
using Sudoku.WPF.Commands;
using Sudoku.WPF.Models;

namespace Sudoku.WPF.Services.ContentHandlers
{
    public class DifficultyContentHandler : ContentHandler
    {
        private readonly int BUTTONS_COUNT = 3;
        public override ButtonTemplate[] CreateButtons(Dictionary<string, Action> commands)
        {
            var colors = new Color[] { Colors.LightGreen, Colors.Orange, Colors.Red };
            var buttons = new ButtonTemplate[BUTTONS_COUNT];

            for (int i = 0; i < BUTTONS_COUNT; ++i)
            {
                buttons[i] = new ButtonTemplate(new SolidColorBrush(colors[i]), new RelayCommand(commands.ElementAt(i).Value),
                                            false, commands.ElementAt(i).Key);
            }

            return buttons;
        }
    }
}
