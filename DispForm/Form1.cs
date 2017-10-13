using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Algo;

namespace DispForm
{
    public partial class Form1 : Form, ITreatConstruct
    {
        private Graphics _g;
        double _mx, _my;

        const int L = 5;

        double Zoom = 0.10;

        double centerX = 1.0;
        double centerY = 0.0;

        bool firstPoint;

        public Form1()
        {
            InitializeComponent();           
            Width = 900;
            Height = 900;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            _g = this.CreateGraphics();
        }

        private void ShowSetUp()
        {
            _mx = Width * Zoom;
            _my = Height * Zoom;

            _g.DrawLine(Pens.Black, GetPoint(0,-10), GetPoint(0,10));
            _g.DrawLine(Pens.Black, GetPoint(-10, 0), GetPoint(10, 0));
        }

        private Point GetPoint(double x, double y)
        {
            return new Point((int)(Width / 2 + _mx * (x+centerX)), (int)(Height / 2 + _my * (y+centerY)));
        }

        private void ShowPoint(P2D M)
        {
            return;
            var p = GetPoint(M.x, M.y);
            Rectangle rectangle = new Rectangle(p.X - L, p.Y - L, 2*L, 2*L);
            if (M.g!=null)
                _g.DrawEllipse(Pens.Red, rectangle);
            else
                _g.DrawEllipse(Pens.Green, rectangle);
        }

        private void ShowLine(P2D A, P2D B, Pen p)
        {
            var pA = GetPoint(A.x, A.y);
            var pB = GetPoint(B.x, B.y);
            _g.DrawLine(p, pA, pB);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowSetUp();
            StartAlgo();
        }

        // Trace arc de cercle centre en M qui passe par P
        void ShowArc(P2D O, P2D P)
        {
            P2D V = P - O;
            double r = Math.Sqrt(V.x * V.x + V.y * V.y);
            var R = new Rectangle(GetPoint(O.x - r, O.y - r), new Size((int)(_mx * 2 * r + 0.5), (int)(_my * 2 * r + 0.5)));

            double a = Math.Atan2(V.y, V.x);
            if (a < 0)
                a += 2 * Math.PI;
            a *= 180 / Math.PI;
            _g.DrawArc(Pens.Green, R, (float)(a - 20), (float)40);

            ShowDist(O, P, Brushes.DarkGray);
        }

        void ShowDist(P2D A, P2D B, Brush b)
        {
            P2D V = B - A;
            double r = Math.Sqrt(V.x * V.x + V.y * V.y);
            var I = 0.5 * (A + B);
            _g.DrawString(String.Format("{0:0.000}", r), new Font("Courier", 8), b, GetPoint(I.x - 0.2, I.y - 0.1));
        }

        public void Display(P2D p)
        {
            ShowPoint(p);
            if (firstPoint)
            {
                // The good point !!!
                var O = new P2D(0, 0);
                ShowLine(p, O, Pens.Red);
                ShowDist(p, O, Brushes.Red);
                firstPoint = false;
            }


            if (p.g != null)
            {
                ShowPoint(p.g.A);
                ShowPoint(p.g.B);

                ShowLine(p, p.g.A, Pens.Gray);
                ShowLine(p, p.g.B, Pens.Gray);
                ShowLine(p.g.A, p.g.B, Pens.Yellow);

                ShowArc(p.g.A, p);
                ShowArc(p.g.B, p);
            }
        }

        public void Display(D2 d)
        {
            ShowLine(d.A, d.B, Pens.Blue);
            ShowDist(d.A, d.B, Brushes.DarkGreen);
            //ShowPoint(d.A);
            //ShowPoint(d.B);
        }

        public void StartAlgo()
        {
            firstPoint = true;

            var mp = new MyPoints();
            mp.GenDistances2();
            Console.WriteLine(mp);
            mp.CheckPointMatch(this);


            mp.GenPoints();
            mp.GenDistances2();
            Console.WriteLine(mp);
            mp.CheckPointMatch(this);

            mp.GenPoints();
            mp.GenDistances2();
            Console.WriteLine(mp);
            mp.CheckPointMatch(this);

            mp.GenPoints();
            mp.CheckPointMatch(this);
        }
    }
}
