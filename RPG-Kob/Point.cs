using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Kob
{
    class Point
    {
        private readonly int x, y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Point(Point a)
        {
            this.x = a.X;
            this.y = a.Y;
        }

        public int X { get { return x; } }
        public int Y { get { return y; } }

        public static Point operator +(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);

        public override bool Equals(object obj) => (obj is Point) && ((Point)obj).X == this.X && ((Point)obj).Y == this.Y;

        public bool CheckPoint(int width, int height)
        {
            if ((this.X < 0 || this.X > width) || (this.Y < 0 || this.Y > height)) return true;
            return false;
        }
    }
}
