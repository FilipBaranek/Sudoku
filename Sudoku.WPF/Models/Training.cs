
namespace Sudoku.WPF.Models
{
    public class Training : Game
    {
        private List<int>[,] _avalaibleElements;
        public Training(Difficulty difficulty) : base(difficulty)
        {
            _gameBoard.GenerateAvalaibleElements();

            _avalaibleElements = _gameBoard.GetTrainingGameBoard();
        }




    }
}
