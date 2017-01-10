namespace storygen
{
    class MainClass
    {
        static void Main(string[] args)
        {
            Storyboard Storyboard = new Storyboard(@"D:\Logiciels x32\osu!\Songs\audio\");

            Storyboard.Export();
        }
    }
}
