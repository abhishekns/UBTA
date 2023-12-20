using System;

namespace ubta.Common.Maths
{
    public class Point2D {
        public double X;
        public double Y;
        public Point2D(double x, double y) {
            X = x;
            Y = y;
        }
        public Point2D() {
        }
        public override string ToString() {
            return "(" + X + ", " + Y + ")";
        }
    } 
}