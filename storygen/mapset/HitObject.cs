﻿using storygen.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storygen
{
    class HitObject
    {
        HitObjectType Type;
        Vector2 Position;
        double Time;
        String[] Parameters;

        public HitObject()
        {
        }

        public HitObject(HitObjectType Type, Vector2 Position, double Time, String[] Parameters)
        {
            this.Type = Type;
            this.Position = Position;
            this.Time = Time;
            this.Parameters = Parameters;
        }

        public HitObjectType getType() => Type.HasFlag(HitObjectType.Circle) ? HitObjectType.Circle : (Type.HasFlag(HitObjectType.Slider) ? HitObjectType.Slider : HitObjectType.Spinner);
        public double getTime() => Time;
        public Vector2 getPosition() => Position;
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