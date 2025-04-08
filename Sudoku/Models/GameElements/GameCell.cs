using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Sudoku.Models.GameElements
{
    public class GameCell : Cell, INotifyPropertyChanged
    {
        private const int DEFAULT_FONTSIZE = 35;
        private const int CANDIDATE_FONTSIZE = 10;
        private Brush _defaultForeground;
        private Brush _candidateForeground;

        private Brush _background;
        public Brush Background
        {
            get => _background;
            set
            {
                _background = value;
                OnPropertyChanged(nameof(Background));
            }
        }

        private string _content;
        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged(nameof(Content));
            }
        }

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

        public event PropertyChangedEventHandler? PropertyChanged;
        public Brush DefaultBackground { get; private set; }
        public Brush WrongMoveBackground { get; private set; }
        public Thickness BorderThickness { get; private set; }
        public ICommand Command { get; private set; }
        public ICommand RightClickCommand { get; private set; }
        public GameCell Parameter { get; private set; }

        public GameCell(int row, int column, string content, Thickness bordrThickness, Brush background, Brush foreground, Brush candidateForeground, ICommand command, ICommand rightClickCommand) :
        base(row, column)
        {
            _content = content;
            _background = background;
            _foreground = foreground;
            _defaultForeground = foreground;
            _candidateForeground = candidateForeground;
            Alignment = VerticalAlignment.Center;
            FontSize = DEFAULT_FONTSIZE;
            DefaultBackground = background;
            WrongMoveBackground = new SolidColorBrush(Colors.Red);
            BorderThickness = bordrThickness;
            Command = command;
            RightClickCommand = rightClickCommand;
            Parameter = this;
        }

        public void SetCandidateFontSize()
        {
            FontSize = CANDIDATE_FONTSIZE;
        }

        public void SetDefaultFontSize()
        {
            FontSize = DEFAULT_FONTSIZE;
        }

        public void SetCandidateForeground()
        {
            Foreground = _candidateForeground;
        }

        public void SetDefaultForeground()
        {
            Foreground = _defaultForeground;
        }

        public void SetCandidateAlignment()
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

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
