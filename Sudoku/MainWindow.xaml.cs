using System.Windows;
using Sudoku.ViewModel;

namespace Sudoku
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}