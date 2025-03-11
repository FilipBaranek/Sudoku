using System.Windows.Controls;
using Sudoku.Models;
using Sudoku.Service;

namespace Sudoku.Views
{
    public partial class TrainingView : UserControl
    {
        public TrainingView(Router router, Difficulty difficulty)
        {
            InitializeComponent();
        }
    }
}
