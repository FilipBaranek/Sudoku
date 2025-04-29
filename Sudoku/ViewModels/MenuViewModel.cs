using System.Windows;
using System.Windows.Input;
using Sudoku.Commands;
using Sudoku.Service;
using Sudoku.Views;

namespace Sudoku.ViewModels
{
    public class MenuViewModel
    {
        private readonly Router _router;
        public ICommand PlayCommand { get; private set; }
        public ICommand TrainingCommand { get; private set; }
        public ICommand HelpCommand { get; private set; }
        public ICommand OptionsCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        public MenuViewModel(Router router)
        {
            _router = router;

            PlayCommand = new RelayCommand(Play);
            TrainingCommand = new RelayCommand(Training);
            HelpCommand = new RelayCommand(Help);
            OptionsCommand = new RelayCommand(Options);
            ExitCommand = new RelayCommand(Exit);
        }

        private void Play()
        {
            _router.CurrentView = new DifficultyView(_router, false);
        }

        private void Training()
        {
            _router.CurrentView = new DifficultyView(_router, true);
        }

        private void Help()
        {
            _router.CurrentView = new HelpView(_router);
        }

        private void Options()
        {
            _router.CurrentView = new OptionsView(_router);
        }

        private void Exit()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _router.DisposeLastView();

                Application.Current.Shutdown();
            }
        }

    }
}
