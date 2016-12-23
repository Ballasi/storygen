using storygen.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storygen
{
    class Layer
    {
        String layer;
        List<Sprite> sprites = new List<Sprite>();

        public Layer(int id)
        {
            if (id == 0)        layer = "Background";
            else if (id == 1)   layer = "Fail";
            else if (id == 2)   layer = "Pass";
            else                layer = "Foreground";
        }

        public Sprite CreateSprite(String Path, Origin Origin)
        {
            Sprite sprite = new Sprite(Path, Origin);
            sprites.Add(sprite);
            return sprite;
        }

        public Animation CreateAnimation(String Path, Origin Origin, int FrameCount, int FrameDelay, LoopType LoopType)
        {
            Animation animation = new Animation(Path, Origin, FrameCount, FrameDelay, LoopType);
            sprites.Add(animation);
            return animation;
        }

        public String getContent()
        {
            String Content = "";
            foreach (Sprite sprite in sprites)
            {
                if (sprite.IsHidden())
                    continue;

                if (sprite is Animation) Content += "Animation," + layer + "," + sprite.getFirstLine() + "\n";
                else if (sprite is Sprite) Content += "Sprite," + layer + "," + sprite.getFirstLine() + "\n";
                Content += sprite.getAffections();
            }

            return Content;
        }
    }
}
