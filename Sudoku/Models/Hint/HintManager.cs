using Sudoku.Commands;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using Sudoku.Models.GameElements;

namespace Sudoku.Models.Hint
{
    public class HintManager : INotifyPropertyChanged
    {
        private bool _toggled;
        private Action _markHintCells;


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
        public List<Hint> HintTypes { get; private set; }
        public List<Cell>? HintCells
        {
            get
            {
                if (_selectedHint != null)
                {
                    return _selectedHint.MarkedHint;
                }
                return null;
            }
        }

        public HintManager(List<int>[,] gameboard, Action markHintCells)
        {
            _message = "";
            _markHintCells = markHintCells;
            MessageVisible = Visibility.Hidden;
            VisibilityTrigger = new RelayCommand(ToggleHintMessage);
            HintTrigger = new RelayCommand(GenerateHint);
            HintTypes = new List<Hint>();

            LoadHints(gameboard);
        }

        private void LoadHints(List<int>[,] gameboard)
        {
            HintTypes.Add(new OptimalHint("Optimal", gameboard));
            HintTypes.Add(new PairHint("Naked / hidden pairs", gameboard));
            HintTypes.Add(new WingHint("Wings", gameboard));
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
                string? message = _selectedHint.GetHint();

                Message = message == null ? "No avalaible hints" : message;
                _markHintCells();

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

        public void ChangeCandidates(List<int>[,] newGameBoard)
        {
            foreach (var hint in HintTypes)
            {
                hint.ChangeCandidates(newGameBoard);
            }
        }

        public void ClearPotentialHint(int row, int column)
        {
            foreach (var hint in HintTypes)
            {
                hint.ClearPotentialHint(row, column);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
