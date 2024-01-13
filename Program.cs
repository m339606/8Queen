using Queen8;

var N = 8;
var nQueen = new NQueen();
var board = new Board(N);
nQueen.Drilling(board);

Console.WriteLine($"解答數量:{nQueen.AnswerBoards.Count}");

foreach (var answerBoard in nQueen.AnswerBoards)
    Console.WriteLine(answerBoard.ToString());