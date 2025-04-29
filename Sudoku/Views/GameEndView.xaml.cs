using System.Windows.Controls;
using Sudoku.Models;
using Sudoku.Service;
using Sudoku.ViewModels;

namespace Sudoku.Views
{
    public partial class GameEndView : UserControl
    {
        public GameEndView(Router router, bool win, int solveTime, Difficulty difficulty)
        {
            InitializeComponent();

            ThemeManager.SetTheme("GameEnd");

            DataContext = new GameEndViewModel(router, win, solveTime, difficulty);
        }

        public GameEndView(Router router)
        {
            InitializeComponent();

            ThemeManager.SetTheme("GameEnd");

            DataContext = new GameEndViewModel(router);
        }

    }
}
