using System.Windows.Controls;
using Sudoku.Model;
using Sudoku.Service;

namespace Sudoku.View
{
    public partial class TrainingView : UserControl
    {
        public TrainingView(Router router, Difficulty difficulty)
        {
            InitializeComponent();
        }
    }
}
