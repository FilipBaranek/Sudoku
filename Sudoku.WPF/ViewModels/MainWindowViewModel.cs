using System.ComponentModel;
using Sudoku.WPF.Models;
using Sudoku.WPF.Views;

namespace Sudoku.WPF.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private const int GAMETYPE_GAME = 0;
        private const int GAMETYPE_TRAINING = 1;
        private object _currentView;
        public int GameType { get; set; }
        public object CurrentView 
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }
        public MainWindowViewModel()
        {
            GameType = -1;
            RedirectToMenu();
        }

        public void RedirectToMenu() => CurrentView = new MenuView(this);
        public void RedirectToDifficulty() => CurrentView = new DifficultyView(RedirectToGame);
        public void RedirectToGame(Difficulty difficulty)
        {
            switch (GameType)
            {
                case GAMETYPE_GAME:
                    CurrentView = new GameView(this, difficulty);
                    break;
                case GAMETYPE_TRAINING:
                    CurrentView = new TrainingView(this, difficulty);
                    break;
                default:
                    RedirectToMenu();
                    break;
            }

            GameType = -1;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
