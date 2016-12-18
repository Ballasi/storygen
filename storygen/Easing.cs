using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storygen
{
    class Easing
    {
        String EasingName;
        int EasingID;
        String[] Easings = new string[] { "Out", "In", "QuadIn", "QuadOut", "QuadInOut", "CubicIn", "CubicOut", "CubicInOut", "QuartIn",
                                          "QuartOut", "QuartInOut", "QuintIn", "QuintOut", "QuintInOut", "SineIn", "SineOut", "SineInOut", "ExpoIn", "ExpoOut",
                                          "ExpoInOut", "CircIn", "CircOut", "CircInOut", "ElasticIn", "ElasticOut", "ElasticHalfOut", "ElasticQuarterOut", "ElasticInOut", "BackIn",
                                          "BackOut", "BackInOut", "BounceIn", "BounceOut", "BounceInOut" };

        public Easing(String EasingName)
        {
            this.EasingName = EasingName;

            EasingID = 0;
            for (int i = 1; i <= Easings.Length; i++)
            {
                if (Easings[i - 1] == EasingName) EasingID = i;
            }
        }

        public String getName()
        {
            return EasingName;
        }

        public int getID()
        {
            return EasingID;
        }
    }
}
