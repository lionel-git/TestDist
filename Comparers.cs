using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDist
{
    public class Constants
    {
        public const double EPS = 1.137731e-10; // no chance of strict equality..
    }

    public class P2DComparer : IComparer<P2D>
    {
        private DoubleComparer dc = new DoubleComparer();

        public int Compare(P2D A, P2D B)
        {
            var ret = dc.Compare(A.x, B.x);
            if (ret != 0)
                return ret;
            else
                return dc.Compare(A.y, B.y);
        }
    }

    public class DoubleComparer : IComparer<double>
    {
        public int Compare(double x, double y)
        {
            if (x < y - Constants.EPS)
                return -1;
            else if (x > y + Constants.EPS)
                return +1;
            else return 0;
        }
    }

    public class D2Comparer : IComparer<D2>
    {
        public int Compare(D2 l2, D2 h2)
        {
            if (l2.Value < h2.Value - Constants.EPS)
                return -1;
            else if (l2.Value > h2.Value + Constants.EPS)
                return +1;
            else return 0;
        }
    }
}
