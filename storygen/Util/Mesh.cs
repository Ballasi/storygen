using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storygen.Util
{
    class Mesh : IDisposable
    {
        String FilePath, FileName;
        double PositionX, PositionY;
        List<Vector3> Vertices = new List<Vector3>();
        Layer Layer;
        Origin Origin;
        bool Disposed;

        public Mesh(String FilePath, double PositionX, double PositionY, Origin Origin, Layer Layer, int Quality = 1)
        {
            this.FilePath = FilePath;
            this.PositionX = PositionX;
            this.PositionY = PositionY;
            this.Layer = Layer;
            this.Origin = Origin;

            FileName = FilePath;
            if (FilePath.Split('/').Length != 1) FileName = FilePath.Split('/')[FilePath.Split('/').Length - 1];
            else if (FilePath.Split('\\').Length != 1) FileName = FilePath.Split('\\')[FilePath.Split('\\').Length - 1];

            Generate(Quality);
        }

        public Mesh(List<Vector3> Vertices, double PositionX, double PositionY, Origin Origin, Layer Layer, int Quality = 1)
        {
            this.PositionX = PositionX;
            this.PositionY = PositionY;
            this.Layer = Layer;
            this.Origin = Origin;

            this.Vertices = Vertices;
        }

        public String getFilePath() => FilePath;
        public Vector2 getPosition() => new Vector2(PositionX, PositionY);
        public List<Vector3> getVertices() => Vertices;
        public Layer getLayer() => Layer;
        public Origin getOrigin() => Origin;

        private void Generate(int Quality)
        {
            using (StreamReader StreamReader = new StreamReader(FilePath))
            {
                try
                {
                    int LineCount = -1;
                    while (!StreamReader.EndOfStream)
                    {
                        String Line = StreamReader.ReadLine();

                        if (Line.StartsWith("#"))
                            continue;

                        String[] Values = Line.Split(' ');

                        if (Values[0] == "v")
                        {
                            LineCount++;

                            if (LineCount % Quality != 0)
                                continue;

                            var X = double.Parse(Values[1]);
                            var Y = double.Parse(Values[2]);
                            var Z = double.Parse(Values[3]);

                            Vertices.Add(new Vector3(X, Y, Z));
                        }
                    }
                }
                finally
                {
                    StreamReader.Close();
                    StreamReader.Dispose();
                }
            }
        }

        public List<Sprite> Render(double StartTime, double EndTime, double RevolutionDuration, double Scale, double Tilt, double SpriteScale, String SpritePath, bool ImageRotation = false, bool AutoFade = true, bool AutoScale = true, bool Revolution = true)
        {
            List<Sprite> Sprites = new List<Sprite>();

            foreach (Vector3 Vertex in Vertices)
            {
                Sprite Sprite = Layer.CreateSprite(SpritePath, Origin);
                double Angle = Math.Atan2(Vertex.Z, Vertex.X);
                double Delay = Angle / (Math.PI * 2) * RevolutionDuration;
                double Radius = Scale * Vector2.Distance(new Vector2(Vertex.X, Vertex.Z), new Vector2());

                if (SpriteScale != 0 && AutoScale)
                    Sprite.Scale(StartTime, SpriteScale);

                Sprite.Fade(StartTime - Delay - RevolutionDuration, 0);

                if (AutoFade)
                {
                    Sprite.Fade(StartTime, 1);
                    Sprite.Fade(EndTime, 0);
                }

                Sprite.BeginLoop(StartTime - Delay - RevolutionDuration, (EndTime - StartTime) / RevolutionDuration + 3);

                if (Revolution)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        double StartAngle = Math.PI * i / 2;
                        double EndAngle = Math.PI * (i + 1) / 2;
                        double SpriteStartTime = RevolutionDuration * i / 4;
                        double SpriteEndTime = RevolutionDuration * (i + 1) / 4;

                        Sprite.MoveX(i % 2 + 1, SpriteStartTime, SpriteEndTime,
                            PositionX + Radius * Math.Sin(StartAngle),
                            PositionX + Radius * Math.Sin(EndAngle));
                        Sprite.MoveY((i + 1) % 2 + 1, SpriteStartTime, SpriteEndTime,
                            PositionY - Vertex.Y * Scale + Tilt * Radius * Math.Cos(StartAngle),
                            PositionY - Vertex.Y * Scale + Tilt * Radius * Math.Cos(EndAngle));
                    }
                }
                else
                {
                    double X = PositionX + Radius * Math.Sin(Angle);
                    double Y = PositionY + Tilt * Radius * Math.Cos(Angle) - Vertex.Y * Scale;

                    Sprite.Move(StartTime, X, Y);
                }

                if (ImageRotation)
                    Sprite.Rotate(0, RevolutionDuration, 0, -Math.PI * 2);

                Sprite.EndLoop();

                Sprites.Add(Sprite);
            }

            return Sprites;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool Disposing)
        {
            if (!Disposed && Disposing)
            {
                Disposed = Disposing;
            }
        }
    }
}
