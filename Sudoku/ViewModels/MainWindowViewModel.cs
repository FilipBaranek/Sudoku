using Sudoku.Service;

namespace Sudoku.ViewModels
{
    public class MainWindowViewModel
    {
        public Router Router { get; private set; }

        public MainWindowViewModel()
        {
            Router = new Router();
        }
    }
}
