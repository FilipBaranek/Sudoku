using System.Windows.Controls;
using Sudoku.Service;
using Sudoku.ViewModels;

namespace Sudoku.Views
{
    public partial class HelpView : UserControl
    {
        public HelpView(Router router)
        {
            InitializeComponent();

            ThemeManager.SetTheme("Rules");

            DataContext = new HelpViewModel(router);
        }

    }
}
