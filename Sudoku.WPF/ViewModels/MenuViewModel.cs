using System.Windows;
using System.Windows.Media;
using Sudoku.WPF.Interfaces;
using Sudoku.WPF.Models;
using Sudoku.WPF.Services;
using Sudoku.WPF.Services.ContentHandlers;
using Sudoku.WPF.Views;

namespace Sudoku.WPF.ViewModels
{
    public class MenuViewModel : IContentLoad
    {
        private readonly int GAMETYPE_GAME = 0;
        private readonly int GAMETYPE_TRAINING = 1;
        private readonly Router _router;
        private Dictionary<string, Action> _commands;
        public ButtonTemplate[] Buttons { get; private set; }
        public ImageSource Background { get; private set; }

        public MenuViewModel(Router router)
        {

            _router = router;
            _commands = new Dictionary<string, Action>();
            _commands.Add("PLAY", Play);
            _commands.Add("TRAINING", Training);
            _commands.Add("RULES", Rules);
            _commands.Add("OPTIONS", Options);
            _commands.Add("QUIT", Quit);

            LoadContent();
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

        public void LoadContent()
        {
            var contentHandler = new MenuContentHandler();
            Background = contentHandler.GetBackground();
            Buttons = contentHandler.CreateButtons(_commands);
        }
    }
}
