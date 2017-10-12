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
        private SortedSet<double> _distances2;

        public MyPoints()
        {
            _points = new SortedSet<P2D>(new P2DComparer());
            _points.Add(new P2D(-1, 0));
            _points.Add(new P2D(+1, 0));
            _distances2 = new SortedSet<double>(new DoubleComparer());
        }

        public void GenDistances2()
        {
            _distances2.Clear();
            var p = _points.ToArray();
            for (int i = 0; i < p.Length - 1; i++)
                for (int j = i + 1; j < p.Length; j++)
                {
                    var V = p[j] - p[i];
                    _distances2.Add(V.Norm2());
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
                            nps.AddRange(Generate(p[i], p[j], d2[k], d2[l]));
            int nbp = _points.Count;
            foreach (var np in nps)
                _points.Add(np);
            Console.WriteLine("Points: {0} => {1}", nbp, _points.Count);
        }

        // Genere 4 points depuis segment [A,B] avec compas et distance a2 et b2 (carre)
        // Pour generer la construction
        // garder les references de generation 
        // points => distances
        // M <= (A, B, l2, h2)
        // d2 <= (C, D)
        public static List<P2D> Generate(P2D A, P2D B, double l2, double h2)
        {
            var AB = B - A;
            var d2 = AB.Norm2();
            var d = Math.Sqrt(AB.Norm2());

            var p = new List<P2D>();
            if (l2 + h2 > d2)
            {
                double alpha = 0.5 * (d + ((l2 - h2) / d));
                double beta = 0.5 * (d + ((h2 - l2) / d));
                double lambda = Math.Sqrt(l2 - alpha * alpha);
                p.Add(A + (1 / d) * (alpha * AB + lambda * P2D.Orthogonal(AB)));
                p.Add(A + (1 / d) * (alpha * AB - lambda * P2D.Orthogonal(AB)));
                p.Add(A + (1 / d) * (beta * AB + lambda * P2D.Orthogonal(AB)));
                p.Add(A + (1 / d) * (beta * AB - lambda * P2D.Orthogonal(AB)));
            }
            return p;
        }

        public void CheckPointMatch()
        {
            foreach (var p in _points)
            {
                if (_distances2.Contains(p.Norm2()))
                    Console.WriteLine("Match: {0} {1}", p, p.Norm2());
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
    }
}

