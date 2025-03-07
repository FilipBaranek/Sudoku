using System.Windows.Media;
using Sudoku.WPF.Models;

namespace Sudoku.WPF.Services.ContentHandlers
{
    public abstract class ContentHandler
    {
        public virtual ImageSource GetBackground()
        {
            var backgroundTemplate = new BackgroundTemplate();

            return backgroundTemplate.Background;
        }

        public abstract ButtonTemplate[] CreateButtons(Dictionary<string, Action> commands);
    }
}
