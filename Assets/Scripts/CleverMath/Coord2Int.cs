namespace CleverMath
{
    public struct Coord2Int
    {
        public readonly int x, y;
        public static Coord2Int Zero { get; } = new Coord2Int(0, 0);
        public static Coord2Int Up { get; } = new Coord2Int(0, 1);
        public static Coord2Int Down { get; } = new Coord2Int(0, -1);
        public static Coord2Int Left { get; } = new Coord2Int(-1, 0);
        public static Coord2Int Right { get; } = new Coord2Int(1, 0);
        public Coord2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            return obj is Coord2Int @int &&
                   x == @int.x &&
                   y == @int.y;
        }

        public override int GetHashCode()
        {
            int hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }

    }
}