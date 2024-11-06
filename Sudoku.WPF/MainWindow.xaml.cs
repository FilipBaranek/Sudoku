using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sudoku.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnClickPlay(object sender, RoutedEventArgs e)
        {
            DifficultyWindow difficultyWindow = new DifficultyWindow();
            difficultyWindow.Show();
            Close();
        }

        private void OnClickQuit(object sender, RoutedEventArgs e)
        {
            MessageBoxResult quit = MessageBox.Show("Do you want to quit the SUDOKU?", "Quit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (quit == MessageBoxResult.Yes)
            {
                Close();
            }
        }
    }
}