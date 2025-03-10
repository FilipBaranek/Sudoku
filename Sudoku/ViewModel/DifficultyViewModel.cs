using System.Windows.Input;
using Sudoku.Commands;
using Sudoku.Model;
using Sudoku.Service;
using Sudoku.View;

namespace Sudoku.ViewModel
{
    public class DifficultyViewModel
    {
        private readonly Router _router;
        private Difficulty _difficulty;
        private bool _isTraining;

        public ICommand EasyCommand { get; private set; }
        public ICommand MediumCommand { get; private set; }
        public ICommand HardCommand { get; private set; }

        public DifficultyViewModel(Router router, bool isTraining)
        {
            _router = router;
            _isTraining = isTraining;

            EasyCommand = new RelayCommand(() => SetDifficulty(Difficulty.Easy));
            MediumCommand = new RelayCommand(() => SetDifficulty(Difficulty.Medium));
            HardCommand = new RelayCommand(() => SetDifficulty(Difficulty.Hard));
        }

        private void SetDifficulty(Difficulty difficulty)
        {
            _difficulty = difficulty;

            Play();
        }

        private void Play()
        {
            if (_isTraining)
            {
                _router.RedirectTo(new TrainingView(_router, _difficulty));
            }
            else
            {
                _router.RedirectTo(new GameView(_router, _difficulty));
            }
        }

    }
}
