using storygen.Elements;
using System;
using System.Collections.Generic;

namespace storygen
{
    class Layer
    {
        String layer;
        public List<Sprite> sprites = new List<Sprite>();

        public Layer(int id)
        {
            if (id == 0)        layer = "Background";
            else if (id == 1)   layer = "Fail";
            else if (id == 2)   layer = "Pass";
            else                layer = "Foreground";
        }

        public Sprite CreateSprite(String Path, Origin Origin)
        {
            Sprite sprite = new Sprite(Path, Origin, 320, 240);
            sprites.Add(sprite);
            return sprite;
        }

        public Sprite CreateSprite(String Path, Origin Origin, double X, double Y)
        {
            Sprite sprite = new Sprite(Path, Origin, X, Y);
            sprites.Add(sprite);
            return sprite;
        }

        public Animation CreateAnimation(String Path, Origin Origin, int FrameCount, int FrameDelay, LoopType LoopType)
        {
            Animation animation = new Animation(Path, Origin, 320, 240, FrameCount, FrameDelay, LoopType);
            sprites.Add(animation);
            return animation;
        }

        public Animation CreateAnimation(String Path, Origin Origin, double X, double Y, int FrameCount, int FrameDelay, LoopType LoopType)
        {
            Animation animation = new Animation(Path, Origin, X, Y, FrameCount, FrameDelay, LoopType);
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
