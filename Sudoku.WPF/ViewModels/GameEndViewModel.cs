using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using Sudoku.WPF.Commands;
using Sudoku.WPF.Models;
using Sudoku.WPF.Services;
using Sudoku.WPF.Views;

namespace Sudoku.WPF.ViewModels
{
    public class GameEndViewModel : INotifyPropertyChanged
    {
        private Router _router;
        private string _gameEndMessage;
        private Brush _messageColor;
        private Brush _buttonColor;
        private ImageSource _background;
        private ICommand _buttonCommand;
        public string EndGameMessage
        {
            get => _gameEndMessage;
            set
            {
                _gameEndMessage = value;
                OnPropertyChanged(nameof(EndGameMessage));
            }
        }
        public Brush MessageColor
        {
            get => _messageColor;
            set
            {
                _messageColor = value;
                OnPropertyChanged(nameof(MessageColor));
            }
        }
        public Brush ButtonColor
        {
            get => _buttonColor;
            set
            {
                _buttonColor = value;
                OnPropertyChanged(nameof(ButtonColor));
            }
        }
        public ImageSource Background
        {
            get => _background;
            set
            {
                _background = value;
                OnPropertyChanged(nameof(Background));
            }
        }
        public ICommand ButtonCommand
        {
            get => _buttonCommand;
            set
            {
                _buttonCommand = value;
                OnPropertyChanged(nameof(ButtonCommand));
            }
        }
        public GameEndViewModel(Router router, bool win) 
        {
            _router = router;

            Theme themeHander = new Theme();
            Background = themeHander.Background();
            ButtonColor = themeHander.ButtonsColor();
            ButtonCommand = new RelayCommand(Redirect);

            if (win)
            {
                SetUpPageContent("You won", new SolidColorBrush(Colors.Green));
            }
            else
            {
                SetUpPageContent("You lost", new SolidColorBrush(Colors.Red));
            }
        }

        private void SetUpPageContent(string message, SolidColorBrush messageColor)
        {
            EndGameMessage = message;
            MessageColor = messageColor;
        }

        private void Redirect()
        {
            _router.NavigateTo(new MenuView(_router));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
