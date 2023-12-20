#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : Vector3d.cs
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
    
    public class Vector3d 
    {
        public Point2D my2DPoint;
        public Vector3d()
        {
        }

        public Vector3d(Vector4d v)
        {
            my2DPoint.X = v.X;
            my2DPoint.Y = v.Y;
            myZval = v.Z;
        }

        public double X
        {
            get
            {
                return my2DPoint.X;
            }
            set
            {
                my2DPoint.X = value;
            }
        }
        public double Y
        {
            get
            {
                return my2DPoint.Y;
            }
            set
            {
                my2DPoint.Y = value;
            }
        }

        public Vector3d(double x, double y) : this(x, y, 0)
        {
        }

        #region Enums
        public enum Orientation : short
        {
            Orthogonal = 0,
            SameDirection,
            OppositeDirection,
            Parallel,
            AntiParallel
        };
        #endregion Enums

        #region Constructors

        public Vector3d(Vector3d vec3D)
        {
            this.my2DPoint.X = vec3D.my2DPoint.X;
            this.my2DPoint.Y = vec3D.my2DPoint.Y;
            this.myZval = vec3D.myZval;
        }

        public Vector3d(double x_value, double y_value, double z_value)
        {
            this.my2DPoint.X = x_value;
            this.my2DPoint.Y = y_value;
            this.myZval = z_value;
        }
        #endregion

        #region Operators

        public static Vector3d operator +(Vector3d v1, Vector3d v2)
        {
            Vector3d sum = new Vector3d(
                v1.my2DPoint.X + v2.my2DPoint.X,
                v1.my2DPoint.Y + v2.my2DPoint.Y,
                v1.myZval + v2.myZval);
            return (sum);
        }

        public static Vector3d Add(Vector3d v1, Vector3d v2)
        {
            Vector3d sum = new Vector3d(
                v1.my2DPoint.X + v2.my2DPoint.X,
                v1.my2DPoint.Y + v2.my2DPoint.Y,
                v1.myZval + v2.myZval);
            return (sum);
        }

        public static Vector3d operator -(Vector3d v1, Vector3d v2)
        {
            Vector3d diff = new Vector3d(
                v1.my2DPoint.X - v2.my2DPoint.X,
                v1.my2DPoint.Y - v2.my2DPoint.Y,
                v1.myZval - v2.myZval);
            return (diff);
        }

        public static Vector3d Subtract(Vector3d v1, Vector3d v2)
        {
            Vector3d diff = new Vector3d(
                v1.my2DPoint.X - v2.my2DPoint.X,
                v1.my2DPoint.Y - v2.my2DPoint.Y,
                v1.myZval - v2.myZval);
            return (diff);
        }

        public static Vector3d operator *(Vector3d v1, double mul)
        {
            Vector3d result = new Vector3d(
                v1.my2DPoint.X * mul,
                v1.my2DPoint.Y * mul,
                v1.myZval * mul);
            return (result);
        }

        public static Vector3d operator *(double mul, Vector3d v1)
        {
            Vector3d result = new Vector3d(
                v1.my2DPoint.X * mul,
                v1.my2DPoint.Y * mul,
                v1.myZval * mul);
            return (result);
        }

        public static Vector3d Multiply(Vector3d v1, double mul)
        {
            Vector3d result = new Vector3d(
                v1.my2DPoint.X * mul,
                v1.my2DPoint.Y * mul,
                v1.myZval * mul);
            return (result);
        }

        public static Vector3d operator /(Vector3d v1, double div)
        {
            if ((System.Double.IsNaN(div)) || (div == 0.0))
            {
                throw new System.ArgumentOutOfRangeException("div", "Invalid division by zero. The value was: " + div + " .");
            }

            Vector3d result = new Vector3d(
                v1.my2DPoint.X / div,
                v1.my2DPoint.Y / div,
                v1.myZval / div);
            return (result);
        }

        public static Vector3d Divide(Vector3d v1, double div)
        {
            if ((System.Double.IsNaN(div)) || (div == 0.0))
            {
                throw new System.ArgumentOutOfRangeException("div", "Invalid division by zero. The value was: " + div + " .");
            }

            Vector3d result = new Vector3d(
                v1.my2DPoint.X / div,
                v1.my2DPoint.Y / div,
                v1.myZval / div);
            return (result);
        }
        #endregion

        #region Set / get Methods

        public void Init(double initVal)
        {
            this.my2DPoint.X = initVal;
            this.my2DPoint.Y = initVal;
            this.myZval = initVal;
        }

        public void SetComponents(double xval, double yval, double zval)
        {
            this.my2DPoint.X = xval;
            this.my2DPoint.Y = yval;
            this.myZval = zval;
        }

        public void GetComponents(out double xval, out double yval, out double zval)
        {
            xval = this.my2DPoint.X;
            yval = this.my2DPoint.Y;
            zval = this.myZval;
        }

        public void SetFromVector3d(Vector3d vec3D)
        {
            this.my2DPoint.X = vec3D.my2DPoint.X;
            this.my2DPoint.Y = vec3D.my2DPoint.Y;
            this.myZval = vec3D.myZval;
        }

        public void SetFromInvertedVector3d(Vector3d vec3D)
        {
            this.my2DPoint.X = -vec3D.my2DPoint.X;
            this.my2DPoint.Y = -vec3D.my2DPoint.Y;
            this.myZval = -vec3D.myZval;
        }

        public double myZval = 0;
        public double Z
        {
            get
            {
                return this.myZval;
            }

            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("Z", "invalid set Z component " + value);
                }
                this.myZval = value;
            }
        }

        public void SetFromPolar(double vectorLength_in, Vector3d directionCosinus_in)
        {
            this.SetFromPolar(vectorLength_in, directionCosinus_in, GraphicMath.DEFAULT_TOLERANCE);
        }

        public void SetFromPolar(double vectorLength_in, Vector3d directionCosinus_in, double tolerance)
        {
            this.my2DPoint.X = vectorLength_in * directionCosinus_in.my2DPoint.X;
            this.my2DPoint.Y = vectorLength_in * directionCosinus_in.my2DPoint.Y;
            this.myZval = vectorLength_in * directionCosinus_in.myZval;

            // check isNil and throw an exception if isNil
            double length2 = ((this.my2DPoint.X * this.my2DPoint.X) +
                              (this.my2DPoint.Y * this.my2DPoint.Y) +
                              (this.myZval * this.myZval));

            if (length2 < (tolerance * tolerance))
            {
                throw new System.ArithmeticException("The vector IsNil: Its length is: " + Math.Sqrt(length2) + " .");
            }
        }

        public void SetFromPolar(double vectorLength_in, double anglePhi_in, double angleTheta_in)
        {
            this.SetFromPolar(vectorLength_in, anglePhi_in, angleTheta_in, GraphicMath.DEFAULT_TOLERANCE);
        }

        public void SetFromPolar(double vectorLength_in, double anglePhi_in, double angleTheta_in, double tolerance)
        {
            double theSinTheta = Math.Sin(angleTheta_in);

            this.my2DPoint.X = vectorLength_in * theSinTheta * Math.Cos(anglePhi_in);
            this.my2DPoint.Y = vectorLength_in * theSinTheta * Math.Sin(anglePhi_in);
            this.myZval = vectorLength_in * Math.Cos(angleTheta_in);

            // check isNil and throw an exception if isNil
            double length2 = ((this.my2DPoint.X * this.my2DPoint.X) +
                              (this.my2DPoint.Y * this.my2DPoint.Y) +
                              (this.myZval * this.myZval));

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


        public Vector3d GetInverted()
        {
            Vector3d vec_out = new Vector3d(
            -this.my2DPoint.X,
            -this.my2DPoint.Y,
            -this.myZval);
            return vec_out;
        }

        public void Invert()
        {
            this.my2DPoint.X = -this.my2DPoint.X;
            this.my2DPoint.Y = -this.my2DPoint.Y;
            this.myZval = -this.myZval;
            return;
        }

        public double GetLength()
        {
            return Math.Sqrt((this.my2DPoint.X * this.my2DPoint.X) +
                             (this.my2DPoint.Y * this.my2DPoint.Y) +
                             (this.myZval * this.myZval));
        }


        public void SetLength(double lenght)
        {
            this.SetLength(lenght, GraphicMath.DEFAULT_TOLERANCE);
        }

        public void SetLength(double lenght, double tolerance)
        {
            this.Normalize(tolerance);
            this.my2DPoint.X *= lenght;
            this.my2DPoint.Y *= lenght;
            this.myZval *= lenght;
        }

        public Vector3d GetNormalized()
        {
            return this.GetNormalized(GraphicMath.DEFAULT_TOLERANCE);
        }

        public Vector3d GetNormalized(double tolerance)
        {
            double length = Math.Sqrt((this.my2DPoint.X * this.my2DPoint.X) +
                                       (this.my2DPoint.Y * this.my2DPoint.Y) +
                                       (this.myZval * this.myZval));

            if (length < tolerance)
            {
                throw new System.ArithmeticException("The vector IsNil: Its length is: " + length + " .");
            }
            else
            {
                Vector3d vec_out = new Vector3d(
                this.my2DPoint.X / length,
                this.my2DPoint.Y / length,
                this.myZval / length);
                return vec_out;
            }
        }

        public void Normalize()
        {
            this.Normalize(GraphicMath.DEFAULT_TOLERANCE);
        }

        public void Normalize(double tolerance)
        {
            double length = Math.Sqrt((this.my2DPoint.X * this.my2DPoint.X) +
                                       (this.my2DPoint.Y * this.my2DPoint.Y) +
                                       (this.myZval * this.myZval));

            if (length < tolerance)
            {
                throw new System.ArithmeticException("The vector IsNil: Its length is: " + length + " .");
            }
            else
            {
                this.my2DPoint.X /= length;
                this.my2DPoint.Y /= length;
                this.myZval /= length;
                return;
            }
        }

        public bool IsNormalized()
        {
            return IsNormalized(GraphicMath.DEFAULT_TOLERANCE);
        }

        public bool IsNormalized(double tolerance)
        {
            double length2 = ((this.my2DPoint.X * this.my2DPoint.X) +
                              (this.my2DPoint.Y * this.my2DPoint.Y) +
                              (this.myZval * this.myZval));

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

        public bool IsNil()
        {
            return IsNil(GraphicMath.DEFAULT_TOLERANCE);
        }

        public bool IsNil(double tolerance)
        {
            double length2 = ((this.my2DPoint.X * this.my2DPoint.X) +
                              (this.my2DPoint.Y * this.my2DPoint.Y) +
                              (this.myZval * this.myZval));

            if (length2 < (tolerance * tolerance))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsAlmostEqual(Vector3d v1)
        {
            return IsAlmostEqual(v1, GraphicMath.DEFAULT_TOLERANCE);
        }

        public bool IsAlmostEqual(Vector3d v1, double tolerance)
        {
            return (((v1.my2DPoint.X - this.my2DPoint.X) * (v1.my2DPoint.X - this.my2DPoint.X) +
                     (v1.my2DPoint.Y - this.my2DPoint.Y) * (v1.my2DPoint.Y - this.my2DPoint.Y) +
                     (v1.myZval - this.myZval) * (v1.myZval - this.myZval)) <
                     (tolerance * tolerance));
        }

        public bool IsLonger(Vector3d v1)
        {
            return IsLonger(v1, GraphicMath.DEFAULT_TOLERANCE);
        }

        public bool IsLonger(Vector3d v1, double tolerance)
        {
            double l1 =
                (this.my2DPoint.X * this.my2DPoint.X) +
                (this.my2DPoint.Y * this.my2DPoint.Y) +
                (this.myZval * this.myZval);

            double l2 =
                (v1.my2DPoint.X * v1.my2DPoint.X) +
                (v1.my2DPoint.Y * v1.my2DPoint.Y) +
                (v1.myZval * v1.myZval);

            if ((l1 - l2) > tolerance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsAlmostParallel(Vector3d v1)
        {
            return IsAlmostParallel(v1, GraphicMath.DEFAULT_TOLERANCE);
        }

        public bool IsAlmostParallel(Vector3d v1, double tolerance)
        {
            Vector3d nthis = this.GetNormalized(tolerance);
            Vector3d nv1 = v1.GetNormalized(tolerance);
            return (nthis.IsNormalAlmostParallel(nv1, tolerance));
        }

        public bool IsNormalAlmostParallel(Vector3d v1)
        {
            return IsNormalAlmostParallel(v1, GraphicMath.DEFAULT_TOLERANCE);
        }

        public bool IsNormalAlmostParallel(Vector3d v1, double tolerance)
        {
            return ((
                (Math.Abs(v1.my2DPoint.X - this.my2DPoint.X) < tolerance) &&
                (Math.Abs(v1.my2DPoint.Y - this.my2DPoint.Y) < tolerance) &&
                (Math.Abs(v1.myZval - this.myZval) < tolerance)) || (
                (Math.Abs(v1.my2DPoint.X + this.my2DPoint.X) < tolerance) &&
                (Math.Abs(v1.my2DPoint.Y + this.my2DPoint.Y) < tolerance) &&
                (Math.Abs(v1.myZval + this.myZval) < tolerance)
                ));
        }

        public double GetInnerProduct(Vector3d v1)
        {
            return ((v1.my2DPoint.X * this.my2DPoint.X) +
                    (v1.my2DPoint.Y * this.my2DPoint.Y) +
                    (v1.myZval * this.myZval));
        }

        public Vector3d GetOuterProduct(Vector3d v1)
        {
            Vector3d outer = new Vector3d(
            this.my2DPoint.Y * v1.myZval - this.myZval * v1.my2DPoint.Y,
            this.myZval * v1.my2DPoint.X - this.my2DPoint.X * v1.myZval,
            this.my2DPoint.X * v1.my2DPoint.Y - this.my2DPoint.Y * v1.my2DPoint.X);
            return (outer);
        }

        public double GetDistanceToLine(Vector3d line_start, Vector3d line_end)
        {
            // Get the nearest point upon the line
            Vector3d nearestPointOnLine = GetNearestPointUponLine(line_start, line_end);

            // Calculate the distance
            Vector3d dist_v = this - nearestPointOnLine;

            return dist_v.GetLength();
        }

        public Vector3d GetNearestPointUponLine(Vector3d line_start, Vector3d line_end)
        {
            // calculate the line vector
            Vector3d line = line_end - line_start;

            // calculate the length of the line
            double lineLength = line.GetLength();

            // find the nearest point on the line to the given point
            Vector3d nearestPointOnLine = null;

            if (GraphicMath.DEFAULT_TOLERANCE > lineLength)
            {
                // line is degenerated to a point
                // so the nearest point is either start or end
                nearestPointOnLine = new Vector3d(line_start);
            }
            else
            {
                // v_st is a vector from the beginning of the line to the 
                // point in question
                Vector3d v_st = this - line_start;

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
                    nearestPointOnLine = new Vector3d(line_start);
                }
                else if (u > lineLength) // after end of line
                {
                    nearestPointOnLine = new Vector3d(line_end);
                }
                else                      // on line
                {
                    nearestPointOnLine = (line_start + (line * u));
                }
            }
            return nearestPointOnLine;
        }

        public double GetDistanceToInfiniteLine(Vector3d line_P1, Vector3d line_P2)
        {
            // find the nearest point on the line to the given point
            Vector3d nearestPointOnLine = GetNearestPointUponInfiniteLine(line_P1, line_P2);

            // calculate the distance
            Vector3d dist_v = this - nearestPointOnLine;

            return dist_v.GetLength();
        }

        public Vector3d GetNearestPointUponInfiniteLine(Vector3d line_P1, Vector3d line_P2)
        {
            // calculate the line vector
            Vector3d line = line_P2 - line_P1;

            // calculate the length of the line
            double lineLength = line.GetLength();

            // find the nearest point on the line to the given point
            Vector3d nearestPointOnLine = null;

            if (GraphicMath.DEFAULT_TOLERANCE > lineLength)
            {
                // line is degenerated to a point
                // so the nearest point is either P1 or P2
                nearestPointOnLine = new Vector3d(line_P1);
            }
            else
            {
                // v_st is a vector from P1 of the line to the 
                // point in question
                Vector3d v_st = this - line_P1;

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

        //public void MultiplyAsColumnVector(Matrix4x4d matrix)
        //{
        //    double x = this.my2DPoint.X;
        //    double y = this.my2DPoint.Y;
        //    double z = this.myZval;

        //    this.my2DPoint.X =
        //        matrix.myMatrix4x4.myr1c1 * x +
        //        matrix.myMatrix4x4.myr1c2 * y +
        //        matrix.myMatrix4x4.myr1c3 * z +
        //        matrix.myMatrix4x4.myr1c4;

        //    this.my2DPoint.Y =
        //        matrix.myMatrix4x4.myr2c1 * x +
        //        matrix.myMatrix4x4.myr2c2 * y +
        //        matrix.myMatrix4x4.myr2c3 * z +
        //        matrix.myMatrix4x4.myr2c4;

        //    this.myZval =
        //        matrix.myMatrix4x4.myr3c1 * x +
        //        matrix.myMatrix4x4.myr3c2 * y +
        //        matrix.myMatrix4x4.myr3c3 * z +
        //        matrix.myMatrix4x4.myr3c4;
        //}

        //public void MultiplyAsRowVector(Matrix4x4d matrix)
        //{
        //    double x = this.my2DPoint.X;
        //    double y = this.my2DPoint.Y;
        //    double z = this.myZval;

        //    this.my2DPoint.X =
        //        matrix.myMatrix4x4.myr1c1 * x +
        //        matrix.myMatrix4x4.myr2c1 * y +
        //        matrix.myMatrix4x4.myr3c1 * z +
        //        matrix.myMatrix4x4.myr4c1;

        //    this.my2DPoint.Y =
        //        matrix.myMatrix4x4.myr1c2 * x +
        //        matrix.myMatrix4x4.myr2c2 * y +
        //        matrix.myMatrix4x4.myr3c2 * z +
        //        matrix.myMatrix4x4.myr4c2;

        //    this.myZval =
        //        matrix.myMatrix4x4.myr1c3 * x +
        //        matrix.myMatrix4x4.myr2c3 * y +
        //        matrix.myMatrix4x4.myr3c3 * z +
        //        matrix.myMatrix4x4.myr4c3;
        //}

        public void Rotate2D(double rotation_angle, ref Vector3d[] vector_list)
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
                foreach (Vector3d vec in vector_list)
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
                foreach (Vector3d vec in vector_list)
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


        private double GetDistanceToOrthogonalEllipse(double width, double height, double tolerance, out double nearestX, out double nearestY, out int iterationCount)
        {
            int max_cycle = 10000; // max count of iterations
            int cycle = 0;

            double XDivWidth = 0.0;
            double YDivHeight = 0.0;

            // first try for Newton's method
            double Try = height * (this.my2DPoint.Y - height);

            // solve via Newton�s method maximum 1000 iterations
            for (cycle = 0; cycle < max_cycle + 1; cycle++)
            {
                double TpWidthSqr = Try + width * width;
                double TpHeigthSqr = Try + height * height;

                double InvTpWidthSqr = 1.0 / TpWidthSqr;
                double InvTpHeigthSqr = 1.0 / TpHeigthSqr;

                XDivWidth = width * this.my2DPoint.X * InvTpWidthSqr;
                YDivHeight = height * this.my2DPoint.Y * InvTpHeigthSqr;

                double XDivWidthSqr = XDivWidth * XDivWidth;
                double YDivHeighthSqr = YDivHeight * YDivHeight;

                double Fn = XDivWidthSqr + YDivHeighthSqr - 1.0;
                if (Fn < tolerance)
                {
                    // Fn is close enough to zero i.e. distance to ellipse is less than tolerance 
                    // -> terminate the iteration
                    break;
                }

                // Newton's try_n+1 = try_n - Fn / Fnp1
                double Fnp1 = 2.0 * (XDivWidthSqr * InvTpWidthSqr + YDivHeighthSqr * InvTpHeigthSqr);

                // Ratio = Fb/fnp1 = try_n - try_n+1
                double Ratio = Fn / Fnp1;
                if (Ratio < tolerance)
                {
                    // try_n - try_n+1 is close enough to zero -> terminate the iteration
                    break;
                }
                Try += Ratio;
            }

            if (cycle >= max_cycle)
            {
                // method failed to converge -> exception
                throw new System.ArithmeticException("The Newton's method does not converge till iteration cycle: " + cycle + " .");
            }

            // return of iteration count
            iterationCount = cycle;

            // return nearest x/y on ellipse
            nearestX = XDivWidth * width;
            nearestY = YDivHeight * height;

            // return distance = length of distance vector of this point to nearest point on ellipse
            double dX = nearestX - this.my2DPoint.X;
            double dY = nearestY - this.my2DPoint.Y;
            return Math.Sqrt(dX * dX + dY * dY);
        }

        public double GetDistanceToEllipse(Vector3d width, Vector3d height, Vector3d center, double tolerance, Vector3d nearest, out int iterationCount)
        {
            // distance point ellipse (positive if point is inside the ellipse)
            double distance = 0.0;

            // setup origin of coordinate system = 0/0/0
            Vector2d origin2D = new Vector2d();
            // setup ellipse's width, height and center 2D vector (skip z-coordinate)
            Vector2d theWidth = new Vector2d(width.my2DPoint.X, width.my2DPoint.Y);
            Vector2d theHeight = new Vector2d(height.my2DPoint.X, height.my2DPoint.Y);
            Vector2d center2D = new Vector2d(center.my2DPoint.X, center.my2DPoint.Y);
            // setup 2D vector to the point of interest 
            Vector2d orig_point2D = new Vector2d(this.my2DPoint.X, this.my2DPoint.Y);

            // transform original point coordinates to ellipse's system
            // point in ellipse's system = original point2D - center_of_ellipse
            Vector2d point2D = new Vector2d(orig_point2D - center2D);

            // get the length of width vector 2D   a = half of ellipse axis
            double a = Math.Sqrt(theWidth.my2DPoint.X * theWidth.my2DPoint.X + theWidth.my2DPoint.Y * theWidth.my2DPoint.Y);

            // get the length of height vector 2D  b = half of the other ellipse axis
            double b = Math.Sqrt(theHeight.my2DPoint.X * theHeight.my2DPoint.X + theHeight.my2DPoint.Y * theHeight.my2DPoint.Y);

            // if it is a circle
            if (Math.Abs(a - b) < tolerance)
            {
                // exception if width and height vector are not orthogonal
                if (!GraphicMath.IsEqual(0.0, theWidth.GetInnerProduct(theHeight), GraphicMath.DEFAULT_TOLERANCE))
                {
                    throw new System.ArgumentOutOfRangeException("width", "The width and the height vector are not orthogonal.");
                }

                // get length of point regarding center of circle
                double pointLen = Math.Sqrt(point2D.my2DPoint.X * point2D.my2DPoint.X + point2D.my2DPoint.Y * point2D.my2DPoint.Y);
                // calculate nearest x/y on circle
                if (pointLen < GraphicMath.DEFAULT_TOLERANCE) // if no point direction
                {
                    // point of interest is at the center of the circle -> nearest x / y = 0.0 / a
                    nearest.my2DPoint.X = 0.0 + center2D.my2DPoint.X;
                    nearest.my2DPoint.Y = a + center2D.my2DPoint.Y;
                    nearest.myZval = center.myZval;
                }
                else
                {
                    // adjust points length according radius of the circle
                    point2D.Normalize(GraphicMath.DEFAULT_TOLERANCE);
                    point2D *= a;
                    // and calculate coordinates of nearest point
                    nearest.my2DPoint.X = point2D.my2DPoint.X + center2D.my2DPoint.X;
                    nearest.my2DPoint.Y = point2D.my2DPoint.Y + center2D.my2DPoint.Y;
                    nearest.myZval = center.myZval;
                }

                // no iteration
                iterationCount = 0;
                // calculate distance point of interest to circle
                distance = a - pointLen;

                // finished
                return distance;
            }

            // no circle -> ellipse or line

            // calculate rotation angle of the ellipse
            //
            // It is that angle about the bigger ellipse axis has to be rotated
            // so that it is mapped onto the x-axis
            double angle = 0.0;
            if (a > b) // take bigger axis for angle calculation
            {
                angle = Math.Abs(Math.Asin(width.my2DPoint.Y / a));
                if (width.my2DPoint.Y >= 0 && width.my2DPoint.X >= 0) // 1. Q
                {
                }
                else if (width.my2DPoint.Y >= 0 && width.my2DPoint.X < 0)  // 2. Q
                {
                    angle = Math.PI - angle;
                }
                else if (width.my2DPoint.Y < 0 && width.my2DPoint.X < 0)  // 3. Q
                {
                    angle = angle - Math.PI;
                }
                else if (width.my2DPoint.Y < 0 && width.my2DPoint.X >= 0) // 4. Q
                {
                    angle = -angle;
                }
            }
            else
            {
                angle = Math.Abs(Math.Asin(height.my2DPoint.Y / b));
                if (height.my2DPoint.Y >= 0 && height.my2DPoint.X >= 0)
                {
                }
                else if (height.my2DPoint.Y >= 0 && height.my2DPoint.X < 0)
                {
                    angle = Math.PI - angle;
                }
                else if (height.my2DPoint.Y < 0 && height.my2DPoint.X < 0)
                {
                    angle = angle - Math.PI;
                }
                else if (height.my2DPoint.Y < 0 && height.my2DPoint.X >= 0)
                    if (height.my2DPoint.Y < 0 && height.my2DPoint.X >= 0)
                    {
                        angle = -angle;
                    }
            }

            // ajust (rotate) point according ellipses rotation angle
            Vector2d[] vec_array = new Vector2d[1];
            vec_array[0] = new Vector2d(point2D);
            origin2D.Rotate(-angle, ref vec_array);
            point2D.X = vec_array[0].my2DPoint.X;
            point2D.Y = vec_array[0].my2DPoint.Y;

            // correct points components to put point into first quatrant of ellipse
            // and clamp to zero if necessary to assure newton's method will converge

            bool pXreflect = false;
            if (point2D.my2DPoint.X > tolerance)
            {   // nothing to do
            }
            else if (point2D.my2DPoint.X < -tolerance)
            {   // reflect
                pXreflect = true;
                point2D.my2DPoint.X = -point2D.my2DPoint.X;
            }
            else
            {   // clamp to zero
                point2D.my2DPoint.X = 0.0;
            }
            bool pYreflect = false;
            if (point2D.my2DPoint.Y > tolerance)
            {   // nothing to do
            }
            else if (point2D.my2DPoint.Y < -tolerance)
            {   // reflect
                pYreflect = true;
                point2D.my2DPoint.Y = -point2D.my2DPoint.Y;
            }
            else
            {   // clamp to zero
                point2D.my2DPoint.Y = 0.0;
            }

            // transpose ellipse's major and minor axes if necessary
            if (a < b)
            {
                double save = a;
                a = b;
                b = save;
            }

            // if it is only a line
            if (b < tolerance)
            {
                // set line
                Vector2d start = new Vector2d();
                Vector2d end = new Vector2d();

                // calculate nearest point upon the line
                end.my2DPoint.X = a;
                if (point2D.my2DPoint.X > a)
                {
                    nearest.my2DPoint.X = a;
                }
                else
                {
                    nearest.my2DPoint.X = point2D.my2DPoint.X;
                }
                nearest.my2DPoint.Y = 0.0;
                nearest.myZval = center.myZval;

                // no iteration
                iterationCount = 0;

                // and calculate distance point to line
                distance = -point2D.GetDistanceToLine(start, end);

            }

            //  it is an ellipse

            else if (point2D.my2DPoint.X != 0.0)   // if the point is not upon the minor axis of the ellipse
            {

                // exception if width and height vector are not orthogonal
                if (!GraphicMath.IsEqual(0.0, theWidth.GetInnerProduct(theHeight), GraphicMath.DEFAULT_TOLERANCE))
                {
                    throw new System.ArgumentOutOfRangeException("width", "The width and the height vector are not orthogonal.");
                }

                double nearestX = 0.0;
                double nearestY = 0.0;

                if (point2D.my2DPoint.Y != 0.0)  // if the point is not upon the major axis of the ellipse
                {
                    // calculate distance and nearest point upon ellise and iteration count
                    Vector3d point3D = new Vector3d(point2D.my2DPoint.X, point2D.my2DPoint.Y, 0.0);
                    distance = point3D.GetDistanceToOrthogonalEllipse(a, b, tolerance, out nearestX, out nearestY, out iterationCount);
                    nearest.my2DPoint.X = nearestX;
                    nearest.my2DPoint.Y = nearestY;
                    nearest.myZval = center.myZval;

                    // invert distance if point is outside the ellipse
                    double inout = (point2D.my2DPoint.X * point2D.my2DPoint.X) / (a * a) + (point2D.my2DPoint.Y * point2D.my2DPoint.Y) / (b * b);
                    if (inout > 1.0)
                    {
                        distance = -distance;
                    }
                }
                else
                {
                    // point is upon major axis !

                    // is point near center ? x-component <= a - b*b/a !
                    double bSqr = b * b;
                    if (point2D.my2DPoint.X < (a - bSqr / a))
                    {
                        // point is upon minor axis and near center
                        double aSqr = a * a;
                        nearestX = aSqr * point2D.my2DPoint.X / (aSqr - bSqr);

                        double xDIVa = nearestX / a;
                        nearestY = b * Math.Sqrt(Math.Abs(1.0 - xDIVa * xDIVa));
                        double xDif = nearestX - point2D.my2DPoint.X;

                        distance = Math.Sqrt(xDif * xDif + nearestY * nearestY);

                        nearest.my2DPoint.X = nearestX;
                        nearest.my2DPoint.Y = nearestY;
                        nearest.myZval = center.myZval;

                        // no iteration
                        iterationCount = 0;
                    }
                    else
                    {
                        // point is upon major axis and far of center i.e. x-component > a - b*b/a
                        // -> nearest point on ellipse is end of major axis ! 
                        distance = a - point2D.my2DPoint.X;

                        nearest.my2DPoint.X = a;
                        nearest.my2DPoint.Y = 0.0;
                        nearest.myZval = center.myZval;

                        // no iteration
                        iterationCount = 0;
                    }
                }
            }
            else
            {
                // point is upon the minor axis  -> nearest point upon ellipse is end of minor axis
                distance = b - point2D.my2DPoint.Y;

                nearest.my2DPoint.X = 0.0;
                nearest.my2DPoint.Y = b;
                nearest.myZval = center.myZval;

                // no iteration
                iterationCount = 0;
            }

            // set correct sign of nearest x/y
            if (pXreflect)
            {
                nearest.my2DPoint.X = -nearest.my2DPoint.X;
            }
            if (pYreflect)
            {
                nearest.my2DPoint.Y = -nearest.my2DPoint.Y;
            }

            // transform nearest point coordinates from ellipse's to original system  means
            // rotate nearest point according rotation of ellipse
            // original point2D = point in ellipses system + center_of_ellipse
            vec_array[0] = new Vector2d(nearest.my2DPoint.X, nearest.my2DPoint.Y);
            origin2D.Rotate(angle, ref vec_array);
            nearest.my2DPoint.X = vec_array[0].my2DPoint.X + center2D.my2DPoint.X; ;
            nearest.my2DPoint.Y = vec_array[0].my2DPoint.Y + center2D.my2DPoint.Y;
            nearest.myZval = center.myZval;

            return distance;
        }

        public double GetAngle(Vector3d v1)
        {
            return GetAngle(v1, GraphicMath.DEFAULT_TOLERANCE);
        }


        public double GetAngle(Vector3d v1, double tolerance)
        {
            double len1 = v1.GetLength();
            double len2 = this.GetLength();
            if (len1 < tolerance)
            {
                throw new ArithmeticException("The specified vector is too small. The length is: " + len1 + " .");
            }
            if (len2 < tolerance)
            {
                throw new ArithmeticException("The actual vector is too small. The length is: " + len2 + " .");
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


        public void MultiplyAsColumnVector(Matrix4x4d matrix)
        {
            double x = this.X;
            double y = this.Y;
            double z = this.Z;

            this.X =
                matrix.myMatrix4x4.myr1c1 * x +
                matrix.myMatrix4x4.myr1c2 * y +
                matrix.myMatrix4x4.myr1c3 * z +
                matrix.myMatrix4x4.myr1c4;

            this.Y =
                matrix.myMatrix4x4.myr2c1 * x +
                matrix.myMatrix4x4.myr2c2 * y +
                matrix.myMatrix4x4.myr2c3 * z +
                matrix.myMatrix4x4.myr2c4;

            this.Z =
                matrix.myMatrix4x4.myr3c1 * x +
                matrix.myMatrix4x4.myr3c2 * y +
                matrix.myMatrix4x4.myr3c3 * z +
                matrix.myMatrix4x4.myr3c4;
        }

        public void MultiplyAsRowVector(Matrix4x4d matrix)
        {
            double x = this.X;
            double y = this.Y;
            double z = this.Z;

            this.X =
                matrix.myMatrix4x4.myr1c1 * x +
                matrix.myMatrix4x4.myr2c1 * y +
                matrix.myMatrix4x4.myr3c1 * z +
                matrix.myMatrix4x4.myr4c1;

            this.Y =
                matrix.myMatrix4x4.myr1c2 * x +
                matrix.myMatrix4x4.myr2c2 * y +
                matrix.myMatrix4x4.myr3c2 * z +
                matrix.myMatrix4x4.myr4c2;

            this.Z =
                matrix.myMatrix4x4.myr1c3 * x +
                matrix.myMatrix4x4.myr2c3 * y +
                matrix.myMatrix4x4.myr3c3 * z +
                matrix.myMatrix4x4.myr4c3;
        }

        public bool IsAffine(double tolerance)
        {
            if (Math.Abs(this.myZval - 1.0) > tolerance)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Orientation CompareOrientation(Vector3d v1)
        {
            return CompareOrientation(v1, GraphicMath.DEFAULT_TOLERANCE);
        }

        public Orientation CompareOrientation(Vector3d v1, double tolerance)
        {
            Vector3d thisNorm = this.GetNormalized(tolerance);
            Vector3d v1Norm = v1.GetNormalized(tolerance);

            double iP = thisNorm.GetInnerProduct(v1Norm);
            // iP = 0 if angle = 90 degrees
            // iP > 0 if v_st and line are same direction
            // iP < 0 if v_st and line are opposite direction

            // is orthogonal
            if (Math.Abs(iP) <= tolerance)
            {
                return Orientation.Orthogonal;
            }
            else if (0.0 < iP)
            {
                // IP > 0
                // same direction
                if (Math.Abs(1.0 - iP) < tolerance)
                {
                    // parallel
                    return Orientation.Parallel;
                }
                else
                {
                    // only same direction
                    return Orientation.SameDirection;
                }
            }
            else
            {
                // IP < 0
                // opposite direction
                if (Math.Abs(1.0 + iP) < tolerance)
                {
                    // anti parallel
                    return Orientation.AntiParallel;
                }
                else
                {
                    // only opposite direction
                    return Orientation.OppositeDirection;
                }
            }
        }

        #endregion

        public void SetComponents(Vector3d vector3d)
        {
            X = vector3d.X;
            Y = vector3d.Y;
            Z = vector3d.Y;
        }
    }
}