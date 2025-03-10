using System.Windows.Controls;
using Sudoku.Service;
using Sudoku.ViewModel;

namespace Sudoku.View
{
    public partial class DifficultyView : UserControl
    {
        public DifficultyView(Router router, bool isTraining)
        {
            InitializeComponent();
            ThemeManager.SetTheme(true, "DifficultyButton");
            DataContext = new DifficultyViewModel(router, isTraining);
        }
    }
}
