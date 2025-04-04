﻿using System.ComponentModel;
using Sudoku.Models.GameElements;

namespace Sudoku.Models.Game
{
    public class RegularGame : Game, INotifyPropertyChanged
    {
        private const int TOTAL_INCORRECT = 3;

        private int _incorrect;
        public int Incorrect
        {
            get => _incorrect;
            set
            {
                _incorrect = value;
                OnPropertyChanged(nameof(Incorrect));
            }
        }

        public bool Lose { get; set; }

        public RegularGame(Difficulty difficulty) : base(difficulty)
        {
            Incorrect = TOTAL_INCORRECT;
        }

        public override void PlaceNumber(GameCell cell)
        {
            if (SelectedNumber == 0 || _sudokuGameBoard[cell.Row, cell.Column] != 0)
            {
                return;
            }
            else if (_solutionGameBoard[cell.Row, cell.Column] == SelectedNumber)
            {
                _sudokuGameBoard[cell.Row, cell.Column] = SelectedNumber;
                ++_correct;

                cell.Content = SelectedNumber.ToString();
                cell.Background = cell.DefaultBackground;

                if (_correct == TOTAL_CORRECT)
                {
                    Win = true;
                }
            }
            else
            {
                --Incorrect;

                if (_incorrect == -1)
                {
                    Lose = true;
                }

                _isWrongMove = true;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
