using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace FirstLab
{
    public class Polygon:IEnumerable
    {
        public List<Point> polygon = new List<Point>();
        public Polygon(List<Point> polygon) {
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
