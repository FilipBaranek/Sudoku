using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Sudoku.WPF.Commands;
using Sudoku.WPF.Interfaces;
using Sudoku.WPF.Models;

namespace Sudoku.WPF.ViewModels
{
    public class GameViewModel : ISetTheme
    {
        private readonly int GAMETYPE_TRAINING = 1;
        public ICommand BackToMenuCommand { get; }
        public ICommand SwitchToTrainingCommand { get; }
        private MainWindowViewModel _mainWindowModel;
        private Game _game;
        public GameViewModel(MainWindowViewModel mainWindowModel, Grid grid, Difficulty difficulty)
        {
            _mainWindowModel = mainWindowModel;
            _game = new Game(difficulty);

            BackToMenuCommand = new RelayCommand(BackToMenu);
            SwitchToTrainingCommand = new RelayCommand(SwitchToTraining);

            SetTheme(grid);
        }



        private void BackToMenu()
        {
            _mainWindowModel.RedirectToMenu();
        }

        private void SwitchToTraining()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to switch to training mode?", "Switch to training",
                                                        MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _mainWindowModel.GameType = GAMETYPE_TRAINING;
                _mainWindowModel.RedirectToDifficulty();
            }
        }

        public void SetTheme(Grid grid)
        {
            Theme themeHandler = new Theme();
            Grid innerGrid = grid.Children.OfType<Grid>().FirstOrDefault();

            themeHandler.SetBackground(grid, innerGrid.RowDefinitions.Count);
            themeHandler.SetButtonsInStackPanelColor(innerGrid);
        }
    }
}
