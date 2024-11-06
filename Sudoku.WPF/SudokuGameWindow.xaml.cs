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
    /// Interaction logic for SudokuGameWindow.xaml
    /// </summary>
    public partial class SudokuGameWindow : Window
    {
        private GameBoard gameBoard;
        private int[,] gameBoardElements;
        private int selectedNumber;
        private int wrong;
        public SudokuGameWindow(Difficulty difficulty)
        {
            InitializeComponent();

            GenerateSudokuElements(difficulty);



        }

        public void GenerateSudokuElements(Difficulty difficulty)
        {
            gameBoard = new GameBoard(difficulty);
            gameBoardElements = gameBoard.GetGameBoard();

            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    string buttonName = $"button{i}{j}";
                    Button button = (Button)FindName(buttonName);

                    if (gameBoardElements[i, j] != 0)
                    {
                        button.Content = gameBoardElements[i, j];
                    }
                }
            }
        }

        private void OnClickMenu(object sender, RoutedEventArgs e)
        {
            MessageBoxResult menuConfirmationWindow = MessageBox.Show("Do you want to go back to menu?", "Menu", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (menuConfirmationWindow == MessageBoxResult.Yes)
            {
                MainWindow menu = new MainWindow();
                menu.Show();
                Close();
            }
        }

        private void OnClickSelectButton(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int number = int.Parse(button.Name[6].ToString());

            if (selectedNumber !=  number)
            {
                selectedNumber = number;
                button.Background = new SolidColorBrush(Colors.LightBlue);
            }
            else
            {
                selectedNumber = 0;
                button.Background = new SolidColorBrush(Colors.White);
            }

            for (int i = 1; i < 10; ++i)
            {
                Button unselectedButton = (Button)FindName($"button{i}");
                if (selectedNumber != i)
                {
                    unselectedButton.Background = new SolidColorBrush(Colors.White);
                }
            }
        }

        private void OnClickSetNumber(object sender, RoutedEventArgs e)
        {
            Button element = sender as Button;
            int rowIndex = int.Parse(element.Name[6].ToString());
            int columnIndex = int.Parse(element.Name[7].ToString());

            if (selectedNumber != 0)
            {
                if (gameBoardElements[rowIndex, columnIndex] == 0)
                {
                    if (!gameBoard.CheckPossibility(rowIndex, columnIndex, selectedNumber, GameBoardType.Game))
                    {
                        element.Background = new SolidColorBrush(Colors.Red);

                        ++wrong;
                        if (wrong == 3)
                        {
                            MessageBoxResult backToMenu = MessageBox.Show("YOU LOSE\nPress OK to get back to menu", "Back to menu", MessageBoxButton.OK, MessageBoxImage.Stop);

                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            Close();
                        }
                        return;
                    }

                    gameBoardElements[rowIndex, columnIndex] = selectedNumber;
                    element.Background = new SolidColorBrush(Colors.White);
                    element.Content = selectedNumber;
                }
            }
        }
    }
}
