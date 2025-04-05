using Sudoku.Models.GameElements;

namespace Sudoku.Models.Hint
{
    public class WingHint : Hint
    {
        private bool _isIndependent;
        private string _type;
        private string _location;

        public WingHint(string name, List<int>[,] gameboard) : base(name, gameboard)
        {
            _isIndependent = true;
            _type = "";
            _location = "";
        }

        public WingHint(string name, List<int>[,] gameboard, List<Cell> markedHints) : base(name, gameboard)
        {
            _isIndependent = false;
            _type = "";
            _location = "";
            MarkedHint = markedHints;
        }

        private bool HasXWing()
        {
            for (int i = 0; i < GAMEBOARD_SIZE; ++i)
            {
                if (HasRowXWing(i))
                {
                    _location = "Row";

                    return true;
                }
                else if (HasColumnXWing(i))
                {
                    _location = "Column";

                    return true;
                }
            }

            return false;
        }

        private bool HasRowXWing(int row)
        {
            for (int column = 0; column < GAMEBOARD_SIZE; ++column)
            {
                foreach (int candidate in _gameBoard[row, column])
                {
                    Cell? leftTopCell, rightTopCell, leftBottomCell, rightBottomCell;

                    leftTopCell = new Cell(row, column);
                    rightTopCell = ContainsCandidateInRow(candidate, leftTopCell);

                    if (rightTopCell != null)
                    {
                        leftBottomCell = ContainsCandidateInColumn(candidate, leftTopCell);
                        var otherTopRowPair = ContainsCandidateInRow(candidate, leftTopCell, rightTopCell.Column);
                    
                        if (leftBottomCell != null && otherTopRowPair == null)
                        {
                            rightBottomCell = ContainsCandidateAt(leftBottomCell.Row, rightTopCell.Column, candidate);
                            var otherBottomRowPair = rightBottomCell != null ? ContainsCandidateInRow(candidate, leftBottomCell, rightBottomCell.Column) : null;

                            if (rightBottomCell != null && otherBottomRowPair == null)
                            {
                                bool isNewHint = IsNewHint(leftTopCell.Row, leftTopCell.Column) ||
                                                 IsNewHint(rightTopCell.Row, rightTopCell.Column) || 
                                                 IsNewHint(leftBottomCell.Row, leftBottomCell.Column) || 
                                                 IsNewHint(rightBottomCell.Row, rightBottomCell.Column);

                                if (isNewHint)
                                {
                                    var hints = new List<Cell>
                                    {
                                        leftTopCell, rightTopCell,leftBottomCell, rightBottomCell
                                    };
                                    
                                    UpdateHints(hints);

                                    return true;
                                }
                            }
                        }
                    }

                    leftTopCell = null;
                    rightTopCell = null;
                    leftBottomCell = null;
                    rightBottomCell = null;
                }
            }

            return false;
        }

        private bool HasColumnXWing(int column)
        {
            for (int row = 0; row < GAMEBOARD_SIZE; ++row)
            {
                foreach (int candidate in _gameBoard[row, column])
                {
                    Cell? leftTopCell, rightTopCell, leftBottomCell, rightBottomCell;

                    leftTopCell = new Cell(row, column);
                    leftBottomCell = ContainsCandidateInColumn(candidate, leftTopCell);

                    if (leftBottomCell != null)
                    {
                        rightTopCell = ContainsCandidateInRow(candidate, leftTopCell);
                        var otherLeftColumnPair = ContainsCandidateInColumn(candidate, leftTopCell, leftBottomCell.Row);

                        if (rightTopCell != null && otherLeftColumnPair == null)
                        {
                            rightBottomCell = ContainsCandidateAt(leftBottomCell.Row, rightTopCell.Column, candidate);
                            var otherRightColumnPair = rightBottomCell != null ? ContainsCandidateInColumn(candidate, rightTopCell, rightBottomCell.Row) : null;

                            if (rightBottomCell != null && otherRightColumnPair == null)
                            {
                                bool isNewHint = IsNewHint(leftTopCell.Row, leftTopCell.Column) ||
                                                 IsNewHint(rightTopCell.Row, rightTopCell.Column) ||
                                                 IsNewHint(leftBottomCell.Row, leftBottomCell.Column) ||
                                                 IsNewHint(rightBottomCell.Row, rightBottomCell.Column);

                                if (isNewHint)
                                {
                                    var hints = new List<Cell>
                                    {
                                        leftTopCell, rightTopCell,leftBottomCell, rightBottomCell
                                    };

                                    UpdateHints(hints);

                                    return true;
                                }
                            }
                        }
                    }

                    leftTopCell = null;
                    rightTopCell = null;
                    leftBottomCell = null;
                    rightBottomCell = null;
                }
            }

            return false;
        }

