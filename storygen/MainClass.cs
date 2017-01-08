using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storygen
{
    class MainClass
    {
        static void Main(string[] args)
        {
            Storyboard storyboard = new Storyboard(@"D:\Logiciels x32\osu!\Songs\340048 Memme - Avalanche\");
            
            storyboard.export();
        }
    }
}
