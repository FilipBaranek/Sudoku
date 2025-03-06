using System.Windows.Controls;
using Sudoku.WPF.Models;
using Sudoku.WPF.Services;
using Sudoku.WPF.ViewModels;


namespace Sudoku.WPF.Views
{
    public partial class DifficultyView : UserControl
    {
        public DifficultyView(Router router, int gameType)
        {
            InitializeComponent();
            DataContext = new DifficultyViewModel(router, gameType);
        }
    }
}
