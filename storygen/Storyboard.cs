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
            
            int TryAt = 64484;

            Sprite circle = Foreground.CreateSprite("bg.jpg", Centre);
            circle.Fade(62190, 1.0);
            circle.MoveX(62190, 64308, 10, 500);
            circle.MoveY(12, 62896, 65014, 400, 100);
            circle.Scale(62190, 65014, 0.1, 0.02);
            circle.Fade(TryAt, 0.0);
            circle.Rotate(12, 62190, 65014, 0, 2 * Math.PI);

            Sprite circle2 = Foreground.CreateSprite("bg.jpg", Centre);
            circle2.Move(TryAt, (int) circle.getXPositionAt(TryAt), (int) circle.getYPositionAt(TryAt));
            circle2.Scale(ExpoOut, TryAt, circle.getScaleAt(TryAt));
            circle2.Fade(TryAt, TryAt + 500, 1.0, 0.0);
            circle2.Rotate(TryAt, circle.getRotationAt(TryAt));
        }
    }
}
