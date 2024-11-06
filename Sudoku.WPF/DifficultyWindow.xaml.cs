using Sudoku.GameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sudoku.WPF
{
    /// <summary>
    /// Interaction logic for DifficultyWindow.xaml
    /// </summary>
    public partial class DifficultyWindow : Window
    {
        private Difficulty difficulty;

        public DifficultyWindow()
        {
            InitializeComponent();
        }

        private void OnClickEasy(object sender, RoutedEventArgs e)
        {
            SudokuGameWindow game = new SudokuGameWindow(Difficulty.Easy);
            game.Show();
            Close();
        }

        private void OnClickMedium(object sender, RoutedEventArgs e)
        {
            SudokuGameWindow game = new SudokuGameWindow(Difficulty.Medium);
            game.Show();
            Close();
        }

        private void OnClickHard(object sender, RoutedEventArgs e)
        {
            SudokuGameWindow game = new SudokuGameWindow(Difficulty.Hard);
            game.Show();
            Close();
        }
    }
}
