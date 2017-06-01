#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : Vector2d.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion
using System;

namespace ubta.Common.Maths
{
    public class Vector2d
    {
        #region Constructors

        public Vector2d()
        {
            this.my2DPoint.X = 0.0;
            this.my2DPoint.Y = 0.0;
        }

        public Vector2d(Vector2d vec2D)
        {
            this.my2DPoint.X = vec2D.my2DPoint.X;
            this.my2DPoint.Y = vec2D.my2DPoint.Y;
        }

        public Vector2d(double x_value, double y_value)
        {
            this.my2DPoint.X = x_value;
            this.my2DPoint.Y = y_value;
        }
        #endregion

        #region Operators

        public static Vector2d operator +(Vector2d v1, Vector2d v2)
        {
            Vector2d sum = new Vector2d(
                v1.my2DPoint.X + v2.my2DPoint.X,
                v1.my2DPoint.Y + v2.my2DPoint.Y);
            return (sum);
        }

        public static Vector2d Add(Vector2d v1, Vector2d v2)
        {
            Vector2d sum = new Vector2d(
                v1.my2DPoint.X + v2.my2DPoint.X,
                v1.my2DPoint.Y + v2.my2DPoint.Y);
            return (sum);
        }

        public static Vector2d operator -(Vector2d v1, Vector2d v2)
        {
            Vector2d diff = new Vector2d(
                v1.my2DPoint.X - v2.my2DPoint.X,
                v1.my2DPoint.Y - v2.my2DPoint.Y);
            return (diff);
        }

        public static Vector2d Subtract(Vector2d v1, Vector2d v2)
        {
            Vector2d diff = new Vector2d(
                v1.my2DPoint.X - v2.my2DPoint.X,
                v1.my2DPoint.Y - v2.my2DPoint.Y);
            return (diff);
        }

        public static Vector2d operator *(Vector2d v1, double mul)
        {
            Vector2d result = new Vector2d(
                v1.my2DPoint.X * mul,
                v1.my2DPoint.Y * mul);
            return (result);
        }

        public static Vector2d operator *(double mul, Vector2d v1)
        {
            Vector2d result = new Vector2d(
                v1.my2DPoint.X * mul,
                v1.my2DPoint.Y * mul);
            return (result);
        }

        public static Vector2d Multiply(Vector2d v1, double mul)
        {
            Vector2d result = new Vector2d(
                v1.my2DPoint.X * mul,
                v1.my2DPoint.Y * mul);
            return (result);
        }

        public static Vector2d operator /(Vector2d v1, double div)
        {
            if ((System.Double.IsNaN(div)) || (div == 0.0))
            {
                throw new System.ArgumentOutOfRangeException("div", "Invalid division by zero. The value was: " + div + " .");
            }

            Vector2d result = new Vector2d(
            v1.my2DPoint.X / div,
            v1.my2DPoint.Y / div);
            return (result);
        }

        public static Vector2d Divide(Vector2d v1, double div)
        {
            if ((System.Double.IsNaN(div)) || (div == 0.0))
            {
                throw new System.ArgumentOutOfRangeException("div", "Invalid division by zero. The value was: " + div + " .");
            }

            Vector2d result = new Vector2d(
                v1.my2DPoint.X / div,
                v1.my2DPoint.Y / div);
            return (result);
        }

        #endregion

        #region Set / get Methods

        public void Init(double initVal)
        {
            this.my2DPoint.X = initVal;
            this.my2DPoint.Y = initVal;
        }

        public void SetComponents(double xval, double yval)
        {
            this.my2DPoint.X = xval;
            this.my2DPoint.Y = yval;
        }

        public void GetComponents(out double xval, out double yval)
        {
            xval = this.my2DPoint.X;
            yval = this.my2DPoint.Y;
        }

        public void SetFromVector2d(Vector2d vec2D)
        {
            this.my2DPoint.X = vec2D.my2DPoint.X;
            this.my2DPoint.Y = vec2D.my2DPoint.Y;
        }

        public void SetFromInvertedVector2d(Vector2d vec2D)
        {
            this.my2DPoint.X = -vec2D.my2DPoint.X;
            this.my2DPoint.Y = -vec2D.my2DPoint.Y;
        }

