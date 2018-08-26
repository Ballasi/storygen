using storygen.Util;
using storygen.Elements;
using System;
using System.Threading;
using System.IO;

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

        public LoopType LoopForever, LoopOnce;

        public Random rnd;

        public String FolderPath = "";

        public Mapset Mapset;

        public Text Text;

        public static string getScriptFolder()
            => System.IO.Directory.GetParent(
                                  System.IO.Directory.GetParent(
                                    System.IO.Directory.GetCurrentDirectory()
                                  ).ToString()
                                ).ToString() + "\\";

        public Osb()
        {
        }

        public Osb(String FolderPath)
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

            // Setting up LoopTypes
            LoopForever = new LoopType("LoopForever");
            LoopOnce = new LoopType("LoopOnce");

            // Setting up FolderPath
            setFolderPath(FolderPath);

            // Setting up Mapset
            Mapset = new Mapset(FolderPath, getAllOsuFiles());

            // Setting up Text
            Text = new Text(@"sb\text\", FolderPath);
        }

        public void setFolderPath(String FolderPath)
        {
            String Path;
            if (FolderPath.Substring(FolderPath.Length - 1) == @"\") Path = FolderPath;
            else Path = FolderPath + @"\";
            this.FolderPath = Path;
        }

        public String[] getAllOsuFiles()
        {
            if (FolderPath == "") return null;

            String[] rawFiles = Directory.GetFiles(FolderPath, "*.osu");
            String[] osuFiles = new String[rawFiles.Length];

            int count = 0;
            foreach (String file in rawFiles)
            {
                osuFiles[count] = file.Substring(FolderPath.Length);
                count++;
            }

            return osuFiles;
        }

        public int Random(int Minimum, int Maximum)
        {
            return rnd.Next(Minimum, Maximum);
        }

        public double Random(double Minimum, double Maximum)
        {
            return rnd.NextDouble() * (Maximum - Minimum) + Minimum;
        }

        public void MergeWith(String FilePath)
        {
            String[] Content = System.IO.File.ReadAllLines(FilePath);
            
            Sprite CurrentSprite = null;
            int GroupDepth = 0;

            foreach (String Line in Content)
            {
                if (Line.StartsWith("//") || Line == "[Events]")
                    continue;

                if (GroupDepth > 0 && !Line.Substring(GroupDepth).StartsWith(" "))
                {
                    GroupDepth--;
                    CurrentSprite.EndLoop();
                }

                String[] Values = Line.Trim().Split(',');
                
                switch (Values[0])
                {
                    case "Sprite":
                        {
                            Layer SpriteLayer = getEquivalentLayer(Values[1]);
                            Origin SpriteOrigin = new Origin(Values[2]);
                            String SpritePath = Values[3].Substring(1, Values[3].Length - 2);
                            var X = Double.Parse(Values[4]);
                            var Y = Double.Parse(Values[5]);
                            CurrentSprite = SpriteLayer.CreateSprite(SpritePath, SpriteOrigin, X, Y);
                        }
                        break;
                    case "Animation":
                        {
                            Layer SpriteLayer = getEquivalentLayer(Values[1]);
                            Origin SpriteOrigin = new Origin(Values[2]);
                            String SpritePath = Values[3].Substring(1, Values[3].Length - 2);
                            var X = Double.Parse(Values[4]);
                            var Y = Double.Parse(Values[5]);
                            int FrameCount = Int32.Parse(Values[6]);
                            int FrameDelay = Int32.Parse(Values[7]);
                            LoopType LoopType = new LoopType(Values[8]);
                            CurrentSprite = SpriteLayer.CreateAnimation(SpritePath, SpriteOrigin, X, Y, FrameCount, FrameDelay, LoopType);
                        }
                        break;
                    case "T":
                        {
                            String TriggerType = Values[1];
                            int StartTime = Int32.Parse(Values[2]);
                            int EndTime = Int32.Parse(Values[3]);
                            CurrentSprite.OnTrigger(StartTime, EndTime, TriggerType);
                            GroupDepth++;
                        }
                        break;
                    case "L":
                        {
                            int StartTime = Int32.Parse(Values[1]);
                            int LoopCount = Int32.Parse(Values[2]);
                            CurrentSprite.BeginLoop(StartTime, LoopCount);
                            GroupDepth++;
                        }
                        break;
                    default:
                        {
                            Easing SpriteEasing = new Easing(Int32.Parse(Values[1]));
                            int StartTime = Int32.Parse(Values[2]);
                            int EndTime = String.IsNullOrEmpty(Values[3]) ? StartTime : Int32.Parse(Values[3]);

                            switch (Values[0])
                            {
                                case "F":
                                    {
                                        if (Values.Length > 5)
                                            CurrentSprite.Fade(SpriteEasing, StartTime, EndTime, double.Parse(Values[4]), double.Parse(Values[5]));
                                        else if (StartTime != EndTime)
                                            CurrentSprite.Fade(SpriteEasing, StartTime, EndTime, double.Parse(Values[4]), double.Parse(Values[4]));
                                        else
                                            CurrentSprite.Fade(StartTime, double.Parse(Values[4]));
                                    }
                                    break;
                                case "S":
                                    {
                                        if (Values.Length > 5)
                                            CurrentSprite.Scale(SpriteEasing, StartTime, EndTime, double.Parse(Values[4]), double.Parse(Values[5]));
                                        else if (StartTime != EndTime)
                                            CurrentSprite.Scale(SpriteEasing, StartTime, EndTime, double.Parse(Values[4]), double.Parse(Values[4]));
                                        else
                                            CurrentSprite.Scale(StartTime, double.Parse(Values[4]));
                                    }
                                    break;
                                case "V":
                                    {
                                        if (Values.Length > 6)
                                            CurrentSprite.ScaleVec(SpriteEasing, StartTime, EndTime, double.Parse(Values[4]), double.Parse(Values[5]), double.Parse(Values[6]), double.Parse(Values[7]));
                                        else if (StartTime != EndTime)
                                            CurrentSprite.ScaleVec(SpriteEasing, StartTime, EndTime, double.Parse(Values[4]), double.Parse(Values[5]), double.Parse(Values[4]), double.Parse(Values[5]));
                                        else
                                            CurrentSprite.ScaleVec(StartTime, double.Parse(Values[4]), double.Parse(Values[5]));
                                    }
                                    break;
                                case "R":
                                    {
                                        if (Values.Length > 5)
                                            CurrentSprite.Rotate(SpriteEasing, StartTime, EndTime, double.Parse(Values[4]), double.Parse(Values[5]));
                                        else if (StartTime != EndTime)
                                            CurrentSprite.Rotate(SpriteEasing, StartTime, EndTime, double.Parse(Values[4]), double.Parse(Values[4]));
                                        else
                                            CurrentSprite.Rotate(StartTime, double.Parse(Values[4]));
                                    }
                                    break;
                                case "M":
                                    {
                                        if (Values.Length > 6)
                                            CurrentSprite.Move(SpriteEasing, StartTime, EndTime, double.Parse(Values[4]), double.Parse(Values[5]), double.Parse(Values[6]), double.Parse(Values[7]));
                                        else if (StartTime != EndTime)
                                            CurrentSprite.Move(SpriteEasing, StartTime, EndTime, double.Parse(Values[4]), double.Parse(Values[5]), double.Parse(Values[4]), double.Parse(Values[5]));
                                        else
                                            CurrentSprite.Move(StartTime, double.Parse(Values[4]), double.Parse(Values[5]));
                                    }
                                    break;
                                case "MX":
                                    {
                                        if (Values.Length > 5)
                                            CurrentSprite.MoveX(SpriteEasing, StartTime, EndTime, double.Parse(Values[4]), double.Parse(Values[5]));
                                        else if (StartTime != EndTime)
                                            CurrentSprite.MoveX(SpriteEasing, StartTime, EndTime, double.Parse(Values[4]), double.Parse(Values[4]));
                                        else
                                            CurrentSprite.MoveX(StartTime, double.Parse(Values[4]));
                                    }
                                    break;
                                case "MY":
                                    {
                                        if (Values.Length > 5)
                                            CurrentSprite.MoveY(SpriteEasing, StartTime, EndTime, double.Parse(Values[4]), double.Parse(Values[5]));
                                        else if (StartTime != EndTime)
                                            CurrentSprite.MoveY(SpriteEasing, StartTime, EndTime, double.Parse(Values[4]), double.Parse(Values[4]));
                                        else
                                            CurrentSprite.MoveY(StartTime, double.Parse(Values[4]));
                                    }
                                    break;
                                case "C":
                                    {
                                        if (Values.Length > 7)
                                            CurrentSprite.Color(SpriteEasing, StartTime, EndTime, double.Parse(Values[4]) / 255.0, double.Parse(Values[5]) / 255.0, double.Parse(Values[6]) / 255.0, double.Parse(Values[7]) / 255.0, double.Parse(Values[8]) / 255.0, double.Parse(Values[9]) / 255.0);
                                        else if (StartTime != EndTime)
                                            CurrentSprite.Color(SpriteEasing, StartTime, EndTime, double.Parse(Values[4]) / 255.0, double.Parse(Values[5]) / 255.0, double.Parse(Values[6]) / 255.0, double.Parse(Values[4]) / 255.0, double.Parse(Values[5]) / 255.0, double.Parse(Values[6]) / 255.0);
                                        else
                                            CurrentSprite.Color(StartTime, double.Parse(Values[4]) / 255.0, double.Parse(Values[5]) / 255.0, double.Parse(Values[6]) / 255.0);
                                    }
                                    break;
                                case "P":
                                    {
                                        switch (Values[4])
                                        {
                                            case "A":
                                                CurrentSprite.Additive(StartTime, EndTime);
                                                break;
                                            case "H":
                                                CurrentSprite.HFlip(StartTime, EndTime);
                                                break;
                                            case "V":
                                                CurrentSprite.VFlip(StartTime, EndTime);
                                                break;
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        private Layer getEquivalentLayer(String LayerName)
        {
            switch(LayerName)
            {
                default:
                case "Background": return Background;
                case "Fail": return Fail;
                case "Pass": return Pass;
                case "Foreground": return Foreground;
            }
        }

        public void Export()
        {
            String FilePath = FolderPath + Mapset.getArtistName() + " - " + Mapset.getTitle() + " (" + Mapset.getCreator() + ").osb";

            foreach (char c in new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars()))
            {
                if (c == '\\' || c == ':')
                    continue;

                FilePath = FilePath.Replace(c.ToString(), "");
            }

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
            
            System.IO.File.WriteAllText(FilePath, Content);               

            foreach (Beatmap Beatmap in Mapset.getBeatmaps())
                Beatmap.Export();
        }
    }
}
