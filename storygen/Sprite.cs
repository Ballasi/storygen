﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storygen
{
    class Sprite
    {
        String Path;
        Origin Origin;
        String Affections;

        public Sprite(String Path, Origin Origin)
        {
            this.Path = Path;
            this.Origin = Origin;
            Affections = "";
        }

        public String getPath()
        {
            return Path;
        }

        public Origin getOrigin()
        {
            return Origin;
        }

        public String getAffections()
        {
            return Affections;
        }

        public void Move(int Time, int X, int Y) { Affections += "\n M,0," + Time + ",," + X + "," + Y; }
        public void Move(int StartTime, int EndTime, int StartX, int StartY, int EndX, int EndY) { Affections += "\n M,0," + StartTime + "," + EndTime + "," + StartX + "," + StartY + "," + EndX + "," + EndY; }
        public void Fade(int Time, double Fade) { Affections += "\n F,0," + Time + ",," + Fade; }
        public void Fade(int StartTime, int EndTime, int StartFade, int EndFade) { Affections += "\n F,0," + StartTime + "," + EndTime + "," + StartFade + "," + EndFade; }
    }
}
