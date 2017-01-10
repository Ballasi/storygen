using storygen.Util;

namespace storygen
{
    class Spinner : HitObject
    {
        public double endTime;
        public override double EndTime => endTime;

        public Spinner(HitObjectType Type, Vector2 Position, double StartTime, double EndTime) : base(Type, Position, StartTime)
        {
            endTime = EndTime;
        }
    }
}
