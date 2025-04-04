using System.IO;
using System.Text.Json;

namespace Sudoku.Service.Config
{
    public class ConfigHandler
    {
        private Config _config;
        private readonly string _configPath = "C:\\Users\\filip\\Desktop\\skola\\C#\\bakalarka\\Sudoku\\Sudoku\\Config\\config.json"; // TREBA PREROBIT PATH

        public bool AutomaticCandidates => _config.AutomaticCandidates;
        public bool MarkSelectedNumber => _config.MarkSelectedNumber;
        public bool Crosshair => _config.Crosshair;
        public string? Algorithm => _config.Algorithm;
        public string Theme => _config.Theme;
        public int Wins => _config.Wins;

        public ConfigHandler()
        {
            _config = LoadConfig();
        }

        private Config LoadConfig()
        {
            FileInfo file = new FileInfo(_configPath);

            if (!file.Exists)
            {
                throw new FileNotFoundException();
            }

            string jsonData = File.ReadAllText(file.FullName);

            Config? config = JsonSerializer.Deserialize<Config>(jsonData);
        
            if (config == null)
            {
                throw new InvalidOperationException("Wrong config format");
            }

            return config;
        }

        private void SaveConfig()
        {
            FileInfo file = new FileInfo(_configPath);

            string jsonData = JsonSerializer.Serialize(_config);

            File.WriteAllText(file.FullName, jsonData);
        }

        public void SwitchTheme()
        {
            _config.Theme = _config.Theme.Equals("dark") ? "light" : "dark";
            SaveConfig();
        }

        public void UpdateWins()
        {
            ++_config.Wins;
            SaveConfig();
        }

        public void UpdateSettings(bool automaticCandidates, bool markSelectedNumbers, bool automaticHints)
        {
            _config.AutomaticCandidates = automaticCandidates;
            _config.MarkSelectedNumber = markSelectedNumbers;
            _config.Crosshair = automaticHints;
            SaveConfig();
        }

        public void UpdateAlgorithm(string algorithm)
        {
            _config.Algorithm = algorithm;
            SaveConfig();
        }

    }
}
