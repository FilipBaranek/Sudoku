using System.Collections.ObjectModel;
using System.Windows.Input;
using Sudoku.Commands;
using Sudoku.Models.GameElements;
using Sudoku.Service;

namespace Sudoku.ViewModels
{
    public abstract class GameViewModel
    {
        protected readonly Router _router;

        public ObservableCollection<SudokuPivot> PivotElements { get; private set; }
        public ICommand SelectNumberByKeyTrigger { get; private set; }

        public GameViewModel(Router router)
        {
            _router = router;
            PivotElements = SudokuGenerator.GeneratePivotCells(SelectNumber);
            SelectNumberByKeyTrigger = new RelayCommand<string>(SelectNumberByKey);
        }
        
        protected void SelectNumberByKey(string number)
        {
            SelectNumber(int.Parse(number));

            PivotElements[int.Parse(number) - 1].IsChecked = true;
        }

        public abstract void SelectNumber(int pivot);

        public abstract void PlaceNumber(GameCell cell);

        public abstract void GameEnd();

    }
}
