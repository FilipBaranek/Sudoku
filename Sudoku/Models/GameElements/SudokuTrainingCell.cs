using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Sudoku.Models.GameElements
{
    public class SudokuTrainingCell : GameCell, INotifyPropertyChanged
    {
        private Brush _crosshairBackground;

        public ICommand MouseOverCommand { get; private set; }

        public SudokuTrainingCell(int row, int column, string content, Thickness bordrThickness,
                                  Brush background, Brush foreground, Brush candidateForeground, Brush crosshairBackground,
                                  ICommand command, ICommand rightClickCommand, ICommand mouseOverCommand) :
        base(row, column, content, bordrThickness, background, foreground, candidateForeground, command, rightClickCommand)
        {
            MouseOverCommand = mouseOverCommand;
            _crosshairBackground = crosshairBackground;
        }

        public void SetSelectedNumberBackground()
        {
            Background = new SolidColorBrush(Colors.LightGreen);
        }

        public void SetSelectedFilledNumberBackgorund()
        {
            Background = new SolidColorBrush(Colors.Green);
        }

        public void SetHintBackground()
        {
            Background = new SolidColorBrush(Colors.Turquoise);
        }

        public void SetCrosshairBackground()
        {
            Background = _crosshairBackground;
        }

    }
}
