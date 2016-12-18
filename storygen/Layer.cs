﻿using System;
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

        public Sprite createSprite(String Path, Origin Origin)
        {
            Sprite sprite = new Sprite(Path, Origin);
            sprites.Add(sprite);
            return sprite;
        }

        public String getContent()
        {
            if (sprites.Count == 0) return null;

            String content = "";
            foreach (Sprite sprite in sprites)
            {
                content += "Sprite," + layer + "," + sprite.getOrigin().getName() + ",\"" + sprite.getPath() + "\",320,240";
                content += sprite.getAffections();
            }

            return content;
        }
    }
}
