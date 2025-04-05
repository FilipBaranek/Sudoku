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

        public List<Cell> GetRow(int rowIndex, int candidateSize)
        {
            var row = new List<Cell>();

            for (int columnIndex = 0; columnIndex < GAMEBOARD_SIZE; ++columnIndex)
            {
                if (_gameBoard[rowIndex, columnIndex].Count >= candidateSize)
                {
                    row.Add(new Cell(rowIndex, columnIndex));
                }
            }

            return row;
        }

        public List<Cell> GetColumn(int columnIndex, int candidateSize)
        {
            var column = new List<Cell>();

            for (int rowIndex = 0; rowIndex < GAMEBOARD_SIZE; ++rowIndex)
            {
                if (_gameBoard[rowIndex, columnIndex].Count >= candidateSize)
                {
                    column.Add(new Cell(rowIndex, columnIndex));
                }
            }

            return column;
        }

        public List<Cell> GetBlock(int blockIndex, int candidateSize)
        {
            var block = new List<Cell>();

            int rowIndex = (blockIndex / 3) * 3;
            int columnIndex = (blockIndex % 3) * 3;

            for (int i = rowIndex; i < rowIndex + 3; ++i)
            {
                for (int j = columnIndex; j < columnIndex + 3; ++j)
                {
                    if (_gameBoard[i, j].Count >= candidateSize)
                    {
                        block.Add(new Cell(i, j));
                    }
                }
            }

            return block;
        }

        public List<Cell> GetBlock(int row, int column, int candidateSize)
        {
            var block = new List<Cell>();

            int rowIndex = (row / 3) * 3;
            int columnIndex = (column / 3) * 3;

            for (int i = rowIndex; i < rowIndex + 3; ++i)
            {
                for (int j = columnIndex; j < columnIndex + 3; ++j)
                {
                    if (_gameBoard[i, j].Count >= candidateSize)
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
                var rowCells = GetRow(i, 2);
                var columnCells = GetColumn(i, 2);
                var blockCells = GetBlock(i, 2);

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
                if (cellSequence.Count >= 2 && HasNakedPairOfTwo(cellSequence, pivotIndex, true))
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

        private bool HasNakedPairOfTwo(List<Cell> cellSequence, int pivotIndex, bool isNakedDouble)
        {
            var pivotCell = cellSequence[pivotIndex];
            var pivotCandidates = _gameBoard[pivotCell.Row, pivotCell.Column];

            for (int compareCellIndex = pivotIndex + 1; compareCellIndex < cellSequence.Count; ++compareCellIndex)
            {
                var compareCell = cellSequence[compareCellIndex];
                var compareCandidates = _gameBoard[compareCell.Row, compareCell.Column];

                if (IsNakedDouble(pivotCandidates, compareCandidates, isNakedDouble) &&
                    (IsNewHint(compareCell.Row, compareCell.Column) ||
                    IsNewHint(pivotCell.Row, pivotCell.Column)))
                {
                    var hintCells = new List<Cell>
                    {
                        pivotCell,
                        compareCell
                    };

                    UpdateHints(hintCells);

                    _size = "Double";

                    return true;
                }
            }

            return false;
        }

        public bool IsNakedDouble(List<int> pivotCandidates, List<int> compareCandidates, bool isNakedDouble)
        {
            if (isNakedDouble && (pivotCandidates.Count != 2 || compareCandidates.Count != 2))
            {
                return false;
            }

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

                    int cellsWithSizeOfThree = (pivotCandidates.Count == 3 ? 1 : 0) + (secondCandidates.Count == 3 ? 1 : 0) + (thirdCandidates.Count == 3 ? 1 : 0);

                    bool correctSizes = cellsWithSizeOfThree <= 1;
                    bool isNotNakedDouble = !IsNakedDouble(pivotCandidates, secondCandidates, true) &&
                                            !IsNakedDouble(pivotCandidates, thirdCandidates, true) &&
                                            !IsNakedDouble(secondCandidates, thirdCandidates, true);
                    bool isNewHint = IsNewHint(pivotCell.Row, pivotCell.Column) ||
                                     IsNewHint(secondCell.Row, secondCell.Column) ||
                                     IsNewHint(thirdCell.Row, thirdCell.Column);

                    if (correctSizes && combined.Count == 3 && isNotNakedDouble && isNewHint)
                    {
                        var hintCells = new List<Cell>
                        {
                            pivotCell,
                            secondCell,
                            thirdCell
                        };

                        UpdateHints(hintCells);

                        _size = "Triple";

                        return true;
                    }
                }
            }

            return false;
        }

        private bool HasHiddenPair()
        {
            for (int i = 0; i < GAMEBOARD_SIZE; ++i)
            {
                if (HasHiddenSingle(GetBlock(i, 2)))
                {
                    _size = "single";

                    return true;
                }
                else if (TryFindHiddenPair(GetRow(i, 1)))
                {
                    _location = "row";

                    return true;
                }
                else if (TryFindHiddenPair(GetColumn(i, 1)))
                {
                    _location = "column";
                    
                    return true;
                }
                else if (TryFindHiddenPair(GetBlock(i, 1)))
                {
                    _location = "block";

                    return true;
                }
            }

            return false;
        }

        private bool HasHiddenSingle(List<Cell> emptyCells)
        {
            var candidatesCount = new int[9];

            foreach (var cell in emptyCells)
            {
                foreach (int candidate in _gameBoard[cell.Row, cell.Column])
                {
                    ++candidatesCount[candidate - 1];
                }
            }

            for (int i = 0; i < 9; ++i)
            {
                foreach (var cell in emptyCells)
                {
                    if (_gameBoard[cell.Row, cell.Column].Contains(i + 1) && candidatesCount[i] == 1 && IsNewHint(cell.Row, cell.Column))
                    {
                        _size = "single";

                        var hintCell = new List<Cell>
                        {
                            cell
                        };

                        UpdateHints(hintCell);

                        return true;
                    }
                }
            }

            return false;
        }

        private bool TryFindHiddenPair(List<Cell> cellSequence)
        {
            for (int pivotIndex = 0; pivotIndex < cellSequence.Count; ++pivotIndex)
            {
                if (cellSequence.Count >= 2 &&
                    HasNakedPairOfTwo(cellSequence, pivotIndex, false) &&
                    IsDoubleHiddenPair(cellSequence))
                {

                    return true;
                }
            }

            return false;
        }

        private bool IsDoubleHiddenPair(List<Cell> cellSequence)
        {
            var candidates = new int[9];
            var potentialPairCandidates = new List<int>();

            foreach (var cell in MarkedHint)
            {
                foreach (int candidate in _gameBoard[cell.Row, cell.Column])
                {
                    ++candidates[candidate - 1];

                    if (candidates[candidate - 1] == 2)
                    {
                        potentialPairCandidates.Add(candidate);
                    }
                }
            }

            foreach (int candidate in potentialPairCandidates)
            {
                foreach (var cell in cellSequence)
                {
                    if (!MarkedHint.Contains(cell))
                    {
                        foreach (int segmentCellCandidate in _gameBoard[cell.Row, cell.Column])
                        {
                            if (segmentCellCandidate == candidate)
                            {
                                ClearHiddenHints();

                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        private void ClearHiddenHints()
        {
            MarkedHint.Clear();

            for (int i = 1; i <= 2; ++i)
            {
                _usedHints.Remove(_usedHints[_usedHints.Count - 1]);
            }
        }

        public override string Message()
        {
            if (_size.Equals("single"))
            {
                return "This cell has single hidden candidate.";
            }
            else
            {
                return $"{_size} {_type} pair in the same {_location}. " +
                       $"Every other candidate of cell at the same {_location} can be deleted.";
            }
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
