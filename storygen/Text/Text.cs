using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storygen
{
    class Text
    {
        List<Font> Fonts { get; set; }
        String Path, StoryboardPath;

        public Text(String Path, String StoryboardPath)
        {
            Fonts = new List<Font>();
            this.Path = Path;
            this.StoryboardPath = StoryboardPath;
        }

        public void ChangePath(String Path)
            => this.Path = Path;

        public Font LoadFont(String Name, double Size)
        {
            int currentId = 0;
            foreach (Font Font in Fonts)
            {
                if (Font.getName() == Name && Font.getSize() == Size)
                    return Font;
                currentId++;
            }

            Font f = new Font(currentId, Name, Size, Path, StoryboardPath);
            Fonts.Add(f);
            return f;
        }
    }
}
