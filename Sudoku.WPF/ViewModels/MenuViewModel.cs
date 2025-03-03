using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Sudoku.WPF.Commands;
using Sudoku.WPF.Interfaces;
using Sudoku.WPF.Models;

namespace Sudoku.WPF.ViewModels
{
    public class MenuViewModel : ISetTheme
    {
        private readonly int GAMETYPE_GAME = 0;
        private readonly int GAMETYPE_TRAINING = 1;
        private MainWindowViewModel _mainWindowModel;
        public ICommand PlayCommand { get; }
        public ICommand TrainingCommand { get; }
        public ICommand RulesCommand { get; }
        public ICommand OptionsCommand { get; }
        public ICommand QuitCommand { get; }
        
        public MenuViewModel(MainWindowViewModel mainWindowModel, Grid grid)
        {
            _mainWindowModel = mainWindowModel;

            PlayCommand = new RelayCommand(Play);
            TrainingCommand = new RelayCommand(Training);
            RulesCommand = new RelayCommand(Rules);
            OptionsCommand = new RelayCommand(Options);
            QuitCommand = new RelayCommand(Quit);

            SetTheme(grid);
        }

        private void Play()
        {
            _mainWindowModel.GameType = GAMETYPE_GAME;
            _mainWindowModel.RedirectToDifficulty();
        }

        private void Training()
        {
            _mainWindowModel.GameType = GAMETYPE_TRAINING;
            _mainWindowModel.RedirectToDifficulty();
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

        public void SetTheme(Grid grid)
        {
            Theme themeHandler = new Theme();
            themeHandler.SetBackground(grid, grid.RowDefinitions.Count);
            themeHandler.SetButtonsColor(grid);
        }
    }
}
