using Sudoku.WPF.Services;
using Sudoku.WPF.Views;

namespace Sudoku.WPF.ViewModels
{
    public class MainWindowViewModel
    {
        public Router Router { get; }
        public MainWindowViewModel()
        {
            Router = new Router();
            Router.NavigateTo(new MenuView(Router));
        }
    }
}
