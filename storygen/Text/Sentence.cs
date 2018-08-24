using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storygen
{
    class Sentence
    {
        public double Size;
        public List<Sprite> Sprites;
        public TextAlign Alignment;
        public int Length;

        public Sentence(double Size, List<Sprite> Sprites, TextAlign Alignment)
        {
            this.Size = Size;
            this.Sprites = Sprites;
            this.Alignment = Alignment;
            Length = Sprites.Count;
        }
    }
}
