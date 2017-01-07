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

        public Beatmap(String FolderPath, String FileName)
        {
            this.FolderPath = FolderPath;
            this.FileName = FileName;

            Content = System.IO.File.ReadAllLines(FolderPath + FileName);

            DifficultyName = getProperty("Version");
            SliderVelocity = Double.Parse(getProperty("SliderMultiplier"));
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
