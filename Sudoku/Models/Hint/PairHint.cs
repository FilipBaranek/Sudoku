using System.Collections.ObjectModel;
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

        public PairHint(string name, List<int>[,] gameboard, ObservableCollection<SudokuTrainingCell> gameCells, bool isIndependend = true) : base(name, gameboard, gameCells) 
        {
            _isIndependent = isIndependend;
        }

        private bool CompareCandidates(int foundRow, int foundColumn, int potentialRow, int potentialColumn)
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
                    _usedHints.Add(new Pair(foundRow, foundColumn));
                    _usedHints.Add(new Pair(potentialRow, potentialColumn));

                    return true;
                }
            }

            return false;
        }

        private bool TryFindOtherPair(int foundPairRow, int foundPairColumn)
        {
            for (int i = 0; i < GAMEBOARD_SIZE; ++i)
            {
                if (_gameBoard[i, foundPairColumn].Count == _pairSize && CompareCandidates(foundPairRow, foundPairColumn, i, foundPairColumn))
                {
                    MarkCells(foundPairRow, foundPairColumn);
                    MarkCells(i, foundPairColumn);

                    _foundAtRow = false;

                    return true;
                }
                if (_gameBoard[foundPairRow, i].Count == _pairSize && CompareCandidates(foundPairRow, foundPairColumn, foundPairRow, i))
                {
                    MarkCells(foundPairRow, foundPairColumn);
                    MarkCells(foundPairRow, i);

                    _foundAtRow = true;

                    return true;
                }
            }

            return false;
        }

        private bool HasNakedPair(int pairSize)
        {
            for (int i = 0; i < GAMEBOARD_SIZE; ++i)
            {
                for (int j = 0; j < GAMEBOARD_SIZE; ++j)
                {
                    if (_gameBoard[i, j].Count == pairSize && TryFindOtherPair(i, j))
                    {
                        return true;
                    }
                }
            }

            return false;
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
                for (_pairSize = 2; _pairSize <= OPTIMAL_MAX_PAIR_LENGTH; ++_pairSize)
                {
                    if (HasNakedPair(_pairSize))
                    {
                        return Message();
                    }
                }

                /*
                for (_pairSize = OPTIMAL_MAX_PAIR_LENGTH; _pairSize >= 2; --_pairSize)
                {
                    if (HasHiddenPair(_pairSize))
                    {
                        return Message();
                    }
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
