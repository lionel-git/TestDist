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

            ShowPoint(-1.0, 0.0);
            ShowPoint(+1.0, 0.0);
        }

        private Point GetPoint(double x, double y)
        {
            return new Point((int)(Width / 2 + _mx * x), (int)(Height / 2 + _my * y));
        }

        private void ShowPoint(double x, double y)
        {
            var p = GetPoint(x, y);
            Rectangle rectangle = new Rectangle(p.X - L, p.Y - L, 2*L, 2*L);
            _g.DrawEllipse(Pens.Red, rectangle);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowSetUp();
        }

        public void Display(P2D p)
        {
          
        }

        public void Display(D2 d)
        {
          
        }
    }
}
