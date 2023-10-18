using System.Drawing;
using System.Windows.Forms;

namespace FirstLab
{
    public partial class Form1 : Form
    {
        private Bitmap drawArea;
        private Polygon points = new Polygon(new List<Point>());
        private List<Line> lines = new List<Line>();
        public List<Polygon> polygons = new List<Polygon>();
        private const int RADIUS = 10;
        private Pen pen = new Pen(Color.Black, 2);
        private Point previousMouse;

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
                    if (points.Count() == 0)
                    {
                        (int, int) cords = checkIfVerticeClicked(e.X, e.Y);

                        if (cords.Item1 != -1)
                        {
                            return;
                        }

                        int whichPolygonClicked = CheckWhichInsidePolygon(e.X, e.Y);

                        if (whichPolygonClicked != -1)
                        {
                            return;
                        }
                    }

                    // drawing the line
                    if (points.Count() > 2 && Math.Abs(e.X - points.polygon[0].X) < RADIUS && Math.Abs(e.Y - points.polygon[0].Y) < RADIUS)
                    {
                        polygons.Add(points);
                        points = new Polygon(new List<Point>());
                    }
                    else
                    {
                        // adding point
                        Point point = new Point(e.X, e.Y);
                        points.AddToPolygon(point);
                    }
                }
            }

        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                using (Graphics g = Graphics.FromImage(drawArea))
                {
                    if (points.Count() == 0)
                    {
                        (int, int) cords = checkIfVerticeClicked(e.X, e.Y);

                        if (cords.Item1 != -1)
                            polygons[cords.Item1][cords.Item2] = new Point(e.X, e.Y);
                        else
                        {
                            int whichPolygonClicked = CheckWhichInsidePolygon(e.X, e.Y);

                            if (whichPolygonClicked != -1)
                            {

                                polygons[whichPolygonClicked].movePolygon(e.X - previousMouse.X, e.Y - previousMouse.Y);
                            }
                        }
                    }
                }

            }

            previousMouse = new Point(e.X, e.Y);
            Canvas.Invalidate();
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 2);

            e.Graphics.DrawImage(drawArea, 0, 0);
            if (points.Count() > 0)
            {
                g.DrawLine(pen, points[points.Count() - 1], Canvas.PointToClient(Cursor.Position));

                pen.Dispose();
            }

            Point? previousPoint;
            pen = new Pen(Color.Black, 2);
            foreach (var polygon in polygons)
            {
                previousPoint = null;

                foreach (var point in polygon)
                {
                    g.FillEllipse(Brushes.Black, point.X - RADIUS, point.Y - RADIUS, RADIUS * 2, RADIUS * 2);

                    if (previousPoint != null)
                        g.DrawLine(pen, previousPoint.Value, point);

                    previousPoint = point;
                }

                g.DrawLine(pen, previousPoint.Value, polygon[0]);
            }
            pen.Dispose();
            pen = new Pen(Color.Black, 2);

            previousPoint = null;
            foreach (var point in points)
            {
                g.FillEllipse(Brushes.Black, point.X - RADIUS, point.Y - RADIUS, RADIUS * 2, RADIUS * 2);

                if (previousPoint != null)
                    g.DrawLine(pen, previousPoint.Value, point);
                previousPoint = point;
            }

            pen.Dispose();
        }

        public int CheckWhichInsidePolygon(int x, int y)
        {
            int[] polygonsIntersected = new int[polygons.Count];

            Point? previousPoint;
            int i = 0;
            foreach (var polygon in polygons)
            {
                var copyPolygon = new Polygon(polygon);
                copyPolygon.AddToPolygon(polygon[0]);

                previousPoint = null;
                foreach (var point in copyPolygon)
                {

                    if (previousPoint != null)
                        if (TellIfIntersected(previousPoint.Value, point, x, y))
                            polygonsIntersected[i]++;

                    previousPoint = point;
                }

                i++;
            }

            for (i = 0; i < polygonsIntersected.Count(); i++)
            {
                if (polygonsIntersected[i] == 1)
                    return i;
            }

            return -1;
        }

        public bool TellIfIntersected(Point point1, Point point2, int x, int y)
        {
            float x1 = point1.X;
            float y1 = point1.Y;
            float x2 = point2.X;
            float y2 = point2.Y;

            if (x < x1 && x < x2)  // line is on the right
            {
                if (y <= Math.Max(y1, y2) && y >= Math.Min(y1, y2))
                    return true;
            }
            else if (x <= Math.Max(x1, x2) && x >= Math.Min(x1, x2) && y <= Math.Max(y1, y2) && y >= Math.Min(y1, y2)) // point is in between
            {
                y1 = -y1; y2 = -y2; y = -y;
                float a = (y2 - y1) / (x2 - x1);
                float b = y1 - a * x1;
                float valueY = a * x + b;

                if (a > 0 && y > valueY)
                    return true;
                else if (a < 0 && y < valueY)
                    return true;
            }

            return false;
        }

        public (int, int) checkIfVerticeClicked(int x, int y)
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