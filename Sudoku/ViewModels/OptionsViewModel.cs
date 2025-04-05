using System.Collections.ObjectModel;
using System.Windows.Input;
using Sudoku.Commands;
using Sudoku.Models.Bindings;
using Sudoku.Service;
using Sudoku.Service.Config;
using Sudoku.Views;

namespace Sudoku.ViewModels
{
    public class OptionsViewModel
    {
        private readonly Router _router;
        private readonly ConfigHandler _config;

        public string ThemeTriggerContent { get; private set; }
        public string Wins { get; private set; }
        public ObservableCollection<Hotkey> Hotkeys { get; private set; }
        public ICommand ThemeSwitchTrigger { get; private set; }
        public ICommand RedirectBackTrigger { get; private set; }

        public OptionsViewModel(Router router)
        {
            _config = new ConfigHandler();
            _router = router;

            string switchTheme = _config.Theme.Equals("dark") ? "light" : "dark";
            ThemeTriggerContent = $"Switch to {switchTheme} theme";
            Wins = _config.Wins.ToString() + " total wins";
            Hotkeys = new ObservableCollection<Hotkey>();
            ThemeSwitchTrigger = new RelayCommand(SwitchTheme);
            RedirectBackTrigger = new RelayCommand(RedirectToMenu);

            LoadBindings();
        }

        private void LoadBindings()
        {
            for (int i = 1; i <= 9; ++i)
            {
                Hotkeys.Add(new Hotkey(i.ToString(), $"Select number {i}"));
            }
            Hotkeys.Add(new Hotkey("H", "Hint"));
            Hotkeys.Add(new Hotkey("C", "Clear hints"));
            Hotkeys.Add(new Hotkey("ESC", "Pause menu toggle"));
        }

        private void SwitchTheme()
        {
            _config.SwitchTheme();

            _router.CurrentView = new OptionsView(_router);
        }

        private void RedirectToMenu()
        {
            _router.CurrentView = new MenuView(_router);
        }

    }
}
