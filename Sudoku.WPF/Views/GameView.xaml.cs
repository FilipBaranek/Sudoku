using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Sudoku.WPF.Models;
using Sudoku.WPF.Services;
using Sudoku.WPF.ViewModels;


namespace Sudoku.WPF.Views
{
    public partial class GameView : UserControl
    {
        public GameView(Router router, Difficulty difficulty)
        {
            InitializeComponent();
            DataContext = new GameViewModel(router, difficulty);
            Loaded += GameView_Loaded;
        }

        private void GameView_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            foreach (InputBinding ib in this.InputBindings)
            {
                window.InputBindings.Add(ib);
            }
        }
    }
}
