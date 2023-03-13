using AdventOfCode.Tools.Models;

namespace Day13;

public enum CartTurnOption
{
    Left,
    Straight,
    Right
}

public class Cart
{
    public int Id { get; set; }
    public Position Location { get; set; }
    public char Direction { get; set; }
    public bool IsCrashed { get; set; }
    public CartTurnOption TurnOption { get; set; } = CartTurnOption.Left;
    private static readonly char[] _directions = new[] { '^', '>', 'v', '<' };
    private static int _index;

    public Cart(int x, int y, char direction)
    {
        Id = _index++;
        Location = new(x, y);
        Direction = direction;
    }

    public void MoveNext(Position location, char trackPiece)
    {
        Location = location;
        Direction = (Direction, trackPiece) switch
        {
            (_, '+') => HandleIntersection(),
            ('^', '/') or ('>', '\\') or ('v', '/') or ('<', '\\') => HandleRightTurn(),
            ('^', '\\') or ('>', '/') or ('v', '\\') or ('<', '/') => HandleLeftTurn(),
            _ => Direction
        };
    }

    private char HandleIntersection()
    {
        char newDirection = TurnOption switch
        {
            CartTurnOption.Left => HandleLeftTurn(),
            CartTurnOption.Right => HandleRightTurn(),
            _ => Direction
        };

        TurnOption = TurnOption == CartTurnOption.Right ? CartTurnOption.Left : TurnOption + 1;
        return newDirection;
    }

    private char HandleLeftTurn()
    {
        var newIndex = (Array.IndexOf(_directions, Direction) + 3) % 4;
        return _directions[newIndex];
    }

    private char HandleRightTurn()
    {
        var newIndex = (Array.IndexOf(_directions, Direction) + 1) % 4;
        return _directions[newIndex];
    }
}
