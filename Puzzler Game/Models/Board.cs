#nullable disable
namespace Puzzler_Game;
public class Board
{
    public Board(GameLevel level)
    {
        var tiles = new List<Puzzle>();
        int totalTiles = (int)level * (int)level;

        var indexes = new List<int>();
        for (int i = 0; i < totalTiles; i++)
        {
            indexes.Add(i);
        }

        var random = new Random();
        for (int value = 0; value < totalTiles; value++)
        {
            // Choose a random position from the list and remove it
            int index = random.Next(indexes.Count);
            var i = indexes[index];
            indexes.RemoveAt(index);

            tiles.Add(new Puzzle(value, i, value == totalTiles - 1));
        }
        // Add the empty tile
        //tiles.Add(new Puzzle(totalTiles, totalTiles / (int)level, totalTiles % (int)level));
        GameState = tiles.OrderBy(t => t.Index).ToList();
        GridSize = (int)level;
    }

    public List<Puzzle> GameState { get; set; }
    public int GridSize;
    public int EmptyFieldIndex => GameState.FindIndex(t => t.IsEmpty);
    public bool IsSolved => GameState.All(t => t.Value == t.Index);

    #region Get Possible Moves
    private int GetRowNumber(int index, int gridSize) => index / gridSize;
    private int GetColumnNumber(int index, int gridSize) => index % gridSize;

    public List<int> GetPossibleMoves()
    {
        int emptyRow = GetRowNumber(EmptyFieldIndex, GridSize);
        int emptyColumn = GetColumnNumber(EmptyFieldIndex, GridSize);
        var options = new List<int>();

        if (GetRowNumber(EmptyFieldIndex - 1, GridSize) == emptyRow) options.Add(EmptyFieldIndex - 1);
        if (GetRowNumber(EmptyFieldIndex + 1, GridSize) == emptyRow) options.Add(EmptyFieldIndex + 1);
        if (EmptyFieldIndex + GridSize < GridSize * GridSize && GetColumnNumber(EmptyFieldIndex + GridSize, GridSize) == emptyColumn)
            options.Add(EmptyFieldIndex + GridSize);
        if (EmptyFieldIndex - GridSize >= 0 && GetColumnNumber(EmptyFieldIndex - GridSize, GridSize) == emptyColumn)
            options.Add(EmptyFieldIndex - GridSize);

        return options;
    }
    #endregion

    #region Swap Puzzle
    public void SwapPuzzle(int index1, int index2)
    {
        var tempPuzzle = GameState[index1];
        GameState[index1].Index = GameState[index2].Index;
        GameState[index2].Index = index1;
        GameState = GameState.OrderBy(t => t.Index).ToList();
    }
    #endregion
}

public enum GameLevel
{
    Easy = 3,
    Medium = 4,
    Hard = 5
}
