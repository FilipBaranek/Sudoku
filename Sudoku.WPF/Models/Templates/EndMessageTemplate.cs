using System.Windows.Media;

namespace Sudoku.WPF.Models.Templates
{
    public class EndMessageTemplate
    {
        public string Message { get; private set; }
        public Brush MessageColor { get; private set; }
        public EndMessageTemplate(string message, Brush messageColor)
        {
            Message = message;
            MessageColor = messageColor;
        }
    }
}
