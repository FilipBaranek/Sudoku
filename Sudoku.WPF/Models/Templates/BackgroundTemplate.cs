using System.ComponentModel;
using System.Windows.Media;
using Sudoku.WPF.Services;

namespace Sudoku.WPF.Models
{
    public class BackgroundTemplate : INotifyPropertyChanged
    {
        private ImageSource _background;
        public ImageSource Background
        {
            get => _background;
            set
            {
                _background = value;
                OnPropertyChanged(nameof(Background));
            }
        }
        public BackgroundTemplate()
        {
            Theme themeHandler = new Theme();

            Background = themeHandler.Background();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
