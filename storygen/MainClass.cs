namespace storygen
{
    class MainClass
    {
        static void Main(string[] args)
        {
            MainStoryboard Storyboard = new MainStoryboard(@"E:\Program Files\osu!\Songs\Camellia+-+\");
            
            Storyboard.Export();
        }
    }
}
