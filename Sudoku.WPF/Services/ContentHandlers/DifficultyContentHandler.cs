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
            var buttons = new ButtonTemplate[BUTTONS_COUNT];

            buttons[0] = new ButtonTemplate(new SolidColorBrush(Colors.LightGreen), new RelayCommand(commands.ElementAt(0).Value),
                                            false, commands.ElementAt(0).Key);

            buttons[1] = new ButtonTemplate(new SolidColorBrush(Colors.Orange), new RelayCommand(commands.ElementAt(1).Value),
                                            false, commands.ElementAt(1).Key);

            buttons[2] = new ButtonTemplate(new SolidColorBrush(Colors.Red), new RelayCommand(commands.ElementAt(2).Value),
                                            false, commands.ElementAt(2).Key);

            return buttons;
        }
    }
}
