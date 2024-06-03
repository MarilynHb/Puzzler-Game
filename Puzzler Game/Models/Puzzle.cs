namespace Puzzler_Game;
public class Puzzle
{
    public int Value { get; set; }
    public int Index { get;set; }
    public Puzzle(int value, int index)
    {
        Value = value;
        Index = index;
    }
}
