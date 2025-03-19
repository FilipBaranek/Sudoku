namespace Sudoku.Models
{
    public class Game
    {
        private const int TOTAL_CORRECT = 81;
        private int _correct;
        private int[,] _solutionGameBoard;
        private int[,] _sudokuGameBoard;
        private GameBoard _gameBoard;

        public bool Win {  get; set; }
        public int SelectedNumber { get; set; }

        public Game(Difficulty difficulty)
        {
            _gameBoard = new GameBoard();
            _solutionGameBoard = _gameBoard.SolutionGameBoard();
            _sudokuGameBoard = _gameBoard.SudokuGameBoard(_solutionGameBoard, difficulty);
        }

        public bool PlaceNumber(int row, int column)
        {
            if (_solutionGameBoard[row, column] == SelectedNumber)
            {
                _sudokuGameBoard[row, column] = SelectedNumber;
                ++_correct;

                if (_correct == TOTAL_CORRECT)
                {
                    Win = true;
                }

                return true;
            }

            return false;
        }

    }
}
