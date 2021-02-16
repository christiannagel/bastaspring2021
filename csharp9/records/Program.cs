using System;

Rectangle r1 = new(new Position(33, 22), new Size(200, 100));
Rectangle r2 = r1 with { Position = new Position(100, 22) };
Ellipse e1 = new(new Position(122, 200), new Size(40, 20));

Rectangle r3 = r2 with { Position = new Position(33, 22) };

// equality
if (!object.ReferenceEquals(r1, r3))
{
    Console.WriteLine("r1 and r3 are not the same reference");
}

if (r1 == r3)
{
    Console.WriteLine("r1 and r3 are equal");
}

// deconstruction
(Position p, Size s) = r1;
Console.WriteLine($"rectangle r1 is in this position: {p}");

((int x, int y), _) = r1;
Console.WriteLine($"rectangle r1 is in this position: {x} {y}");


DisplayShapes(r1, r2, e1);

void DisplayShapes(params Shape[] shapes)
{
    foreach (var shape in shapes)
    {
        shape.Draw();
    }
}

public record Position(int X, int Y);

public record Size(int Width, int Height);

public abstract record Shape(Position Position, Size Size)
{
    public void Draw() => DisplayShape();

    protected virtual void DisplayShape()
    {
        Console.WriteLine($"Shape with {Position} and {Size}");
    }
}

public record Rectangle(Position Position, Size Size) : Shape(Position, Size)
{
    protected override void DisplayShape()
    {
        Console.WriteLine($"Rectangle at position {Position} with size {Size}");
    }
}

public record Ellipse(Position Position, Size Size) : Shape(Position, Size)
{
    protected override void DisplayShape()
    {
        Console.WriteLine($"Ellipse at position {Position} with size {Size}");
    }
}