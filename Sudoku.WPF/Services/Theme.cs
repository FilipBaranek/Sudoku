using System.Windows.Media;
using System.Windows.Media.Imaging;
using Sudoku.WPF.Services.Config;

namespace Sudoku.WPF.Services
{
    public class Theme
    {
        public ConfigHandler _configHandler;
        public Theme()
        {
            _configHandler = new ConfigHandler();
        }

        public BitmapImage Background()
        {
            string backgroundPath = _configHandler.Theme().Equals("dark") ? "darkBackground" : "background";

            return new BitmapImage(new Uri($"pack://application:,,,/images/{backgroundPath}.jpg", UriKind.Absolute));
        }

        public Brush ButtonsColor()
        {
            return _configHandler.Theme().Equals("dark") ? new SolidColorBrush(Colors.Gray) : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFF0"));
        }
    }
}
