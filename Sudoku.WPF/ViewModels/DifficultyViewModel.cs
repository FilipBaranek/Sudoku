using Sudoku.WPF.Models;
using Sudoku.WPF.Interfaces;
using Sudoku.WPF.Services;
using Sudoku.WPF.Views;
using Sudoku.WPF.Services.ContentHandlers;
using System.Windows.Media;

namespace Sudoku.WPF.ViewModels
{
    public class DifficultyViewModel : IContentLoad
    {
        private const int GAMETYPE_GAME = 0;
        private const int GAMETYPE_TRAINING = 1;
        private readonly int _gameType;
        private readonly Router _router;
        private Difficulty _difficulty;
        private Dictionary<string, Action> _commands;
        public ImageSource Background { get; private set; }
        public ButtonTemplate[] Buttons { get; private set; }
        public DifficultyViewModel(Router router, int gameType)
        {
            _router = router;
            _gameType = gameType;
            _commands = new Dictionary<string, Action>();

            _commands.Add("Easy", () => SelectDifficulty(Difficulty.Easy));
            _commands.Add("Medium", () => SelectDifficulty(Difficulty.Medium));
            _commands.Add("Hard", () => SelectDifficulty(Difficulty.Hard));

            LoadContent();
        }

        private void SelectDifficulty(Difficulty difficulty)
        {
            _difficulty = difficulty;

            StartGame();
        }

        private void StartGame()
        {
            switch (_gameType)
            {
                case GAMETYPE_GAME:
                    _router.NavigateTo(new GameView(_router, _difficulty));
                    break;
                case GAMETYPE_TRAINING:
                    _router.NavigateTo(new TrainingView(_router, _difficulty));
                    break;
                default:
                    _router.NavigateTo(new MenuViewModel(_router));
                    break;
            }
        }

        public void LoadContent()
        {
            var contentHandler = new DifficultyContentHandler();
            Background = contentHandler.GetBackground();
            Buttons = contentHandler.CreateButtons(_commands);
        }
    }
}
