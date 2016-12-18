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
            Sprite sprite = Background.createSprite("SB/dot.png", Centre);
            sprite.Move(Out, 152, 135, 12);
            sprite.Move(ExpoIn, 152, 192, -120, 130, 150, 130);
            sprite.Fade(185, 0.5);

            Sprite sprite2 = Foreground.createSprite("SB/dot2.png", TopLeft);
            sprite2.Move(152, 192, 120, 130, 150, 130);
        }
    }
}
