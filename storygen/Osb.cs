using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storygen
{
    class Osb
    {
        public Layer Background, Fail, Pass, Foreground;

        public Origin TopLeft, TopCentre, TopRight, CentreLeft, Centre, CentreRight, BottomLeft, BottomCentre, BottomRight;

        public Osb()
        {
            Background = new Layer(0);
            Fail = new Layer(1);
            Pass = new Layer(2);
            Foreground = new Layer(3);

            TopLeft = new Origin("TopLeft");
            TopCentre = new Origin("TopCentre");
            TopRight = new Origin("TopRight");

            CentreLeft = new Origin("CentreLeft");
            Centre = new Origin("Centre");
            CentreRight = new Origin("CentreRight");

            BottomLeft = new Origin("BottomLeft");
            BottomCentre = new Origin("BottomCentre");
            BottomRight = new Origin("BottomRight");
        }

        public void render()
        {
            Console.WriteLine("[Events]\n//Background and Video events");

            Console.WriteLine("//Storyboard Layer 0 (Background)");
            if (Background.getContent() != null) Console.WriteLine(Background.getContent());

            Console.WriteLine("//Storyboard Layer 1 (Fail)");
            if (Fail.getContent() != null) Console.WriteLine(Fail.getContent());

            Console.WriteLine("//Storyboard Layer 2 (Pass)");
            if (Pass.getContent() != null) Console.WriteLine(Pass.getContent());

            Console.WriteLine("//Storyboard Layer 3 (Foreground)");
            if (Foreground.getContent() != null) Console.WriteLine(Foreground.getContent());
        }
    }
}
