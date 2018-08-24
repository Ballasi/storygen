namespace storygen
{
    class MainClass
    {
        static void Main(string[] args)
        {
            Storyboard Storyboard = new Storyboard(@"D:\path");
            
            Storyboard.Export();
        }
    }
}
