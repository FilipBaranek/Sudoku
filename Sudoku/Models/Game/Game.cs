using System.Windows.Media;
using Sudoku.Models.GameElements;

namespace Sudoku.Models.Game
{
    public abstract class Game
    {
        protected const int TOTAL_CORRECT = 81;
        protected int _correct;
        protected int _selectedNumber;
        protected int[,] _solutionGameBoard;
        protected int[,] _sudokuGameBoard;
        protected GameBoard _gameBoard;

        public bool Win { get; set; }

        public int[,] SudokuGameBoard
        {
            get => _sudokuGameBoard;
        }

        public Game(Difficulty difficulty)
        {
            _gameBoard = new GameBoard();
            _solutionGameBoard = _gameBoard.SolutionGameBoard();
            _sudokuGameBoard = _gameBoard.SudokuGameBoard(_solutionGameBoard, difficulty);
            _correct = (int)difficulty;
        }

        protected async void WrongMove(SudokuCell cell)
        {
            cell.Background = new SolidColorBrush(Colors.Red);

            await Task.Delay(2000);

            cell.Background = cell.DefaultBackground;
        }

        public void SelectNumber(int pivot)
        {
            _selectedNumber = pivot;
        }

        public abstract void PlaceNumber(SudokuCell cell);

    }
}
