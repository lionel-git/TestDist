using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    public class MyPoints
    {
        private SortedSet<P2D> _points;
        private SortedSet<D2> _distances2;

        // Track Already displayed
        private SortedSet<P2D> _dPoints;
        private SortedSet<D2> _dDistances2;

        public MyPoints()
        {
            _distances2 = new SortedSet<D2>(new D2Comparer());
            _points = new SortedSet<P2D>(new P2DComparer());
            _points.Add(new P2D(-1, 0));
            _points.Add(new P2D(+1, 0));
            
            _dPoints= new SortedSet<P2D>(new P2DComparer());
            _dDistances2 = new SortedSet<D2>(new D2Comparer());
        }

        public void GenDistances2()
        {
            _distances2.Clear();
            var p = _points.ToArray();
            for (int i = 0; i < p.Length - 1; i++)
                for (int j = i + 1; j < p.Length; j++)
                {
                    var V = p[j] - p[i];
                    _distances2.Add(new D2(V.Norm2(), p[i], p[j]));
                }
        }

        public void GenPoints()
        {
            var nps = new List<P2D>();
            var p = _points.ToArray();
            var d2 = _distances2.ToArray();
            for (int i = 0; i < p.Length - 1; i++)
                for (int j = i + 1; j < p.Length; j++)
                    for (int k = 0; k < d2.Length; k++)
                        for (int l = k; l < d2.Length; l++)
                        {
                            var g = new Generator(p[i], p[j], d2[k], d2[l]);
                            nps.AddRange(g.Generate());
                        }
            int nbp = _points.Count;
            foreach (var np in nps)
                _points.Add(np);
            Console.WriteLine("Points: {0} => {1}", nbp, _points.Count);
        }

        public void CheckPointMatch()
        {
            foreach (var p in _points)
            {
                var r2 = new D2(p.Norm2(), null, null);
                if (_distances2.Contains(r2))
                {
                    Console.WriteLine("Match: {0} {1}", p, r2);
                    var dc = new D2Comparer();
                    DisplayAllConstruct(p, _distances2.First(x => dc.Compare(x, r2) == 0));
                    return;
                }
            }
        }

        public void DisplayAllConstruct(P2D p, D2 l2)
        {
            _dPoints.Clear();
            _dDistances2.Clear();
            Console.WriteLine("=== Point ===");
            DisplayConstruct(p);
            Console.WriteLine("=== Distance ===");
            DisplayConstruct(l2);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0} distances: | ", _distances2.Count);
            foreach (var d2 in _distances2)
                sb.Append(d2).Append(" | ");
            sb.AppendLine();
            sb.AppendFormat("{0} points: | ", _points.Count);
            foreach (var p in _points)
                sb.Append(p).Append(" | ");
            return sb.ToString();
        }

        public void DisplayConstruct(P2D p)
        {
            if (!_dPoints.Contains(p))
            {
                _dPoints.Add(p);
                Console.WriteLine("point: {0}", p.ToString(true));
                if (p.g != null)
                {
                    DisplayConstruct(p.g.A);
                    DisplayConstruct(p.g.B);
                    DisplayConstruct(p.g.l2);
                    DisplayConstruct(p.g.h2);
                }
            }
        }

        public void DisplayConstruct(D2 d)
        {
            if (!_dDistances2.Contains(d))
            {
                _dDistances2.Add(d);
                Console.WriteLine("distance: {0}", d.ToString(true));
                DisplayConstruct(d.A);
                DisplayConstruct(d.B);
            }
        }
    }
}

