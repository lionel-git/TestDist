using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDist
{
    class Program
    {
        static void Test()
        {
            var p = new P2D(2.0, 3.0);
            var points = new SortedSet<P2D>(new P2DComparer());
            points.Add(new P2D(2.0, 3.0));
            Console.WriteLine(points.Count);
            points.Add(new P2D(2.01, 3.01));
            Console.WriteLine(points.Count);
            points.Add(new P2D(2.0+5e-10, 3.0-3e-10));
            Console.WriteLine(points.Count);
        }


        static void Main(string[] args)
        {
           // Test(); return;

            try
            {
                var mp = new MyPoints();
                mp.GenDistances2();
                Console.WriteLine(mp);
                mp.CheckPointMatch();
                

                mp.GenPoints();
                mp.GenDistances2();
                Console.WriteLine(mp);
                mp.CheckPointMatch();

                mp.GenPoints();
                mp.GenDistances2();
                Console.WriteLine(mp);
                mp.CheckPointMatch();

                mp.GenPoints();
                mp.CheckPointMatch();

                //mp.GenDistances2();


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
