using System.Windows.Controls;
using Sudoku.Service;
using Sudoku.ViewModel;

namespace Sudoku.View
{
    public partial class MenuView : UserControl
    {
        public MenuView(Router router)
        {
            InitializeComponent();
            ThemeManager.SetTheme(false, "MenuButton");
            DataContext = new MenuViewModel(router);
        }
    }
}
