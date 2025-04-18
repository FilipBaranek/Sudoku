﻿using System.Windows.Controls;
using Sudoku.Service;
using Sudoku.ViewModels;

namespace Sudoku.Views
{
    public partial class GameEndView : UserControl
    {
        public GameEndView(Router router, bool win, bool isTraining)
        {
            InitializeComponent();

            ThemeManager.SetTheme("GameEnd");

            DataContext = new GameEndViewModel(router, win, isTraining);
        }
    }
}
