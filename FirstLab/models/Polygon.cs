﻿using FirstLab.models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace FirstLab
{
    public class Polygon : IEnumerable
    {
        public List<Point> polygon = new List<Point>();
        public LinesOptionList linesOption = new LinesOptionList(0);
        public List<Point> outlinePolygon = new List<Point>();

        public Polygon(List<Point> polygon)
        {
            this.polygon = new List<Point>(polygon);
            this.linesOption = new LinesOptionList(polygon.Count);
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

        public void AddToPolygon(Point point)
        {
            this.polygon.Add(point);
            this.linesOption.Add();
        }

        public void Remove(int index)
        {
            if (polygon.Count() > 3)
            {
                this.polygon.RemoveAt(index);
                this.linesOption.RemoveAt(index);
            }
        }

        public void InsertAtIndex(int index, Point p)
        {
            this.polygon.Insert(index, p);
            this.linesOption.Insert(index);
        }

        public int Count()
        {
            return this.polygon.Count;
        }

        public void ModifyPoint(int index, int x, int y, Point previousPoint)
        {
            OptionTypeEnum first = linesOption[index];
            OptionTypeEnum second = linesOption[index - 1];

            (int, int) moveCoords = (x - previousPoint.X, y - previousPoint.Y);
            //MessageBox.Show($"({moveCoords.Item1}, {moveCoords.Item2})");
            this.polygon[index] = new Point(this.polygon[index].X + moveCoords.Item1, this.polygon[index].Y + moveCoords.Item2);

            if (first == OptionTypeEnum.Nothing && second == OptionTypeEnum.Nothing)
                return;

            int count = this.polygon.Count;
            int i;

            for (i = index - 1; i > index - count; i--)
            {
                if (linesOption[i] == OptionTypeEnum.Nothing)
                    break;

                Point p = this[i];
                this[i] = new Point(p.X + moveCoords.Item1, p.Y + moveCoords.Item2);
            }

            int j;
            for (j = index + 1; j < i + count + 1; j++)
            {
                if (linesOption[(j - 1) % count] == OptionTypeEnum.Nothing)
                    break;

                Point p = this[j % count];
                this[j % count] = new Point(p.X + moveCoords.Item1, p.Y + moveCoords.Item2);
            }

        }

        public void moveLine(int index, int x, int y, Point previousPoint)
        {
            Point point1 = this.polygon[index];
            Point point2 = this.polygon[(index + 1) % polygon.Count()];

            (int, int) moveCoords = (x - previousPoint.X, y - previousPoint.Y);
            this.polygon[index] = new Point(point1.X + moveCoords.Item1, point1.Y + moveCoords.Item2);
            this.polygon[(index + 1) % polygon.Count()] = new Point(point2.X + moveCoords.Item1, point2.Y + moveCoords.Item2);

            OptionTypeEnum first = linesOption[(index + 1) % polygon.Count()];
            OptionTypeEnum second = linesOption[index - 1];

            if (first == OptionTypeEnum.Nothing && second == OptionTypeEnum.Nothing)
                return;

            int count = this.polygon.Count;
            int i;

            for (i = index; i > index - count + 2; i--)
            {
                if (linesOption[i - 1] == OptionTypeEnum.Nothing)
                    break;

                point2 = this[i - 1];
                this[i - 1] = new Point(point2.X + moveCoords.Item1, point2.Y + moveCoords.Item2);
                //this[i - 1].Offset(moveCoords.Item1, moveCoords.Item2);
            }

            int j;
            for (j = index + 1; j < i + count - 1; j++)
            {
                if (linesOption[j % count] == OptionTypeEnum.Nothing)
                    break;

                point2 = this[(j + 1) % count];
                //point1.Offset
                this[(j + 1) % count] = new Point(point2.X + moveCoords.Item1, point2.Y + moveCoords.Item2);
            }
        }

        public bool CheckIntersectUp(int x, int y)
        {
            Point? previousPoint = null;
            var copyPolygon = new List<Point>(polygon);
            copyPolygon.Add(polygon[0]);
            int howManyTimes = 0;

            foreach (var point in copyPolygon)
            {
                if (previousPoint != null)
                {
                    float x1 = previousPoint.Value.X;
                    float y1 = previousPoint.Value.Y;
                    float x2 = point.X;
                    float y2 = point.Y;

                    if (y > y1 && y > y2)  // line is above
                    {
                        if (x <= Math.Max(x1, x2) && x >= Math.Min(x1, x2))
                            howManyTimes++;
                    }
                    else if (x <= Math.Max(x1, x2) && x >= Math.Min(x1, x2) && y <= Math.Max(y1, y2) && y >= Math.Min(y1, y2)) // point is in between
                    {
                        int newY = -y;
                        (float a, float b) = CalculateLinearFunction(x1, y1, x2, y2);
                        float valueY = a * x + b;

                        //if (a > 0 && y > valueY)
                        //    return true;
                        //else if (a < 0 && y < valueY)
                        //    return true;
                        if (newY - 5 > valueY) howManyTimes++;
                    }
                }

                previousPoint = point;
            }
            return howManyTimes % 2 == 1 ? false : true;
        }

        public static (float, float) NormalizeVec(float x, float y)
        {
            float distance = (float)Math.Sqrt(x * x + y * y);
            return (x/distance, y/distance);

        }

        public void PrepareOutline(int distance)
        {
            //int count = this.polygon.Count;
            //int outer_ccw = 1;
            //outlinePolygon = new List<Point>();

            //for (int i = 0; i < count; i++)
            //{
            //    int prev = (i + count - 1) % count;
            //    int next = (i + 1) % count;

            //    int vnX = polygon[next].X - polygon[i].X;
            //    int vnY = polygon[next].Y - polygon[i].Y;
            //    (float vnnX, float vnnY) = NormalizeVec(vnX, vnY);
            //    float nnnX = vnnX;
            //    float nnnY = -vnnY;

            //    int vpX = polygon[i].X - polygon[prev].X;
            //    int vpY = polygon[i].Y - polygon[prev].Y;

            //    (float vpnX, float vpnY) = NormalizeVec(vpX, vpY);

            //    float npnX = vpnY * outer_ccw;
            //    float npnY = -vpnX * outer_ccw;

            //    float bisX = (nnnX + npnX) * outer_ccw;
            //    float bisY = (nnnY + npnY) * outer_ccw;

            //    (float bisnX, float bisnY) = NormalizeVec(bisX, bisY);
            //    float bislen = (float)(distance / Math.Sqrt((1 + nnnX * npnX + nnnY * npnY) / 2));

            //    float newX = polygon[i].X + bislen * bisnX;
            //    float newY = polygon[i].Y + bislen * bisnY;
            //    outlinePolygon.Add(new Point((int)newX, (int)newY));
            //}
            this.outlinePolygon = new List<Point>();

            var copyPolygon = new List<Point>(polygon);
            copyPolygon.Add(polygon[0]);
            copyPolygon.Add(polygon[1]);
            Point? previousPoint = null;
            Point? previousPreviousPoint = null;
            int x = 0, y = 0;
            int i = 2;
            int count = Count();
            Point firstCenter = FindCenterOfLine(polygon[0], polygon[1]);
            Point secondCenter = FindCenterOfLine(polygon[1], polygon[2]);

            //(bool, bool) isUp = (CheckIntersectUp(firstCenter.X, firstCenter.Y), CheckIntersectUp(secondCenter.X, secondCenter.Y));
            (bool, bool) isUp = (false, false);


            foreach (var point in copyPolygon)
            {
                if (previousPoint != null && previousPreviousPoint != null)
                {
                    firstCenter = FindCenterOfLine(previousPreviousPoint.Value, previousPoint.Value);
                    secondCenter = FindCenterOfLine(previousPoint.Value, point);

                    isUp = (CheckIntersectUp(firstCenter.X, firstCenter.Y), CheckIntersectUp(secondCenter.X, secondCenter.Y));

                    (float a1, float b1) = CalculateLinearFunction(previousPreviousPoint.Value.X, previousPreviousPoint.Value.Y, previousPoint.Value.X, previousPoint.Value.Y);
                    (float a2, float b2) = CalculateLinearFunction(previousPoint.Value.X, previousPoint.Value.Y, point.X, point.Y);

                    if (float.IsInfinity(a1) || float.IsInfinity(a2))
                    {
                        if (float.IsInfinity(a1) && float.IsFinite(a2))
                        {
                            (float aNew2, float bNew2) = CalculateParallelLine(a2, b2, true, (float)distance);
                            (x, y) = (previousPoint.Value.X - distance, (int)(a2 * previousPoint.Value.X + b2 + distance));

                        }
                        else if (float.IsFinite(a1) && float.IsInfinity(a2))
                        {
                            (x, y) = (previousPoint.Value.X - distance, (int)(a1 * previousPoint.Value.X + b1 + distance));
                        }
                    }
                    else
                    {


                        (float aNew1, float bNew1) = CalculateParallelLine(a1, b1, isUp.Item1, (float)distance);
                        (float aNew2, float bNew2) = CalculateParallelLine(a2, b2, isUp.Item2, (float)distance);

                        (x, y) = FindIntersectionLinearFunction(aNew1, bNew1, aNew2, bNew2);
                    }

                    //isUp.Item1 = isUp.Item2;
                    ////if ( point.X - previousPoint.Value.X < 0 && previousPoint.Value.X - previousPreviousPoint.Value.X < 0)
                    //if (HaveSameSign(point.X - previousPoint.Value.X, previousPoint.Value.X - previousPreviousPoint.Value.X))
                    //    isUp.Item2 = isUp.Item2;
                    //else
                    //    isUp.Item2 = !isUp.Item2;
                    //isUp = (isUp.Item2, CheckIntersectUp(copyPolygon[i % count].X, copyPolygon[i % count].Y));

                    firstCenter = FindCenterOfLine(previousPreviousPoint.Value, previousPoint.Value);
                    secondCenter = FindCenterOfLine(previousPoint.Value, point);



                    outlinePolygon.Add(new Point(x, -y));
                    i++;
                }
                if (previousPoint != null)
                    previousPreviousPoint = previousPoint;

                previousPoint = point;


            }


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

                    if (float.IsPositiveInfinity(a) || float.IsNegativeInfinity(a))
                    {
                        if (x >= point.X - Form1.LINE_ERROR && x <= point.X + Form1.LINE_ERROR && -y >= Math.Min(point.Y, previousPoint.Value.Y) && -y <= Math.Max(point.Y, previousPoint.Value.Y))
                            return i;
                    } else
                    {
                        if (x >= Math.Min(point.X, previousPoint.Value.X) && x <= Math.Max(point.X, previousPoint.Value.X))
                            if (CalculateDistancePointLine(x, y, a, b) <= Form1.LINE_ERROR)
                                return i;
                    }
                }

                i++;
                previousPoint = point;
            }

            return -1;
        }

        public void ChangeOptionLineHandling(int index, OptionTypeEnum option)
        {
            this.linesOption.ChangeOption(index, option);

            if (option == OptionTypeEnum.Nothing) return;

            if (option == OptionTypeEnum.Horizontal)
            {
                Point point = this.polygon[(index + 1) % Count()];
                Point previousPoint = this.polygon[index];

                int distance = previousPoint.X - point.X;
                
                this.polygon[(index + 1) % Count()] = new Point(previousPoint.X - (int)distance, previousPoint.Y);

            }
            else if (option == OptionTypeEnum.Vertical)
            {
                Point point = this.polygon[(index + 1) % Count()];
                Point previousPoint = this.polygon[index];

                int distance = point.Y - previousPoint.Y;
                this.polygon[(index + 1) % Count()] = new Point(previousPoint.X, previousPoint.Y + (int)distance);
            }
        }

        public static bool HaveSameSign(double expressionA, double expressionB)
        {
            if (expressionA > 0 && expressionB > 0)
            {
                return true; // Both positive
            }
            else if (expressionA < 0 && expressionB < 0)
            {
                return true; // Both negative
            }
            else if (expressionA == 0 && expressionB == 0)
            {
                return true; // Both zero
            }
            else
            {
                return false; // Different signs
            }
        }


        public static (float, float) CalculateParallelLine(float a, float b, bool shiftUp, float distance)
        {
             distance = shiftUp ? distance : -distance;

            float newB = b + distance * (float)Math.Sqrt(Math.Pow(a, 2) + 1);

            return (a, newB);
        }
        
        public static (int, int) FindIntersectionLinearFunction(float a1, float b1, float a2, float b2)
        {
            int x = (int)((b2 - b1) / (a1 - a2));
            int y = (int)(a1 * x + b1);

            return (x, y);
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

        // calculates center of line and makes two icons on two sides of line (prostopadle) to the line
        public static (Point, Point) CalculatePointsDistanceFromPoint(Point p, Point p1, Point p2, int distance)
        {

            (float a, float b) = Polygon.CalculateLinearFunction(p1.X, p1.Y, p2.X, p2.Y);

            float aNew = -1 / a;
            float bNew = -p.X * aNew + p.Y;

            Point icon1 = new Point((int)(p.X + 1.5 * distance * (float)(1 / (Math.Sqrt(1 + Math.Pow(aNew, 2))))),
                (int)(p.Y + 1.5 * distance * (float)(aNew / Math.Sqrt(1 + Math.Pow(aNew, 2)))));

            Point icon2 = new Point((int)(p.X - 0.5 * distance * (float)(1 / (Math.Sqrt(1 + Math.Pow(aNew, 2))))),
                (int)(p.Y - 0.5 * distance * (float)(aNew / Math.Sqrt(1 + Math.Pow(aNew, 2)))));


            return (icon1, icon2);
        }

        public static Point FindCenterOfLine(Point p1, Point p2)
        {
            return new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
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
                    if (index < 0 && Math.Abs(index) <= polygon.Count)
                    {
                        return this.polygon[this.polygon.Count + index];
                    }

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
                    if (index < 0 && Math.Abs(index) <= polygon.Count)
                    {
                        polygon[this.polygon.Count + index] = value;
                    } else
                        throw new IndexOutOfRangeException("Index is out of range.");


                }
            }
        }
    }
}
