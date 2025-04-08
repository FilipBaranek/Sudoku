using System.Windows.Input;
using Sudoku.Commands;
using Sudoku.Service;

namespace Sudoku.ViewModels
{
    public class HelpViewModel
    {
        private readonly Router _router;

        public ICommand Redirect { get; private set; }

        public HelpViewModel(Router router)
        {
            _router = router;

            Redirect = new RelayCommand(RedirectBack);
        }

        public void RedirectBack()
        {
            _router.NavigateBack();
        }

    }
}