        public double X
        {
            get
            {
                return this.my2DPoint.X;
            }

            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("X", "invalid set X component " + value);
                }
                this.my2DPoint.X = value;
            }
        }
        public double Y
        {
            get
            {
                return this.my2DPoint.Y;
            }

            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("Y", "invalid set Y component " + value);
                }
                this.my2DPoint.Y = value;
            }
        }

        public void SetFromPolar(double vectorLength_in, Vector2d directionCosinus_in, double tolerance)
        {
            this.my2DPoint.X = vectorLength_in * directionCosinus_in.my2DPoint.X;
            this.my2DPoint.Y = vectorLength_in * directionCosinus_in.my2DPoint.Y;

            // check and throw an exception if the resultant vector isNil
            double length2 = ((this.my2DPoint.X * this.my2DPoint.X) +
                              (this.my2DPoint.Y * this.my2DPoint.Y));

            if (length2 < (tolerance * tolerance))
            {
                throw new System.ArithmeticException("The vector IsNil. Its length is: " + Math.Sqrt(length2) + " .");
            }
        }

        public void SetFromPolar(double vectorLength_in, double angle_in, double tolerance)
        {
            this.my2DPoint.X = vectorLength_in * Math.Cos(angle_in);
            this.my2DPoint.Y = vectorLength_in * Math.Sin(angle_in);

            double length2 = ((this.my2DPoint.X * this.my2DPoint.X) +
                              (this.my2DPoint.Y * this.my2DPoint.Y));

            // check and throw an exception if the resultant vector isNil
            if (length2 < (tolerance * tolerance))
            {
                throw new System.ArithmeticException("The vector IsNil. Its length is: " + Math.Sqrt(length2) + " .");
            }
        }

        public override string ToString()
        {
            return this.my2DPoint.ToString();
        }

        #endregion

        #region Arithmetic Methods

        public void Invert()
        {
            this.my2DPoint.X = -this.my2DPoint.X;
            this.my2DPoint.Y = -this.my2DPoint.Y;
            return;
        }

        public double GetLength()
        {
            return Math.Sqrt((this.my2DPoint.X * this.my2DPoint.X) +
                             (this.my2DPoint.Y * this.my2DPoint.Y));
        }

        public void SetLength(double length, double tolerance)
        {
            this.Normalize(tolerance);
            this.my2DPoint.X *= length;
            this.my2DPoint.Y *= length;
        }

        public Vector2d GetNormalized(double tolerance)
        {
            double length;
            length = Math.Sqrt((this.my2DPoint.X * this.my2DPoint.X) +
                               (this.my2DPoint.Y * this.my2DPoint.Y));

            if (length < tolerance)
            {
                throw new System.ArithmeticException("The vector IsNil. Its length is " + length + " .");
            }
            else
            {
                Vector2d vec_out = new Vector2d(
                this.my2DPoint.X / length,
                this.my2DPoint.Y / length);
                return vec_out;
            }
        }

        public void Normalize(double tolerance)
        {
            double length;
            length = Math.Sqrt((this.my2DPoint.X * this.my2DPoint.X) +
                               (this.my2DPoint.Y * this.my2DPoint.Y));

            if (length < tolerance)
            {
                throw new System.ArithmeticException("The vector IsNil. Its length is " + length + " .");
            }
            else
            {
                this.my2DPoint.X /= length;
                this.my2DPoint.Y /= length;
                return;
            }
        }

        public bool IsNormalized(double tolerance)
        {
            double length2 = ((this.my2DPoint.X * this.my2DPoint.X) +
                              (this.my2DPoint.Y * this.my2DPoint.Y));

            if ((length2 < ((1.0 + tolerance) * (1.0 + tolerance))) &&
                 (length2 > ((1.0 - tolerance) * (1.0 - tolerance))))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsNil(double tolerance)
        {
            double length2 = ((this.my2DPoint.X * this.my2DPoint.X) +
                              (this.my2DPoint.Y * this.my2DPoint.Y));

            if (length2 < (tolerance * tolerance))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsAlmostEqual(Vector2d v1, double tolerance)
        {
            return (((v1.my2DPoint.X - this.my2DPoint.X) * (v1.my2DPoint.X - this.my2DPoint.X) +
                     (v1.my2DPoint.Y - this.my2DPoint.Y) * (v1.my2DPoint.Y - this.my2DPoint.Y)) <
                     (tolerance * tolerance));
        }

        public bool IsLonger(Vector2d v1, double tolerance)
        {
            double l1 = (this.my2DPoint.X * this.my2DPoint.X) + (this.my2DPoint.Y * this.my2DPoint.Y);
            double l2 = (v1.my2DPoint.X * v1.my2DPoint.X) + (v1.my2DPoint.Y * v1.my2DPoint.Y);
            if ((l1 - l2) > tolerance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public double GetInnerProduct(Vector2d v1)
        {
            return ((v1.my2DPoint.X * this.my2DPoint.X) + (v1.my2DPoint.Y * this.my2DPoint.Y));
        }

        public double GetDistanceToLine(Vector2d line_start, Vector2d line_end)
        {
            // Get the nearest point upon the line
            Vector2d nearestPointOnLine = GetNearestPointUponLine(line_start, line_end);

            // Calculate the distance
            Vector2d dist_v = this - nearestPointOnLine;

            return dist_v.GetLength();
        }

        public Vector2d GetNearestPointUponLine(Vector2d line_start, Vector2d line_end)
        {
            // calculate the line vector
            Vector2d line = line_end - line_start;

            // calculate the length of the line
            double lineLength = line.GetLength();

            // find the nearest point on the line to the given point
            Vector2d nearestPointOnLine = null;

            if (GraphicMath.DEFAULT_TOLERANCE > lineLength)
            {
                // line is degenerated to a point
                // so the nearest point is either start or end
                nearestPointOnLine = new Vector2d(line_start);
            }
            else
            {
                // v_st is a vector from the beginning of the line to the 
                // point in question
                Vector2d v_st = this - line_start;

                // normalize line
                line /= lineLength;

                // calculate inner product u = v_st * line     i.e.
                // u = |v_st| * |1| * cos (angle)
                // angle is the angle between v_st and line
                // u = 0 if angle = 90 degrees
                // u > 0 if v_st and line are      parallel (when v_st is rotated about angle)
                // u < 0 if v_st and line are anti parallel (when v_st is rotated about angle)
                double u = v_st.GetInnerProduct(line);  // scalar product

                // explicitly calculate the nearest point on line
                if (u < 0.0)             // befor start of line
                {
                    nearestPointOnLine = new Vector2d(line_start);
                }
                else if (u > lineLength) // after end of line
                {
                    nearestPointOnLine = new Vector2d(line_end);
                }
                else                      // on line
                {
                    nearestPointOnLine = (line_start + (line * u));
                }
            }
            return nearestPointOnLine;
        }

        public double GetDistanceToInfiniteLine(Vector2d line_P1, Vector2d line_P2)
        {
            // find the nearest point on the line to the given point
            Vector2d nearestPointOnLine = GetNearestPointUponInfiniteLine(line_P1, line_P2);

            // calculate the distance
            Vector2d dist_v = this - nearestPointOnLine;

            return dist_v.GetLength();
        }


        public Vector2d GetNearestPointUponInfiniteLine(Vector2d line_P1, Vector2d line_P2)
        {
            // calculate the line vector
            Vector2d line = line_P2 - line_P1;

            // calculate the length of the line
            double lineLength = line.GetLength();

            // The nearest point
            Vector2d nearestPointOnLine = null;

            if (GraphicMath.DEFAULT_TOLERANCE > lineLength)
            {
                // line is degenerated to a point
                // so the nearest point is either P1 or P2
                nearestPointOnLine = new Vector2d(line_P1);
            }
            else
            {
                // v_st is a vector from P1 of the line to the 
                // point in question
                Vector2d v_st = this - line_P1;

                // normalize line
                line /= lineLength;

                // calculate inner product u = v_st * line     i.e.
                // u = |v_st| * |1| * cos (angle)
                // angle is the angle between v_st and line
                // u = 0 if angle = 90 degrees
                // u > 0 if v_st and line are      parallel (when v_st is rotated about angle)
                // u < 0 if v_st and line are anti parallel (when v_st is rotated about angle)
                double u = v_st.GetInnerProduct(line);  // scalar product

                // calculate the nearest point on the line
                nearestPointOnLine = (line_P1 + (line * u));
            }
            return nearestPointOnLine;
        }

        public void Rotate(double rotation_angle, ref Vector2d[] vector_list)
        {
            double r_sin = Math.Sin(-rotation_angle);
            double r_cos = Math.Cos(-rotation_angle);
            double x_rot = 0.0;
            double y_rot = 0.0;
            double x_scal = 0.0;
            double y_scal = 0.0;

            //rotate about origin if rotation_point = (0.0, 0.0)
            if ((this.my2DPoint.X == 0.0) && (this.my2DPoint.Y == 0.0))
            {
                // x_rot =   x_orig * cos(-angle) + y_orig * sin(-angle)
                // y_rot = - x_orig * sin(-angle) + y_orig * cos(-angle)
                foreach (Vector2d vec in vector_list)
                {
                    x_rot = vec.my2DPoint.X * r_cos + vec.my2DPoint.Y * r_sin;
                    y_rot = -vec.my2DPoint.X * r_sin + vec.my2DPoint.Y * r_cos;
                    vec.my2DPoint.X = x_rot;
                    vec.my2DPoint.Y = y_rot;
                }
            }
            // if rotation point is not the origin
            // shift to origin - rotate - and shift back
            else
            {
                foreach (Vector2d vec in vector_list)
                {
                    x_scal = vec.my2DPoint.X - this.my2DPoint.X;
                    y_scal = vec.my2DPoint.Y - this.my2DPoint.Y;
                    x_rot = x_scal * r_cos + y_scal * r_sin;
                    y_rot = -x_scal * r_sin + y_scal * r_cos;
                    vec.my2DPoint.X = x_rot + this.my2DPoint.X;
                    vec.my2DPoint.Y = y_rot + this.my2DPoint.Y;
                }
            }
        }

        public double GetAngle(Vector2d v1, double tolerance)
        {
            double len1 = v1.GetLength();
            double len2 = this.GetLength();
            if (len1 < GraphicMath.DEFAULT_TOLERANCE)
            {
                throw new ArithmeticException("The specified vector is too small. Its length is: " + len1 + " .");
            }
            if (len2 < GraphicMath.DEFAULT_TOLERANCE)
            {
                throw new ArithmeticException("The actual vector is too small. Its length is: " + len2 + " .");
            }
            // (inner product)/(len1 * len2) should be within the range -1 <= 0 <= 1 
            double cos = this.GetInnerProduct(v1) / (len1 * len2);
            if ((-1.0 <= cos) && (1.0 >= cos))
            {
                return Math.Acos(cos);
            }
            else if (-1.0 > cos)
            {
                return Math.PI; // -1 = cos( 180 deg )
            }
            else
            {
                return 0.0; //  1 = cos( 0 deg )
            }
        }

        public static Vector2d Project2dPointOntoLine(
            Vector2d point, Vector2d linePoint, Vector2d lineDirection)
        {
            // compute the normal vector of the 2D line
            // i.e. swap x and y coordinate and negate new x coordinate
            Vector2d normal = new Vector2d(-lineDirection.my2DPoint.Y, lineDirection.my2DPoint.X);
            normal.Normalize(GraphicMath.DEFAULT_TOLERANCE); // exception if direction isNil

            // compute the vector from the point on the line to the 2D point which should e projected
            Vector2d diff = point - linePoint;

            // compute distance 2D point to line using   inner product = |normal=1| * |projection of diff onto normal| 
            // Sign of inner product is positive if normal and projection of diff onto normal are of the same direction.
            double distance = normal.GetInnerProduct(diff);

            // compute the projected 2D point
            return (point - normal * distance);
            // if the direction of the normal vector is pointing towards the point 
            // the distance is positive and the direction normal*distance is pointing away from the line
            // -> result = point - normal*distance
            // if the direction of the normal vector is pointing away from the point 
            // the distance is negative and the direction normal*distance is also pointing away from the line
            // because the negative distance inverses the direction of the normal vector. And again
            // -> result = point - (normal*distance)  
        }
        #endregion

        #region Protected Methods


        #endregion

        #region Data

        public System.Windows.Point my2DPoint;

        #endregion
    }
}