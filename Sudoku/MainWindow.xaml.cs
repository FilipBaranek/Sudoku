using System.Windows;
using Sudoku.ViewModels;

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