using System.Windows.Controls;
using Sudoku.WPF.Services;
using Sudoku.WPF.ViewModels;

namespace Sudoku.WPF.Views
{
    public partial class GameEndView : UserControl
    {
        public GameEndView(Router router, bool win)
        {
            InitializeComponent();
            DataContext = new GameEndViewModel(router, win);
        }
    }
}
