using System;

namespace CleverMath
{
    public readonly struct Coord2
    {
        public readonly float x, y;

        public static Coord2 Zero { get; } = new Coord2(0, 0);
        public static Coord2 Up { get; } = new Coord2(0, 1);
        public static Coord2 Down { get; } = new Coord2(0, -1);
        public static Coord2 Left { get; } = new Coord2(-1, 0);
        public static Coord2 Right { get; } = new Coord2(1, 0);

        public Coord2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Coord2 operator *(Coord2 a, float b)
        {
            return new Coord2(a.x * b, a.y * b);
        }

        public static Coord2 operator *(float a, Coord2 b)
        {
            return new Coord2(b.x * a, b.y * a);
        }

        public static Coord2 operator /(Coord2 a, float b)
        {
            return new Coord2(a.x / b, a.y / b);
        }

        public static float SquareMagnitude(Coord2 coord2)
        {
            return coord2.x * coord2.x + coord2.y * coord2.y;
        }

        public static float Magnitude(Coord2 coord2)
        {
            return (float)Math.Sqrt(SquareMagnitude(coord2));
        }

        public static Coord2 operator +(Coord2 a, Coord2 b)
        {
            return new Coord2(a.x + b.x, a.y + b.y);
        }

        internal static Coord2 Normalize(Coord2 coord2)
        {
            var magnitude = Coord2.Magnitude(coord2);
            if (magnitude < 0.00001f)
            {
                return Coord2.Zero;
            }
            else
            {
                return coord2 / magnitude;
            }
        }

        public static Coord2 operator -(Coord2 a, Coord2 b)
        {
            return new Coord2(a.x - b.x, a.y - b.y);
        }

        public static Coord2 operator -(Coord2 a)
        {
            return new Coord2(-a.x, -a.y);
        }

        public static float Dot(Coord2 a, Coord2 b)
        {
            return a.x * b.x + a.y * b.y;
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }

        public override bool Equals(object obj)
        {
            return obj is Coord2 coord &&
                   x == coord.x &&
                   y == coord.y;
        }

        public override int GetHashCode()
        {
            int hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }

        public static implicit operator string(Coord2 value)
        {
            return value.ToString();
        }
    }
}