using AdventOfCode.Tools.Models;

namespace Day13;

public class Program
{
    private static readonly string[] _input = Input.ReadAllLines();
    private static void Main()
    {
        Console.WriteLine($"Part 1: {SimulateCarts()}");
        Console.WriteLine($"Part 2: {SimulateCarts(true)}");
    }

    private static string SimulateCarts(bool removeCrashedCarts = false)
    {
        var (track, carts) = LoadMine(_input);
        var keepGoing = true;
        Position result = new(-1,-1);

        while (keepGoing)
        {
            // move each of the carts, they move in the order of their positions, so sort by column then row
            carts = carts.OrderBy(x => x.Location.Y).ThenBy(x => x.Location.X).ToList();

            foreach (var cart in carts)
            {
                if (cart.IsCrashed) continue;

                var nextPosition = cart.Location.MoveInDirection(cart.Direction);
                var nextPiece = track[(nextPosition.X, nextPosition.Y)];

                cart.MoveNext(nextPosition, nextPiece);

                if (carts.Any(x => x.Location == nextPosition && x.Id != cart.Id && !x.IsCrashed))
                {
                    if (removeCrashedCarts)
                    {
                        cart.IsCrashed = true;
                        carts.Single(x => x.Location == nextPosition && x.Id != cart.Id && !x.IsCrashed).IsCrashed = true;
                    }
                    else
                    {
                        result = nextPosition;
                        keepGoing = false;
                        break;
                    }
                }
            }

            if (removeCrashedCarts && carts.Count(x => !x.IsCrashed) <= 1)
            {
                result = carts.Single(x => !x.IsCrashed).Location;
                break;
            }
        }

        return result.ToString();
    }

    private static readonly char[] trackChars = new[] { '-', '|', '+', '/', '\\' };
    private static readonly char[] cartChars = new[] { '^', '>', 'v', '<' };

    private static (Dictionary<(int, int), char> Track, List<Cart> Carts) LoadMine(string[] input)
    {
        var track = new Dictionary<(int, int), char>();
        var carts = new List<Cart>();

        for (int row = 0; row < input.Length; row++)
        {
            for (int col = 0; col < input[0].Length; col++)
            {
                var current = input[row][col];

                if (trackChars.Contains(current))
                {
                    track.Add((col, row), current);
                }
                else if (cartChars.Contains(current))
                {
                    carts.Add(new(col, row, current));
                    char piece = current switch
                    {
                        '^' or 'v' => '|',
                        '<' or '>' => '-'
                    };
                    track.Add((col, row), piece); // track piece under the cart
                }
            }
        }

        return (track, carts);
    }
}
