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

            if (!Application.Current.Resources.MergedDictionaries.Contains(backgroundThemeDictionary))
            {
                Application.Current.Resources.MergedDictionaries.Add(backgroundThemeDictionary);
            }
        }

        private static void SetPageTheme(string theme, string page)
        {
            string pageFile = $"Resources/{theme}Theme/{theme}{page}.xaml";

            var pageThemeDictionary = new ResourceDictionary
            {
                Source = new System.Uri(pageFile, System.UriKind.Relative)
            };

            if (!Application.Current.Resources.MergedDictionaries.Contains(pageThemeDictionary))
            {
                Application.Current.Resources.MergedDictionaries.Add(pageThemeDictionary);
            }
        }

        public static Brush GameButtonColor()
        {
            var darkColor = Application.Current.Resources["DarkGameButton"] as Brush;
            var lightColor = Application.Current.Resources["LightGameButton"] as Brush;

            if (darkColor != null)
            {
                return darkColor;
            }
            else if (lightColor != null)
            {
                return lightColor;
            }

            throw new KeyNotFoundException("Game button background not defined");
        }

        public static Brush GameButtonTextColor()
        {
            var darkColor = Application.Current.Resources["DarkGameButtonText"] as Brush;
            var lightColor = Application.Current.Resources["LightGameButtonText"] as Brush;

            if (darkColor != null)
            {
                return darkColor;
            }
            else if(lightColor != null)
            {
                return lightColor;
            }

            throw new KeyNotFoundException("Game button foreground color not defined");
        }

        public static string ThemeType()
        {
            var config = new ConfigHandler();

            return config.Theme;
        }

    }
}
