using System.Windows.Input;
using System.Windows.Media;
using Sudoku.Commands;
using Sudoku.Models;
using Sudoku.Service;
using Sudoku.Service.Config;
using Sudoku.Views;

namespace Sudoku.ViewModels
{
    public class GameEndViewModel
    {
        private readonly Router _router;

        public string Message { get; private set; }
        public string SolveTime { get; private set; }
        public Brush FinalMessageColor { get; private set; }
        public ICommand BackToMenu { get; private set; }

        public GameEndViewModel(Router router, bool win, int solveTime, Difficulty difficulty)
        {
            _router = router;
            SolveTime = $"Solved in {solveTime}seconds";
            BackToMenu = new RelayCommand(RedirectToMenu);

            if (win)
            {
                Message = "You won";
                FinalMessageColor = new SolidColorBrush(Colors.LightGreen);

                UpdateWins(solveTime, difficulty);
            }
            else
            {
                Message = "You lose";
                FinalMessageColor = new SolidColorBrush(Colors.Red);
            }
        }

        public GameEndViewModel(Router router)
        {
            _router = router;
            SolveTime = "";
            Message = "You won";
            FinalMessageColor = new SolidColorBrush(Colors.LightGreen);
            BackToMenu = new RelayCommand(RedirectToMenu);
        }

        private void RedirectToMenu()
        {
            _router.CurrentView = new MenuView(_router);
        }

        private void UpdateWins(int solveTime, Difficulty difficulty)
        {
            ConfigHandler configHandler = new ConfigHandler();
            configHandler.UpdateWins();

            if (solveTime < configHandler.Record(difficulty))
            {
                configHandler.UpdateRecord(difficulty, solveTime);
            }
        }

    }
}
