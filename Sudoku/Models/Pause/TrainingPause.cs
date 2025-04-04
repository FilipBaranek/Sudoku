using System.Windows;
using System.Windows.Input;
using Sudoku.Commands;
using Sudoku.Service;

namespace Sudoku.Models.Pause
{
    public class TrainingPause : Pause
    {
        private Visibility _hintsVisible;
        public Visibility HintsVisible
        {
            get => _hintsVisible;
            set
            {
                _hintsVisible = value;
                OnPropertyChanged(nameof(HintsVisible));
            }
        }

        public ICommand HintOptions { get; private set; }

        public TrainingPause(Router router) : base(router)
        {
            HintsVisible = Visibility.Hidden;

            HintOptions = new RelayCommand(RedirectToHintOptions);
        }

        private Visibility ToggleVisibility(Visibility menuVisibility)
        {
            return menuVisibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }

        private void RedirectToHintOptions()
        {
            HintsVisible = ToggleVisibility(HintsVisible);
            Visible = ToggleVisibility(Visible);
        }

        public override void PauseToggle()
        {
            if (HintsVisible == Visibility.Hidden)
            {
                Visible = ToggleVisibility(Visible);
            }
        }
    }
}
