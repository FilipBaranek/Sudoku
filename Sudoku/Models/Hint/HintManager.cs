using Sudoku.Commands;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using Sudoku.Models.GameElements;

namespace Sudoku.Models.Hint
{
    public class HintManager : INotifyPropertyChanged
    {
        private readonly ObservableCollection<SudokuTrainingCell> _gameCells;
        private bool _toggled;

        private Hint? _selectedHint;
        public Hint? SelectedHint
        {
            get => _selectedHint;
            set
            {
                _selectedHint = value;
                OnPropertyChanged(nameof(SelectedHint));
            }
        }

        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        private Visibility _messageVisible;
        public Visibility MessageVisible
        {
            get => _messageVisible;
            set
            {
                _messageVisible = value;
                OnPropertyChanged(nameof(MessageVisible));
            }
        }

        public ICommand VisibilityTrigger { get; private set; }
        public ICommand HintTrigger { get; private set; }
        public ObservableCollection<Hint> HintTypes { get; private set; }

        public HintManager(List<int>[,] gameboard, ObservableCollection<SudokuTrainingCell> gameCells)
        {
            _gameCells = gameCells;
            _message = "";
            MessageVisible = Visibility.Hidden;
            VisibilityTrigger = new RelayCommand(ToggleHintMessage);
            HintTrigger = new RelayCommand(GenerateHint);
            HintTypes = new ObservableCollection<Hint>();

            LoadHints(gameboard);
        }

        private void LoadHints(List<int>[,] gameboard)
        {
            HintTypes.Add(new OptimalHint("Optimal", gameboard, _gameCells));
            HintTypes.Add(new PairHint("Naked / hidden pairs", gameboard, _gameCells));
            HintTypes.Add(new WingHint("Wings", gameboard, _gameCells));
        }

        private void ClearHints()
        {
            foreach (var cell in _gameCells)
            {
                cell.Background = cell.DefaultBackground;
            }
        }

        private void GenerateHint()
        {
            if (_toggled)
            {
                ToggleHintMessage();
                _toggled = false;
            }
            else if (!_toggled && _selectedHint != null)
            {
                ClearHints();

                string? message = _selectedHint.GetHint();

                Message = message == null ? "No avalaible hints" : message;

                ToggleHintMessage();
                _toggled = true;
            }
            else
            {
                Message = "You need to select hint type in hint menu first";

                ToggleHintMessage();
            }
        }

        private void ToggleHintMessage()
        {
            MessageVisible = MessageVisible == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
