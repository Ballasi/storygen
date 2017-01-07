using storygen.Mapset.HitObjectTypes;
using storygen.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storygen
{
    class Beatmap
    {
        String FolderPath, FileName;
        String[] Content;
        String DifficultyName;
        double SliderVelocity;
        ControlPoint[] ControlPoints;
        public HitObject[] HitObjects;

        public Beatmap(String FolderPath, String FileName)
        {
            this.FolderPath = FolderPath;
            this.FileName = FileName;

            Content = System.IO.File.ReadAllLines(FolderPath + FileName);

            DifficultyName = getProperty("Version");
            SliderVelocity = Double.Parse(getProperty("SliderMultiplier"));
            ControlPoints = parseControlPoints();
            HitObjects = parseHitObjects();
        }

        public String getDifficultyName() => DifficultyName;

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
                HitObjectType Type = (HitObjectType) Int32.Parse(Parameters[3]);
                Vector2 Vector = new Vector2(Int32.Parse(Parameters[0]) + 64, Int32.Parse(Parameters[1]) + 56);

                HitObject Object = new HitObject();

                if (Type.HasFlag(HitObjectType.Circle))
                    Object = new Circle(Type, Vector, Int32.Parse(Parameters[2]), new String[0]);
                else if (Type.HasFlag(HitObjectType.Slider))
                    Object = new Slider(Type, Vector, Int32.Parse(Parameters[2]), new String[0]);
                else
                    Object = new Spinner(Type, Vector, Int32.Parse(Parameters[2]), new String[0]);

                ParsedObjects.Add(Object);
            }
            return ParsedObjects.ToArray();
        }

        private ControlPoint[] parseControlPoints()
        {
            String[] RawPoints = getContent("TimingPoints");
            List<ControlPoint> ParsedPoints = new List<ControlPoint>();
            foreach (String Point in RawPoints)
            {
                String[] Parameters = Point.Split(',');
                ParsedPoints.Add(
                    new ControlPoint(Int32.Parse(Parameters[6]) == 1 ? ControlPointTypes.Timing : ControlPointTypes.Inherited, Int32.Parse(Parameters[0]), Double.Parse(Parameters[1]))
                );
            }
            return ParsedPoints.ToArray();
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
    }
}
