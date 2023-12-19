using System.Numerics;

namespace Day03;

public class Symbol{
    public Symbol(Vector2 position, char character)
    {
        Position = position;
        Character = character;
    }

    public Vector2 Position { get; set; }
    public char Character { get; set; }
}