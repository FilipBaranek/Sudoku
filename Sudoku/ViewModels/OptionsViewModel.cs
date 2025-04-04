using System.Windows.Input;
using Sudoku.Commands;
using Sudoku.Service;
using Sudoku.Service.Config;

namespace Sudoku.ViewModels
{
    public class OptionsViewModel
    {
        private readonly Router _router;
        private readonly ConfigHandler _config;

        public string Wins { get; private set; }
        public ICommand ThemeSwitchTrigger { get; private set; }

        public OptionsViewModel(Router router)
        {
            _config = new ConfigHandler();
            _router = router;
            Wins = _config.Wins.ToString() + " total wins";
            ThemeSwitchTrigger = new RelayCommand(SwitchTheme);
        }

        private void SwitchTheme()
        {
            _config.SwitchTheme();
        }

    }
}
