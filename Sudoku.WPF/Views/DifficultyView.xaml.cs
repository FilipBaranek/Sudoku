using System.Windows.Controls;
using Sudoku.WPF.Models;
using Sudoku.WPF.ViewModels;


namespace Sudoku.WPF.Views
{
    public partial class DifficultyView : UserControl
    {
        public DifficultyView(Action<Difficulty> onDifficultySelected)
        {
            InitializeComponent();
            DataContext = new DifficultyViewModel(onDifficultySelected, DifficultyGrid);
        }
    }
}
