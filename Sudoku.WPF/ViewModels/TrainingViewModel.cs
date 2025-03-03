using Sudoku.WPF.Models;

namespace Sudoku.WPF.ViewModels
{
    public class TrainingViewModel
    {
        public TrainingViewModel(MainWindowViewModel mainWindowViewModel, Difficulty difficulty)
        {
            Training training = new Training(difficulty);
        }
    }
}
