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

        public string ToString(bool displayPoints=false)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}", Value);
            if (displayPoints)
                sb.AppendFormat(" {0} {1}", A, B);
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToString(false);
        }
    }
}
