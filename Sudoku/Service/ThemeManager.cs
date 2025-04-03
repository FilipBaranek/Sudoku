using System.Windows;
using System.Windows.Media;
using Sudoku.Service.Config;

namespace Sudoku.Service
{
    public static class ThemeManager
    {
        public static void SetTheme(string page)
        {
            var configHandler = new ConfigHandler();
            string theme = configHandler.Theme.Equals("dark") ? "Dark" : "Light";

            Application.Current.Resources.MergedDictionaries.Clear();
            
            SetBackground(theme);
            SetPageTheme(theme, page);
        }

        private static void SetBackground(string theme)
        {
            string backgroundThemeFile = $"Resources/{theme}Theme/{theme}Background.xaml";

            var backgroundThemeDictionary = new ResourceDictionary
            {
                Source = new System.Uri(backgroundThemeFile, System.UriKind.Relative)
            };

            Application.Current.Resources.MergedDictionaries.Add(backgroundThemeDictionary);
        }

        private static void SetPageTheme(string theme, string page)
        {
            string pageFile = $"Resources/{theme}Theme/{theme}{page}.xaml";

            var pageThemeDictionary = new ResourceDictionary
            {
                Source = new System.Uri(pageFile, System.UriKind.Relative)
            };

            Application.Current.Resources.MergedDictionaries.Add(pageThemeDictionary);
        }

        public static Brush? GameButtonColor()
        {
            var darkColor = Application.Current.Resources["DarkGameButton"] as Brush;
            var lightColor = Application.Current.Resources["LightGameButton"] as Brush;

            return darkColor != null ? darkColor : lightColor;
        }

        public static Brush? GameButtonTextColor()
        {
            var darkColor = Application.Current.Resources["DarkGameButtonText"] as Brush;
            var lightColor = Application.Current.Resources["LightGameButtonText"] as Brush;

            return darkColor != null ? darkColor : lightColor;
        }

    }
}
