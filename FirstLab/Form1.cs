using System.Drawing;
using System.Windows.Forms;

namespace FirstLab
{
    public partial class Form1 : Form
    {
        private Bitmap drawArea;
        private List<Point> points = new List<Point>();
        private List<Line> lines = new List<Line>();
        private List<List<Point>> polygons = new List<List<Point>>();
        private const int RADIUS = 5;
        private Pen pen = new Pen(Color.Black, 2);

        public Form1()
        {
            InitializeComponent();

            drawArea = new Bitmap(Canvas.Size.Width, Canvas.Size.Height);
            Canvas.Image = drawArea;
            using (Graphics g = Graphics.FromImage(drawArea))
            {
                g.Clear(Color.White);
            }
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                using (Graphics g = Graphics.FromImage(drawArea))
                {
                    // if clicked on another polygons' vertice
                    if (points.Count == 0)
                    {
                        (int, int) cords = checkIfPoint(e.X, e.Y);

                        if (cords.Item1 != -1)
                        {
                            polygons[cords.Item1][cords.Item2] = new Point(10, 20);
                            PictureBox pictureBox = (PictureBox)sender;
                            g.Clear(Color.White);
                            pictureBox.Invalidate();
                            return;
                        }
                    }

                    // drawing the line
                    if (points.Count > 2 && Math.Abs(e.X - points[0].X) < RADIUS && Math.Abs(e.Y - points[0].Y) < RADIUS)
                    {
                        g.DrawLine(pen, points[points.Count - 1], points[0]);
                        polygons.Add(points);
                        points = new List<Point>();
                    } else
                    {
                        // adding point
                        Point point = new Point(e.X, e.Y);
                        points.Add(point);
                        g.FillEllipse(Brushes.Black, e.X - RADIUS, e.Y - RADIUS, RADIUS * 2, RADIUS * 2);

                        if (points.Count > 1)
                        {
                            g.DrawLine(pen, points[points.Count - 2], points[points.Count - 1]);
                            lines.Add(new Line(points[points.Count - 2], points[points.Count - 1]));
                        }
                    }
                }
                Canvas.Refresh();
            }

        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Canvas.Invalidate();
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 2);

            e.Graphics.DrawImage(drawArea, 0, 0);
            if (points.Count > 0)
            {
                
                g.DrawLine(pen, points[points.Count - 1], Canvas.PointToClient(Cursor.Position));

                pen.Dispose();
            }


            foreach (var polygon in polygons)
            {
                Point? previousPoint = null;
                foreach (var point in polygon)
                {
                    g.FillEllipse(Brushes.Black, point.X - RADIUS, point.Y - RADIUS, RADIUS * 2, RADIUS * 2);

                    if (previousPoint != null)
                    {
                        g.DrawLine(pen, previousPoint.Value, point);
                    }

                    previousPoint = point;
                }

                g.DrawLine(pen, previousPoint.Value, polygon[0]);
            }
        }

        private void CheckIfInsidePolygon(int x, int y)
        {
            int intersections = CountHorizontalLineIntersections(lines, y, x);
        }

        public static int CountHorizontalLineIntersections(List<Line> lines, int y, int x)
        {
            int intersectionCount = 0;

            foreach (var line in lines)
            {
                int x1 = line.Start.X;
                int y1 = line.Start.Y;
                int x2 = line.End.X;
                int y2 = line.End.Y;

                // Check if the line intersects the horizontal line
                if ( y1 < y && y2 > y || y1 > y && y2 < y)
                {
                    intersectionCount++;
                }
            }

            return intersectionCount;
        }

        public (int, int) checkIfPoint(int x, int y)
        {
            int i = 0;
            int j = 0;

            foreach (var polygon in polygons)
            {
                foreach (var point in polygon)
                {
                    if (Math.Abs(point.X - x) < RADIUS && Math.Abs(point.Y - y) < RADIUS)
                        return (i, j);
                    j++;
                }
                i++;
            }

            return (-1, -1);
        }
    }

}