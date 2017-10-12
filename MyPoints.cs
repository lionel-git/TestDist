using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDist
{
    public class MyPoints
    {
        private SortedSet<P2D> _points;
        private SortedSet<D2> _distances2;

        public MyPoints()
        {
            _points = new SortedSet<P2D>(new P2DComparer());
            _points.Add(new P2D(-1, 0));
            _points.Add(new P2D(+1, 0));
            _distances2 = new SortedSet<D2>(new D2Comparer());
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
                if (_distances2.Contains(new D2(p.Norm2(), null, null)))
                {
                    Console.WriteLine("Match: {0} {1}", p, p.Norm2());
                    DisplayConstruct(p);   
                }
            }
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
            if (p.g != null)
            {
                Console.WriteLine("point: {0}", p);
                Console.WriteLine("Gen: {0}", p.g);
                DisplayConstruct(p.g.A);
                DisplayConstruct(p.g.B);
                DisplayConstruct(p.g.l2);
                DisplayConstruct(p.g.h2);
            }
        }

        public void DisplayConstruct(D2 d)
        {
            Console.WriteLine("distance: {0}", d);
            DisplayConstruct(d.A);
            DisplayConstruct(d.B);
        }
    }
}

