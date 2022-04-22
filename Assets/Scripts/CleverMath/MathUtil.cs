using System;

namespace CleverMath
{
    public static class MathUtil
    {
        private static readonly Coord2Int[] GRAD_2D =
        {
            new Coord2Int(-1,-1), new Coord2Int( 1,-1), new Coord2Int(-1, 1), new Coord2Int( 1, 1),
            new Coord2Int( 0,-1), new Coord2Int(-1, 0), new Coord2Int( 0, 1), new Coord2Int( 1, 0),
        };

        private static float SmoothLerp(float a, float b, float t)
        {
            t = t * t * t * (6 * t * t - 15 * t + 10);
            return a + (b - a) * t;
        }

        private static Coord2Int GetGradient2D(int x, int y, int seed)
        {
            int hash = Random(x, y, seed);
            hash = hash > 0 ? hash : hash + int.MaxValue;
            int index = hash % 8;
            return GRAD_2D[index];
        }

        private static float DotGridGradient2D(int ix, int iy, float x, float y, int seed)
        {
            float dx = x - ix;
            float dy = y - iy;

            Coord2Int gradient = GetGradient2D(ix, iy, seed);
            return dx * gradient.x + dy * gradient.y;
        }

        /// <summary>
        /// 范围约为-0.633~0.633
        /// 返回时候可以根据需求通过*1.578955678714098将值定在-1~1,此时占比约算:
        /// -1~-0.8 : 2.2%
        /// -0.8~-0.6 : 9.0%
        /// -0.6~-0.4 : 9.2%
        /// -0.4~-0.2 : 12.8%
        /// -0.2~0 : 16.6%
        /// ...整数与之对称
        /// 0.8~1 : 2.2%
        /// </summary>
        /// <param name="seed"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static float PerlinNoise2(int seed, float x, float y)
        {
            // Determine grid cell coordinates
            int x0 = (int)Math.Floor(x);
            int x1 = x0 + 1;
            int y0 = (int)Math.Floor(y);
            int y1 = y0 + 1;
            // Determine interpolation weights
            // Could also use higher order polynomial/s-curve here
            float sx = x - x0;
            float sy = y - y0;
            // Interpolate between grid point gradients
            float n0, n1, ix0, ix1, value;
            n0 = DotGridGradient2D(x0, y0, x, y, seed);
            n1 = DotGridGradient2D(x1, y0, x, y, seed);
            ix0 = SmoothLerp(n0, n1, sx);
            n0 = DotGridGradient2D(x0, y1, x, y, seed);
            n1 = DotGridGradient2D(x1, y1, x, y, seed);
            ix1 = SmoothLerp(n0, n1, sx);
            value = SmoothLerp(ix0, ix1, sy);

            return value;
        }

        //Cantor pairing function
        public static int CantorPair(int a, int b)
        {
            return (a + b) * (a + b + 1) / 2 + b;
        }

        //Linear congruential generator
        public static int LCGRandom(int v)
        {
            return (1140671485 * v + 12820163) % 16777216;
        }

        public static int Random(int a, int b, int seed)
        {
            int value = CantorPair(a, b);
            return CantorPair(LCGRandom(seed), LCGRandom(value));
        }

        public static int Random(int a, int seed)
        {
            return CantorPair(LCGRandom(seed), LCGRandom(a));
        }
    }
}