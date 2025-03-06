using System.Windows.Controls;
using Sudoku.WPF.Models;
using Sudoku.WPF.Services;
using Sudoku.WPF.ViewModels;


namespace Sudoku.WPF.Views
{
    public partial class TrainingView : UserControl
    {
        public TrainingView(Router router, Difficulty difficulty)
        {
            InitializeComponent();
            DataContext = new TrainingViewModel(router, difficulty);
            
            //GameContentViewModel gameViewContent = new GameContentViewModel();
        }
    }
}
