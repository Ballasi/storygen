using storygen.Other;
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
            Sprite bg = Background.CreateSprite("bg.jpg", Centre);
            bg.Fade(0, 0.5);
            bg.Scale(62190, 0.625);

            for (int i = 0; i < 11367; i += 100)
            {
                int x = Random(-100, 750);
                double scale = Random(0.2, 0.7);

                Sprite s = Foreground.CreateSprite("SB/dot.png", Centre);
                s.Move(i, i + Random(1000, 2000), x, -10, x, 500);
                s.Scale(i, scale);
            }

            Sprite sm = Background.CreateSprite("bg.jpg", Centre);
            sm.BeginLoop(39602, 32);
                sm.Fade(In, 0, 75, 0.0, 0.4);
                sm.Scale(In, 0, 75, 0.625, 0.64);
                sm.Fade(Out, 75, 705, 0.4, 0.0);
                sm.Scale(Out, 75, 705, 0.64, 0.625);
            sm.EndLoop();
            
            int TryAt = 63130;

            Sprite circle = Foreground.CreateSprite("SB/dot.png", Centre);
            circle.Move(BounceIn, 62190, 65014, 20, 50, 300, 200.5);
            circle.Fade(62190, 1.0);
            circle.Fade(TryAt, 0.0);

            Vector2 pos = circle.getPositionAt(TryAt);

            Sprite circle2 = Foreground.CreateSprite("SB/dot.png", Centre);
            circle2.Move(TryAt, pos.X, pos.Y);
            circle2.Scale(ExpoOut, TryAt, TryAt + 1000, 1.0, 2.0);
            circle2.Fade(TryAt + 200, TryAt + 1200, 1.0, 0.0);
        }
    }
}
