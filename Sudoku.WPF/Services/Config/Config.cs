namespace Sudoku.WPF.Services.Config
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
