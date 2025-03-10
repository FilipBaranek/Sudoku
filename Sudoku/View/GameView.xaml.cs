using System.Windows.Controls;
using Sudoku.Model;
using Sudoku.Service;
using Sudoku.ViewModel;

namespace Sudoku.View
{
    public partial class GameView : UserControl
    {
        public GameView(Router router, Difficulty difficulty)
        {
            InitializeComponent();
            ThemeManager.SetTheme(false, "GameButton"); // PREROBIT NA LIST
            DataContext = new GameViewModel(router, difficulty);
        }
    }
}
