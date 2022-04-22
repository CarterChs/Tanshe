using CleverMath;
namespace GameSystem
{
    internal interface IPhysicalObject
    {
        bool IsMovable { get; }
        Coord2 Position { get; set; }
        Coord2 Velocity { get; set; }
        float Length { get; }
        float Width { get; }
        bool IsTrigger { get; }
        float Restitution { get; }
        float Mass { get; }
    }
}