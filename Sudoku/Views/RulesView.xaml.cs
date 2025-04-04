using System.Windows.Controls;
using Sudoku.Service;
using Sudoku.ViewModels;

namespace Sudoku.Views
{
    public partial class RulesView : UserControl
    {
        public RulesView(Router router)
        {
            InitializeComponent();

            ThemeManager.SetTheme("Rules");

            DataContext = new RulesViewModel(router);
        }
    }
}
