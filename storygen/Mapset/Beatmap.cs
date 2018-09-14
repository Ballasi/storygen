using storygen.Util;
using System;
using System.IO;
using System.Collections.Generic;

namespace storygen
{
    class Beatmap
    {
        String FolderPath, FileName;
        String[] Content;
        String DifficultyName;
        Color[] Colors;
        int ColorIndex = 0;
        double SliderVelocity;
        public ControlPoint[] ControlPoints;
        public HitObject[] HitObjects;
        public Layer Background, Fail, Pass, Foreground;

        public Beatmap(String FolderPath, String FileName)
        {
            this.FolderPath = FolderPath;
            this.FileName = FileName;

            Content = System.IO.File.ReadAllLines(FolderPath + FileName);

            Background = new Layer(0);
            Fail = new Layer(1);
            Pass = new Layer(2);
            Foreground = new Layer(3);

            DifficultyName = getProperty("Version");
            SliderVelocity = Double.Parse(getProperty("SliderMultiplier"));
            ControlPoints = parseControlPoints();
            Colors = parseColors();
            HitObjects = parseHitObjects();
        }

        public String getDifficultyName() => DifficultyName;

        public void Export()
        {
            String ExportContent = "";
            String FilePath = FolderPath + getProperty("Artist") + " - " + getProperty("Title") + " (" + getProperty("Creator") + ") [" + DifficultyName + "].osu";

            foreach (char c in new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars()))
            {
                if (c == '\\' || c == ':')
                    continue;

                FilePath = FilePath.Replace(c.ToString(), "");
            }

            bool SBPart = false;
            for (int i = 0; i < Content.Length; i++)
            {
                String Line = Content[i];

                if (Line == "//Storyboard Layer 0 (Background)")
                    SBPart = true;
                else if (!SBPart)
                    ExportContent += Line + "\n";

                if (Line == "" && SBPart)
                {
                    SBPart = false;
                    String BackgroundContent = Background.getContent(), FailContent = Fail.getContent(), PassContent = Pass.getContent(), ForegroundContent = Foreground.getContent();
                    if (BackgroundContent == "" && FailContent == "" && PassContent == "" && ForegroundContent == "")
                        return;
                    ExportContent += getStoryboardContent(BackgroundContent, FailContent, PassContent, ForegroundContent);
                    ExportContent += "\n";
                }
            }
            System.IO.File.WriteAllText(FilePath, ExportContent.ToString());
        }

        private String getStoryboardContent(String BackgroundContent, String FailContent, String PassContent, String ForegroundContent)
        {
            String StoryboardContent = "";

            StoryboardContent += "//Storyboard Layer 0 (Background)\n";
            if (BackgroundContent != null) StoryboardContent += Background.getContent();

            StoryboardContent += "//Storyboard Layer 1 (Fail)\n";
            if (FailContent != null) StoryboardContent += Fail.getContent();

            StoryboardContent += "//Storyboard Layer 2 (Pass)\n";
            if (PassContent != null) StoryboardContent += Pass.getContent();

            StoryboardContent += "//Storyboard Layer 3 (Foreground)\n";
            if (ForegroundContent != null) StoryboardContent += Foreground.getContent();

            StoryboardContent += "//Storyboard Sound Samples\n";

            return StoryboardContent;
        }

        public String[] getContent(String Part)
        {
            bool IsInPart = false;
            List<String> PartContent = new List<String>();
            int count = 0;
            foreach (String Line in Content)
            {
                if (IsInPart == false)
                {
                    if (Line == "[" + Part + "]") IsInPart = true;
                }
                else
                {
                    if (Line == "") return PartContent.ToArray();

                    PartContent.Add(Line);
                    count++;
                }
            }
            return PartContent.ToArray();
        }

