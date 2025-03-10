using System.Windows;
using Sudoku.Service.Config;

namespace Sudoku.Service
{
    public static class ThemeManager
    {
        public static void SetTheme(bool setText, string buttonType)
        {
            var configHandler = new ConfigHandler();
            string theme = configHandler.Theme().Equals("dark") ? "Dark" : "Light";

            Application.Current.Resources.MergedDictionaries.Clear();
            
            SetBackground(theme);
            SetButtons(theme, buttonType);
            if (setText)
            {
                SetText(theme);
            }
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

        private static void SetButtons(string theme, string buttonType)
        {
            string buttonThemeFile = $"Resources/{theme}Theme/{theme}{buttonType}.xaml";

            var buttonThemeDictionary = new ResourceDictionary
            {
                Source = new System.Uri(buttonThemeFile, System.UriKind.Relative)
            };

            Application.Current.Resources.MergedDictionaries.Add(buttonThemeDictionary);
        }

        private static void SetText(string theme)
        {
            string textThemeFile = $"Resources/{theme}Theme/{theme}Text.xaml";
            
            var textDictionary = new ResourceDictionary
            {
                Source = new System.Uri(textThemeFile, System.UriKind.Relative)
            };

            Application.Current.Resources.MergedDictionaries.Add(textDictionary);
        }
    }
}
