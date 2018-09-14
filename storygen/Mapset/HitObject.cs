using storygen.Util;
using System;

namespace storygen
{
    class HitObject
    {
        HitObjectType Type;
        Vector2 Position;
        public double StartTime;
        public Color Color { get; }
        public virtual double EndTime => StartTime;

        public HitObject()
        {
        }

        public HitObject(HitObjectType Type, Vector2 Position, double Time, Color Color)
        {
            this.Type = Type;
            this.Position = Position;
            StartTime = Time;
            this.Color = Color;
        }

        public HitObjectType getType() => Type.HasFlag(HitObjectType.Circle) ? HitObjectType.Circle : (Type.HasFlag(HitObjectType.Slider) ? HitObjectType.Slider : HitObjectType.Spinner);
        public virtual Vector2 PositionAt(double Time) => getInitialPosition();
        public Vector2 getInitialPosition() => Position;
    }

    [Flags]
    public enum HitObjectType
    {
        Circle = 1,
        Slider = 2,
        NewCombo = 4,
        Spinner = 8,
        SkipColor1 = 16,
        SkipColor2 = 32,
        SkipColor3 = 64,
        Hold = 128
    }
}
