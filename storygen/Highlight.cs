using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream e = new FileStream("E:/osu!/Songs/515637 Minori Chihara - Aitakatta Sora/Minori Chihara - Aitakatta Sora (Hokichi) [Everlasting].osu", FileMode.Open, FileAccess.Read);
            using (StreamReader text = new StreamReader("E:/osu!/Songs/515637 Minori Chihara - Aitakatta Sora/Minori Chihara - Aitakatta Sora (Hokichi) [Everlasting].osu"))
            using (StreamWriter eh = new StreamWriter("D:/SB/code.osb"))
            {
                int lineno = 825;
                int a, i;
                int count = 0;
                string[] line = new string[3000];
                string[] array = new string[lineno];
                for (i = 1; i < lineno; i++)
                {
                    line[i] = text.ReadLine();
                }
                i = 0;
                for (a = 811; a < lineno; a++)
                {
                    foreach (char c in line[a])
                    {
                        if (c == ',') count++;
                        if (count == 3) break;
                        array[i] += c;
                    }
                    eh.WriteLine(array[i]);
                    i++;
                    count = 0;
                }
            }
            using (StreamWriter array = new StreamWriter("D:/SB/hitobject.txt"))
            {
                string[] a = new string[3000]; //a is x value in array
                string[] b = new string[3000]; //b is y value in array
                string[] c = new string[3000]; //c is time value in array
                int[] x = new int[3000];
                int[] y = new int[3000];
                int[] time = new int[3000];
                int i = 0;
                array.WriteLine();
                foreach (string str in System.IO.File.ReadAllLines(@"D:/SB/code.osb"))
                {
                    a[i] = str.Split(',')[0];
                    b[i] = str.Split(',')[1];
                    c[i] = str.Split(',')[2];
                    x[i] = int.Parse(a[i]);
                    y[i] = int.Parse(b[i]);
                    time[i] = int.Parse(c[i]);
                    array.WriteLine("Sprite,Foreground,Centre,\"SB/highlight.png\",320,240");
                    array.WriteLine(" M,0,{0},,{1},{2}", time[i], x[i] + 64, y[i] + 56);
                    array.WriteLine(" S,0,{0},{1},1.1", time[i], time[i] + 93 * 8);
                    array.WriteLine(" C,0,{0},,170,170,255", time[i]);
                    array.WriteLine(" P,0,{0},,A", time[i]);
                    array.WriteLine(" F,0,{0},{1},0.6,0", time[i] + 93, time[i] + 93 * 8);
                    array.WriteLine(" F,0,{0},{1},0,0.6", time[i] - 47, time[i] + 93);
                    i++;
                }
            }
        }
    }
}