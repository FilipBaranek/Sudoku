using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Sudoku.Service.Config;

namespace Sudoku.Models.GameElements
{
    public class SudokuTrainingCell : SudokuCell, INotifyPropertyChanged
    {
        private const int DEFAULT_FONTSIZE = 35;
        private const int HINT_FONTSIZE = 10;
        private Brush _defaultForeground;
        private Brush _hintForeground;

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
        public SudokuTrainingCell RightClickParameter { get; private set; }

        public SudokuTrainingCell(int row, int column, string content, Thickness bordrThickness, Brush background, Brush foreground, ICommand command, ICommand rightClickCommand) :
                base(row, column, content, bordrThickness, background, command)
        {
            ConfigHandler configHandler = new ConfigHandler();

            RightClickCommand = rightClickCommand;
            RightClickParameter = this;
            Alignment = VerticalAlignment.Center;
            FontSize = DEFAULT_FONTSIZE;
            Foreground = foreground;
            _defaultForeground = foreground;
            _hintForeground = configHandler.Theme().Equals("dark") ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Gray);
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

        public void SetHintBackground()
        {
            Background = new SolidColorBrush(Colors.Turquoise);
        }

    }
}
