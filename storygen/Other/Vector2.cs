using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storygen.Other
{
    class Vector2
    {
        public double X;
        public double Y;

        public Vector2()
        {
        }

        public Vector2(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public static double Distance(Vector2 Vec1, Vector2 Vec2)
            => Math.Abs(Math.Sqrt(Math.Pow(Vec2.X - Vec1.X, 2) + Math.Pow(Vec2.Y - Vec1.Y, 2)));

        public static double Angle(Vector2 Vec1, Vector2 Vec2)
            => Math.Atan2(Vec2.Y - Vec1.Y, Vec2.X - Vec1.X);
    }
}
