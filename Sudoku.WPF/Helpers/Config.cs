namespace Sudoku.WPF.Helpers
{
    public class Config
    {
        public string Theme { get; set; }
        public string Algorithm { get; set; }

        public Config(string theme, string algorithm)
        {
            Theme = theme;
            Algorithm = algorithm;
        }
    }
}
