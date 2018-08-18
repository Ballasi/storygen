using System;

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

        public Easing(int ID)
        {
            this.EasingName = Easings[ID];
            EasingID = ID;
        }

        public String getName()
            => EasingName;

        public int getID()
            => EasingID;

        /*
            The code here is mainly taken from Damnae's storybrew repository
            https://github.com/Damnae/storybrew/blob/master/common/Animations/EasingFunctions.cs
        */

        public static double Reverse(Func<double, double> function, double value) => 1 - function(1 - value);
        public static double ToInOut(Func<double, double> function, double value) => .5 * (value < .5 ? function(2 * value) : (2 - function(2 - 2 * value)));

        public static Func<double, double> Step = x => x >= 1 ? 1 : 0;
        public static Func<double, double> Linear = x => x;

        public static Func<double, double> QuadIn = x => x * x;
        public static Func<double, double> QuadOut = x => Reverse(QuadIn, x);
        public static Func<double, double> QuadInOut = x => ToInOut(QuadIn, x);
        public static Func<double, double> CubicIn = x => x * x * x;
        public static Func<double, double> CubicOut = x => Reverse(CubicIn, x);
        public static Func<double, double> CubicInOut = x => ToInOut(CubicIn, x);
        public static Func<double, double> QuartIn = x => x * x * x * x;
        public static Func<double, double> QuartOut = x => Reverse(QuartIn, x);
        public static Func<double, double> QuartInOut = x => ToInOut(QuartIn, x);
        public static Func<double, double> QuintIn = x => x * x * x * x * x;
        public static Func<double, double> QuintOut = x => Reverse(QuintIn, x);
        public static Func<double, double> QuintInOut = x => ToInOut(QuintIn, x);

        public static Func<double, double> SineIn = x => 1 - Math.Cos(x * Math.PI / 2);
        public static Func<double, double> SineOut = x => Reverse(SineIn, x);
        public static Func<double, double> SineInOut = x => ToInOut(SineIn, x);

        public static Func<double, double> ExpoIn = x => Math.Pow(2, 10 * (x - 1));
        public static Func<double, double> ExpoOut = x => Reverse(ExpoIn, x);
        public static Func<double, double> ExpoInOut = x => ToInOut(ExpoIn, x);

        public static Func<double, double> CircIn = x => 1 - Math.Sqrt(1 - x * x);
        public static Func<double, double> CircOut = x => Reverse(CircIn, x);
        public static Func<double, double> CircInOut = x => ToInOut(CircIn, x);

        public static Func<double, double> BackIn = x => x * x * ((1.70158 + 1) * x - 1.70158);
        public static Func<double, double> BackOut = x => Reverse(BackIn, x);
        public static Func<double, double> BackInOut = x => ToInOut((y) => y * y * ((1.70158 * 1.525 + 1) * y - 1.70158 * 1.525), x);

        public static Func<double, double> BounceIn = x => Reverse(BounceOut, x);
        public static Func<double, double> BounceOut = x => x < 1 / 2.75 ? 7.5625 * x * x : x < 2 / 2.75 ? 7.5625 * (x -= (1.5 / 2.75)) * x + .75 : x < 2.5 / 2.75 ? 7.5625 * (x -= (2.25 / 2.75)) * x + .9375 : 7.5625 * (x -= (2.625 / 2.75)) * x + .984375;
        public static Func<double, double> BounceInOut = x => ToInOut(BounceIn, x);

        public static Func<double, double> ElasticIn = x => Reverse(ElasticOut, x);
        public static Func<double, double> ElasticOut = x => Math.Pow(2, -10 * x) * Math.Sin((x - 0.075) * (2 * Math.PI) / .3) + 1;
        public static Func<double, double> ElasticInOut = x => ToInOut(ElasticIn, x);

        public static double Ease(Easing Easing, double Value)
        {
            return Ease(Easing.getID(), Value);
        }

        public static double Ease(int EasingID, double Value)
        {
            return EaseFunction(EasingID).Invoke(Value);
        }

        private static Func<double, double> EaseFunction(int EasingID)
        {
            switch (EasingID)
            {
                default:
                case 0: return Linear;
                case 1: return QuadOut;
                case 2: return QuadIn;
                case 3: return QuadIn;
                case 4: return QuadOut;
                case 5: return QuadInOut;
                case 6: return CubicIn;
                case 7: return CubicOut;
                case 8: return CubicInOut;
                case 9: return QuartIn;
                case 10: return QuartOut;
                case 11: return QuartInOut;
                case 12: return QuintIn;
                case 13: return QuintOut;
                case 14: return QuintInOut;
                case 15: return SineIn;
                case 16: return SineOut;
                case 17: return SineInOut;
                case 18: return ExpoIn;
                case 19: return ExpoOut;
                case 20: return ExpoInOut;
                case 21: return CircIn;
                case 22: return CircOut;
                case 23: return CircInOut;
                case 24: return ElasticIn;
                case 25: return ElasticOut;
                case 26: return ElasticOut;
                case 27: return ElasticOut;
                case 28: return ElasticInOut;
                case 29: return BackIn;
                case 30: return BackOut;
                case 31: return BackInOut;
                case 32: return BounceIn;
                case 33: return BounceOut;
                case 34: return BounceInOut;
            }
        }
    }
}
