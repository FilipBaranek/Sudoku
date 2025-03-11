using Sudoku.Service;
using Sudoku.Views;

namespace Sudoku.ViewModels
{
    public class MainWindowViewModel
    {
        public Router Router { get; private set; }

        public MainWindowViewModel()
        {
            this.Router = new Router();
            this.Router.RedirectTo(new MenuView(Router));
        }
    }
}
