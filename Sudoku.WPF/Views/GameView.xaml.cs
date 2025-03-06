using System.Windows.Controls;
using Sudoku.WPF.Models;
using Sudoku.WPF.Services;
using Sudoku.WPF.ViewModels;


namespace Sudoku.WPF.Views
{
    public partial class GameView : UserControl
    {
        public GameView(Router router, Difficulty difficulty)
        {
            InitializeComponent();
            DataContext = new GameViewModel(router, difficulty);
        }
    }
}
