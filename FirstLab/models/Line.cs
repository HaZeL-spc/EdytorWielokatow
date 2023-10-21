using FirstLab.models;
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
        public OptionTypeEnum option;

        public Line(Point start, Point end)
        {
            this.Start = start;
            this.End = end;
            this.option = OptionTypeEnum.Nothing;
        }

        public void Move(int xMove, int yMove)
        {
            this.Start = new Point(Start.X + xMove, Start.Y + yMove);
            this.End = new Point(End.X + xMove, End.Y + yMove);
        }
    }
}
