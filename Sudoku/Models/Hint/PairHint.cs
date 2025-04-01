using Sudoku.Models.GameElements;

namespace Sudoku.Models.Hint
{
    public class PairHint : Hint
    {
        private bool _isIndependent;
        private string _type;
        private string _size;
        private string _location;

        public PairHint(string name, List<int>[,] gameboard) : base(name, gameboard) 
        {
            _isIndependent = true;
            _type = "";
            _size = "";
            _location = "";
        }

        public PairHint(string name, List<int>[,] gameboard, List<Cell> markedHints) : base(name, gameboard)
        {
            _isIndependent = false;
            _type = "";
            _size = "";
            _location = "";
            MarkedHint = markedHints;
        }

        private List<Cell> GetRow(int rowIndex)
        {
            var row = new List<Cell>();

            for (int columnIndex = 0; columnIndex < GAMEBOARD_SIZE; ++columnIndex)
            {
                if (_gameBoard[rowIndex, columnIndex].Count >= 2)
                {
                    row.Add(new Cell(rowIndex, columnIndex));
                }
            }

            return row;
        }

        private List<Cell> GetColumn(int columnIndex)
        {
            var column = new List<Cell>();

            for (int rowIndex = 0; rowIndex < GAMEBOARD_SIZE; ++rowIndex)
            {
                if (_gameBoard[rowIndex, columnIndex].Count >= 2)
                {
                    column.Add(new Cell(rowIndex, columnIndex));
                }
            }

            return column;
        }

        private List<Cell> GetBlock(int blockIndex)
        {
            var block = new List<Cell>();

            int rowIndex = (blockIndex / 3) * 3;
            int columnIndex = (blockIndex % 3) * 3;

            for (int i = rowIndex; i < rowIndex + 3; ++i)
            {
                for (int j = columnIndex; j < columnIndex + 3; ++j)
                {
                    if (_gameBoard[i, j].Count >= 2)
                    {
                        block.Add(new Cell(i, j));
                    }
                }
            }

            return block;
        }

        private bool HasNakedPair()
        {
            for (int i = 0; i < GAMEBOARD_SIZE; ++i)
            {
                var rowCells = GetRow(i);
                var columnCells = GetColumn(i);
                var blockCells = GetBlock(i);

                if (TryFindNakedPair(rowCells))
                {
                    _location = "row";

                    return true;
                }
                else if (TryFindNakedPair(columnCells))
                {
                    _location = "column";

                    return true;
                }
                else if (TryFindNakedPair(blockCells))
                {
                    _location = "block";

                    return true;
                }
            }

            return false;
        }
        
        private bool TryFindNakedPair(List<Cell> cellSequence)
        {
            for (int pivotIndex = 0; pivotIndex < cellSequence.Count; ++pivotIndex)
            {
                if (cellSequence.Count >= 2 && HasNakedPairOfTwo(cellSequence, pivotIndex))
                {
                    return true;
                }
                else if (cellSequence.Count >= 3 && HasNakedPairOfThree(cellSequence, pivotIndex))
                {
                    return true;
                }
            }

            return false;
        }

        private bool HasNakedPairOfTwo(List<Cell> cellSequence, int pivotIndex)
        {
            var pivotCell = cellSequence[pivotIndex];
            var pivotCandidates = _gameBoard[pivotCell.Row, pivotCell.Column];

            if (pivotCandidates.Count == 2)
            {
                for (int compareCellIndex = pivotIndex + 1; compareCellIndex < cellSequence.Count; ++compareCellIndex)
                {
                    var compareCell = cellSequence[compareCellIndex];
                    var compareCandidates = _gameBoard[compareCell.Row, compareCell.Column];

                    if (compareCandidates.Count == 2 &&
                        IsNakedDouble(pivotCandidates, compareCandidates) &&
                        (IsNewHint(compareCell.Row, compareCell.Column) ||
                        IsNewHint(pivotCell.Row, pivotCell.Column)))
                    {
                        MarkedHint.Clear();
                        MarkedHint.Add(new Cell(compareCell.Row, compareCell.Column));
                        MarkedHint.Add(new Cell(pivotCell.Row, pivotCell.Column));
                        _usedHints.Add(new Cell(compareCell.Row, compareCell.Column));
                        _usedHints.Add(new Cell(pivotCell.Row, pivotCell.Column));

                        _size = "Double";

                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsNakedDouble(List<int> pivotCandidates, List<int> compareCandidates)
        {
            int pairCount = 0;

            foreach (int candidate in compareCandidates)
            {
                if (pivotCandidates.Contains(candidate))
                {
                    ++pairCount;

                    if (pairCount == 2)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool HasNakedPairOfThree(List<Cell> cellSequence, int pivotIndex)
        {
            var pivotCell = cellSequence[pivotIndex];
            var pivotCandidates = _gameBoard[pivotCell.Row, pivotCell.Column];

            for (int secondIndex = pivotIndex + 1; secondIndex < cellSequence.Count; ++secondIndex)
            {
                var secondCell = cellSequence[secondIndex];
                var secondCandidates = _gameBoard[secondCell.Row, secondCell.Column];

                for (int thirdIndex = secondIndex + 1; thirdIndex < cellSequence.Count; ++thirdIndex)
                {
                    var thirdCell = cellSequence[thirdIndex];
                    var thirdCandidates = _gameBoard[thirdCell.Row, thirdCell.Column];

                    var combined = pivotCandidates.Union(secondCandidates).Union(thirdCandidates).ToList();

                    bool isNotNakedDouble = !IsNakedDouble(pivotCandidates, secondCandidates) && !IsNakedDouble(pivotCandidates, thirdCandidates) && !IsNakedDouble(secondCandidates, thirdCandidates);
                    bool isNewHint = IsNewHint(pivotCell.Row, pivotCell.Column) || IsNewHint(secondCell.Row, secondCell.Column) || IsNewHint(thirdCell.Row, thirdCell.Column);

                    if (combined.Count == 3 && isNotNakedDouble && isNewHint)
                    {
                        MarkedHint.Clear();
                        MarkedHint.Add(new Cell(pivotCell.Row, pivotCell.Column));
                        MarkedHint.Add(new Cell(secondCell.Row, secondCell.Column));
                        MarkedHint.Add(new Cell(thirdCell.Row, thirdCell.Column));
                        _usedHints.Add(new Cell(pivotCell.Row, pivotCell.Column));
                        _usedHints.Add(new Cell(secondCell.Row, secondCell.Column));
                        _usedHints.Add(new Cell(thirdCell.Row, thirdCell.Column));

                        _size = "Triple";

                        return true;
                    }
                }
            }

            return false;
        }

        private bool HasHiddenPair()
        {
            return false;
        }

        public override string Message()
        {
            return $"{_size} {_type} pair in the same {_location}. " +
                   $"Every other candidate of cell at the same {_location} can be deleted.";
        }

        public override string? GetHint()
        {
            while (true)
            {
                _type = "naked";
                if (HasNakedPair())
                {
                    return Message();
                }

                _type = "hidden";
                if (HasHiddenPair())
                {
                    return Message();
                }

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
