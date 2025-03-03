using System.Windows.Controls;
using Sudoku.WPF.Models;
using Sudoku.WPF.ViewModels;


namespace Sudoku.WPF.Views
{
    public partial class GameView : UserControl
    {
        public GameView(MainWindowViewModel mainWindowViewModel,Difficulty difficulty)
        {
            InitializeComponent();
            GameViewContent gameViewContent = new GameViewContent(GameGrid.Children.OfType<Grid>().FirstOrDefault());

            DataContext = new GameViewModel(mainWindowViewModel, GameGrid, difficulty);
        }
    }
}
