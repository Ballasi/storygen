using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace storygen
{
    class Osb
    {
        public Layer Background, Fail, Pass, Foreground;

        public Origin TopLeft, TopCentre, TopRight, CentreLeft, Centre, CentreRight, BottomLeft, BottomCentre, BottomRight;

        public Easing Out, In, QuadIn, QuadOut, QuadInOut, CubicIn, CubicOut, CubicInOut, QuartIn,
                      QuartOut, QuartInOut, QuintIn, QuintOut, QuintInOut, SineIn, SineOut, SineInOut, ExpoIn, ExpoOut,
                      ExpoInOut, CircIn, CircOut, CircInOut, ElasticIn, ElasticOut, ElasticHalfOut, ElasticQuarterOut, ElasticInOut, BackIn,
                      BackOut, BackInOut, BounceIn, BounceOut, BounceInOut;

        public Random rnd;

        public Osb()
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            rnd = new Random();

            // Setting up Layers
            Background = new Layer(0);
            Fail = new Layer(1);
            Pass = new Layer(2);
            Foreground = new Layer(3);

            // Setting up Origins
            TopLeft = new Origin("TopLeft"); TopCentre = new Origin("TopCentre"); TopRight = new Origin("TopRight");
            CentreLeft = new Origin("CentreLeft"); Centre = new Origin("Centre"); CentreRight = new Origin("CentreRight");
            BottomLeft = new Origin("BottomLeft"); BottomCentre = new Origin("BottomCentre"); BottomRight = new Origin("BottomRight");

            // Setting up Easings
            Out = new Easing("Out"); In = new Easing("In");
            QuadIn = new Easing("QuadIn"); QuadOut = new Easing("QuadOut"); QuadInOut = new Easing("QuadInOut");
            CubicIn = new Easing("CubicIn"); CubicOut = new Easing("CubicOut"); CubicInOut = new Easing("CubicInOut");
            QuartIn = new Easing("QuartIn"); QuartOut = new Easing("QuartOut"); QuartInOut = new Easing("QuartInOut");
            QuintIn = new Easing("QuintIn"); QuintOut = new Easing("QuintOut"); QuintInOut = new Easing("QuintInOut");
            SineIn = new Easing("SineIn"); SineOut = new Easing("SineOut"); SineInOut = new Easing("SineInOut");
            ExpoIn = new Easing("ExpoIn"); ExpoOut = new Easing("ExpoOut"); ExpoInOut = new Easing("ExpoInOut");
            CircIn = new Easing("CircIn"); CircOut = new Easing("CircOut"); CircInOut = new Easing("CircInOut");
            ElasticIn = new Easing("ElasticIn"); ElasticOut = new Easing("ElasticOut"); ElasticHalfOut = new Easing("ElasticHalfOut"); ElasticQuarterOut = new Easing("ElasticQuarterOut"); ElasticInOut = new Easing("ElasticInOut");
            BackIn = new Easing("BackIn"); BackOut = new Easing("BackOut"); BackInOut = new Easing("BackInOut");
            BounceIn = new Easing("BounceIn"); BounceOut = new Easing("BounceOut"); BounceInOut = new Easing("BounceInOut");
        }

        public int Random(int Minimum, int Maximum)
        {
            return rnd.Next(Minimum, Maximum);
        }

        public double Random(double Minimum, double Maximum)
        {
            return rnd.NextDouble() * (Maximum - Minimum) + Minimum;
        }

        public void export(String FilePath)
        {
                // MAKING UP CONTENT
            String Content = "";

            Content += "[Events]\n//Background and Video events\n";

            Content += "//Storyboard Layer 0 (Background)\n";
            if (Background.getContent() != null) Content += Background.getContent();

            Content += "//Storyboard Layer 1 (Fail)\n";
            if (Fail.getContent() != null) Content += Fail.getContent();

            Content += "//Storyboard Layer 2 (Pass)\n";
            if (Pass.getContent() != null) Content += Pass.getContent();

            Content += "//Storyboard Layer 3 (Foreground)\n";
            if (Foreground.getContent() != null) Content += Foreground.getContent();

            Content += "//Storyboard Sound Samples\n";

                // CHECKING PATH
            String Path = FilePath;
            if (Path.Length > 4 && Path.Substring(Path.Length - 1) != "/") Path += ".osb";
            else if (Path.Length < 4 && Path.Substring(Path.Length - 1) != "/") Path += "/";
            if (Path.Length < 4 || Path.Substring(Path.Length - 4) != ".osb") Path += "storyboard.osb";

            System.IO.File.WriteAllText(Path, Content);
        }
    }
}
