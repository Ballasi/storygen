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
            string FileDirectoryIn, FileDirectoryOut;
            Console.WriteLine("Enter directory for the input: ");
            FileDirectoryIn = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter directory for the output: ");
            FileDirectoryOut = Convert.ToString(Console.ReadLine());
            using (StreamReader text = new StreamReader(FileDirectoryIn))
            using (StreamWriter eh = new StreamWriter(@"D:/SB/code.osb"))
            {
                int lineno = 0;
                int i = 0;
                int count = 0;
                string[] line = new string[3000];
                string[] array = new string[3000];
                    foreach (string str in System.IO.File.ReadAllLines(@"E:/osu!/Songs/547883 NoboruP ft Hatsune Miku - Shiroi Yuki No Princess Wa/NoboruP ft. Hatsune Miku - Shiroi Yuki No Princess Wa (Hokichi) [Zerkichi's Insane].osu"))
                    {
                    line[i] = str;
                    i++;
                    lineno++;
                    }
                for (i = 0; i < lineno; i++)
                {
                    if (line[i] == "[HitObjects]")
                    {
                        count = i + 1;
                        break;
                    }
                }
                Console.WriteLine(i);
                for (i = count; i < lineno; i++)
                {
                    count = 0;
                    foreach(char c in line[i])
                    {
                        if (c == ',') count++;
                        array[i] += c;
                        if (count == 3) break;
                    }
                    eh.WriteLine(array[i]);
                }
            }
            using (StreamWriter array = new StreamWriter(FileDirectoryOut))
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
                    x[i] = int.Parse(a[i]);;
                    y[i] = int.Parse(b[i]);
                    time[i] = int.Parse(c[i]);
                    array.WriteLine("Sprite,Foreground,Centre,\"SB/highlight.png\",320,240");
                    array.WriteLine(" M,0,{0},,{1},{2}", time[i], x[i] + 64, y[i] + 56);
                    array.WriteLine(" S,0,{0},{1},0.25,0.85", time[i], time[i] + 93 * 8);
                    array.WriteLine(" C,0,{0},,255,255,255", time[i]);
                    array.WriteLine(" P,0,{0},,A", time[i]);
                    array.WriteLine(" F,0,{0},{1},0.6,0", time[i] + 93, time[i] + 93 * 8);
                    array.WriteLine(" F,0,{0},{1},0,0.6", time[i] - 47, time[i] + 93);
                    i++;
                }
            }
        }
    }
}