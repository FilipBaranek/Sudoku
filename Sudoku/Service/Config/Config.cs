using System.Text.Json.Serialization;

namespace Sudoku.Service.Config
{
    public class Config
    {
        public bool AutomaticCandidates { get; set; }
        public bool MarkSelectedNumber { get; set; }
        public bool Crosshair { get; set; }
        public string? Algorithm { get; set; }
        public string Theme { get; set; }
        public int Wins { get; set; }
        public int EasyRecord {  get; set; }
        public int MediumRecord { get; set; }
        public int HardRecord { get; set; }

        [JsonConstructor]
        public Config(bool automaticCandidates, bool markSelectedNumber, bool crosshair, string? algorithm, string theme, int wins, int easyRecord, int mediumRecord, int hardRecord)
        {
            AutomaticCandidates = automaticCandidates;
            MarkSelectedNumber = markSelectedNumber;
            Crosshair = crosshair;
            Algorithm = algorithm;
            Theme = theme;
            Wins = wins;
            EasyRecord = easyRecord;
            MediumRecord = mediumRecord;
            HardRecord = hardRecord;
        }
    }
}
