using System.ComponentModel;
using System.Windows.Media;
using Sudoku.WPF.Interfaces;
using Sudoku.WPF.Services;
using Sudoku.WPF.Services.ContentHandlers;
using Sudoku.WPF.Views;

namespace Sudoku.WPF.ViewModels
{
    public class GameEndViewModel : IContentLoad
    {
        private Router _router;
        public ImageSource Background { get; private set; }
        public GameEndViewModel(Router router, bool win) 
        {
            _router = router;

            LoadContent();
        }

        private void Redirect()
        {
            _router.NavigateTo(new MenuView(_router));
        }

        public void LoadContent()
        {
            var contentHandler = new GameEndContentHandler();
            Background = contentHandler.GetBackground();
        }
    }
}
