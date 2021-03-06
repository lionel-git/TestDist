﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    public class P2D
    {
        public double x { get; set; }
        public double y { get; set; }

        public Generator g { get; set; }

        public P2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public static P2D operator +(P2D V1, P2D V2)
        {
            return new P2D(V1.x + V2.x, V1.y + V2.y);
        }
        public static P2D operator -(P2D V1, P2D V2)
        {
            return new P2D(V1.x - V2.x, V1.y - V2.y);
        }

        public static P2D Orthogonal(P2D V)
        {
            return new P2D(-V.y, V.x);
        }

        public double Norm2()
        {
            return x * x + y * y;
        }

        public static P2D operator *(double l, P2D V)
        {
            return new P2D(l * V.x, l * V.y);
        }

        public string ToString(bool displayGen)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("({0:0.0000},{1:0.0000})", x, y);
            if (displayGen)
            {
                if (g != null)
                    sb.AppendFormat(" Gen: {0}", g);
                else
                    sb.Append(" **");
            }
            return sb.ToString();
        }


        public override string ToString()
        {
            return ToString(false);
        }
    }
}

