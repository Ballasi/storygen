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

        public Beatmap(String FolderPath, String FileName)
        {
            this.FolderPath = FolderPath;
            this.FileName = FileName;

            Content = System.IO.File.ReadAllLines(FolderPath + FileName);

            DifficultyName = getProperty("Version");
            SliderVelocity = Double.Parse(getProperty("SliderMultiplier"));
            ControlPoints = parseControlPoints();
        }

        public ControlPoint[] parseControlPoints()
        {
            String[] Points = getContent("TimingPoints");
            List<ControlPoint> ParsedPoints = new List<ControlPoint>();
            foreach (String Point in Points)
            {
                String[] Parameters = Point.Split(',');
                ParsedPoints.Add(
                    new ControlPoint(Int32.Parse(Parameters[6]) == 1 ? ControlPointTypes.Timing : ControlPointTypes.Inherited, Int32.Parse(Parameters[0]), Double.Parse(Parameters[1]))
                );
            }
            return ParsedPoints.ToArray();
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
            return null;
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
