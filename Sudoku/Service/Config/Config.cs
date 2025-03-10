namespace Sudoku.Service.Config
{
    public class Config
    {
        public string Theme { get; set; }

        public int Wins { get; set; }

        public Config(string theme, int wins)
        {
            Theme = theme;
            Wins = wins;
        }
    }
}
