using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    public class Generator
    {
        public P2D A { get; set; }
        public P2D B { get; set; }
        public D2 l2 { get; set; }
        public D2 h2 { get; set; }

        public Generator(P2D A, P2D B, D2 l2, D2 h2)
        {
            this.A = A;
            this.B = B;
            this.l2 = l2;
            this.h2 = h2;
        }

        // Genere 4 points depuis segment [A,B] avec compas et distance a2 et b2 (carre)
        // Pour generer la construction
        // garder les references de generation 
        // points => distances
        // M <= (A, B, l2, h2)
        // d2 <= (C, D)
        public List<P2D> Generate() //P2D A, P2D B, double l2, double h2)
        {
            var AB = B - A;
            var d2 = AB.Norm2();
            var d = Math.Sqrt(AB.Norm2());

            var lp = new List<P2D>();
            if (l2.Value + h2.Value > d2)
            {
                double alpha = 0.5 * (d + ((l2.Value - h2.Value) / d));
                double beta = 0.5 * (d + ((h2.Value - l2.Value) / d));
                double lambda = Math.Sqrt(l2.Value - alpha * alpha);
                lp.Add(A + (1 / d) * (alpha * AB + lambda * P2D.Orthogonal(AB)));
                lp.Add(A + (1 / d) * (alpha * AB - lambda * P2D.Orthogonal(AB)));
                lp.Add(A + (1 / d) * (beta * AB + lambda * P2D.Orthogonal(AB)));
                lp.Add(A + (1 / d) * (beta * AB - lambda * P2D.Orthogonal(AB)));
            }
            foreach (var p in lp)
                p.g = this;
            return lp;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("A:{0} B:{1} l2:{2} h2:{3}", A, B, l2, h2);
            return sb.ToString();
        }
    }
}
