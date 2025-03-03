using System.Windows.Input;
using Sudoku.WPF.Models;
using Sudoku.WPF.Commands;
using Sudoku.WPF.Interfaces;
using System.Windows.Controls;

namespace Sudoku.WPF.ViewModels
{
    public class DifficultyViewModel : ISetTheme
    {
        private Action<Difficulty> _onDifficultySelected;
        public ICommand EasyCommand { get; }
        public ICommand MediumCommand { get; }
        public ICommand HardCommand { get; }
        public DifficultyViewModel(Action<Difficulty> onDifficultySelected, Grid grid)
        {
            _onDifficultySelected = onDifficultySelected;

            EasyCommand = new RelayCommand(() => SelectDifficulty(Difficulty.Easy));
            MediumCommand = new RelayCommand(() => SelectDifficulty(Difficulty.Medium));
            HardCommand = new RelayCommand(() => SelectDifficulty(Difficulty.Hard));

            SetTheme(grid);
        }

        private void SelectDifficulty(Difficulty difficulty)
        {
            _onDifficultySelected?.Invoke(difficulty);
        }

        public void SetTheme(Grid grid)
        {
            Theme themeHandler = new Theme();
            themeHandler.SetBackground(grid, grid.RowDefinitions.Count);
        }
    }
}
