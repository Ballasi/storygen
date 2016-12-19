using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storygen
{
    class Storyboard : Osb
    {
        public Storyboard()
        {
            Sprite bg = Background.createSprite("bg.jpg", Centre);
            bg.Fade(0, 0.2);
            bg.Scale(201487, 0.625);

            for (int i = 0; i < 2000; i += 100)
            {
                int x = Random(-100, 750);
                double scale = Random(0.2, 0.7);

                Sprite s = Foreground.createSprite("SB/dot.png", Centre);
                s.Move(i, i + Random(1000, 2000), x, -10, x, 500);
                s.Scale(i, scale);
            }
        }
    }
}
