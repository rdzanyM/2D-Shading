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
        private class TriangleInfo
        {
            /// <summary>
            /// The power in phong reflection model is equal to 2 ^ <see cref="phongFactor"/>.
            /// The higher the power the narrower the reflection.
            /// </summary>
            public int phongFactor = 4;
            /// <summary>
            /// How much of the light is reflected.
            /// 1 - <see cref="phongWeight"/> is diffused.
            /// </summary>
            public double phongWeight = 0.7;
            /// <summary>
            /// Triangle color
            /// </summary>
            public DirectBitmap texture;
            /// <summary>
            /// Map of normal vectors from image
            /// </summary>
            public NormalMap normalMap = new NormalMap();
            /// <summary>
            /// Map of height vectors from image
            /// </summary>
            public HeightMap heightMap = new HeightMap();
            /// <summary>
            /// Map of normal vectors with applied height disturbance
            /// </summary>
            public NormalMap vectorMap;
            /// <summary>
            /// Assigns the value to <see cref="vectorMap"/> based on <see cref="normalMap"/> and <see cref="heightMap"/>
            /// Dimensions of <see cref="vectorMap"/> are the same as in c.
            /// </summary>
            public void Disturb(Control c)
            {
                vectorMap = normalMap.Disturb(heightMap, c);
            }
        }
        Graphics gfx;
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
        /// Has nothing changed since last call to <see cref="Redraw"/>
        /// </summary>
        bool drawn = false;
        TriangleInfo[] triangles = { new TriangleInfo(), new TriangleInfo()};
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
        /// <summary>
        /// Main bitmap displayed on screen
        /// </summary>
        DirectBitmap bitmap = new DirectBitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
        /// <summary>
        /// Vertices of two triangles
        /// </summary>
        Point[] points = { new Point(10, 10), new Point(20, 500), new Point(800, 50), new Point(300, 600), new Point(800, 600), new Point(666, 20) };

        public Form1()
        {
            InitializeComponent();
            pictureBox.Image = bitmap.Bitmap;
            gfx = Graphics.FromImage(bitmap.Bitmap);
            gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            gfx.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            CrystalTreadPlateToolStripMenuItem_Click(null, null);
            pictureBox_T1_Normal.BackColor = Color.FromArgb(127, 127, 255);
            pictureBox_T2_Normal.BackColor = Color.FromArgb(127, 127, 255);
            pictureBox_T1_Height.BackColor = Color.FromArgb(255, 255, 255);
            pictureBox_T2_Height.BackColor = Color.FromArgb(255, 255, 255);
            timer.Enabled = true;
        }
        
        /// <summary>
        /// Draws a new Frame.
        /// </summary>
        private void Redraw()
        {
            gfx.Clear(Color.White);
            Fill();
            DrawTriangle(points[0], points[1], points[2]);
            DrawTriangle(points[3], points[4], points[5]);
            pictureBox.Refresh();

            void DrawTriangle(Point p1, Point p2, Point p3)
            {
                gfx.DrawLine(pen, p1, p2);
                gfx.DrawLine(pen, p2, p3);
                gfx.DrawLine(pen, p3, p1);
                gfx.DrawEllipse(pen, p1.X - 2, p1.Y - 2, 4, 4);
                gfx.DrawEllipse(pen, p2.X - 2, p2.Y - 2, 4, 4);
                gfx.DrawEllipse(pen, p3.X - 2, p3.Y - 2, 4, 4);
            }
        }

        private bool OffScreen(Point p)
        {
            return p.X < 4 || p.Y < 4 || p.X > pictureBox.Width - 4 || p.Y > pictureBox.Height - 4;
        }

        private void Fill()
        {
            List<Task> tasks = new List<Task>();
            if (radioButton_Constant.Checked)
            {
                lightPos[0] = 111;
                lightPos[1] = 111;
                lightPos[2] = 222;
            }
            else
            {
                lightPos[0] = Math.Abs(pictureBox.Width * Math.Sin(t/2));
                lightPos[1] = Math.Abs(pictureBox.Height * Math.Sin(t/2.37));
                lightPos[2] = 60 + Math.Sin(2*t) * 20;
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
                for (int i = 0; i < AET.Count - 1; i += 2)  //Filled polygons are triangles, so this loop will never be executed more than once (each line intersects triangle at no more than 2 places).
                    FillLineAsync((int)AET[i].x, (int)AET[i + 1].x, y, triangles[0]);
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
                for (int i = 0; i < AET.Count - 1; i += 2)  //Filled polygons are triangles, so this loop will never be executed more than once (each line intersects triangle at no more than 2 places).
                    FillLineAsync((int)AET[i].x, (int)AET[i + 1].x, y, triangles[1]);
                y++;
                foreach (Edge e in AET) e.x += e.d;
            }
            Task.WaitAll(tasks.ToArray());

            void FillLineAsync(int i, int max, int yy, TriangleInfo triangle)
            {
                Task t = new Task(() => FillLine(i, max, yy, triangle));
                tasks.Add(t);
                t.Start();
            }
            
            void FillLine(int s, int max, int yy, TriangleInfo triangle)
            {
                double[] lightV = { 0, lightPos[1] - yy, lightPos[2] };
                for (int i = s; i < max; i++)
                {
                    lightV[0] = lightPos[0] - i;
                    double length = Math.Sqrt(lightV[0] * lightV[0] + lightV[1] * lightV[1] + lightV[2] * lightV[2]);
                    double[] lightV1 = { lightV[0] / length, lightV[1] / length, lightV[2] / length };
                    double[] normalV = triangle.vectorMap.GetVector(i, yy);
                    double diffuseReflection = Cos(normalV, lightV1);
                    double ln = lightV1[0] * normalV[0] + lightV1[1] * normalV[1] + lightV1[2] * normalV[2];
                    double specularReflection = 2 * normalV[2] * ln - lightV1[2];
                    if(specularReflection < 0)
                        specularReflection = 0;
                    else for (int p = 0; p < triangle.phongFactor; p++)
                        specularReflection *= specularReflection;
                    bitmap.SetPixel(i, yy, Multiply(triangle.texture.GetPixel(i, yy), lightColor, (specularReflection * triangle.phongWeight + diffuseReflection * (1 - triangle.phongWeight)) / 255));
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
                return Color.FromArgb((int)(c1.R * c2.R * d), (int)(c1.G * c2.G * d), (int)(c1.B * c2.B * d));
            }

            double Cos(double[] v1, double[] v2) //v1, v2 are vectors of length 1
            {
                return Math.Max((v1[0] * v2[0] + v1[1] * v2[1] + v1[2] * v2[2]), 0);
            }

        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
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

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            moving = -1;
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving >= 0 && !OffScreen(e.Location))
            {
                points[moving] = e.Location;
                drawn = false;
            }
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            if (radioButton_Animated.Checked || !drawn)
            {
                drawn = true;
                Redraw();
            }
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

        /// <summary>
        /// Returns the bitmap scaled to width 90
        /// </summary>
        private Bitmap Scale90(Bitmap b)
        {
            double d = b.Width / 90.0;
            return new Bitmap(b as Image, 90, (int)(b.Height / d));
        }

        //Light
        private void Button_Light_Color_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                lightColor = cd.Color;
                pictureBox_Light_Color.BackColor = lightColor;
                pictureBox_Light_Color.Refresh();
                drawn = false;
            }
        }
        private void RadioButton_Constant_Click(object sender, EventArgs e)
        {
            radioButton_Constant.Text = "Constant [111, 111, 222]";
            radioButton_Constant.Refresh();
            drawn = false;
        }
        private void RadioButton_Animated_CheckedChanged(object sender, EventArgs e)
        {
            radioButton_Constant.Text = "Constant";
            radioButton_Constant.Refresh();
            drawn = false;
        }

        //Triangles
        private void Button_T1_Color_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Do you want to load a texture from an image?", "Color settings", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (OpenFileDialog dlg = new OpenFileDialog())
                {
                    dlg.Title = "Select Image";
                    dlg.Filter = "bmp files (*.bmp)|*.bmp";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        Bitmap b = new Bitmap(dlg.FileName);
                        triangles[0].texture = new DirectBitmap(b, Math.Min(pictureBox.Width, b.Width), Math.Min(pictureBox.Height, b.Height));
                        pictureBox_T1_Color.Image = Scale90(b);
                        pictureBox_T1_Color.Refresh();
                    }
                }
                drawn = false;
            }
            else
            {
                ColorDialog cd = new ColorDialog();
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    triangles[0].texture = new DirectBitmap(1, 1);
                    triangles[0].texture.SetPixel(0, 0, cd.Color);
                    pictureBox_T1_Color.BackColor = cd.Color;
                    pictureBox_T1_Color.Image = null;
                    pictureBox_T1_Color.Refresh();
                }
                drawn = false;
            }
        }

        private void Button_T2_Color_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to load a texture from an image?", "Color settings", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (OpenFileDialog dlg = new OpenFileDialog())
                {
                    dlg.Title = "Select Image";
                    dlg.Filter = "bmp files (*.bmp)|*.bmp";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        Bitmap b = new Bitmap(dlg.FileName);
                        triangles[1].texture = new DirectBitmap(b, Math.Min(pictureBox.Width, b.Width), Math.Min(pictureBox.Height, b.Height));
                        pictureBox_T2_Color.Image = Scale90(b);
                        pictureBox_T2_Color.Refresh();
                    }
                }
                drawn = false;
            }
            else
            {
                ColorDialog cd = new ColorDialog();
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    triangles[1].texture = new DirectBitmap(1, 1);
                    triangles[1].texture.SetPixel(0, 0, cd.Color);
                    pictureBox_T2_Color.BackColor = cd.Color;
                    pictureBox_T2_Color.Image = null;
                    pictureBox_T2_Color.Refresh();
                }
                drawn = false;
            }
        }

        private void Button_T1_Normal_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to load a default(flat) normal map?", "Remove normal map?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                triangles[0].normalMap = new NormalMap();
                triangles[0].Disturb(pictureBox);
                pictureBox_T1_Normal.Image = null;
                pictureBox_T1_Normal.Refresh();
            }
            else using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Select Image";
                dlg.Filter = "bmp files (*.bmp)|*.bmp";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    bool up = MessageBox.Show("Is the y(green) axis on image pointed up?", "Axis settings", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                    Bitmap b = new Bitmap(dlg.FileName);
                    triangles[0].normalMap = new NormalMap(b, Math.Min(pictureBox.Width, b.Width), Math.Min(pictureBox.Height, b.Height), up);
                    triangles[0].Disturb(pictureBox);
                    pictureBox_T1_Normal.Image = Scale90(b);
                    pictureBox_T1_Normal.Refresh();
                }
            }
            drawn = false;
        }

        private void Button_T2_Normal_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to load a default(flat) normal map?", "Remove normal map?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                triangles[1].normalMap = new NormalMap();
                triangles[1].Disturb(pictureBox);
                pictureBox_T2_Normal.Image = null;
                pictureBox_T2_Normal.Refresh();
            }
            else using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Select Image";
                dlg.Filter = "bmp files (*.bmp)|*.bmp";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    bool up = MessageBox.Show("Is the y(green) axis on image pointed up?", "Axis settings", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                    Bitmap b = new Bitmap(dlg.FileName);
                    triangles[1].normalMap = new NormalMap(b, Math.Min(pictureBox.Width, b.Width), Math.Min(pictureBox.Height, b.Height), up);
                    triangles[1].Disturb(pictureBox);
                    pictureBox_T2_Normal.Image = Scale90(b);
                    pictureBox_T2_Normal.Refresh();
                }
            }
            drawn = false;
        }

        private void Button_T1_Height_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to load a default(flat) height map?", "Remove height map?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                triangles[0].heightMap = new HeightMap();
                triangles[0].Disturb(pictureBox);
                pictureBox_T1_Height.Image = null;
                pictureBox_T1_Height.Refresh();
            }
            else using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "bmp files (*.bmp)|*.bmp";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Bitmap b = new Bitmap(dlg.FileName);
                    triangles[0].heightMap = new HeightMap(new Bitmap(dlg.FileName), Math.Min(pictureBox.Width, b.Width), Math.Min(pictureBox.Height, b.Height));
                    triangles[0].Disturb(pictureBox);
                    b = Scale90(b);
                    for(int i = 0; i < b.Width; i++)
                    {
                        for(int j = 0; j < b.Height; j++)
                        {
                            int k = (int)(b.GetPixel(i, j).GetBrightness() * 255);
                            b.SetPixel(i, j, Color.FromArgb(k, k, k));
                        }
                    }
                    pictureBox_T1_Height.Image = b;
                    pictureBox_T1_Height.Refresh();
                }
            }
            drawn = false;
        }

        private void Button_T2_Height_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to load a default(flat) height map?", "Remove height map?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                triangles[1].heightMap = new HeightMap();
                triangles[1].Disturb(pictureBox);
                pictureBox_T2_Height.Image = null;
                pictureBox_T2_Height.Refresh();
            }
            else using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "bmp files (*.bmp)|*.bmp";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Bitmap b = new Bitmap(dlg.FileName);
                    triangles[1].heightMap = new HeightMap(new Bitmap(dlg.FileName), Math.Min(pictureBox.Width, b.Width), Math.Min(pictureBox.Height, b.Height));
                    triangles[1].Disturb(pictureBox);
                    b = Scale90(b);
                    for (int i = 0; i < b.Width; i++)
                    {
                        for (int j = 0; j < b.Height; j++)
                        {
                            int k = (int)(b.GetPixel(i, j).GetBrightness() * 255);
                            b.SetPixel(i, j, Color.FromArgb(k, k, k));
                        }
                    }
                    pictureBox_T2_Height.Image = b;
                    pictureBox_T2_Height.Refresh();
                }
            }
            drawn = false;
        }

        private void TextBox_T1_Factor_Leave(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBox_T1_Factor.Text, out int i) && i >= 0 && i <= 12)
            {
                triangles[0].phongFactor = i;
                drawn = false;
            }
            else
            {
                textBox_T1_Factor.Text = triangles[0].phongFactor.ToString();
            }
        }

        private void TextBox_T2_Factor_Leave(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBox_T2_Factor.Text, out int i) && i >= 0 && i <= 12)
            {
                triangles[1].phongFactor = i;
                drawn = false;
            }
            else
            {
                textBox_T2_Factor.Text = triangles[1].phongFactor.ToString();
            }
        }

        private void TextBox_T1_Factor_TextChanged(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBox_T1_Factor.Text, out int i) && i >= 0 && i <= 12)
            {
                triangles[0].phongFactor = i;
                drawn = false;
            }
        }

        private void TextBox_T2_Factor_TextChanged(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBox_T2_Factor.Text, out int i) && i >= 0 && i <= 12)
            {
                triangles[1].phongFactor = i;
                drawn = false;
            }
        }

        private void TextBox_T1_Weight_TextChanged(object sender, EventArgs e)
        {
            if (Double.TryParse(textBox_T1_Weight.Text, out double d) && d >= 0 && d <= 1)
            {
                triangles[0].phongWeight = d;
                drawn = false;
            }
        }

        private void TextBox_T2_Weight_TextChanged(object sender, EventArgs e)
        {
            if (Double.TryParse(textBox_T2_Weight.Text, out double d) && d >= 0 && d <= 1)
            {
                triangles[1].phongWeight = d;
                drawn = false;
            }
        }

        private void TextBox_T1_Weight_Leave(object sender, EventArgs e)
        {
            if (Double.TryParse(textBox_T1_Weight.Text, out double d) && d >= 0 && d <= 1)
            {
                triangles[0].phongWeight = d;
                drawn = false;
            }
            else
            {
                textBox_T1_Weight.Text = triangles[0].phongWeight.ToString();
            }
        }

        private void TextBox_T2_Weight_Leave(object sender, EventArgs e)
        {
            if (Double.TryParse(textBox_T2_Weight.Text, out double d) && d >= 0 && d <= 1)
            {
                triangles[1].phongWeight = d;
                drawn = false;
            }
            else
            {
                textBox_T2_Weight.Text = triangles[1].phongWeight.ToString();
            }
        }

        private void CrystalTreadPlateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap b = Properties.Resources.Crystal_Texture;
            triangles[0].texture = new DirectBitmap(b, Math.Min(pictureBox.Width, b.Width), Math.Min(pictureBox.Height, b.Height));
            pictureBox_T1_Color.Image = Scale90(b);
            triangles[0].heightMap = new HeightMap(b, Math.Min(pictureBox.Width, b.Width), Math.Min(pictureBox.Height, b.Height));
            b = Scale90(b);
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    int k = (int)(b.GetPixel(i, j).GetBrightness() * 255);
                    b.SetPixel(i, j, Color.FromArgb(k, k, k));
                }
            }
            pictureBox_T1_Height.Image = b;
            b = Properties.Resources.Crystal_Normal;
            triangles[0].normalMap = new NormalMap(b, Math.Min(pictureBox.Width, b.Width), Math.Min(pictureBox.Height, b.Height), false);
            triangles[0].Disturb(pictureBox);
            pictureBox_T1_Normal.Image = Scale90(b);
            b = Properties.Resources.TreadPlate_Texture;
            triangles[1].texture = new DirectBitmap(b, Math.Min(pictureBox.Width, b.Width), Math.Min(pictureBox.Height, b.Height));
            pictureBox_T2_Color.Image = Scale90(b);
            triangles[1].heightMap = new HeightMap(b, Math.Min(pictureBox.Width, b.Width), Math.Min(pictureBox.Height, b.Height));
            b = Scale90(b);
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    int k = (int)(b.GetPixel(i, j).GetBrightness() * 255);
                    b.SetPixel(i, j, Color.FromArgb(k, k, k));
                }
            }
            pictureBox_T2_Height.Image = b;
            b = Properties.Resources.TreadPlate_Normal;
            triangles[1].normalMap = new NormalMap(b, Math.Min(pictureBox.Width, b.Width), Math.Min(pictureBox.Height, b.Height), false);
            triangles[1].Disturb(pictureBox);
            pictureBox_T2_Normal.Image = Scale90(b);
            triangles[0].phongFactor = 4;
            triangles[0].phongWeight = 0.7;
            triangles[1].phongFactor = 6;
            triangles[1].phongWeight = 0.6;
            textBox_T1_Factor.Text = triangles[0].phongFactor.ToString();
            textBox_T2_Factor.Text = triangles[1].phongFactor.ToString();
            textBox_T1_Weight.Text = triangles[0].phongWeight.ToString();
            textBox_T2_Weight.Text = triangles[1].phongWeight.ToString();
            textBox_T1_Factor.Refresh();
            textBox_T2_Factor.Refresh();
            textBox_T1_Weight.Refresh();
            textBox_T2_Weight.Refresh();
            pictureBox_T1_Color.Refresh();
            pictureBox_T1_Height.Refresh();
            pictureBox_T1_Normal.Refresh();
            pictureBox_T2_Color.Refresh();
            pictureBox_T2_Height.Refresh();
            pictureBox_T2_Normal.Refresh();
            drawn = false;
        }

        private void GlossyMattToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap b = Properties.Resources.Metal_Texture;
            triangles[0].texture = new DirectBitmap(b, Math.Min(pictureBox.Width, b.Width), Math.Min(pictureBox.Height, b.Height));
            triangles[1].texture = new DirectBitmap(b, Math.Min(pictureBox.Width, b.Width), Math.Min(pictureBox.Height, b.Height));
            pictureBox_T1_Color.Image = pictureBox_T2_Color.Image = Scale90(b);
            triangles[0].heightMap = new HeightMap(b, Math.Min(pictureBox.Width, b.Width), Math.Min(pictureBox.Height, b.Height));
            triangles[1].heightMap = new HeightMap(b, Math.Min(pictureBox.Width, b.Width), Math.Min(pictureBox.Height, b.Height));
            b = Scale90(b);
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    int k = (int)(b.GetPixel(i, j).GetBrightness() * 255);
                    b.SetPixel(i, j, Color.FromArgb(k, k, k));
                }
            }
            pictureBox_T1_Height.Image = pictureBox_T2_Height.Image = b;
            b = Properties.Resources.Small_Balls;
            triangles[0].normalMap = new NormalMap(b, Math.Min(pictureBox.Width, b.Width), Math.Min(pictureBox.Height, b.Height), true);
            triangles[1].normalMap = new NormalMap(b, Math.Min(pictureBox.Width, b.Width), Math.Min(pictureBox.Height, b.Height), true);
            triangles[0].Disturb(pictureBox);
            triangles[1].Disturb(pictureBox);
            pictureBox_T1_Normal.Image = pictureBox_T2_Normal.Image = Scale90(b);
            triangles[0].phongFactor = 3;
            triangles[0].phongWeight = 0.8;
            triangles[1].phongFactor = 4;
            triangles[1].phongWeight = 0.1;
            textBox_T1_Factor.Text = triangles[0].phongFactor.ToString();
            textBox_T2_Factor.Text = triangles[1].phongFactor.ToString();
            textBox_T1_Weight.Text = triangles[0].phongWeight.ToString();
            textBox_T2_Weight.Text = triangles[1].phongWeight.ToString();
            textBox_T1_Factor.Refresh();
            textBox_T2_Factor.Refresh();
            textBox_T1_Weight.Refresh();
            textBox_T2_Weight.Refresh();
            pictureBox_T1_Color.Refresh();
            pictureBox_T1_Height.Refresh();
            pictureBox_T1_Normal.Refresh();
            pictureBox_T2_Color.Refresh();
            pictureBox_T2_Height.Refresh();
            pictureBox_T2_Normal.Refresh();
            drawn = false;
        }
    }
}
