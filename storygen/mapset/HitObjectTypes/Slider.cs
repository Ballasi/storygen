﻿using storygen.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storygen
{
    class Slider : HitObject
    {
        public Slider(HitObjectType Type, Vector2 Position, double Time, String[] Parameters) : base(Type, Position, Time, Parameters)
        {
        }
    }
}