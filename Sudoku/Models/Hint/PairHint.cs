using Sudoku.Models.GameElements;

namespace Sudoku.Models.Hint
{
    public class PairHint : Hint
    {
        private const int OPTIMAL_MAX_PAIR_LENGTH = 5;
        private bool _isIndependent;
        private bool _isHidden;
        private bool _foundAtRow;
        private int _pairSize;

        public PairHint(string name, List<int>[,] gameboard, bool isIndependend = true) : base(name, gameboard) 
        {
            _isIndependent = isIndependend;
        }

        private bool HasNakedPair(int pairSize)
        {
            for (int i = 0; i < GAMEBOARD_SIZE; ++i)
            {
                for (int j = 0; j < GAMEBOARD_SIZE; ++j)
                {
                    if (_gameBoard[i, j].Count == pairSize && TryFindOtherNakedPair(i, j))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool TryFindOtherNakedPair(int foundRow, int foundColumn)
        {
            for (int i = 0; i < GAMEBOARD_SIZE; ++i)
            {
                if (_gameBoard[i, foundColumn].Count == _pairSize && CompareNakedPair(foundRow, foundColumn, i, foundColumn))
                {
                    _foundAtRow = false;

                    return true;
                }
                if (_gameBoard[foundRow, i].Count == _pairSize && CompareNakedPair(foundRow, foundColumn, foundRow, i))
                {
                    _foundAtRow = true;

                    return true;
                }
            }

            return false;
        }

        private bool CompareNakedPair(int foundRow, int foundColumn, int potentialRow, int potentialColumn)
        {
            if (!(foundRow == potentialRow && foundColumn == potentialColumn))
            {
                foreach (int i in _gameBoard[foundRow, foundColumn])
                {
                    if (!_gameBoard[potentialRow, potentialColumn].Contains(i))
                    {
                        return false;
                    }
                }

                if (_usedHints.Count == 0 || IsNewHint(foundRow, foundColumn) || IsNewHint(potentialRow, potentialColumn))
                {
                    var foundPair = new Cell(foundRow, foundColumn);
                    var potentialdPair = new Cell(potentialRow, potentialColumn);

                    Update(foundPair, potentialdPair);

                    return true;
                }
            }

            return false;
        }

        private void Update(Cell foundPair, Cell potentialPair)
        {
            MarkedHint.Clear();
            MarkedHint.Add(foundPair);
            MarkedHint.Add(potentialPair);

            _usedHints.Add(foundPair);
            _usedHints.Add(potentialPair);
        }

        public override string Message()
        {
            string pairLocation = _foundAtRow ? "row" : "column";
            string pairType = _isHidden ? "hidden" : "naked";

            return $"These cells have a {pairType} pair of {_pairSize} candidates in the same {pairLocation}. " +
                   $"Every cell candidate same as any number in marked candidate pair at the same {pairLocation} and block needs to be deleted.";
        }

        public override string? GetHint()
        {
            while (true)
            {
                _isHidden = false;

                for (_pairSize = 2; _pairSize <= OPTIMAL_MAX_PAIR_LENGTH; ++_pairSize)
                {
                    if (HasNakedPair(_pairSize))
                    {
                        return Message();
                    }
                }

                _isHidden = true;

                /*
                if (HasHiddenPair())
                {
                    return Message();
                }
                */

                if (_usedHints.Count == 0 || !_isIndependent)
                {
                    break;
                }

                _usedHints.Clear();
            }

            return null;
        }

    }
}
