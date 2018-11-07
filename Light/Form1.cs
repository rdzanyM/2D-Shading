using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Light
{
    public partial class Form1 : Form
    {
        Graphics g;
        Bitmap bmp = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
        Point[] points = { new Point(100, 100), new Point(100, 200), new Point(200, 100), new Point(200, 200), new Point(200, 300), new Point(300, 200) };
        Pen pen = new Pen(Brushes.Black, 1);
        int moving = -1;
        List<Edge> AET = new List<Edge>();

        class Edge
        {
            public Edge(int yMax, double xMin, double delta, Edge nextEdge)
            {
                y = yMax;
                x = xMin;
                d = delta;
                next = nextEdge;
            }
            public int y;
            public double x;
            public double d;
            public Edge next;
        }

        public Form1()
        {
            InitializeComponent();
            pictureBox.Image = bmp;
            g = Graphics.FromImage(bmp);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            Redraw();
        }

        private void Redraw()
        {
            g.Clear(Color.White);
            DrawTriangle(points[0], points[1], points[2]);
            DrawTriangle(points[3], points[4], points[5]);
            Fill();
            pictureBox.Refresh();

            void DrawTriangle(Point p1, Point p2, Point p3)
            {
                g.DrawLine(pen, p1, p2);
                g.DrawLine(pen, p2, p3);
                g.DrawLine(pen, p3, p1);
                g.DrawEllipse(pen, p1.X - 2, p1.Y - 2, 4, 4);
                g.DrawEllipse(pen, p2.X - 2, p2.Y - 2, 4, 4);
                g.DrawEllipse(pen, p3.X - 2, p3.Y - 2, 4, 4);
            }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            for (int i = 0; i < 6; i++)
            {
                if (Distance(points[i], e.Location) < 8)
                {
                    moving = i;
                    Redraw();
                    return;
                }
            }
        }

        private double Distance(Point p1, Point p2)
        {
            int a = p1.X - p2.X;
            int b = p1.Y - p2.Y;
            return Math.Sqrt(a * a + b * b);
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            moving = -1;
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving >= 0)
            {
                points[moving] = e.Location;
                Redraw();
            }
        }

        private void Fill()
        {
            Edge[] ET = new Edge[Screen.PrimaryScreen.WorkingArea.Height];
            int k = 0;
            Add(points[0], points[1]);
            Add(points[1], points[2]);
            Add(points[2], points[0]);
            Add(points[3], points[4]);
            Add(points[4], points[5]);
            Add(points[5], points[3]);

            int y = 0;
            while (k < 6 || AET.Count > 0)
            {
                AET.RemoveAll((e) => e.y == y);
                while (ET[y] != null)
                {
                    AET.Add(ET[y]);
                    ET[y] = ET[y].next;
                    k++;
                }
                AET.Sort((e1, e2) => e1.x.CompareTo(e2.x));
                for (int i = 0; i < AET.Count - 1; i += 2)
                {
                    for (int j = (int)AET[i].x; j <= AET[i + 1].x; j++)
                    {
                        bmp.SetPixel(j, y, Color.Black);
                    }
                }
                y++;
                foreach (Edge e in AET)
                    e.x += e.d;
            }


            void Add(Point p1, Point p2)
            {
                if (p1.Y == p2.Y)
                {
                    k++;
                    return;
                }
                if (p1.Y > p2.Y)
                {
                    Point p = p1;
                    p1 = p2;
                    p2 = p;
                }
                Edge e = new Edge(p2.Y, p1.X, (double)(p2.X - p1.X) / (p2.Y - p1.Y), ET[p1.Y]);
                ET[p1.Y] = e;
            }
        }
    }
}
