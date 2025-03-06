using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Sudoku.WPF.Commands;
using Sudoku.WPF.Interfaces;
using Sudoku.WPF.Models;
using Sudoku.WPF.Services;
using Sudoku.WPF.Views;

namespace Sudoku.WPF.ViewModels
{
    public class MenuViewModel : INotifyPropertyChanged, ISetTheme
    {
        private readonly int GAMETYPE_GAME = 0;
        private readonly int GAMETYPE_TRAINING = 1;
        private readonly Router _router;
        private Brush _buttonsBackground;
        private ImageSource _imageSource;
        public ICommand PlayCommand { get; }
        public ICommand TrainingCommand { get; }
        public ICommand RulesCommand { get; }
        public ICommand OptionsCommand { get; }
        public ICommand QuitCommand { get; }
        public Brush ButtonsBackground
        {
            get => _buttonsBackground;
            set
            {
                _buttonsBackground = value;
                OnPropertyChanged(nameof(ButtonsBackground));
            }
        }
        public ImageSource Background
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                OnPropertyChanged(nameof(Background));
            }
        }

        public MenuViewModel(Router router)
        {
            _router = router;

            PlayCommand = new RelayCommand(Play);
            TrainingCommand = new RelayCommand(Training);
            RulesCommand = new RelayCommand(Rules);
            OptionsCommand = new RelayCommand(Options);
            QuitCommand = new RelayCommand(Quit);

            SetTheme();
        }

        private void Play()
        {
            _router.NavigateTo(new DifficultyView(_router, GAMETYPE_GAME));
        }

        private void Training()
        {
            _router.NavigateTo(new DifficultyView(_router, GAMETYPE_TRAINING));
        }

        private void Rules()
        {
            //TODO
        }

        private void Options()
        {
            //TODO
        }

        private void Quit()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        public void SetTheme()
        {
            Theme themeHandler = new Theme();
            Background = themeHandler.Background();
            ButtonsBackground = themeHandler.ButtonsColor();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
