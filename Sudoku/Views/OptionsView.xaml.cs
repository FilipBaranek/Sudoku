using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Sudoku.Service;
using Sudoku.ViewModels;

namespace Sudoku.Views
{
    public partial class OptionsView : UserControl
    {
        public OptionsView(Router router)
        {
            InitializeComponent();

            ThemeManager.SetTheme("Options");

            DataContext = new OptionsViewModel(router);
        }

    }
}
