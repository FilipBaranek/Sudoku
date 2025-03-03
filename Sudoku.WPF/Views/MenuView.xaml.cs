using System.Windows.Controls;
using Sudoku.WPF.ViewModels;

namespace Sudoku.WPF.Views
{

    public partial class MenuView : UserControl
    {
        public MenuView(MainWindowViewModel mainWindowModel)
        {
            InitializeComponent();
            DataContext = new MenuViewModel(mainWindowModel, MenuGrid);
        }
    }
}
