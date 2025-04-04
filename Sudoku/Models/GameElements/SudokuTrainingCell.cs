using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Sudoku.Models.GameElements
{
    public class SudokuTrainingCell : GameCell, INotifyPropertyChanged
    {
        private const int DEFAULT_FONTSIZE = 35;
        private const int HINT_FONTSIZE = 10;
        private Brush _defaultForeground;
        private Brush _hintForeground;
        private Brush _crosshairBackground;

        private int _fontsize;
        public int FontSize
        {
            get => _fontsize;
            set
            {
                _fontsize = value;
                OnPropertyChanged(nameof(FontSize));
            }
        }

        private Brush _foreground;
        public Brush Foreground
        {
            get => _foreground;
            set
            {
                _foreground = value;
                OnPropertyChanged(nameof(Foreground));
            }
        }

        private VerticalAlignment _alignment;
        public VerticalAlignment Alignment
        {
            get => _alignment;
            set
            {
                _alignment = value;
                OnPropertyChanged(nameof(Alignment));
            }
        }

        public ICommand RightClickCommand { get; private set; }
        public ICommand MouseOverCommand { get; private set; }

        public SudokuTrainingCell(int row, int column, string content, Thickness bordrThickness,
                                  Brush background, Brush foreground, Brush hintForeground, Brush crosshairBackground,
                                  ICommand command, ICommand rightClickCommand, ICommand mouseOverCommand) :
        base(row, column, content, bordrThickness, background, command)
        {
            RightClickCommand = rightClickCommand;
            MouseOverCommand = mouseOverCommand;
            Alignment = VerticalAlignment.Center;
            FontSize = DEFAULT_FONTSIZE;
            _foreground = foreground;
            _defaultForeground = foreground;
            _hintForeground = hintForeground;
            _crosshairBackground = crosshairBackground;
        }

        public void SetHintFontSize()
        {
            FontSize = HINT_FONTSIZE;
        }

        public void SetDefaultFontSize()
        {
            FontSize = DEFAULT_FONTSIZE;
        }

        public void SetHintForeground()
        {
            Foreground = _hintForeground;
        }

        public void SetDefaultForeground()
        {
            Foreground = _defaultForeground;
        }

        public void SetHintAlignment()
        {
            Alignment = VerticalAlignment.Bottom;
        }

        public void SetDefaultAlignment()
        {
            Alignment = VerticalAlignment.Center;
        }

        public void SetDefaultBackground()
        {
            Background = DefaultBackground;
        }

        public void SetSelectedNumberBackground()
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
