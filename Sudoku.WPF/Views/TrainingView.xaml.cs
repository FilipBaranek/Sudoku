using System.Windows.Controls;
using Sudoku.WPF.Models;
using Sudoku.WPF.ViewModels;


namespace Sudoku.WPF.Views
{
    public partial class TrainingView : UserControl
    {
        public TrainingView(MainWindowViewModel mainWindowViewModel, Difficulty difficulty)
        {
            InitializeComponent();
            GameViewContent gameViewContent = new GameViewContent(GameGrid);
            DataContext = new TrainingViewModel(mainWindowViewModel, difficulty);
        }
    }
}
