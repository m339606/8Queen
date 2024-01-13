namespace Queen8;

public class NQueen
{
    public List<Board> AnswerBoards = new(); // 答案棋盤

    public void Drilling(Board board, int row = 0)
    {
        // 加入解答
        if (board.IsDone())
        {
            AnswerBoards.Add(board);
            return;
        }

        // 取得可能位置
        var allPoints = board.GetAllPossiblePoints(row);
        foreach (var point in allPoints)
        {
            var _board = (Board)board.Clone();
            _board.PlaceQueen(point);
            Drilling(_board, row + 1);
        }
    }
}