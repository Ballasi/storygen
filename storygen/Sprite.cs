using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storygen
{
    class Sprite
    {
        String Path, Affections;
        Origin Origin;

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
        
            // Linear Events
        public void Move(int Time, int X, int Y) { Move(0, Time, X, Y); }
        public void Move(int StartTime, int EndTime, int StartX, int StartY, int EndX, int EndY) { Move(0, StartTime, EndTime, StartX, StartY, EndX, EndY); }

        public void Fade(int Time, double Opacity) { Fade(0, Time, Opacity); }
        public void Fade(int StartTime, int EndTime, double StartOpacity, double EndOpacity) { Fade(0, StartTime, EndTime, StartOpacity, EndOpacity); }

        public void Scale(int Time, double Ratio) { Scale(0, Time, Ratio); }
        public void Scale(int StartTime, int EndTime, double StartRatio, double EndRatio) { Scale(0, StartTime, EndTime, StartRatio, EndRatio); }

            // Events by Easings
        public void Move(Easing Easing, int Time, int X, int Y) { Move(Easing.getID(), Time, X, Y); }
        public void Move(Easing Easing, int StartTime, int EndTime, int StartX, int StartY, int EndX, int EndY) { Move(Easing.getID(), StartTime, EndTime, StartX, StartY, EndX, EndY); }

        public void Fade(Easing Easing, int Time, double Opacity) { Fade(Easing.getID(), Time, Opacity); }
        public void Fade(Easing Easing, int StartTime, int EndTime, double StartOpacity, double EndOpacity) { Fade(Easing.getID(), StartTime, EndTime, StartOpacity, EndOpacity); }

        public void Scale(Easing Easing, int Time, double Ratio) { Scale(Easing.getID(), Time, Ratio); }
        public void Scale(Easing Easing, int StartTime, int EndTime, double StartRatio, double EndRatio) { Scale(Easing.getID(), StartTime, EndTime, StartRatio, EndRatio); }

            // Events by Easing IDs
        public void Move(int EasingID, int Time, int X, int Y) { Affections += "\n M," + EasingID + "," + Time + ",," + X + "," + Y; }
        public void Move(int EasingID, int StartTime, int EndTime, int StartX, int StartY, int EndX, int EndY) { Affections += "\n M," + EasingID + "," + StartTime + "," + EndTime + "," + StartX + "," + StartY + "," + EndX + "," + EndY; }

        public void Fade(int EasingID, int Time, double Opacity) { Affections += "\n F," + EasingID + "," + Time + ",," + Opacity; }
        public void Fade(int EasingID, int StartTime, int EndTime, double StartOpacity, double EndOpacity) { Affections += "\n F," + EasingID + "," + StartTime + "," + EndTime + "," + StartOpacity + "," + EndOpacity; }

        public void Scale(int EasingID, int Time, double Ratio) { Affections += "\n S," + EasingID + "," + Time + ",," + Ratio; }
        public void Scale(int EasingID, int StartTime, int EndTime, double StartRatio, double EndRatio) { Affections += "\n S," + EasingID + "," + StartTime + "," + EndTime + "," + StartRatio + "," + EndRatio; }
    }
}
