using System.Windows.Input;

namespace Sudoku.Models
{
    public class SudokuPivot
    {
        public int Value { get; private set; }
        public string Content {  get; private set; }
        public ICommand Command { get; private set; }

        public SudokuPivot(int value, string content, ICommand command)
        {
            Value = value;
            Content = content;
            Command = command;
        }
    }
}
