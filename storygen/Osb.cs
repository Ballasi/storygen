using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storygen
{
    class Osb
    {
        public Layer Background;
        public Layer Fail;
        public Layer Pass;
        public Layer Foreground;

        public Origin Centre;
        public Origin TopLeft;

        public Osb()
        {
            Background = new Layer(0);
            Fail = new Layer(1);
            Pass = new Layer(2);
            Foreground = new Layer(3);
            Centre = new Origin("Centre");
            TopLeft = new Origin("TopLeft");
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
