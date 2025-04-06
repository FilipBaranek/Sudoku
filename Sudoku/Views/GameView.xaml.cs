using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Sudoku.Models;
using Sudoku.Service;
using Sudoku.ViewModels;

namespace Sudoku.Views
{
    public partial class GameView : UserControl, IDisposable
    {
        public GameView(Router router, Difficulty difficulty)
        {
            InitializeComponent();

            ThemeManager.SetTheme("Game");

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

        public void Dispose()
        {
            if (DataContext is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

    }
}
