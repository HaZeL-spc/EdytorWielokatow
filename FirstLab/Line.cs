using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstLab
{
    public class Line
    {
        public Point Start;
        public Point End;

        public Line(Point start, Point end)
        {
            this.Start = start;
            this.End = end;
        }
    }
}
