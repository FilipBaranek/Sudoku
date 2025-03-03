using System.Windows;
using Sudoku.WPF.ViewModels;

namespace Sudoku.WPF
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