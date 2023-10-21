using System.Drawing;
using System.Globalization;
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
        public const int LINE_ERROR = 14;
        private Pen pen = new Pen(Color.Black, 2);
        private Point previousMouse;
        private bool mouseIsDown = false;
        public (int, int) indexVerticeClicked = (-1, -1);
        public (int, int) indexWhichLineHover = (-1, -1);
        public (int, int) indexLineClicked = (-1, -1);
        public int indexPolygonClicked = -1;
        public AlgorithmTypeEnum algorithmType = AlgorithmTypeEnum.Biblioteczny;

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
                        if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                        {
                            (int, int) coordsLine = CheckIfLineClicked(e.X, e.Y);
                            if (coordsLine.Item1 != -1)
                            {
                                ShowPopupLineType(e.X, e.Y, coordsLine);
                            }
                        } else
                        {
                            (int, int) coords = CheckIfVerticeClicked(e.X, e.Y);
                            if (coords.Item1 != -1)
                            {
                                polygons[coords.Item1].Remove(coords.Item2);
                                return;
                            }

                            int whichPolygonClicked = CheckWhichInsidePolygon(e.X, e.Y);

                            if (whichPolygonClicked != -1)
                            {
                                polygons.RemoveAt(whichPolygonClicked);
                            }
                        }
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
                        (int, int) coordsCenterVerice = CheckIfCenterVerticeClicked(e.X, e.Y);

                        if (coordsCenterVerice.Item1 != -1)
                            polygons[coordsCenterVerice.Item1].InsertAtIndex(coordsCenterVerice.Item2, new Point(e.X, e.Y));

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
            //if (indexVerticeClicked.Item1 != -1)
            //    indexLineIconsShow = (-1, -1);
            //else
            //{
            //    (int, int) whichLine = CheckIfLineClicked(e.X, e.Y);

            //    if (whichLine.Item1 != -1)
            //        indexLineIconsShow = whichLine;
            //}
            (int, int) whichLineHovered = CheckIfLineClicked(e.X, e.Y);
            if (whichLineHovered.Item1 != -1)
            {
                indexWhichLineHover = whichLineHovered;
            }


            if (e.Button == MouseButtons.Left)
            {
                mouseIsDown = true;
                using (Graphics g = Graphics.FromImage(drawArea))
                {
                    if (points.Count() == 0)
                    {
                        if (indexVerticeClicked != (-1, -1))
                        {
                            polygons[indexVerticeClicked.Item1].ModifyPoint(indexVerticeClicked.Item2, e.X, e.Y, previousMouse);
                        }
                            //polygons[indexVerticeClicked.Item1][indexVerticeClicked.Item2] = new Point(e.X, e.Y);
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
                                //polygons[cords.Item1][cords.Item2] = new Point(e.X, e.Y);
                                //polygons[cords.Item1].ModifyPoint(cords.Item2, e.X, e.Y, previousMouse);
                            }
                            else
                            {
                                (int, int) coordsLine = CheckIfLineClicked(e.X, e.Y);

                                if (coordsLine != (-1, -1))
                                {
                                    indexLineClicked = coordsLine;
                                    polygons[coordsLine.Item1].moveLine(coordsLine.Item2, e.X - previousMouse.X, e.Y - previousMouse.Y);
                                }
                                else
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

            //e.Graphics.DrawImage(drawArea, 0, 0);
            if (points.Count() > 0)
            {
                if (algorithmType == AlgorithmTypeEnum.Biblioteczny)
                    g.DrawLine(pen, points[points.Count() - 1], Canvas.PointToClient(Cursor.Position));
                else
                    Bresenham(points[points.Count() - 1], Canvas.PointToClient(Cursor.Position), g);

                pen.Dispose();
            }

            //if (indexLineIconsShow.Item1 != -1)
            //{
            //    Point p1 = polygons[indexLineIconsShow.Item1][indexLineIconsShow.Item2];
            //    Point p2 = polygons[indexLineIconsShow.Item1][(indexLineIconsShow.Item2 + 1) % polygons[indexLineIconsShow.Item1].Count()];
            //    //DrawIcons(p1, p2, g);
            //} 

            Point? previousPoint;
            pen = new Pen(Color.Black, 2);
            foreach (var polygon in polygons)
            {
                var copyPolygon = new Polygon(polygon);
                copyPolygon.AddToPolygon(polygon[0]);
                previousPoint = null;

                foreach (var point in copyPolygon)
                {
                    g.FillEllipse(Brushes.Black, point.X - RADIUS, point.Y - RADIUS, RADIUS * 2, RADIUS * 2);

                    if (previousPoint != null)
                    {
                        if (algorithmType == AlgorithmTypeEnum.Biblioteczny)
                            g.DrawLine(pen, previousPoint.Value, point);
                        else
                            Bresenham(previousPoint.Value, point, g);

                        Point p = Polygon.FindCenterOfLine(point, previousPoint.Value);
                        g.FillEllipse(Brushes.Gray, p.X - RADIUS / 2, p.Y - RADIUS / 2, RADIUS, RADIUS);
                    }


                    previousPoint = point;
                }
            }

            pen.Dispose();
            pen = new Pen(Color.Black, 2);

            previousPoint = null;
            foreach (var point in points)
            {
                g.FillEllipse(Brushes.Black, point.X - RADIUS, point.Y - RADIUS, RADIUS * 2, RADIUS * 2);

                if (previousPoint != null)
                {
                    if (algorithmType == AlgorithmTypeEnum.Biblioteczny)
                        g.DrawLine(pen, previousPoint.Value, point);
                    else
                        Bresenham(previousPoint.Value, point, g);
                }

                previousPoint = point;
            }

            pen.Dispose();
        }

        private void Bresenham(Point p1, Point p2, Graphics g)
        {

            int x1 = p1.X, x2 = p2.X, y1 = p1.Y, y2 = p2.Y;
            if (x1 > x2)
            {
                x1 = x2;
                x2 = p1.X;

                y1 = y2;
                y2 = p1.Y;
            }

            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);
            int sx = (x1 < x2) ? 1 : -1;
            int sy = (y1 < y2) ? 1 : -1;
            int err = dx - dy;
            Pen pen = new Pen(Color.Black);

            while (true)
            {
                g.DrawRectangle(pen, x1, y1, 1, 1);

                if (x1 == x2 && y1 == y2)
                    break;

                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x1 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y1 += sy;
                }
            }
        }

        private void DrawIcons(Point p1, Point p2, Graphics g)
        {
            Point p = Polygon.FindCenterOfLine(p1, p2);

            //(Point icon1, Point icon2) = Polygon.CalculatePointsDistanceFromPoint(p, p1, p2, RADIUS * 2);

            //g.FillEllipse(Brushes.Blue, icon1.X, icon1.Y, RADIUS * 2, RADIUS * 2);
            //g.FillEllipse(Brushes.Blue, icon2.X, icon2.Y, RADIUS * 2, RADIUS * 2);
        }

        public int MainEventLoop(int x, int y)
        {
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

        public int CheckWhichInsidePolygon(int x, int y)
        {
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
                int whichVertice = polygon.CheckWhichVerticeClicked(x, y);

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

                i++;
            }

            return (-1, -1);
        }

        public (int, int) CheckIfCenterVerticeClicked(int x, int y)
        {
            int i = 0;

            foreach (var polygon in polygons)
            {
                var whichVertice = polygon.CheckWhichCenterVerticeClicked(x, y);

                if (whichVertice != -1)
                    return (i, whichVertice);
                i++;
            }

            return (-1, -1);
        }

        public void ShowPopupLineType(int x, int y, (int, int) indexEl)
        {
            (bool, bool) options = polygons[indexEl.Item1].linesOption.WhichOptionAvailable(indexEl.Item2);

            using (var customPopup = new PopupRelation(options.Item1, options.Item2, this.polygons[indexEl.Item1].linesOption[indexEl.Item2]))
            {
                customPopup.ShowDialog();

                this.polygons[indexEl.Item1].ChangeOptionLineHandling(indexEl.Item2, Form1.OptionChosenPopup);
            }
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
            indexVerticeClicked = (-1, -1);
            indexPolygonClicked = -1;
            indexLineClicked = (-1, -1);
        }

        private void bibliotecznyRadio_CheckedChanged(object sender, EventArgs e)
        {
            algorithmType = AlgorithmTypeEnum.Biblioteczny;
        }

        private void bresenhamRadio_CheckedChanged(object sender, EventArgs e)
        {
            algorithmType = AlgorithmTypeEnum.Bresenham;
        }
    }

}