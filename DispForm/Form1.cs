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
            _mx = (Width / 2.0) / 10.0;
            _my = (Height / 2.0) / 10.0;

            _g.DrawLine(Pens.Black, new Point(Width / 2, 0), new Point(Width / 2, Height));
            _g.DrawLine(Pens.Black, new Point(0, Height / 2), new Point(Width, Height / 2));
        }

        private Point GetPoint(double x, double y)
        {
            return new Point((int)(Width / 2 + _mx * x), (int)(Height / 2 + _my * y));
        }

        private void ShowPoint(P2D M)
        {
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

        public void Display(P2D p)
        {
            ShowPoint(p);
            if (p.g != null)
            {
                ShowPoint(p.g.A);
                ShowPoint(p.g.B);

                ShowLine(p, p.g.A, Pens.Blue);
                ShowLine(p, p.g.B, Pens.Blue);
                ShowLine(p.g.A, p.g.B, Pens.Yellow);         
            }
        }

        public void Display(D2 d)
        {
            ShowPoint(d.A);
            ShowPoint(d.B);
        }

        public void StartAlgo()
        {
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
