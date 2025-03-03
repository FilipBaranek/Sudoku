
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Sudoku.WPF.Helpers;

namespace Sudoku.WPF.Models
{
    public class Theme
    {
        public ConfigHandler _configHandler;
        public Theme()
        {
            _configHandler = new ConfigHandler();
        }

        public void SetBackground(Grid grid, int rows)
        {
            string backgroundPath = _configHandler.Theme().Equals("dark") ? "darkBackground" : "background";

            ImageBrush background = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/{backgroundPath}.jpg", UriKind.Absolute)),
                Stretch = Stretch.Fill
            };

            grid.Background = background;
        }

        public void SetButtonsColor(Grid grid)
        {
            SolidColorBrush color = _configHandler.Theme().Equals("dark") ? new SolidColorBrush(Colors.Gray)
                                                                          :  new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFF0"));

            foreach (Button button in grid.Children.OfType<Button>())
            {
                button.Background = color;
            }
        }

        public void SetButtonsInStackPanelColor(Grid grid)
        {
            SolidColorBrush color = _configHandler.Theme().Equals("dark") ? new SolidColorBrush(Colors.Gray)
                                                                          : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFF0"));

            foreach (StackPanel stackPanel in grid.Children.OfType<StackPanel>())
            {
                foreach (Button button in stackPanel.Children.OfType<Button>())
                {
                    button.Background = color;
                }
            }
        }
    }
}
