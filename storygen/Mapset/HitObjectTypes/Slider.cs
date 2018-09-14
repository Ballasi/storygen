using storygen.Util;
using System;
using System.Collections.Generic;

namespace storygen
{
    class Slider : HitObject
    {
        String[] Parameters;
        double Velocity, Length;
        SliderCurveType CurveType;
        List<Vector2> PointsPosition;

        public Slider(HitObjectType Type, Vector2 Position, double Time, Color Color, double[] VelocityParameters, String[] Parameters) : base(Type, Position, Time, Color)
        {
            this.Parameters = Parameters;
            PointsPosition = new List<Vector2>();
            Velocity = ToVelocity(VelocityParameters[0], VelocityParameters[1]);

            String[] SliderValues = Parameters[0].Split('|');
            CurveType = getCurveType(SliderValues[0]);

            for (var i = 0; i < SliderValues.Length - 1; i++)
            {
                String[] RawPoint = SliderValues[i + 1].Split(':');
                int X = Int32.Parse(RawPoint[0]) + 64;
                int Y = Int32.Parse(RawPoint[1]) + 56;
                PointsPosition.Add(new Vector2(X, Y));
            }

            Length = Math.Round(Double.Parse(Parameters[2]), 4);
        }

        private SliderCurveType getCurveType(String Letter)
        {
            switch (Letter)
            {
                default:
                case "L": return SliderCurveType.Linear;
                case "C": return SliderCurveType.Catmull;
                case "B": return SliderCurveType.Bezier;
                case "P": return SliderCurveType.Perfect;
            }
        }

        private double ToVelocity(double BPM, double SV) => (100 * SV) / (60 * 1000 / BPM);
        public double getDuration() => Length / Velocity;
    }

    public enum SliderCurveType
    {
        Linear,
        Catmull,
        Bezier,
        Perfect
    }
}
