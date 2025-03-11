using System.Windows.Controls;
using Sudoku.Service;
using Sudoku.ViewModels;

namespace Sudoku.Views
{
    public partial class MenuView : UserControl
    {
        public MenuView(Router router)
        {
            InitializeComponent();

            ThemeManager.SetTheme("Menu");
            
            DataContext = new MenuViewModel(router);
        }
    }
}
