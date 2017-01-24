using storygen.Elements;
using storygen.Util;
using System;
using System.Collections.Generic;

namespace storygen
{
    class Storyboard : Osb
    {
        public Storyboard(String FolderPath) : base(FolderPath)
        {
            /*Sprite bg = Background.CreateSprite("bg.jpg", Centre);
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

            int TryAt = 86542;
            Sprite Circle1 = Foreground.CreateSprite("SB/dot.png", Centre);
            Circle1.Move(BounceInOut, 84763, 87586, 180, 300, 380, 150);
            Circle1.Fade(84763, 1.0);
            Circle1.Fade(TryAt, 0.0);

            Sprite Circle2 = Foreground.CreateSprite("SB/dot.png", Centre);
            Circle2.Move(TryAt, Circle1.getPositionAt(TryAt));
            Circle2.Scale(Out, TryAt, TryAt + 1000, 1.0, 2.0);
            Circle2.Fade(In, TryAt, TryAt + 1000, 1.0, 0.0);

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
                if (Map.getDifficultyName() != "Glacial Cascade")
                {
                    foreach (HitObject HitObject in Map.HitObjects)
                    {
                        if (!(HitObject is Spinner))
                        {
                            Sprite highlight = Map.Foreground.CreateSprite("SB/highlight.png", Centre);
                            highlight.Scale(CubicOut, HitObject.StartTime - 100, HitObject.StartTime + 100, 0.2, 0.8);
                            highlight.Move(HitObject.StartTime, HitObject.getInitialPosition());
                            highlight.Fade(Out, HitObject.StartTime - 100, HitObject.StartTime + 100, 0.0, 1.0);
                            highlight.Fade(CubicIn, HitObject.StartTime + 300, HitObject.StartTime + 2000, 1.0, 0.0);
                            highlight.Additive(HitObject.StartTime - 100, HitObject.StartTime + 2000);
                        }
                    }
                }
            }

            using (Mesh Violin = new Mesh(@"F:\violin.obj", 320, 265, Centre, Foreground, 5))
                Violin.Render(84763, 107351, Mapset.BeatDuration * 16, 0.35, 1/4.0, 0.05, true, "SB/dot.png", false);

            Sprite BG = Background.CreateSprite("bg.jpg", Centre);
            BG.Fade(0, 1.0);
            BG.Scale(204055, 0.625);

            Sprite RingInterior = Foreground.CreateSprite("SB/ringinterior.png", Centre);
            RingInterior.Fade(0, 1.0);
            RingInterior.Scale(204055, 0.5);

            String[] Content = System.IO.File.ReadAllLines(@"F:\test.lvl");
            foreach (String Line in Content)
            {
                String[] Infos = Line.Split(',');
                if (Infos[0] == "N")
                    hit(Int32.Parse(Infos[1]), Double.Parse(Infos[2]), false);
                else
                {
                    List<int> Timings = new List<int>();
                    List<double> Positions = new List<double>();
                    foreach (String Info in Infos)
                    {
                        if (Info == "S")
                            continue;

                        String[] CurrentPos = Info.Split(':');
                        if (CurrentPos.Length == 1)
                        {
                            Timings.Add(Int32.Parse(CurrentPos[0]));
                            Positions.Add(0.0);
                        }
                        else
                        {
                            Timings.Add(Int32.Parse(CurrentPos[0]));
                            Positions.Add(Double.Parse(CurrentPos[1]));
                        }
                    }
                    hold(Timings.ToArray(), Positions.ToArray());
                }
            }

            /*hit(82597, 0.25, false);
            hold(new int[] { 82597, 82918, 83240, 83454 }, new double[] { 0.4, 0.5, 0.3, 0.3 });

            Sprite Ring = Foreground.CreateSprite("SB/ring.png", Centre);
            Ring.Fade(0, 1.0);
            Ring.Scale(204055, 0.5);

            Sprite Cursor = Foreground.CreateSprite("SB/cursor.png", Centre);
            Cursor.Fade(0, 1.0);
            Cursor.Scale(204055, 480.0 / 1080.0); */

            MergeWith(@"D:\Logiciels x32\osu!\Songs\372144 Cartoon - Whatever I Do (feat Kostja)\Cartoon - Whatever I Do (feat. Kostja) (Nhawak).osb");
        }

        public void hit(int Time, double Position, bool isSlider)
        {
            double Radius = 160;

            Sprite Object = Foreground.CreateSprite("SB/hit.png", Centre);
            Object.Scale(CubicIn, Time - 1000, Time, 0, 0.3);
            Object.Rotate(Time, Position * Math.PI * 2);
            Object.Move(CubicIn, Time - 1000, Time, 320, 240, Math.Sin(Position * Math.PI * 2) * Radius + 320, -Math.Cos(Position * Math.PI * 2) * Radius + 240);

            for (int i = 0; i < (isSlider ? 1 : 20); i++)
            {
                double Rot = Random(Position * Math.PI * 2 - 0.5, Position * Math.PI * 2 + 0.5);
                Vector2 IniPos = new Vector2(Math.Sin(Position * Math.PI * 2) * (Radius + 20) + 320, -Math.Cos(Position * Math.PI * 2) * (Radius + 20) + 240);
                Vector2 FinPos = new Vector2(Math.Sin(Position * Math.PI * 2) * (Radius + 20) * Random(1.8, 2.0) + 320 + Random(-Random(30, 50), Random(30, 50)), -Math.Cos(Position * Math.PI * 2) * (Radius + 20) * Random(1.8, 2.0) + 240 + Random(-Random(30, 50), Random(30, 50)));

                Sprite S = Foreground.CreateSprite("SB/circle.png", Centre);
                S.Move(QuintOut, Time, Time + 1500, IniPos, FinPos);
                S.Scale(Time, Random(0.02, 0.15)/8);
                S.Fade(CubicIn, Time, Time + Random(500, 1500), Random(0.1, 0.3), 0.0);
            }
        }

        public void hold(int[] Times, double[] Positions)
        {
            int lasttime = Times[0];
            int lastcount = 0;
            for (int i = Times[0]; i < Times[Times.Length-1]; i += 5)
            {
                if (i >= Times[lastcount + 1])
                {
                    bool dood = true;
                    double padd = 0.005;
                    if (Positions[lastcount + 1] - Positions[lastcount] < 0) dood = false;

                    for (double p = Positions[lastcount]; dood ? (p < Positions[lastcount+1]) : (p > Positions[lastcount + 1]); p += dood ? padd : -padd)
                    {
                        hit(Times[lastcount+1], p, true);

                        if (p > 82918 + 100) Console.WriteLine(p);

                        if (p > 1.0) p -= 1.0;
                        if (p < -1.0) p += 1.0;
                    }
                    
                    lastcount++;
                    lasttime = Times[lastcount];

                    hit(lasttime, Positions[lastcount], true);
                }
                else
                {
                    hit(i, Positions[lastcount], true);
                }
            }
        }
    }
}
