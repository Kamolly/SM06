using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SM06
{
    public partial class UserControl1: UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(Form1_Load);
        }
        private List<PointF> points = new List<PointF>();
        private const int PointRadius = 3;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = 800;
            this.Height = 600;

            Button generateButton = new Button
            {
                Text = "Generate Polygon",
                Location = new Point(10, 10)
            };
            generateButton.Click += GenerateButton_Click;
            this.Controls.Add(generateButton);

            this.Paint += DrawingPanel_Paint;
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            points.Clear();
            int n = 10;

            for (int i = 0; i < n; i++)
            {
                points.Add(new PointF(rand.Next(50, 700), rand.Next(50, 450)));
            }

            points = points.OrderBy(p => Math.Atan2(p.Y - points.Average(pt => pt.Y), p.X - points.Average(pt => pt.X))).ToList();

            this.Invalidate();
        }

        private void DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen polygonPen = new Pen(Color.Red, 2);
            Brush pointBrush = Brushes.Blue;

            foreach (var point in points)
            {
                g.FillEllipse(pointBrush, point.X - PointRadius, point.Y - PointRadius, PointRadius * 2, PointRadius * 2);
            }

            if (points.Count > 1)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    PointF p1 = points[i];
                    PointF p2 = points[(i + 1) % points.Count];
                    g.DrawLine(polygonPen, p1, p2);
                }
            }
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
