namespace storygen.Util
{
    class Color
    {
        public double R;
        public double G;
        public double B;

        public Color()
        {
        }

        public Color(double R, double G, double B)
        {
            this.R = R / 255;
            this.G = G / 255;
            this.B = B / 255;
        }
    }
}
