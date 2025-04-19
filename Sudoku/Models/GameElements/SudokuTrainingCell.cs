using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Sudoku.Models.GameElements
{
    public class SudokuTrainingCell : GameCell, INotifyPropertyChanged
    {
        private Brush _crosshairBackground;
        private Brush _selectedNumberBackground;

        public ICommand MouseOverCommand { get; private set; }

        public SudokuTrainingCell(int row, int column, string content, Thickness bordrThickness,
                                  Brush background, Brush foreground, Brush candidateForeground, Brush crosshairBackground,
                                  Brush selectedNumberBackground, ICommand command, ICommand rightClickCommand, ICommand mouseOverCommand) :
        base(row, column, content, bordrThickness, background, foreground, candidateForeground, command, rightClickCommand)
        {
            MouseOverCommand = mouseOverCommand;
            _crosshairBackground = crosshairBackground;
            _selectedNumberBackground = selectedNumberBackground;
        }

        public void SetSelectedNumberBackground()
        {
            Background = _selectedNumberBackground;
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
