using Sudoku.Models.GameElements;

namespace Sudoku.Models.GameLib
{
    public abstract class Game
    {
        protected const int GAME_BOARD_SIZE = 9;
        protected const int TOTAL_CORRECT = 81;
        protected bool _isWrongMove;
        protected bool _isUpdateNeeded;
        protected int _correct;
        protected int[,] _solutionGameBoard;
        protected int[,] _sudokuGameBoard;
        protected GameBoard _gameBoard;
        protected List<int>[,] _userCandidates;
        protected List<int>[,] _automaticCandidates;

        protected List<int>[,] _actualCandidates;
        public List<int>[,] ActualCandidates
        {
            get => _actualCandidates;
            protected set => _actualCandidates = value;
        }

        public int SelectedNumber { get; private set; }
        public bool Win { get; set; }

        public int[,] SudokuGameBoard
        {
            get => _sudokuGameBoard;
        }

        public int[,] SolutionGameBoard
        {
            get => _solutionGameBoard;
        }

        public Game(Difficulty difficulty)
        {
            _gameBoard = new GameBoard();
            _solutionGameBoard = _gameBoard.SolutionGameBoard();
            _sudokuGameBoard = _gameBoard.SudokuGameBoard(_solutionGameBoard, difficulty);
            _correct = (int)difficulty;
            _automaticCandidates = _gameBoard.TrainingGameBoard(_sudokuGameBoard);
            _userCandidates = new List<int>[GAME_BOARD_SIZE, GAME_BOARD_SIZE];
            
            UserCandidatesInit();
            _actualCandidates = _userCandidates;

        }

        public Game(int[,] solutionGameBoard, int[,] sudokuGameBoard, int correct)
        {
            _gameBoard = new GameBoard();
            _solutionGameBoard = solutionGameBoard;
            _sudokuGameBoard = sudokuGameBoard;
            _correct = correct;
            _automaticCandidates = _gameBoard.TrainingGameBoard(_sudokuGameBoard);
            _userCandidates = new List<int>[GAME_BOARD_SIZE, GAME_BOARD_SIZE];

            UserCandidatesInit();
            _actualCandidates = _userCandidates;
        }

        private void UserCandidatesInit()
        {
            for (int i = 0; i < GAME_BOARD_SIZE; ++i)
            {
                for (int j = 0; j < GAME_BOARD_SIZE; ++j)
                {
                    _userCandidates[i, j] = new List<int>();
                }
            }
        }

        public bool HandleCandidate(int row, int column)
        {
            if (SelectedNumber != 0 && IsCandidateCell(row, column))
            {
                if (ActualCandidates[row, column].Contains(SelectedNumber))
                {
                    ActualCandidates[row, column].Remove(SelectedNumber);

                    return true;
                }
                else
                {
                    ActualCandidates[row, column].Add(SelectedNumber);
                }
            }

            return false;
        }

        public bool IsCandidateCell(int row, int column)
        {
            return _sudokuGameBoard[row, column] == 0;
        }

        public List<int>? Candidates(int row, int column)
        {
            if (IsCandidateCell(row, column))
            {
                return ActualCandidates[row, column];
            }

            return null;
        }

        public bool IsUpdateNeeded()
        {
            bool needToUpdate = _isUpdateNeeded;
            
            _isUpdateNeeded = false;

            return needToUpdate;
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
