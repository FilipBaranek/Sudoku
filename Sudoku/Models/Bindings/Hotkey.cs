namespace Sudoku.Models.Bindings
{
    public class Hotkey
    {
        public string Key { get; set; }
        public string Function { get; set; }

        public Hotkey(string key, string function)
        {
            Key = key;
            Function = function;
        }

    }
}
