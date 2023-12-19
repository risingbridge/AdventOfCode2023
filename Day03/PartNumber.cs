using System.Numerics;

namespace Day03;

public class PartNumber{
    public PartNumber(int number, List<Vector2>? positions)
    {
        Number = number;
        Positions = positions;
    }

    public int Number { get; set; }
    public List<Vector2> Positions { get; set; } = [];
}