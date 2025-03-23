using System.Windows;
using Sudoku.Service;

namespace Sudoku.Models.Pause
{
    public class TrainingPause : Pause
    {
        public TrainingPause(Router router) : base(router) { }

        public override void PauseToggle()
        {
            if (Visible == Visibility.Visible)
            {
                Visible = Visibility.Hidden;
            }
            else if (Visible == Visibility.Hidden)
            {
                Visible = Visibility.Visible;
            }
        }
    }
}
