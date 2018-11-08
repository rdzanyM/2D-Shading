using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        /// <summary>
        /// Parameter defining animated light source position.
        /// Incremented after every frame with animated light source.
        /// </summary>
        double t = 0;
        /// <summary>
        /// Which vertex is moving (-1 if none)
        /// </summary>
        int moving = -1;
        /// <summary>
        /// Colors of two triangles
        /// </summary>
        Color[] colors = new Color[2];
        Color lightColor = Color.White;
        /// <summary>
        /// Position of light source (x,y,z) where z > 0
        /// </summary>
        double[] lightPos = new double[3];
        /// <summary>
        /// Used in filling algorithm
        /// </summary>
        List<Edge> AET = new List<Edge>();
        Pen pen = new Pen(Brushes.Black, 2);
        DirectBitmap bmp = new DirectBitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
        /// <summary>
        /// Vertices of two triangles
        /// </summary>
        Point[] points = { new Point(10, 10), new Point(20, 300), new Point(300, 20), new Point(400, 300), new Point(50, 350), new Point(260, 10) };

        public Form1()
        {
            InitializeComponent();
            pictureBox.Image = bmp.Bitmap;
            g = Graphics.FromImage(bmp.Bitmap);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            colors[0] = Color.Aqua;
            colors[1] = Color.Crimson;
            constantLight.Checked = true;
            Redraw();
        }
        
        /// <summary>
        /// Draws a new Frame.
        /// Triggered every 40ms.
        /// </summary>
        private void Redraw()
        {
            g.Clear(Color.White);
            Fill();
            DrawTriangle(points[0], points[1], points[2]);
            DrawTriangle(points[3], points[4], points[5]);
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

        private bool OffScreen(Point p)
        {
            return p.X < 8 || p.Y < 8 || p.X > 800 || p.Y > 600;
        }

        private void Fill()
        {
            List<Task> tasks = new List<Task>();
            if (constantLight.Checked)
            {
                lightPos[0] = 100;
                lightPos[1] = 100;
                lightPos[2] = 100;
            }
            else
            {
                lightPos[0] = Math.Abs(Screen.PrimaryScreen.WorkingArea.Width  * Math.Sin(t/2.22) / 5);
                lightPos[1] = Math.Abs(Screen.PrimaryScreen.WorkingArea.Height * Math.Sin(t/Math.E) / 5);
                lightPos[2] = 60 + Math.Sin(2*t) * 50;
                t += 0.01;
            }
            Edge[] ET = new Edge[Screen.PrimaryScreen.WorkingArea.Height];
            int k = 0;
            Add(points[0], points[1]);
            Add(points[1], points[2]);
            Add(points[2], points[0]);
            int y = 0;
            while (k < 3 || AET.Count > 0)
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
                    FillLine((int)AET[i].x, (int)AET[i + 1].x, y, colors[0]);
                y++;
                foreach (Edge e in AET) e.x += e.d;
            }
            Task.WaitAll(tasks.ToArray());
            tasks.Clear();
            k = 0;
            Add(points[3], points[4]);
            Add(points[4], points[5]);
            Add(points[5], points[3]);
            y = 0;
            while (k < 3 || AET.Count > 0)
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
                    FillLine((int)AET[i].x, (int)AET[i + 1].x, y, colors[1]);
                y++;
                foreach (Edge e in AET) e.x += e.d;
            }
            Task.WaitAll(tasks.ToArray());

            void FillLine(int i, int max, int yy, Color c)
            {
                Task t = new Task(() => TaskFor(i, max, yy, c));
                tasks.Add(t);
                t.Start();
            }
            
            void TaskFor(int s, int max, int yy, Color c)
            {
                double[] lightV = { 0, lightPos[1] - yy, lightPos[2] };
                for (int i = s; i < max; i++)
                {
                    double[] normalV = { 0, 0, 1 };
                    lightV[0] = lightPos[0] - i;
                    bmp.SetPixel(i, yy, Multiply(c, lightColor, Cos(normalV, lightV)));
                }
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

            Color Multiply(Color c1, Color c2, double d)
            {
                return Color.FromArgb((int)(d * c1.R * c2.R / 255), (int)(d * c1.G * c2.G / 255), (int)(d * c1.B * c2.B / 255));
            }

            double Cos(double[] v1, double[] v2)
            {
                double d1 = Math.Sqrt(v1[0] * v1[0] + v1[1] * v1[1] + v1[2] * v1[2]);
                double d2 = Math.Sqrt(v2[0] * v2[0] + v2[1] * v2[1] + v2[2] * v2[2]);
                return (v1[0] * v2[0] + v1[1] * v2[1] + v1[2] * v2[2]) / d1 / d2;
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
                    return;
                }
            }

            double Distance(Point p1, Point p2)
            {
                int a = p1.X - p2.X;
                int b = p1.Y - p2.Y;
                return Math.Sqrt(a * a + b * b);
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            moving = -1;
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving >= 0 && !OffScreen(e.Location))  points[moving] = e.Location;
        }

        //Triangle1
        private void flatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK) colors[0] = cd.Color;
            Redraw();
        }

        private void textureToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //Triangle2
        private void flatToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK) colors[1] = cd.Color;
            Redraw();
        }

        private void textureToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        //Light
        private void colorToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK) lightColor = cd.Color;
            Redraw();
        }

        private void constantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            constantLight.Checked = true;
            variableLight.Checked = false;
        }

        private void variableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            variableLight.Checked = true;
            constantLight.Checked = false;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Redraw();
        }

        /// <summary>
        /// Used in filling algorithm
        /// </summary>
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
    }
}
