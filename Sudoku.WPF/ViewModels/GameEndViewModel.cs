using System.Windows.Media;
using Sudoku.WPF.Interfaces;
using Sudoku.WPF.Models;
using Sudoku.WPF.Models.Templates;
using Sudoku.WPF.Services;
using Sudoku.WPF.Services.ContentHandlers;
using Sudoku.WPF.Views;

namespace Sudoku.WPF.ViewModels
{
    public class GameEndViewModel : IContentLoad
    {
        private bool _win;
        private Router _router;
        private Dictionary<string, Action> _command;
        public ImageSource Background { get; private set; }
        public ButtonTemplate Button { get; private set; }
        public EndMessageTemplate Message { get; private set; }

        public GameEndViewModel(Router router, bool win) 
        {
            _win = win;
            _router = router;
            _command = new Dictionary<string, Action>();

            _command.Add("Redirect", Redirect);

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
            Button = contentHandler.CreateButtons(_command)[0];
            Message = contentHandler.CreateEndMessage(_win);
        }
    }
}
