using System.Windows.Input;
using System.Windows.Media;
using Sudoku.Commands;
using Sudoku.Service;
using Sudoku.Service.Config;
using Sudoku.Views;

namespace Sudoku.ViewModels
{
    public class GameEndViewModel
    {
        private readonly Router _router;

        public string Message { get; private set; }
        public Brush FinalMessageColor { get; private set; }
        public ICommand BackToMenu { get; private set; }

        public GameEndViewModel(Router router, bool win, bool isTraining)
        {
            _router = router;

            if (win)
            {
                Message = "You won";
                FinalMessageColor = new SolidColorBrush(Colors.LightGreen);

                if (!isTraining)
                {
                    UpdateWins();
                }
            }
            else
            {
                Message = "You lose";
                FinalMessageColor = new SolidColorBrush(Colors.Red);
            }

            BackToMenu = new RelayCommand(RedirectToMenu);
        }

        private void RedirectToMenu()
        {
            _router.CurrentView = new MenuView(_router);
        }

        private void UpdateWins()
        {
            ConfigHandler configHandler = new ConfigHandler();
            configHandler.UpdateWins();
        }

    }
}