        private HitObject[] parseHitObjects()
        {
            String[] RawObjects = getContent("HitObjects");
            List<HitObject> ParsedObjects = new List<HitObject>();
            foreach (String RawObject in RawObjects)
            {
                String[] Parameters = RawObject.Split(',');

                HitObject Object = new HitObject();
                HitObjectType Type = (HitObjectType) Int32.Parse(Parameters[3]);
                ColorIndex += (Type.HasFlag(HitObjectType.SkipColor3) ? 4 : 0) + (Type.HasFlag(HitObjectType.SkipColor2) ? 2 : 0) + (Type.HasFlag(HitObjectType.SkipColor1) ? 1 : 0) + (Type.HasFlag(HitObjectType.NewCombo) ? 1 : 0);
                Vector2 Vector = new Vector2(Int32.Parse(Parameters[0]) + 64, Int32.Parse(Parameters[1]) + 56);

                if (Type.HasFlag(HitObjectType.Circle))
                    Object = new Circle(Type, Vector, Int32.Parse(Parameters[2]), Colors[ColorIndex % Colors.Length]);
                else if (Type.HasFlag(HitObjectType.Slider))
                {
                    List<String> SliderParameters = new List<String>();
                    for (int i = 5; i < Parameters.Length; i++)
                    {
                        SliderParameters.Add(Parameters[i]);
                    }
                    int Time = Int32.Parse(Parameters[2]);
                    Object = new Slider(Type, Vector, Time, Colors[ColorIndex % Colors.Length], new double[] { getBPMAt(Time), getSliderVelocityAt(Time) }, SliderParameters.ToArray());
                }
                else if (Type.HasFlag(HitObjectType.Spinner))
                    Object = new Spinner(Type, Vector, Int32.Parse(Parameters[2]), new Color(255,255,255), Int32.Parse(Parameters[5]));

                ParsedObjects.Add(Object);
            }
            return ParsedObjects.ToArray();
        }

        private ControlPoint[] parseControlPoints()
        {
            String[] RawPoints = getContent("TimingPoints");
            List<ControlPoint> ParsedPoints = new List<ControlPoint>();
            double LastBPM = 50.0;
            foreach (String RawPoint in RawPoints)
            {
                String[] Parameters = RawPoint.Split(',');
                ControlPoint Point = new ControlPoint(Int32.Parse(Parameters[6]) == 1 ? ControlPointTypes.Timing : ControlPointTypes.Inherited, Int32.Parse(Parameters[0]), Double.Parse(Parameters[1]), LastBPM);
                ParsedPoints.Add(Point);
                if (Point.getType() == ControlPointTypes.Timing) LastBPM = Point.getBPM();
            }
            return ParsedPoints.ToArray();
        }

        private Color[] parseColors()
        {
            String[] RawColors = getContent("Colours");
            if (RawColors.Length == 0)
                return new Color[]
                {
                    new Color(255,192,0),
                    new Color(0,202,0),
                    new Color(18,124,255),
                    new Color(242,24,57)
                };

            Color[] ColorArray = new Color[RawColors.Length];
            for (int i = 0; i < RawColors.Length; i++)
            {
                String RawColor = RawColors[i];
                String[] c = RawColor.Substring(9).Split(',');
                ColorArray[i] = new Color(Int32.Parse(c[0]), Int32.Parse(c[1]), Int32.Parse(c[2]));
            }
            return ColorArray;
        }

        public String getProperty(String Property)
        {
            foreach (String Line in Content)
            {
                if (Line != "")
                {
                    int pLng = Property.Length;
                    int lLng = Line.Length;

                    if (Line.Substring(0, Math.Min(lLng, pLng)) == Property)
                    {
                        return Line.Substring(pLng + 1);
                    }
                }
            }
            return null;
        }

        public double getBPMAt(double Time)
        {
            double BPM = 50.0;

            foreach (ControlPoint Point in ControlPoints)
            {
                if (Point.getType() == ControlPointTypes.Timing && Point.getOffset() <= Time)
                    BPM = Point.getBPM();
            }
            return BPM;
        }

        public double getSliderVelocityAt(double Time)
        {
            double Multiplier = 1.0;

            foreach (ControlPoint Point in ControlPoints)
            {
                if (Point.getType() == ControlPointTypes.Timing && Point.getOffset() <= Time)
                    Multiplier = Point.getSVMuliplier();
            }
            return SliderVelocity * Multiplier;
        }
    }
}
