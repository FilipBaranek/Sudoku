﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Sudoku.Model
{
    public class SudokuCell : INotifyPropertyChanged
    {
        public int Row { get; private set; }
        public int Column { get; private set; }
        public Thickness BorderThickness { get; private set; }
        public ICommand Command { get; private set; }
        public SudokuCell Parameter { get; private set; }

        private string _content;
        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged(nameof(Content));
            }
        }

        public SudokuCell(int row, int column, string content, Thickness bordrThickness, ICommand command)
        {
            Row = row;
            Column = column;
            Content = content;
            BorderThickness = bordrThickness;
            Command = command;
            Parameter = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
