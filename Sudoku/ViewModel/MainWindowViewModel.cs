using Sudoku.Service;
using Sudoku.View;

namespace Sudoku.ViewModel
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
