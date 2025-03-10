using System.IO;
using System.Text.Json;

namespace Sudoku.Service.Config
{
    public class ConfigHandler
    {
        private Config _config;
        private readonly string _configPath = "C:\\Users\\filip\\Desktop\\skola\\C#\\bakalarka\\Sudoku\\Sudoku\\Config\\config.json"; // TREBA PREROBIT PATH

        public ConfigHandler()
        {
            LoadConfig();
        }

        private void LoadConfig()
        {
            FileInfo file = new FileInfo(_configPath);

            string jsonData = File.ReadAllText(file.FullName);

            _config = JsonSerializer.Deserialize<Config>(jsonData);
        }

        private void SaveConfig()
        {
            FileInfo file = new FileInfo(_configPath);

            string jsonData = JsonSerializer.Serialize(_config);

            File.WriteAllText(file.FullName, jsonData);
        }

        public void UpdateTheme(string theme)
        {
            _config.Theme = theme;
            SaveConfig();
        }

        public void UpdateWins()
        {
            ++_config.Wins;
            SaveConfig();
        }

        public string Theme() => _config.Theme;

        public int Wins() => _config.Wins;
    }
}
