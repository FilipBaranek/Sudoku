using System.Windows.Controls;
using Sudoku.WPF.Services;
using Sudoku.WPF.ViewModels;

namespace Sudoku.WPF.Views
{

    public partial class MenuView : UserControl
    {
        public MenuView(Router router)
        {
            InitializeComponent();
            DataContext = new MenuViewModel(router);
        }
    }
}
