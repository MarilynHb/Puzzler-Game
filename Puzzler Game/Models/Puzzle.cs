namespace Puzzler_Game;
public class Puzzle
{
    public int? Name { get; set; }
    public int Value { get; set; }
    public int Index { get;set; }
    public bool IsEmpty { get; set; }
    public Puzzle(int value, int index, bool isEmpty = false)
    {
        Name = isEmpty ? null : value;
        Value = value;
        Index = index;
        IsEmpty = isEmpty;
    }
}
