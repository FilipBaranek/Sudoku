using Sudoku.Models.GameElements;
using Sudoku.Models.GameLib;

namespace Sudoku.Models.Tools.Candidates
{
    public class Candidates
    {
        protected Game _game;

        public Candidates(Game game)
        {
            _game = game;
        }

        public void HandleCandidate(GameCell cell)
        {
            if (_game.IsCandidateCell(cell.Row, cell.Column))
            {
                if (!_game.HandleCandidate(cell.Row, cell.Column))
                {
                    DrawCandidateCell(cell);
                }
                ShowAllAvailableCandidates(cell);
            }
        }

        public void ShowAllAvailableCandidates(GameCell cell)
        {
            cell.Content = "";

            var candidates = _game.Candidates(cell.Row, cell.Column);

            if (candidates != null)
            {
                foreach (int candidate in candidates)
                {
                    cell.Content += $"{candidate} ";
                }
            }
        }

        public void SetCellToDefault(GameCell trainingCell)
        {
            trainingCell.Content = _game.SelectedNumber.ToString();
            trainingCell.SetDefaultBackground();
            trainingCell.SetDefaultFontSize();
            trainingCell.SetDefaultForeground();
            trainingCell.SetDefaultAlignment();
        }

        public void DrawCandidateCell(GameCell cell)
        {
            cell.SetCandidateFontSize();
            cell.SetCandidateForeground();
            cell.SetCandidateAlignment();
        }

        public void RemoveCandidateCell(GameCell cell)
        {
            cell.SetDefaultAlignment();
            cell.SetDefaultFontSize();
            cell.SetDefaultForeground();
            cell.Content = "";
        }

    }
}
