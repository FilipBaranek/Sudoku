using System.IO;
using System.Text.Json;
using System.Windows.Controls.Primitives;

namespace Sudoku.WPF.Helpers
{
    public class ConfigHandler
    {
        private Config _config;
        public ConfigHandler()
        {
            LoadConfig();
        }

        public string Theme()
        {
            return _config.Theme;
        }

        public void SetDarkTheme()
        {
            ChangeTheme("dark");
        }

        public void SetLightTheme()
        {
            ChangeTheme("light");
        }

        public void SetAlgorithm()
        {
            //LOGIC BEHIND CHANGING ALGORITHMS
        }

        private void ChangeTheme(string theme)
        {
            _config.Theme = theme;

            WriteToConfig();
        }

        private void ChangeAlgorithm(string algorithm)
        {
            _config.Algorithm = algorithm;

            WriteToConfig();
        }

        private void WriteToConfig()
        {
            FileInfo file = new FileInfo("C:\\Users\\filip\\Desktop\\skola\\C#\\bakalarka\\Sudoku\\Sudoku.WPF\\Config\\config.json");       //TREBA PREROBIT PATH
            string jsonData = JsonSerializer.Serialize(_config);

            File.WriteAllText(file.FullName, jsonData);
        }

        private void LoadConfig()
        {
            FileInfo file = new FileInfo("C:\\Users\\filip\\Desktop\\skola\\C#\\bakalarka\\Sudoku\\Sudoku.WPF\\Config\\config.json");       //TREBA PREROBIT PATH

            string jsonData = File.ReadAllText(file.FullName);

            _config = JsonSerializer.Deserialize<Config>(jsonData);
        }
    }
}
