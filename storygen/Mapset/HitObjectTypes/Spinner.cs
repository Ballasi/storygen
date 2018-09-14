using storygen.Util;

namespace storygen
{
    class Spinner : HitObject
    {
        public double endTime;
        public override double EndTime => endTime;

        public Spinner(HitObjectType Type, Vector2 Position, double StartTime, Color Color, double EndTime) : base(Type, Position, StartTime, Color)
        {
            endTime = EndTime;
        }
    }
}
