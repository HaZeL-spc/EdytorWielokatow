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
        public const int RADIUS = 10;
        public const int LINE_ERROR = 15;
        private Pen pen = new Pen(Color.Black, 2);
        private Point previousMouse;
        private bool mouseIsDown = false;
        public (int, int) indexVerticeClicked = (-1, -1);
        public (int, int) indexLineClicked = (-1, -1);
        public int indexPolygonClicked = -1;

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
            if (e.Button == MouseButtons.Right)
            {
                using (Graphics g = Graphics.FromImage(drawArea))
                {
                    if (points.Count() == 0)
                    {
                        (int, int) coords = CheckIfVerticeClicked(e.X, e.Y);
                        if (coords.Item1 != -1)
                        {
                            polygons[coords.Item1].Remove(coords.Item2);
                            return;
                        }
                            


                        int whichPolygonClicked = CheckWhichInsidePolygon(e.X, e.Y);

                        if (whichPolygonClicked != -1)
                            polygons.RemoveAt(whichPolygonClicked);
                    }
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                mouseIsDown = true;
                using (Graphics g = Graphics.FromImage(drawArea))
                {
                    // if clicked on another polygons' vertice
                    if (points.Count() == 0)
                    {
                        (int, int) coordsLine = CheckIfLineClicked(e.X, e.Y);

                        if (coordsLine.Item1 != -1)
                            return;

                        (int, int) cords = CheckIfVerticeClicked(e.X, e.Y);

                        if (cords.Item1 != -1)
                            return;

                        int whichPolygonClicked = CheckWhichInsidePolygon(e.X, e.Y);

                        if (whichPolygonClicked != -1)
                            return;
                    }

                    if (points.Count() > 2 && Math.Abs(e.X - points.polygon[0].X) < RADIUS && Math.Abs(e.Y - points.polygon[0].Y) < RADIUS)
                    {
                        // check if point already exists there

                        polygons.Add(points);
                        points = new Polygon(new List<Point>());
                    }
                    else
                    {
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
                mouseIsDown = true;
                using (Graphics g = Graphics.FromImage(drawArea))
                {
                    if (points.Count() == 0)
                    {
                        if (indexVerticeClicked != (-1, -1))
                            polygons[indexVerticeClicked.Item1][indexVerticeClicked.Item2] = new Point(e.X, e.Y);
                        else if (indexLineClicked != (-1, -1))
                            polygons[indexLineClicked.Item1].moveLine(indexLineClicked.Item2, e.X - previousMouse.X, e.Y - previousMouse.Y);
                        else if (indexPolygonClicked != -1)
                            polygons[indexPolygonClicked].movePolygon(e.X - previousMouse.X, e.Y - previousMouse.Y);
                        else
                        {
                            // first time clicking new vertice
                            (int, int) cords = CheckIfVerticeClicked(e.X, e.Y);

                            if (cords.Item1 != -1)
                            {
                                indexVerticeClicked = cords;
                                polygons[cords.Item1][cords.Item2] = new Point(e.X, e.Y);
                            }
                            else
                            {
                                (int, int) coordsLine = CheckIfLineClicked(e.X, e.Y);

                                if (coordsLine != (-1, -1))
                                {
                                    indexLineClicked = coordsLine;
                                    polygons[coordsLine.Item1].moveLine(coordsLine.Item2, e.X - previousMouse.X, e.Y - previousMouse.Y);
                                } else
                                {
                                    int whichPolygonClicked = CheckWhichInsidePolygon(e.X, e.Y);

                                    if (whichPolygonClicked != -1)
                                    {
                                        indexPolygonClicked = whichPolygonClicked;
                                        polygons[whichPolygonClicked].movePolygon(e.X - previousMouse.X, e.Y - previousMouse.Y);
                                    }
                                }
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
                int howManyTimes = polygon.CountHowManyTimeIntersected(x, y);

                if (howManyTimes % 2 == 1)
                    return i;

                i++;
            }

            return -1;
        }


        public (int, int) CheckIfVerticeClicked(int x, int y)
        {
            int i = 0;

            foreach (var polygon in polygons)
            {
                int whichVertice = polygon.checkWhichVerticeClicked(x, y);

                if (whichVertice != -1)
                    return (i, whichVertice);
                i++;
            }

            return (-1, -1);
        }

        public (int, int) CheckIfLineClicked(int x, int y)
        {
            int i = 0;
            
            foreach (var polygon in polygons)
            {
                int whichLine = polygon.CheckWhichLineClicked(x, y);

                if (whichLine != -1)
                    return (i, whichLine);
            }

            return (-1, -1);
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
            indexVerticeClicked = (-1, -1);
            indexPolygonClicked = -1;
            indexLineClicked = (-1, -1);
        }
    }

}