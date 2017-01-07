using storygen.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storygen
{
    class Circle : HitObject
    {
        public Circle(HitObjectType Type, Vector2 Position, double Time, String[] Parameters) : base(Type, Position, Time, Parameters)
        {
        }
    }
}