        private Cell? ContainsCandidateAt(int row, int column, int candidate)
        {
            return _gameBoard[row, column].Contains(candidate) ? new Cell(row, column) : null;
        }

        private Cell? ContainsCandidateInRow(int candidate, Cell leftCell, int rightCellColumn = -1)
        {
            for (int column = 0; column < GAMEBOARD_SIZE; ++column)
            {
                if (column != leftCell.Column && column != rightCellColumn && _gameBoard[leftCell.Row, column].Contains(candidate))
                {
                    return new Cell(leftCell.Row, column);
                }
            }

            return null;
        }

        private Cell? ContainsCandidateInColumn(int candidate, Cell leftCell, int rightCellRow = -1)
        {
            for (int row = 0; row < GAMEBOARD_SIZE; ++row)
            {
                if (row != leftCell.Row && row != rightCellRow && _gameBoard[row, leftCell.Column].Contains(candidate))
                {
                    return new Cell(row, leftCell.Column);
                }
            }

            return null;
        }

        private bool HasYWing()
        {
            var pairHint = new PairHint("HelpClass", _gameBoard);

            for (int pivotRow = 0; pivotRow < GAMEBOARD_SIZE; ++pivotRow)
            {
                for (int pivotColumn = 0; pivotColumn < GAMEBOARD_SIZE; ++pivotColumn)
                {
                    if (_gameBoard[pivotRow, pivotColumn].Count == 2)
                    {
                        var pivotCell = new Cell(pivotRow, pivotColumn);

                        var row = pairHint.GetRow(pivotRow, 2);
                        var column = pairHint.GetColumn(pivotColumn, 2);
                        var block = pairHint.GetBlock(pivotRow, pivotColumn, 2);

                        if (HasPairInSegment(pivotCell, row, column, pairHint) || HasPairInSegment(pivotCell, row, block, pairHint) || HasPairInSegment(pivotCell, block, column, pairHint))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool HasPairInSegment(Cell pivotCell, List<Cell> firstSegment, List<Cell> secondSegment, PairHint pairHint)
        {
            var pivotCandidates = _gameBoard[pivotCell.Row, pivotCell.Column];

            foreach (var firstCompareCell in firstSegment)
            {
                if (IsPotentialYWingPair(pivotCell, firstCompareCell))
                {
                    var firstCompareCandidates = _gameBoard[firstCompareCell.Row, firstCompareCell.Column];

                    foreach (var secondCompareCell in secondSegment)
                    {
                        if (IsPotentialYWingPair(pivotCell, secondCompareCell))
                        {
                            var secondCompareCandidates = _gameBoard[secondCompareCell.Row, secondCompareCell.Column];

                            var combined = pivotCandidates.Union(firstCompareCandidates).Union(secondCompareCandidates).ToList();

                            bool isNakedDouble = pairHint.IsNakedDouble(pivotCandidates, firstCompareCandidates, true) ||
                                                 pairHint.IsNakedDouble(pivotCandidates, secondCompareCandidates, true) ||
                                                 pairHint.IsNakedDouble(firstCompareCandidates, secondCompareCandidates, true);
                            bool isNewHint = IsNewHint(pivotCell.Row, pivotCell.Column) ||
                                             IsNewHint(firstCompareCell.Row, firstCompareCell.Column) ||
                                             IsNewHint(secondCompareCell.Row, secondCompareCell.Column);

                            if (combined.Count == 3 && !isNakedDouble && isNewHint)
                            {
                                var hints = new List<Cell>
                                    {
                                        pivotCell, firstCompareCell, secondCompareCell
                                    };

                                UpdateHints(hints);

                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        private bool IsPotentialYWingPair(Cell pivotCell, Cell compareCell)
        {
            return (compareCell.Row != pivotCell.Row || compareCell.Column != pivotCell.Column) && _gameBoard[compareCell.Row, compareCell.Column].Count == 2;
        }

        public override string Message()
        {
            if (_type.Equals("X"))
            {
                string deleteSegment = _location.Equals("Row") ? "Column" : "Row";

                return $"{_location} X-Wing was found. Every cell candidate same as one in marked cells from the same {deleteSegment} can be deleted.";
            }
            else
            {
                return $"Y-Wing was found. Every cell candidate where the Y-Wing intersect might be deleted.";
            }
        }

        public override string? GetHint()
        {
            while (true)
            {
                _type = "X";
                if (HasXWing())
                {
                    return Message();
                }

                _type = "Y";
                if(HasYWing())
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
