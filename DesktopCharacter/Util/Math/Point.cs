using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCharacter.Util.Math
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point()
        {

        }

        public Point(System.Drawing.Point pt)
        {
            X = pt.X;
            Y = pt.Y;
        }

        public Point(System.Windows.Point pt)
        {
            X = pt.X;
            Y = pt.Y;
        }

        public Point( Point pt)
        {
            X = pt.X;
            Y = pt.Y;
        }

        public Point( double x, double y)
        {
            X = x;
            Y = y;
        }

        public Point(float x, float y)
        {
            X = (float)x;
            Y = (float)y;
        }

        public Point(int x, int y)
        {
            X = (double)x;
            Y = (double)y;
        }

        public Point( double v)
        {
            X = Y = v;
        }

        public static Point operator +(Point r, Point l)
        {
            return new Point { X = r.X + l.X, Y = r.Y + l.Y  };
        }

        public static Point operator -(Point r, Point l)
        {
            return new Point { X = r.X - l.X, Y = r.Y - l.Y };
        }

        public static Point operator *(Point r, Point l)
        {
            return new Point { X = r.X * l.X, Y = r.Y * l.Y };
        }

        public static Point operator /(Point r, Point l)
        {
            return new Point { X = r.X / l.X, Y = r.Y / l.Y };
        }

        public static bool operator ==(Point r, Point l)
        {
            return r.X == l.X && r.Y == l.Y;
        }

        public static bool operator !=(Point r, Point l)
        {
            return r.X != l.X && r.Y != l.Y;
        }

        public override string ToString()
        {
            return string.Format( "X = {0}, Y = {1} \n", X, Y );
        }

        protected bool Equals(Point other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Point)obj);
        }

        public override int GetHashCode()
        {
            return (X + Y).GetHashCode();
        }
    }
}
