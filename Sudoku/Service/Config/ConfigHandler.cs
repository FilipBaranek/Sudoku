﻿using System.IO;
using System.Text.Json;
using Sudoku.Models;

namespace Sudoku.Service.Config
{
    public class ConfigHandler
    {
        private const int DEFAULT_TIME = 500;
        private readonly string _configPath;
        private Config _config;

        public bool AutomaticCandidates => _config.AutomaticCandidates;
        public bool MarkSelectedNumber => _config.MarkSelectedNumber;
        public bool Crosshair => _config.Crosshair;
        public string? Algorithm => _config.Algorithm;
        public string Theme => _config.Theme;
        public int Wins => _config.Wins;

        public ConfigHandler()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string sudokuConfigDir = Path.Combine(appData, "Sudoku");
            if (!File.Exists(sudokuConfigDir))
            {
                Directory.CreateDirectory(sudokuConfigDir);
            }

            _configPath = Path.Combine(sudokuConfigDir, "config.json");
            _config = LoadConfig();
        }

        private Config LoadConfig()
        {

            if (!File.Exists(_configPath))
            {
                _config = new Config(false, false, false, null, "light", 0, DEFAULT_TIME, DEFAULT_TIME, DEFAULT_TIME);

                SaveConfig();

                return _config;
            }

            string jsonData = File.ReadAllText(_configPath);
            Config? config = JsonSerializer.Deserialize<Config>(jsonData);

            return config ?? throw new InvalidOperationException("Wrong config format");
        }

        private void SaveConfig()
        {
            string jsonData = JsonSerializer.Serialize(_config);

            File.WriteAllText(_configPath, jsonData);
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

        public double Record(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    return _config.EasyRecord;
                case Difficulty.Medium:
                    return _config.MediumRecord;
                default:
                    return _config.HardRecord;
            }
        }

        public void UpdateRecord(Difficulty difficulty, int record)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    _config.EasyRecord = record;
                    break;
                case Difficulty.Medium:
                    _config.MediumRecord = record;
                    break;
                default:
                    _config.HardRecord = record;
                    break;
            }
            SaveConfig();
        }

    }
}
