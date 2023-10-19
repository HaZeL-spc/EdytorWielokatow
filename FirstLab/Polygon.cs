﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace FirstLab
{
    public class Polygon : IEnumerable
    {
        public List<Point> polygon = new List<Point>();
        public Polygon(List<Point> polygon)
        {
            this.polygon = new List<Point>(polygon);
        }

        public Polygon(Polygon newPolygon)
        {
            this.polygon = new List<Point>(newPolygon.polygon);
        }

        public void movePolygon(int x, int y)
        {
            List<Point> newPolygon = new List<Point>();

            for (int i = 0; i < polygon.Count; i++)
            {
                Point point = polygon[i];
                newPolygon.Add(new Point(point.X + x, point.Y + y));
            }

            this.polygon = new List<Point>(newPolygon);
        }

        public void moveLine(int index, int xMove, int yMove)
        {
            Point point1 = this.polygon[index];
            Point point2 = this.polygon[(index + 1) % polygon.Count()];

            this.polygon[index] = new Point(point1.X + xMove, point1.Y + yMove);
            this.polygon[(index + 1) % polygon.Count()] = new Point(point2.X + xMove, point2.Y + yMove);
        }

        public void AddToPolygon(Point point)
        {
            this.polygon.Add(point);
        }

        public int Count()
        {
            return this.polygon.Count;
        }

        // Implement the IEnumerable.GetEnumerator method
        public IEnumerator<Point> GetEnumerator()
        {
            return polygon.GetEnumerator();
        }

        // Implement the non-generic IEnumerable.GetEnumerator method
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int CountHowManyTimeIntersected(int x, int y)
        {
            var copyPolygon = new Polygon(polygon);
            copyPolygon.AddToPolygon(polygon[0]);

            Point? previousPoint = null;
            int howManyTimes = 0;

            foreach (var point in copyPolygon)
            {
                if (previousPoint != null)
                    if (TellIfIntersected(previousPoint.Value, point, x, y))
                        howManyTimes++;

                previousPoint = point;
            }

            return howManyTimes;
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
                y = -y;
                (float a, float b) = CalculateLinearFunction(x1, y1, x2, y2);
                float valueY = a * x + b;

                if (a > 0 && y > valueY)
                    return true;
                else if (a < 0 && y < valueY)
                    return true;
            }

            return false;
        }

        public int CheckWhichVerticeClicked(int x, int y)
        {
            int j = 0;
            foreach (var point in polygon)
            {
                if (Math.Abs(point.X - x) < Form1.RADIUS * 2 && Math.Abs(point.Y - y) < Form1.RADIUS * 2)
                    return j;
                j++;
            }

            return -1;
        }

        public int CheckWhichCenterVerticeClicked(int x, int y)
        {
            var copyPolygon = new Polygon(polygon);
            copyPolygon.AddToPolygon(polygon[0]);
            Point? previousPoint = null;
            int i = 0;

            foreach (var point in copyPolygon)
            {
                if (previousPoint != null)
                {
                    Point centerPoint = FindCenterOfLine(previousPoint.Value, point);

                    if (CalculateDistancePoints(new Point(x, y), centerPoint) <= Form1.RADIUS)
                        return i;
                }

                i++;
                previousPoint = point;
            }

            return -1;
        }

        public int CheckWhichLineClicked(int x, int y)
        {
            var copyPolygon = new Polygon(polygon);
            copyPolygon.AddToPolygon(polygon[0]);
            Point? previousPoint = null;
            int i = -1;
            y = -y;

            foreach (var point in copyPolygon)
            {
                if (previousPoint != null)
                {
                    (float a, float b) = CalculateLinearFunction(point.X, point.Y, previousPoint.Value.X, previousPoint.Value.Y);


                    if (x >= Math.Min(point.X, previousPoint.Value.X) && x <= Math.Max(point.X, previousPoint.Value.X))
                        if (CalculateDistancePointLine(x, y, a, b) <= Form1.LINE_ERROR)
                            return i;
                }

                i++;
                previousPoint = point;
            }

            return -1;
        }

        public static float CalculateDistancePointLine(int x, int y, float a, float b)
        {
            float numerator = Math.Abs(-a * x + y - b);
            float denominator = (float)Math.Sqrt(Math.Pow(a, 2.0) + 1);

            return numerator / denominator;
        }

        public static float CalculateDistancePoints(Point p1, Point p2)
        {
            return (float)Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        public static (float, float) CalculateLinearFunction(float x1, float y1, float x2, float y2)
        {
            y1 = -y1; y2 = -y2;
            float a = (y2 - y1) / (x2 - x1);
            float b = y1 - a * x1;

            return (a, b);
        }

        public static Point FindCenterOfLine(Point p1, Point p2)
        {
            return new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
        }

        public void Remove(int index)
        {
            if (polygon.Count() > 3)
                this.polygon.RemoveAt(index);
        }

        public void InsertAtIndex(int index, Point p)
        {
            this.polygon.Insert(index, p);
        }

        public Point this[int index]
        {
            get
            {
                if (index >= 0 && index < polygon.Count())
                {
                    return this.polygon[index];
                }
                else
                {
                    throw new IndexOutOfRangeException("Index is out of range.");
                }
            }
            set
            {
                if (index >= 0 && index < polygon.Count())
                {
                    polygon[index] = value;
                }
                else
                {
                    throw new IndexOutOfRangeException("Index is out of range.");
                }
            }
        }
    }
}
