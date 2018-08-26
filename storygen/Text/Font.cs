using storygen.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storygen
{
    class Font
    {
        int id;
        String Name;
        double Size;
        System.Drawing.Font FontFamily;
        String Path, StoryboardPath;
        List<FontText> Sprites;

        public String getName() => Name;
        public double getSize() => Size;
        public int getId() => id;

        public Font(int id, String Name, double Size, String Path, String StoryboardPath)
        {
            this.id = id;
            this.Name = Name;
            this.Size = Size;
            this.FontFamily = new System.Drawing.Font(Name, (float) (Size));
            this.Path = Path + Convert.ToString(id, 16) + "\\";
            this.StoryboardPath = StoryboardPath;
            Sprites = new List<FontText>();

            Directory.CreateDirectory(StoryboardPath + this.Path);
        }

        public Sentence WriteSentence(String Text, int Time, Vector2 Position, Origin Origin, Layer Layer, TextAlign Alignment, bool ScaleSprite = true, bool CharByChar = true, double Scale = 480.0 / 1080.0)
        {
            Text = Text.Trim();

            List<Sprite> TextSprites = new List<Sprite>();

            double X = Position.X;
            double Y = Position.Y;

            double SentenceSize = 0;

            if (CharByChar)
                foreach (char c in Text)
                    SentenceSize += c == ' ' ? Scale * Size * 0.35 : Scale * CreateSprite(c.ToString()).getWidth() - Size * Scale * 0.45;
            else
                SentenceSize = Scale * CreateSprite(Text).getWidth() - Size * Scale;

            double XDec = - Size / 10;
            switch (Alignment)
            {
                case TextAlign.Left:
                    {
                        break;
                    }
                case TextAlign.Centre:
                    {
                        XDec -= SentenceSize / 2;
                        break;
                    }
                case TextAlign.Right:
                    {
                        XDec -= SentenceSize;
                        break;
                    }
            }

            if (CharByChar)
            {
                foreach (char c in Text)
                {
                    if (c.Equals(' '))
                    {
                        X += Scale * Size * 0.35;
                        continue;
                    }
                    
                    FontText s = CreateSprite(c.ToString());
                    Vector2 Dec = decByOrigin(Origin, Scale, s.getWidth(), s.getHeight());
                    double LayerDecX = Dec.X, LayerDecY = Dec.Y;

                    Sprite Sprite = Layer.CreateSprite(Path + s.getFileName() + ".png", Origin, X + LayerDecX + XDec, Y + LayerDecY);
                    if (ScaleSprite)
                        Sprite.Scale(Time, Scale);

                    X += Scale * s.getWidth() - Size * Scale * 0.45;
                    TextSprites.Add(Sprite);
                }
            }
            else
            {
                FontText s = CreateSprite(Text);
                Vector2 Dec = decByOrigin(Origin, Scale, s.getWidth(), s.getHeight());
                double LayerDecX = Dec.X, LayerDecY = Dec.Y;
                Sprite Sprite = Layer.CreateSprite(Path + s.getFileName() + ".png", Origin, X + LayerDecX + XDec, Y + LayerDecY);
                if (ScaleSprite)
                    Sprite.Scale(Time, Scale);

                TextSprites.Add(Sprite);
            }

            return new Sentence(SentenceSize, TextSprites, Alignment);
        }

        private FontText CreateSprite(String Text)
        {
            foreach (FontText Sprite in Sprites)
            {
                if (Sprite.getContent() == Text)
                    return Sprite;
            }

            String StringId = Convert.ToString(Sprites.Count, 16);
            Image i = DrawText(Text);
            i.Save(StoryboardPath + Path + StringId + ".png", System.Drawing.Imaging.ImageFormat.Png);
            FontText s = new FontText(Text, StringId, i.Width, i.Height);
            Sprites.Add(s);
            return s;
        }

        private Image DrawText(String text)
        {
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);
            
            SizeF textSize = drawing.MeasureString(text, FontFamily);
            
            img.Dispose();
            drawing.Dispose();
            
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);

            drawing = Graphics.FromImage(img);
            
            Brush textBrush = new SolidBrush(System.Drawing.Color.White);

            drawing.CompositingQuality = CompositingQuality.HighQuality;
            drawing.InterpolationMode = InterpolationMode.HighQualityBilinear;
            drawing.PixelOffsetMode = PixelOffsetMode.HighQuality;
            drawing.SmoothingMode = SmoothingMode.HighQuality;
            drawing.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            drawing.DrawString(text, FontFamily, textBrush, 0, 0);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }

        private Vector2 decByOrigin(Origin Origin, double Scale, double Width, double Height)
        {
            double LayerDecX = 0, LayerDecY = 0;
            switch (Origin.getName())
            {
                case "TopLeft":
                    {
                        break;
                    }
                case "TopCentre":
                    {
                        LayerDecX += Scale * Width / 2;
                        break;
                    }
                case "TopRight":
                    {
                        LayerDecX += Scale * Width;
                        break;
                    }
                case "CentreLeft":
                    {
                        LayerDecY += Scale * Height / 2;
                        break;
                    }
                case "Centre":
                    {
                        LayerDecX += Scale * Width / 2;
                        LayerDecY += Scale * Height / 2;
                        break;
                    }
                case "CentreRight":
                    {
                        LayerDecX += Scale * Width;
                        LayerDecY += Scale * Height / 2;
                        break;
                    }
                case "BottomLeft":
                    {
                        LayerDecY += Scale * Height;
                        break;
                    }
                case "BottomCentre":
                    {
                        LayerDecX += Scale * Width / 2;
                        LayerDecY += Scale * Height;
                        break;
                    }
                case "BottomRight":
                    {
                        LayerDecX += Scale * Width;
                        LayerDecY += Scale * Height;
                        break;
                    }
            }
            return new Vector2(LayerDecX, LayerDecY);
        }
    }

    class FontText
    {
        String Content;
        String FileName;
        int Width;
        int Height;

        public String getContent() => Content;
        public String getFileName() => FileName;
        public int getWidth() => Width;
        public int getHeight() => Height;

        public FontText(String Content, String FileName, int Width, int Height)
        {
            this.Content = Content;
            this.FileName = FileName;
            this.Width = Width;
            this.Height = Height;
        }
    }

    public enum TextAlign
    {
        Left,
        Centre,
        Right
    }
}
