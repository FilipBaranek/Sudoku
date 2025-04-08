using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Sudoku.Models;
using Sudoku.Service;
using Sudoku.ViewModels;

namespace Sudoku.Views
{
    public partial class TrainingView : UserControl
    {
        public TrainingView(Router router, Difficulty difficulty)
        {
            InitializeComponent();

            ThemeManager.SetTheme("Training");

            DataContext = new TrainingViewModel(router, difficulty);
            Loaded += TrainingView_Loaded;
        }

        public TrainingView(Router router, int[,] solutionGameBoard, int[,] sudokuGameBoard, int correct)
        {
            InitializeComponent();

            ThemeManager.SetTheme("Training");

            DataContext = new TrainingViewModel(router, solutionGameBoard, sudokuGameBoard, correct);
            Loaded += TrainingView_Loaded;

        }

        private void TrainingView_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            foreach (InputBinding ib in this.InputBindings)
            {
                window.InputBindings.Add(ib);
            }
        }
    }
}
