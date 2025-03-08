using System.Windows.Media;
using Sudoku.WPF.Services;

namespace Sudoku.WPF.Models
{
    public class BackgroundTemplate
    {
        public ImageSource Background {  get; private set; }
        public BackgroundTemplate()
        {
            Theme themeHandler = new Theme();

            Background = themeHandler.Background();
        }
    }
}
