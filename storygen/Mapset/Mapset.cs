using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storygen
{
    class Mapset
    {
        String FolderPath, ArtistName, Title, Creator;
        Beatmap[] Beatmaps;

        public Mapset(String FolderPath, String[] FileNames)
        {
            this.FolderPath = FolderPath;

            Beatmaps = new Beatmap[FileNames.Length];
            for (int i = 0; i < FileNames.Length; i++)
            {
                Beatmaps[i] = new Beatmap(FolderPath, FileNames[i]);
            }
            
            ArtistName = Beatmaps[0].getProperty("Artist");
            Title = Beatmaps[0].getProperty("Title");
            Creator = Beatmaps[0].getProperty("Creator");
        }

        public Beatmap[] getBeatmaps() => Beatmaps;
        public String getArtistName() => ArtistName;
        public String getTitle() => Title;
        public String getCreator() => Creator;
    }
}
