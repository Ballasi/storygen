﻿using System;
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
            
            int TryAt = 63072;

            Sprite circle = Foreground.CreateSprite("SB/dot.png", Centre);
            circle.MoveX(62190, 64308, 10, 500);
            circle.MoveY(62190, 400);
            circle.Fade(62190, 65014, 1.0, 0.0);

            Sprite circle2 = Foreground.CreateSprite("SB/dot.png", Centre);
            circle2.MoveX(TryAt, (int) circle.getXPositionAt(TryAt));
            circle2.MoveY(ExpoOut, TryAt, TryAt + 1000, 400, 300);
            circle2.Fade(TryAt + 10000, circle.getOpacityAt(TryAt));
        }
    }
}
