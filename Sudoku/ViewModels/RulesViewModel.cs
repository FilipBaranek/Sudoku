using System.Windows.Input;
using Sudoku.Commands;
using Sudoku.Service;

namespace Sudoku.ViewModels
{
    public class RulesViewModel
    {
        private readonly Router _router;

        public ICommand Redirect { get; private set; }

        public RulesViewModel(Router router)
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
