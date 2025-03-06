using System.Windows.Input;
using Sudoku.WPF.Models;
using Sudoku.WPF.Commands;
using Sudoku.WPF.Interfaces;
using Sudoku.WPF.Services;
using Sudoku.WPF.Views;
using System.Windows.Media;
using System.ComponentModel;

namespace Sudoku.WPF.ViewModels
{
    public class DifficultyViewModel : INotifyPropertyChanged, ISetTheme
    {
        private const int GAMETYPE_GAME = 0;
        private const int GAMETYPE_TRAINING = 1;
        private readonly Router _router;
        private readonly int _gameType;
        private Difficulty _difficulty;
        private ImageSource _imageSource;
        public ICommand EasyCommand { get; }
        public ICommand MediumCommand { get; }
        public ICommand HardCommand { get; }
        public ImageSource Background
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                OnPropertyChanged(nameof(Background));
            }
        }
        public DifficultyViewModel(Router router, int gameType)
        {
            _router = router;
            _gameType = gameType;

            EasyCommand = new RelayCommand(() => SelectDifficulty(Difficulty.Easy));
            MediumCommand = new RelayCommand(() => SelectDifficulty(Difficulty.Medium));
            HardCommand = new RelayCommand(() => SelectDifficulty(Difficulty.Hard));

            SetTheme();
        }

        private void SelectDifficulty(Difficulty difficulty)
        {
            _difficulty = difficulty;
            
            StartGame();
        }

        private void StartGame()
        {
            switch (_gameType)
            {
                case GAMETYPE_GAME:
                    _router.NavigateTo(new GameView(_router, _difficulty));
                    break;
                case GAMETYPE_TRAINING:
                    _router.NavigateTo(new TrainingView(_router, _difficulty));
                    break;
                default:
                    _router.NavigateTo(new MenuViewModel(_router));
                    break;
            }
        }

        public void SetTheme()
        {
            Theme themeHandler = new Theme();
            Background = themeHandler.Background();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
