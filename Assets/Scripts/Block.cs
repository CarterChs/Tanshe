using CleverMath;
using GameSystem;

public class Block : IPhysicalObject
{
    private int blockID;

    public Block(int blockID, int x, int y)
    {
        this.blockID = blockID;
        Position = new Coord2(x, y);
        Restitution = 0.5f;
    }

    public bool IsMovable => false;

    public Coord2 Position { get; set; }

    public Coord2 Velocity { get; set; }

    public float Length => 1;

    public float Width => 1;

    public bool IsTrigger => false;

    public float Restitution { get; set; }

    public float Mass => 100;
}