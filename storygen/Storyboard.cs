using storygen.Elements;
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
        public Storyboard(String FolderPath) : base(FolderPath)
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

            int poss = 600;
            for (int i = 1; i <= 5; i++)
            {
                int FC = 10;
                if (i == 4) FC = 6;

                int Delay = (int) Math.Pow(10, i);
                if (i == 5) Delay = 60000;

                Animation an = Foreground.CreateAnimation("SB/num/n.png", Centre, FC, Delay, LoopForever);
                an.Move(0, poss, 400);
                an.Scale(0, 0.625);
                an.Fade(201487, 1.0);

                if (i == 2 || i == 4)
                {
                    Sprite s = Foreground.CreateSprite("SB/num/d.png", Centre);
                    s.Move(0, poss - 50, 400);
                    s.Scale(0, 0.625);
                    s.Fade(201487, 1.0);

                    poss -= 100;
                }
                else poss -= 60;
            }

            foreach (Beatmap Map in Mapset.getBeatmaps())
            {
                    foreach (HitObject obj in Map.HitObjects)
                    {
                        if (obj is Circle || obj is Spinner)
                        {
                            Sprite highlight = Foreground.CreateSprite("SB/highlight.png", Centre);
                            highlight.Scale(CubicOut, obj.getTime() - 100, obj.getTime() + 300, 0.2, 0.6);
                            highlight.Move(obj.getTime(), obj.getPosition());
                            highlight.Fade(Out, obj.getTime() - 100, obj.getTime() + 300, 0.0, 1.0);
                            highlight.Fade(CubicIn, obj.getTime() + 300, obj.getTime() + 2000, 1.0, 0.0);
                        } 
                    }
                
            }
        }
    }
}
