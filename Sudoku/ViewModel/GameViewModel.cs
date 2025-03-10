using System.Collections.ObjectModel;
using Sudoku.Model;
using Sudoku.Service;

namespace Sudoku.ViewModel
{
    public class GameViewModel
    {
        private int[,] _sudokuElements;

        public ObservableCollection<SudokuCell> GameCells { get; private set; }
        
        public GameViewModel(Router router, Difficulty difficulty)
        {
            _sudokuElements = new int[9, 9];

            GenerateSudokuCells();
        }

        private void PlaceNumber(SudokuCell cell)
        {

        }

        public void GenerateSudokuCells()
        {
            GameCells = SudokuGenerator.GenerateCells(PlaceNumber, _sudokuElements);
        }

    }
}
