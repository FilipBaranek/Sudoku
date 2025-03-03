
namespace Sudoku.WPF.Models
{
    public class Game
    {
        protected GameBoard _gameBoard;
        private int[,] _playerGameBoard;
        private int[,] _solutionGameBoard;
        public Game(Difficulty difficulty)
        {
            _gameBoard = new GameBoard(difficulty);
            _gameBoard.GenerateSolutionGameBoard();
            _gameBoard.GenerateGameBoard(difficulty);

            _solutionGameBoard = _gameBoard.Solution();
            _playerGameBoard = _gameBoard.GetGameBoard();
        }

        public void LoadSudokuElements()
        {

        }


        //NAJSKOR OPRAVIT ABY VOLANIE POZADIA BOLO MVVM


    }
}
