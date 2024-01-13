namespace Queen8;

public class Board : ICloneable, IEquatable<Board>
{
    private readonly bool?[][] _board;
    private readonly int _nSize;

    public Board(int nSize)
    {
        _nSize = nSize;
        _board = new bool?[_nSize][];
        for (var _ = 0; _ < _nSize; _++) _board[_] = new bool?[_nSize];
    }

    public object Clone()
    {
        var cloned = new Board(_nSize);

        for (var r = 0; r < _nSize; r++)
        {
            cloned._board[r] = new bool?[_nSize];
            for (var c = 0; c < _nSize; c++)
                cloned._board[r][c] = _board[r][c];
        }

        return cloned;
    }

    public bool Equals(Board other)
    {
        for (var r = 0; r < _nSize; r++)
        for (var c = 0; c < _nSize; c++)
            if (_board[r][c] != other._board[r][c])
                return false;

        return true;
    }

    /// <summary>
    ///     確認棋盤是否已擺滿N個Queen
    /// </summary>
    /// <returns></returns>
    public bool IsDone()
    {
        return _board
            .SelectMany(r => r)
            .Count(c => c == true) >= _nSize;
    }

    /// <summary>
    ///     取出下一步的所有能性
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BoardPoint> GetAllPossiblePoints(int atRow)
    {
        if (atRow < _nSize)
            for (var c = 0; c < _nSize; c++)
                if (_board[atRow][c] == null)
                    yield return new BoardPoint(atRow, c);
    }

    /// <summary>
    ///     取出下一步的所有能性
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BoardPoint> GetAllPossiblePoints()
    {
        for (var r = 0; r < _nSize; r++)
        for (var c = 0; c < _nSize; c++)
            if (_board[r][c] == null)
                yield return new BoardPoint(r, c);
    }

    /// <summary>
    ///     放置Queen
    /// </summary>
    /// <returns>The Board</returns>
    public void PlaceQueen(BoardPoint point)
    {
        // 確保要放的位置還沒被標記過才能放
        if (_board[point.Row][point.Col] != null)
            throw new ArgumentOutOfRangeException("The point is invalid");

        // 設置禁區 for Row
        for (var _ = 0; _ < _nSize; _++) _board[_][point.Col] = false;

        // 設置禁區 for Col
        for (var _ = 0; _ < _nSize; _++) _board[point.Row][_] = false;

        // 右下禁區
        for (int r = point.Row + 1, c = point.Col + 1; r < _nSize && c < _nSize; r++, c++)
            _board[r][c] = false;

        // 左下禁區
        for (int r = point.Row + 1, c = point.Col - 1; r < _nSize && c >= 0; r++, c--)
            _board[r][c] = false;

        // 放Queen
        _board[point.Row][point.Col] = true;
    }

    public override bool Equals(object? obj)
    {
        return obj is Board other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_nSize, _board);
    }

    public override string ToString()
    {
        var str = "";
        for (var r = 0; r < _nSize; r++)
        {
            for (var c = 0; c < _nSize; c++) str += _board[r][c] == null ? "N" : _board[r][c]!.Value ? "Q" : ".";

            str += "\r\n";
        }

        return str;
    }
}