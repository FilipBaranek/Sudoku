using System.Windows.Controls;
using Sudoku.Service;
using Sudoku.ViewModels;

namespace Sudoku.Views
{
    public partial class DifficultyView : UserControl
    {
        public DifficultyView(Router router, bool isTraining)
        {
            InitializeComponent();

            ThemeManager.SetTheme("Difficulty");
            
            DataContext = new DifficultyViewModel(router, isTraining);
        }
    }
}
