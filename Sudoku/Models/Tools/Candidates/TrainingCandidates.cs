using System.Collections.ObjectModel;
using Sudoku.Models.GameElements;
using Sudoku.Models.GameLib;

namespace Sudoku.Models.Tools.Candidates
{
    public class TrainingCandidates : Candidates
    {
        public TrainingCandidates(Game game) : base(game) { }

        public void UpdateCandidates(SudokuTrainingCell cell)
        {
            if (_game is Training training)
            {
                training.UpdateSectorCandidates(cell.Row, cell.Column);
                training.UpdateRowAndColumn(cell.Row, cell.Column);
            }
        }

        public void DrawAllCandidates(ObservableCollection<SudokuTrainingCell> trainingCells)
        {
            foreach (SudokuTrainingCell cell in trainingCells)
            {
                if (_game.IsCandidateCell(cell.Row, cell.Column))
                {
                    DrawCandidateCell(cell);
                    ShowAllAvailableCandidates(cell);
                }
            }
        }

        public void RemoveAllCandidates(ObservableCollection<SudokuTrainingCell> trainingCells)
        {
            foreach (SudokuTrainingCell cell in trainingCells)
            {
                if (_game.IsCandidateCell(cell.Row, cell.Column))
                {
                    RemoveCandidateCell(cell);
                }
            }
        }

    }
}
