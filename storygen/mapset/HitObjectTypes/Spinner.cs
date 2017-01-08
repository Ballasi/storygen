using storygen.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
