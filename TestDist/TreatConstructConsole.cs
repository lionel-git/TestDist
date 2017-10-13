using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algo;

namespace TestDist
{
    class TreatConstructConsole : ITreatConstruct
    {
        public void Display(P2D p)
        {
            Console.WriteLine("point: {0}", p.ToString(true));
        }

        public void Display(D2 d)
        {
            Console.WriteLine("distance: {0}", d.ToString(true));
        }
    }
}
