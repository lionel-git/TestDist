using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDist
{
    public class D2
    {
        public D2(double Value, P2D A, P2D B)
        {
            this.Value = Value;
            this.A = A;
            this.B = B;
        }

        public double Value { get; set; }
        public P2D A { get; set; }
        public P2D B { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0} {1} {2}", Value, A, B);
            return sb.ToString();
        }
    }
}
