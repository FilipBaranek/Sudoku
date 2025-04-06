using Sudoku.Models.GameElements;

namespace Sudoku.Models.Game
{
    public abstract class Game
    {
        protected const int TOTAL_CORRECT = 81;
        protected bool _isWrongMove;
        protected int _correct;
        protected int[,] _solutionGameBoard;
        protected int[,] _sudokuGameBoard;
        protected GameBoard _gameBoard;


        public int SelectedNumber { get; private set; }
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

        public bool IsWrongMove()
        {
            bool isWrongMove = _isWrongMove;
            _isWrongMove = false;

            return isWrongMove;
        }

        public void SelectNumber(int pivot)
        {
            SelectedNumber = pivot;
        }

        public abstract void PlaceNumber(GameCell cell);

    }
}
