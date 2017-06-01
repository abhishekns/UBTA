#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : GraphicMath.cs
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
    public class GraphicMath
    {
        public const double DEFAULT_TOLERANCE = 0.000001;

        public const double DOUBLE64BIT_PRECISION = 0.000000000000001;

        public const int INVALID_INDEX = -1;

        public static bool IsSignIdentical(double value1, double value2)
        {
            if ((value1 >= 0) && (value2 >= 0) || (value1 < 0) && (value2 < 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsGreater(double value1, double value2, double Tolerance_in)
        {
            if ((value1 - value2) > Tolerance_in)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsGreater(double value1, double value2)
        {
            if ((value1 - value2) > DEFAULT_TOLERANCE)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsEqual(double value1, double value2, double Tolerance_in)
        {
            if (Math.Abs(value1 - value2) <= Tolerance_in)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsEqual(double value1, double value2)
        {
            if (Math.Abs(value1 - value2) <= DEFAULT_TOLERANCE)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static int _PICK_TOLERANCE=1;
        public static int PICK_TOLERANCE
        {
            get
            {
                return _PICK_TOLERANCE;
            }
        }
        public static int Round(double value_in)
        {
            if (value_in >= 0.0)
            {
                return (int)(value_in + 0.5);
            }
            else
            {
                return (int)(value_in - 0.5);
            }
        }

        public static double AngleToRadian(double angle_in)
        {
            if ((360.0 >= angle_in) && (-360.0 <= angle_in))
            {
                return (angle_in * Math.PI / 180.0);
            }
            else
            {
                if (0.0 > angle_in) // if negative angle
                {
                    double angle = Math.IEEERemainder(-angle_in, 360.0);
                    if (0.0 > angle)
                    {
                        angle = 360.0 + angle;
                    }
                    return -(angle * Math.PI / 180.0);
                }
                else
                {
                    double angle = Math.IEEERemainder(angle_in, 360.0);
                    if (0.0 > angle)
                    {
                        angle = 360.0 + angle;
                    }
                    return (angle * Math.PI / 180.0);
                }
            }
        }

        public static double RadianToAngle(double radian_in)
        {
            double angle_in = (radian_in * 180.0 / Math.PI);

            if ((360.0 >= angle_in) && (-360.0 <= angle_in))
            {
                return angle_in;
            }
            else
            {
                if (0.0 > angle_in) // if negative angle
                {
                    double angle = Math.IEEERemainder(-angle_in, 360.0);
                    if (0.0 > angle)
                    {
                        angle = 360.0 + angle;
                    }
                    return -angle;
                }
                else
                {
                    double angle = Math.IEEERemainder(angle_in, 360.0);
                    if (0.0 > angle)
                    {
                        angle = 360.0 + angle;
                    }
                    return angle;
                }
            }
        }

        public static bool SolveEquation2x2(double a11, double a12, double a21, double a22, double b1, double b2, out double x, out double y, double tolerance)
        {
            // calutale the determinante of the matrix
            double det = a11 * a22 - a21 * a12;
            if (tolerance > Math.Abs(det))
            {
                x = 0.0;
                y = 0.0;
                return false;
            }
            double recDet = 1 / det;
            x = (b1 * a22 - b2 * a12) * recDet;
            y = (a11 * b2 - a21 * b1) * recDet;
            return true;
        }

        //public static bool SolveEquation3x3(Matrix3x3d matrix, Vector3d result, Vector3d solution, double tolerance)
        //{
        //    // calutale the determinante of the matrix
        //    double det = matrix.CalculateDeterminant();
        //    if (tolerance > Math.Abs(det))
        //    {
        //        return false;
        //    }
        //    double recDet = 1.0 / det;

        //    solution.X =
        //       (result.X * matrix.myMatrix3x3.myr2c2 * matrix.myMatrix3x3.myr3c3 -
        //        result.X * matrix.myMatrix3x3.myr2c3 * matrix.myMatrix3x3.myr3c2 +

        //        matrix.myMatrix3x3.myr1c2 * matrix.myMatrix3x3.myr2c3 * result.Z -
        //        matrix.myMatrix3x3.myr1c2 * result.Y * matrix.myMatrix3x3.myr3c3 +

        //        matrix.myMatrix3x3.myr1c3 * result.Y * matrix.myMatrix3x3.myr3c2 -
        //        matrix.myMatrix3x3.myr1c3 * matrix.myMatrix3x3.myr2c2 * result.Z)
        //        * recDet;

        //    solution.Y =
        //       (matrix.myMatrix3x3.myr1c1 * result.Y * matrix.myMatrix3x3.myr3c3 -
        //        matrix.myMatrix3x3.myr1c1 * matrix.myMatrix3x3.myr2c3 * result.Z +

        //        result.X * matrix.myMatrix3x3.myr2c3 * matrix.myMatrix3x3.myr3c1 -
        //        result.X * matrix.myMatrix3x3.myr2c1 * matrix.myMatrix3x3.myr3c3 +

        //        matrix.myMatrix3x3.myr1c3 * matrix.myMatrix3x3.myr2c1 * result.Z -
        //        matrix.myMatrix3x3.myr1c3 * result.Y * matrix.myMatrix3x3.myr3c1)
        //        * recDet;

        //    solution.Z =
        //       (matrix.myMatrix3x3.myr1c1 * matrix.myMatrix3x3.myr2c2 * result.Z -
        //        matrix.myMatrix3x3.myr1c1 * result.Y * matrix.myMatrix3x3.myr3c2 +

        //        matrix.myMatrix3x3.myr1c2 * result.Y * matrix.myMatrix3x3.myr3c1 -
        //        matrix.myMatrix3x3.myr1c2 * matrix.myMatrix3x3.myr2c1 * result.Z +

        //        result.X * matrix.myMatrix3x3.myr2c1 * matrix.myMatrix3x3.myr3c2 -
        //        result.X * matrix.myMatrix3x3.myr2c2 * matrix.myMatrix3x3.myr3c1)
        //        * recDet;

        //    return true;
        //}

        public static int SolveQuatraticEquation(double a, double b, double c, out double[] x)
        {
            // The quatric equation is
            // a *  x*x  + b * x  + c = 0

            // There are two other ways to write the quatratic equation:
            //
            // x = (-b +- SQRT(b*b - 4*a*c) / 2*a
            // x = 2c /(-b +- SQRT(b*b - 4*a*c)
            //
            // To avoid inaccurately roots at a or c or both are nearly 0.0
            // calculate q = -1/2 * [ b + sign(b)* SQRT( b*b - 4 * a*c)]
            // -> x1 = q / a   and x2 = c / q
            //
            // calutale the discriminant D = b*b - 4* a*c
            double Dis = b * b - 4.0 * a * c;
            if (Dis > 0.0)
            {
                // There are two real roots x[0] and x[1]
                double sqrt = Math.Sqrt(Dis);
                double q = -0.5;
                if (b < 0.0)
                {
                    q *= (b - sqrt);
                }
                else
                {
                    q *= (b + sqrt);
                }
                x = new double[] { q / a, c / q };
                return 2;
            }
            else if (Dis == 0.0)
            {
                // There is only one root x[0]
                x = new double[] { -0.5 * b / a };
                return 1;
            }
            else
            {
                // There is no real root
                double q2a = 0.5 / a;
                // x[0] contains the real part: -b/2a
                // x[1] contains the value of the imaginary part: sqrt( 4ac-b*b )
                // of the complex roots
                x = new double[] { -b * q2a, Math.Sqrt(-Dis) * q2a };
                return 0;
            }
        }

        #region ENUMs
        public enum  LineStatus : short
        {
            IsUndefined = 0,
            IsParallel,
            IsSame,
            IntersectionOnLine1,
            IntersectionOnLine2,
            IntersectionOnBothLines,
            IntersectionOutside,

        };
        #endregion
        #region Arithmetic Methods

        public static int IsPointInsideOfPolygon( Vector3d point, Vector3d[] poly_list, 
                                                  int start_index, int end_index)
        {
            bool start_below = true;  // start of polygon line is below actual point
            bool end_below   = true;  // end   of polygon line is below actual point

            int  winding_count = 0;   // winding counter

            // error if start_index < 0 or end_index < startindex+1 (minimum 2 points)
            if ((start_index < 0)||(end_index < start_index+2))
            {
                throw new System.ArgumentException("The polygon's start and end index are inconsistent.");
            }
            if (poly_list.GetLength(0) <= end_index)
            {
                throw new System.ArgumentException("The polygon's end index is beyond the end of the list.");
            }
            
            double sX = poly_list[end_index].X;   // start with last point of polygon
            double sY = poly_list[end_index].Y;   // and connect with
            double eX = poly_list[start_index].X; // first point of polygon
            double eY = poly_list[start_index].Y; // at the beginning

            double aX = point.X; // actual point x value
            double aY = point.Y; // actual point y value

            // is start of polygon line above or equal the actual point ?
            if ( sY >= aY )
            {
                start_below = false;
            }

            int endloop = end_index+1;        // end of loop after last point within list 

            for (int ii = start_index + 1; ii < endloop+1; ii++) // for each line till last line
            {
                // is end of polygon line above or equal the actual point ?
                if ( eY >= aY )
                {
                    end_below = false;
                }
                else
                {
                    end_below = true;
                }
                // is y coordinate of actual point between y-coordinate of start and end point ?
                if ( start_below != end_below )
                {
                    // is actual point left  or upon a rising  (sy < eY) line  or 
                    // is actual point right or upon a falling (sy > eY) line
                    if ( (eY - aY)*(eX -sX) <= (eY - sY)*(eX -aX) )
                    {
                        if (!end_below) // if rising line
                        {
                            winding_count++; // point is left or upon the rising line -> increment winding counter
                        }
                    }
                    else
                    {
                        // actual point is right of a rising  (sy < eY) line  or 
                        // actual point is left  of a falling (sy > eY) line
                        if (end_below) // if falling line
                        {
                            winding_count--; // point is left of the falling line -> decrement winding counter
                                             // points which are upon a falling line are defined to be
                                             // infinitesimal right of the falling line
                        }
                    }
                }

                if ( ii < endloop )  // skip fetch at last cycle
                {
                    // take end values as start values for the next line (iteration)
                    start_below = end_below;
                    sX = eX;
                    sY = eY;
                    // get new end values
                    eX = poly_list[ii].X; // first point of polygon
                    eY = poly_list[ii].Y; // at the beginning
                }
            }

            // If there are the same count of falling and rising lines right of the point
            // the winding number will be zero and the point is outside the polygon.
            // If this is not the case the winding count is not equal zero and the point is inside.

            // Some applications ( e.g. OpenGL) defines a point to be 
            // inside  if the winding count is odd  i.e. the difference of rising and falling line count is odd
            // outside if the winding count is even i.e. the difference of rising and falling line count is even
            // the winding count is even

            return winding_count;
        }

        public static int GetIntersectionLineAndQuadrangle( Vector3d line_start, Vector3d line_end,
            Vector3d[] quad_list, Vector3d[] inter_list, double tolerance )
        {
            return GetIntersectionLineAndQuadrangle(line_start, line_end, quad_list, inter_list, false, tolerance);
        }

        public static int GetIntersectionLineAndQuadrangle(Vector3d line_start, Vector3d line_end,
            Vector3d[] quad_list, Vector3d[] inter_list, bool infinit, double tolerance)
        {
            // check the length of the given lists
            if (quad_list.GetLength(0) < 4)
            {
                throw new System.ArgumentException("The length of the quadrangle list is incorrect.");
            }
            if (inter_list.GetLength(0) < 2)
            {
                throw new System.ArgumentException("The length of the intersection list is incorrect.");
            }

            // the actual found intersection point 
            Vector3d intersection = new Vector3d();
            // count of found intersection points
            int  nb_intersections = 0;

            // Consider all four lines of the quadrangle
            for (int ii = 0; ii<4; ii++)
            {
                // calculate the end index of the actual quadrangle line
                int en   = ii+1;
                if (en >= 4) 
                {
                    en = 0;
                }

                // calculate the intersection of the line and the actual side line of the quadrangle
                LineStatus l_state = GetIntersectionOf2Lines(line_start, line_end, quad_list[ii], quad_list[en], intersection, tolerance);

                // store the found intersections into the intersection list and update the intersection count
                if ((l_state == LineStatus.IntersectionOnBothLines) ||
                    ((l_state == LineStatus.IntersectionOnLine2) && infinit))
                {
                    // intersection found
                    if ( nb_intersections == 0)      // if the first intersection was found
                    {
                        inter_list[0].SetFromVector3d( intersection );
                        nb_intersections = 1;
                    }
                    else if ( nb_intersections == 1)  // there was already an intersection found
                    {
                        // is it the same intersection as already found, i.e. it is a corner
                        if ( intersection.IsAlmostEqual(inter_list[0], tolerance) )
                        {
                            continue;  // search for the next
                        }
                        else
                        {
                            // second intersection found
                            inter_list[1].SetFromVector3d( intersection );
                            nb_intersections = 2;
                            break;
                        }
                    }
                }
            }
            // return the number of found intersections
            return nb_intersections;
        }

        public static int GetIntersectionLineAndPolygon(Vector3d line_start, Vector3d line_end,
            Vector3d[] poly_list, int count, Vector3d[] inter_point_list, int[] inter_index_list, double tolerance)
        {
            // check the length of the polygon list regarding specified count
            if (poly_list.GetLength(0) < count)
            {
                throw new System.ArgumentException("The length of the polygon list is less than the specified count.");
            }
            // check the length of the result list
            if ((inter_point_list.GetLength(0) < 2) || (inter_index_list.GetLength(0) < 2))
            {
                throw new System.ArgumentException("The length of the intersection list is incorrect.");
            }

            // the actual intersection status of the line
            LineStatus l_state = LineStatus.IsUndefined;
            // the actual found intersection point 
            Vector3d intersection = new Vector3d();
            // count of found intersection points
            int nb_intersections = 0;

            // Consider all lines of the polygon
            for (int ii = 0; ii < count-1; ii++)
            {
                // calculate the intersection of the line and the actual side line of the polygon
                l_state = GetIntersectionOf2Lines(line_start, line_end, poly_list[ii], poly_list[ii+1], intersection, tolerance);

                // store the found intersections into the intersection list and update the intersection count
                if (l_state == LineStatus.IntersectionOnBothLines)
                {
                    // intersection found
                    if (nb_intersections == 0)      // if the first intersection was found
                    {
                        inter_index_list[0] = ii; 
                        inter_point_list[0].SetFromVector3d(intersection);
                        nb_intersections = 1;
                    }
                    else if (nb_intersections == 1)  // there was already an intersection found
                    {
                        // is it the same intersection as already found, i.e. it is a corner
                        if (intersection.IsAlmostEqual(inter_point_list[0], tolerance))
                        {
                            continue;  // search for the next
                        }
                        else
                        {
                            // second intersection found
                            inter_index_list[1] = ii;
                            inter_point_list[1].SetFromVector3d(intersection);
                            nb_intersections = 2;
                            break;
                        }
                    }
                }
            }
            // return the number of found intersections
            return nb_intersections;
        }

        public static LineStatus GetIntersectionOf2Lines( Vector3d line1_start, Vector3d line1_end,
                                                          Vector3d line2_start, Vector3d line2_end,
                                                          Vector3d intersection, double tolerance )
        {
            // the z-component of the intersection point will be zero
            intersection.Z = 0.0;
            // comput the direction vector of each line d = line_end - line_start
            double d1X = line1_end.X - line1_start.X;
            double d1Y = line1_end.Y - line1_start.Y;
            double d2X = line2_end.X - line2_start.X;
            double d2Y = line2_end.Y - line2_start.Y;

            // compute len*len of each direction vector
            double len1SQR = d1X*d1X + d1Y*d1Y;
            double len2SQR = d2X*d2X + d2Y*d2Y; 

            // due to the outer product of 3D vectors in 2D only the z-component
            // of the outer product is relevant (here named cross2D)
            // the x- and y-component of the outer product is 0.0
            double outer2D = ( d1X * d2Y - d1Y * d2X );
            double outer2DSQR = outer2D * outer2D;

            // compute the 2D base vector = line2_start - line1_start
            double baseX = line2_start.X - line1_start.X;
            double baseY = line2_start.Y - line1_start.Y;

            // check if the lines are not parallel or the same 
            // (the outer product of the normalized vectors is not zero)
            if (outer2DSQR > tolerance * tolerance * len1SQR * len2SQR)
            {   // the lines are not parallel or the same
                // compute the distance line1_start to the intersection point along the direction of line 1
                double dist = (baseX * d2Y - baseY * d2X) / outer2D;

                // compute the intersection point I = line1_start + dist * direction line 1
                intersection.X = line1_start.X + dist * d1X;
                intersection.Y = line1_start.Y + dist * d1Y;

                // check if the intersection point is upon line 1 or line 2
                bool onLine1 = false;
                bool onLine2 = false;

                // a check is done if the distance is within the range 0...1
                double lo = 0.0 - tolerance;
                double hi = 1.0 + tolerance;

                // if the distance on line 1 is within the given range
                if ((lo <= dist) && (dist <= hi))
                {
                    // the intersection is upon line 1
                    onLine1 = true;
                }

                // compute the distance line2_start to intersection point along the direction of line 2
                // (note: base vector inverted and  outer2D of line 2 = -outer2D of line 1)
                double dist2 = (baseX * d1Y - baseY * d1X) / outer2D;

                // include the following squence only to check the implementation
                //double inter2X = line2_start.X + dist2 * d2X;
                //double inter2Y = line2_start.Y + dist2 * d2Y;
                //if ( !GraphicMath.IsEqual( inter2X,intersection.X, 0.000000001) ||
                //     !GraphicMath.IsEqual( inter2Y,intersection.Y, 0.000000001)     )
                //{
                //    throw new System.ArithmeticException("intersection is not the same point ");
                //}
                // end include squence to check the implementation


                // if the distance on line 2 is within the given range
                if ((lo <= dist2) && (dist2 <= hi))
                {
                    // the intersection is upon line 2
                    onLine2 = true;
                }

                // the resultant line status is calculated 
                // using the distance from the line start to the intersection point:
                // - IntersectionOnBothLines if the distance on both lines is within the range 0....1
                // - IntersectionOnLine1 if the distance on the first line is within the range 0....1  
                // - IntersectionOnLine2 if the distance on the second line is within the range 0....1 
                // - IntersectionOutside if the distance on both lines is not within the range 0....1
                if (onLine1)
                {
                    if (onLine2)
                    {
                        return LineStatus.IntersectionOnBothLines;
                    }
                    else
                    {
                        return LineStatus.IntersectionOnLine1;
                    }
                }
                else
                {
                    if (onLine2)
                    {
                        return LineStatus.IntersectionOnLine2;
                    }
                    else
                    {
                        return LineStatus.IntersectionOutside;
                    }
                }
            }
            else
            {
                // the two lines are parallel or even the same
                intersection.X = 0.0;
                intersection.Y = 0.0;

                // check if the lines are parallel
                double lenBaseSQR = baseX * baseX + baseY * baseY;
                outer2D = baseX * d1Y - baseY * d1X;
                outer2DSQR = outer2D * outer2D;

                if (outer2DSQR > tolerance * tolerance * len1SQR * lenBaseSQR)
                {
                    // the lines are parralel
                    return LineStatus.IsParallel;
                }
                else
                {
                    // the lines are identical
                    return LineStatus.IsSame;
                }
            }
        }
 
        public static double GetDistanceToLine( Vector3d point, 
                                                Vector3d line_start,
                                                Vector3d line_end )
        {
            // reduce to 2D vectors vector
            Vector2d point2D = new Vector2d(point.X,      point.Y);
            Vector2d end2D   = new Vector2d(line_end.X,   line_end.Y);
            Vector2d start2D = new Vector2d(line_start.X, line_start.Y);

            return point2D.GetDistanceToLine(start2D, end2D);
        }

        public static void Rotate (Vector3d axis_point, double rotation_angle, ref Vector3d[] vector_list )
        {
            axis_point.Rotate2D(rotation_angle, ref vector_list);
        }

        public static double GetDistancePointToEllipse (Vector3d point, Vector3d width, Vector3d height, Vector3d center, double tolerance, Vector3d nearest, out int iterationCount )
        {
            double distance = point.GetDistanceToEllipse (width, height, center, tolerance, nearest, out iterationCount );
            return distance;
        }

        public static void GetTipCoordinates2D( Vector3d arrow_start, Vector3d arrow_end,
                                                double tipLength, double tipWithHalf,
                                                out Vector3d tipBase1, out Vector3d tipBase2)
        {
            Vector2d a_start = new Vector2d( arrow_start.X, arrow_start.Y);
            Vector2d a_end   = new Vector2d( arrow_end.X,   arrow_end.Y);
            Vector2d tip2Db1;
            Vector2d tip2Db2;

            Vector2d v_arrow;          // arrow vector
            Vector2d v_tipLength;      // tip length vector
            Vector2d v_tipWidthHalf;   // half tip width vector
            double swap;
            
            // calculate distance vector
            v_arrow = a_end - a_start;

            // normalize arrow vector and get an exception if lentgh < GraphicMath.DEFAULT_TOLERANCE
            v_arrow.Normalize(GraphicMath.DEFAULT_TOLERANCE);
            
            // arrow is not just  point
            // calculate tip length vector
            v_tipLength    = v_arrow * tipLength;
            // calculate half tip width vector
            v_tipWidthHalf = v_arrow * tipWithHalf;
            // rectangular vector (coordinates x, y) as half tip width vector
            swap = v_tipWidthHalf.X;
            v_tipWidthHalf.X = -v_tipWidthHalf.Y;
            v_tipWidthHalf.Y = swap;
            // calculate coordinates of arrow tip base
            tip2Db1 = a_end - v_tipLength + v_tipWidthHalf;
            tip2Db2 = a_end - v_tipLength - v_tipWidthHalf;

            tipBase1 = new Vector3d( tip2Db1.X, tip2Db1.Y, 0.0 );
            tipBase2 = new Vector3d( tip2Db2.X, tip2Db2.Y, 0.0 );

            return;
        }

        public static double GetAngle(Vector3d v1, Vector3d v2, double tolerance)
        {
            // keep in mind z-components are zero!
            double innerProduct =  ((v1.X * v2.X) + (v1.Y * v2.Y));
            double len1 = Math.Sqrt((v1.X * v1.X) + (v1.Y * v1.Y));
            double len2 = Math.Sqrt((v2.X * v2.X) + (v2.Y * v2.Y));
            // check the length of the two vectors
            if ( len1 < tolerance )
            {
                throw new ArithmeticException("The length of the input vector 1 "+len1+" is too small."); 
            }
            if ( len2 < tolerance )
            {
                throw new ArithmeticException("The length of the input vector 2 "+len2+" is too small."); 
            }

            // calculate the angle
            // (inner product)/(len1 * len2) should be within the range -1 <= 0 <= 1 
            double cos = innerProduct / ( len1 * len2 );
            if ( ( -1.0 <= cos ) && ( 1.0 >= cos )) 
            {
                return Math.Acos( cos );
            }
            else if ( -1.0 > cos )
            {
                return Math.PI ; // -1 = cos( 180 deg )
            }
            else
            {
                return 0.0 ; //  1 = cos( 0 deg )
            }
        }

        public static Vector3d RectangleZoom(Vector3d rectPos, int rectSizeX, int rectSizeY, int segmSizeX, int segmSizeY, Matrix4x4d transMatrix, out float zoom)
        {
            if ((0 < rectSizeX) && (0 < rectSizeY) && (segmSizeY > rectSizeX) && (segmSizeY > rectSizeX))
            {
                // Get the transformation matrix segment to image coordinate system
                Matrix4x4d inversMat = Matrix4x4d.GetInverseMatrix(transMatrix);

                // Calculate segment zoom in x and y direction
                double xZoom = ((double)segmSizeX / (double)rectSizeX);
                double yZoom = ((double)segmSizeY / (double)rectSizeY);

                // Take the smaller zoom factor
                if (xZoom > yZoom)
                {
                    // Take the zoom in segment's y direction
                    // Calculate the zoom in image system
                    // Calculate the unzoomed image height
                    // Setup unzoomed top left and bottom left coordinates in segment systen
                    Vector4d segTL = new Vector4d(rectPos);
                    Vector4d segBL = new Vector4d(rectPos.X, rectPos.Y + (double)rectSizeY, 0.0, 1.0);
                    // Transform these segment coordinates to image coordinates
                    Vector4d imaTL = inversMat * segTL;
                    Vector4d imaBL = inversMat * segBL;
                    // Calculate the unzoomed image height
                    double imaHeight = imaBL.Y - imaTL.Y;

                    // Setup zoomed top left and bottom left in segement system
                    segTL.SetComponents(0.0, 0.0, 0.0, 1.0);
                    segBL.SetComponents(0.0, (double)segmSizeY, 0.0, 1.0);
                    // Transform these segment coordinates to image coordinates
                    imaTL = inversMat * segTL;
                    imaBL = inversMat * segBL;
                    // Calculate the zoomed image height
                    double imaZoomedHeight = imaBL.Y - imaTL.Y;

                    // Calculate the actual zoom image to segment
                    imaTL.X += 1.0;
                    segBL = transMatrix * imaTL;
                    segTL.SetFromVector4d(segBL - segTL);
                    double actualZoom = segTL.GetLength();

                    // Calculate the zoom image to segment
                    zoom = (float)((imaZoomedHeight / imaHeight) * actualZoom);
                }
                else
                {
                    // Take the zoom in segment's x direction
                    // Calculate the zoom in image system
                    // Calculate the unzoomed image width
                    // Setup unzoomed top left and top right coordinates in segment systen
                    Vector4d segTL = new Vector4d(rectPos);
                    Vector4d segTR = new Vector4d(rectPos.X + (double)rectSizeX, rectPos.Y, 0.0, 1.0);
                    // Transform these segment coordinates to image coordinates
                    Vector4d imaTL = inversMat * segTL;
                    Vector4d imaTR = inversMat * segTR;
                    // Calculate the unzoomed image width
                    double imaWidth = imaTR.X - imaTL.X;

                    // Setup zoomed top left and top right in segement system
                    segTL.SetComponents(0.0, 0.0, 0.0, 1.0);
                    segTR.SetComponents((double)segmSizeX, 0.0, 0.0, 1.0);
                    // Transform these segment coordinates to image coordinates
                    imaTL = inversMat * segTL;
                    imaTR = inversMat * segTR;
                    // Calculate the zoomed image width
                    double imaZoomedWidth = imaTR.X - imaTL.X;

                    // Calculate the actual zoom image to segment
                    imaTL.X += 1.0;
                    segTR = transMatrix * imaTL;
                    segTL.SetFromVector4d(segTR - segTL);
                    double actualZoom = segTL.GetLength();

                    // Calculate the zoom image to segment
                    zoom = (float)((imaZoomedWidth / imaWidth) * actualZoom);
                }

                // Setup the center vector in segment coordinates
                Vector4d center4D = new Vector4d(
                    rectPos.X + (double)rectSizeX * 0.5,
                    rectPos.Y + (double)rectSizeY * 0.5, 0.0, 1.0);

                // Transform the segment coordinates to image coordinates
                center4D = inversMat * center4D;

                // Provide the center vector
                return (new Vector3d(center4D.X, center4D.Y, 0.0));
            }
            else
            {
                throw new System.ArgumentException("The selection rectangle size is illegal.");
            }
        }

        public static int GetIntersectionLineAndOrthogonalEllipse(double width, double height, Vector3d LP1, Vector3d LP2, out Vector3d[] intersections, double tolerance)
        {
            if (GraphicMath.IsEqual(LP1.X, LP2.X, tolerance))
            {
                // The line is vertical -> the x value is already known.
                double a = 0.5 * width;
                double absLP1x = Math.Abs(LP1.X);
                double absLP2x = Math.Abs(LP2.X);
                if (GraphicMath.IsGreater(absLP1x, a, tolerance) && GraphicMath.IsGreater(absLP2x, a, tolerance)) 
                {
                    // The line is outside ->
                    // There is no real intersection
                    intersections = null;
                    return 0;
                }
                else if (GraphicMath.IsGreater(a, absLP1x, tolerance) && GraphicMath.IsGreater(a, absLP2x, tolerance))
                {
                    // The line is inside ->
                    // There are two real intersections
                    double aa = a * a;
                    double bb = 0.25 * height * height;
                    double y1 = Math.Sqrt((1.0 - LP1.X * LP1.X / aa) * bb);
                    double y2 = Math.Sqrt((1.0 - LP2.X * LP2.X / aa) * bb);
                    if (LP1.Y > LP2.Y)
                    {
                        intersections = new Vector3d[] { new Vector3d(LP1.X, y1, 0.0), new Vector3d(LP2.X, -y2, 0.0) };
                    }
                    else
                    {
                        intersections = new Vector3d[] { new Vector3d(LP1.X, -y1, 0.0), new Vector3d(LP2.X, y2, 0.0) };
                    }
                    return 2;
                }
                else
                {
                    // The line is tangency to the ellipse
                    // There is one real intersection 
                    if (LP1.X > 0.0)
                    {
                        intersections = new Vector3d[] { new Vector3d(a, 0.0, 0.0) };
                    }
                    else
                    {
                        intersections = new Vector3d[] { new Vector3d(-a, 0.0, 0.0) };
                    }
                    return 1;
                }
            }
            else if (GraphicMath.IsEqual(LP1.Y, LP2.Y, tolerance))
            {
                // The line is horizontal -> the y value is already known
                double b = 0.5 * height;
                double absLP1y = Math.Abs(LP1.Y);
                double absLP2y = Math.Abs(LP2.Y);
                if (GraphicMath.IsGreater(absLP1y, b, tolerance) && GraphicMath.IsGreater(absLP2y, b, tolerance))
                {
                    // The line is outside ->
                    // There is no real intersection
                    intersections = null;
                    return 0;
                }
                else if (GraphicMath.IsGreater(b, absLP1y, tolerance) && GraphicMath.IsGreater(b, absLP2y, tolerance))
                {
                    // The line is inside ->
                    // There are two real intersections
                    double aa = 0.25 * width * width;
                    double bb = b * b;
                    double x1 = Math.Sqrt((1.0 - LP1.Y * LP1.Y / bb) * aa);
                    double x2 = Math.Sqrt((1.0 - LP2.Y * LP2.Y / bb) * aa);
                    if (LP1.X > LP2.X)
                    {
                        intersections = new Vector3d[] { new Vector3d(x1, LP1.Y, 0.0), new Vector3d(-x2, LP2.Y, 0.0) };
                    }
                    else
                    {
                        intersections = new Vector3d[] { new Vector3d(-x1, LP1.Y, 0.0), new Vector3d(x2, LP2.Y, 0.0) };
                    }
                    return 2;
                }
                else
                {
                    // The line is tangency to the ellipse
                    // There is one real intersection 
                    if (LP1.Y > 0.0)
                    {
                        intersections = new Vector3d[] { new Vector3d(0.0, b, 0.0) };
                    }
                    else
                    {
                        intersections = new Vector3d[] { new Vector3d(0.0, -b, 0.0) };
                    }
                    return 1;
                }
            }
            else
            {
                // it's an oblique line

                // Calculate the m and n value of the line
                // y = mx + n
                //
                // y - Yp1 = (Yp2 -Yp1) / (Xp2 - Xp1)  * (x - Xp1) 
                // y = (Yp2 -Yp1) / (Xp2 -Xp1)* x - (Yp2 -Yp1) / (Xp2 -Xp1)* Xp1 + Yp1
                //
                // m = (Yp2 -Yp1) / (Xp2 -Xp1);
                // n = Yp1 - m * Xp1
                double m = (LP2.Y - LP1.Y) / (LP2.X - LP1.X);
                double n = LP1.Y - m * LP1.X;

                // Solve these two equations
                // by replacing y within the ellipse equation
                // 
                // (x/a)*(x/a) + (y/b)*(y/b) = 1  replace y with (mx + n)
                //
                // (x/a)*(x/a) + ((mx + n)/b) * ((mx + n)/b) = 1 ->
                // xx/aa + (mx+n)/b * (mx+n)/b  = 1
                // xxbb/aa + (mx+n) * (mx+n)  = bb
                // xxbb/aa + mmxx + 2*mnx + nn = bb
                // xxbb + mmaaxx + 2*mnaax + nnaa = bbaa
                // (aamm + bb) * xx + 2aamn * x + aa * (nn - bb) = 0 
                //
                // Coef_a = (aamm + bb)
                // Coef_b = 2aamn
                // Coef_c = aa * ( nn - bb )
                //
                // The discriminant
                // D = Coef_b * Coef_b - 4 * Coef_a * Coef_c
                // D = 4aaaammnn - 4aa(aamm + bb)(nn - bb)
                // D = 4aaaammnn - 4aa(aammnn + bbnn - aammbb - bbbb)
                // D = 4[ a*a * a*a * m*m * n*n - a*a * (a*a * m*m + b*b) * (n*n - b*b)]
                //
                // Calculate the discriminant
                double aa = 0.25 * width * width;
                double bb = 0.25 * height * height;
                double mm = m * m;
                double nn = n * n;
                double aamm = aa * mm;
                double Dis = 4.0 * (aa * aamm * nn - aa * (aamm + bb) * (nn - bb));

                // If Dis < 0 there is no intersection
                // if Dis = 0 there is only one tangency point, i.e. the line is tangent to the ellipse
                // if Dis > 0 there are two intersection points, i.e. the line is secant to the ellipse

                if (GraphicMath.IsEqual(Dis, 0.0, tolerance))
                {
                    // There is only one tangency point
                    // x = -0.5 * Coef_b / Coef_a
                    double x = -(aa * m * n) / (aamm + bb);
                    double y = m * x + n;
                    intersections = new Vector3d[] { new Vector3d(x, y, 0.0) };
                    return 1;
                }
                else if (Dis < 0.0)
                {
                    // There is no real intersection
                    intersections = null;
                    return 0;
                }
                else
                {
                    // There are two real intersections
                    double sqrt = Math.Sqrt(Dis);
                    double q = -0.5;
                    double Coef_b = 2.0 * aa * m * n;

                    if (Coef_b < 0.0)
                    {
                        q *= (Coef_b - sqrt);
                    }
                    else
                    {
                        q *= (Coef_b + sqrt);
                    }
                    // x1 = q / Coef_a
                    double x1 = q / (aamm + bb);
                    double y1 = m * x1 + n;
                    // x2 = Coef_c / q
                    double x2 = aa * (nn - bb) / q;
                    double y2 = m * x2 + n;

                    intersections = new Vector3d[] { new Vector3d(x1, y1, 0.0), new Vector3d(x2, y2, 0.0) };
                    return 2;
                }
            }
        }

        public static int GetIntersectionLineAndCircle(double rad, Vector3d center, Vector3d LP1, Vector3d LP2, out Vector3d[] intersections, double tolerance)
        {
            // Transform line points due to circle center
            double traLP1x = LP1.X - center.X;
            double traLP1y = LP1.Y - center.Y;
            double traLP2x = LP2.X - center.X;
            double traLP2y = LP2.Y - center.Y;

            if (GraphicMath.IsEqual(LP1.X, LP2.X, tolerance))
            {
                // The line is vertical -> the x value is already known.
                double absLP1x = Math.Abs(traLP1x);
                double absLP2x = Math.Abs(traLP2x);
                if (GraphicMath.IsGreater(absLP1x, rad, tolerance) && GraphicMath.IsGreater(absLP2x, rad, tolerance))
                {
                    // The line is outside ->
                    // There is no real intersection
                    intersections = null;
                    return 0;
                }
                else if (GraphicMath.IsGreater(rad, absLP1x, tolerance) && GraphicMath.IsGreater(rad, absLP2x, tolerance))
                {
                    // The line is inside ->
                    // There are two real intersections
                    double y1 = Math.Sqrt(rad * rad - traLP1x * traLP1x);
                    double y2 = Math.Sqrt(rad * rad - traLP2x * traLP2x);
                    if (LP1.Y > LP2.Y)
                    {
                        intersections = new Vector3d[] { 
                            new Vector3d(traLP1x + center.X, y1 + center.Y, 0.0), 
                            new Vector3d(traLP2x + center.X, center.Y - y2, 0.0) };
                    }
                    else
                    {
                        intersections = new Vector3d[] { 
                            new Vector3d(traLP1x + center.X, center.Y - y1, 0.0), 
                            new Vector3d(traLP2x + center.X, y2 + center.Y, 0.0) };
                    }
                    return 2;
                }
                else
                {
                    // The line is tangency to the circle
                    // There is one real intersection 
                    if (traLP1x > 0.0)
                    {
                        intersections = new Vector3d[] { new Vector3d(center.X + rad, center.Y, 0.0) };
                    }
                    else
                    {
                        intersections = new Vector3d[] { new Vector3d(center.X - rad, center.Y, 0.0) };
                    }
                    return 1;
                }
            }
            else if (GraphicMath.IsEqual(LP1.Y, LP2.Y, tolerance))
            {
                // The line is horizontal -> the y value is already known
                double absLP1y = Math.Abs(traLP1y);
                double absLP2y = Math.Abs(traLP2y);
                if (GraphicMath.IsGreater(absLP1y, rad, tolerance) && GraphicMath.IsGreater(absLP2y, rad, tolerance))
                {
                    // The line is outside ->
                    // There is no real intersection
                    intersections = null;
                    return 0;
                }
                else if (GraphicMath.IsGreater(rad, absLP1y, tolerance) && GraphicMath.IsGreater(rad, absLP2y, tolerance))
                {
                    // The line is inside ->
                    // There are two real intersections
                    double x1 = Math.Sqrt(rad * rad - traLP1y * traLP1y);
                    double x2 = Math.Sqrt(rad * rad - traLP2y * traLP2y);
                    if (LP1.X > LP2.X)
                    {
                        intersections = new Vector3d[] { 
                            new Vector3d(x1 + center.X, traLP1y + center.Y, 0.0), 
                            new Vector3d(center.X -x2, traLP2y + center.Y, 0.0) };
                    }
                    else
                    {
                        intersections = new Vector3d[] { 
                            new Vector3d(center.X - x1, traLP1y + center.Y, 0.0), 
                            new Vector3d(x2 + center.X, traLP2y + center.Y, 0.0) };
                    }
                    return 2;
                }
                else
                {
                    // The line is tangency to the circle
                    // There is one real intersection 
                    if (traLP1y > 0.0)
                    {
                        intersections = new Vector3d[] { new Vector3d(center.X, center.Y + rad, 0.0) };
                    }
                    else
                    {
                        intersections = new Vector3d[] { new Vector3d(center.X, center.Y - rad, 0.0) };
                    }
                    return 1;
                }
            }
            else
            {
                // it's an oblique line

                // Calculate the m and n value of the line
                // y = mx + n
                //
                // y - Yp1 = (Yp2 -Yp1) / (Xp2 - Xp1)  * (x - Xp1) 
                // y = (Yp2 -Yp1) / (Xp2 -Xp1)* x - (Yp2 -Yp1) / (Xp2 -Xp1)* Xp1 + Yp1
                //
                // m = (Yp2 -Yp1) / (Xp2 -Xp1);
                // n = Yp1 - m * Xp1
                double m = (traLP2y - traLP1y) / (traLP2x - traLP1x);
                double n = traLP1y - m * traLP1x;

                // Solve these two equations
                // by replacing y within the circle equation
                // 
                // x*x + y*y = r*r  replace y with (mx + n)
                //
                // xx + (mx + n)*(mx + n) = rr
                // xx + mmxx + 2*mnx + nn = rr;
                // (1 + mm) * xx + 2mn * x + nn-rr = 0;
                //
                // Coef_a = (1 + mm)
                // Coef_b = 2mn
                // Coef_c = ( nn - rr )
                //
                // The discriminant
                // D = Coef_b * Coef_b - 4 * Coef_a * Coef_c
                // D = 4mmnn - 4(1 + mm)(nn - rr)
                // D = 4mmnn - 4(nn -rr + mmnn - mmrr)
                // D = 4mmnn - 4nn +4rr - 4mmnn + 4mmrr
                // D = 4[ rr + mmrr - nn]
                //
                // Calculate the discriminant
                double rr = rad*rad;
                double nn = n * n;
                double mm = m * m;
                double Dis = 4.0 * (rr + mm * rr - nn); 

                // If Dis < 0 there is no intersection
                // if Dis = 0 there is only one tangency point, i.e. the line is tangent to the circle
                // if Dis > 0 there are two intersection points, i.e. the line is secant to the circle

                if (GraphicMath.IsEqual(Dis, 0.0, tolerance))
                {
                    // There is only one tangency point
                    // x = -0.5 * Coef_b / Coef_a
                    double x = -(m * n) / (1 + mm);
                    double y = m * x + n;
                    intersections = new Vector3d[] { 
                        new Vector3d(x + center.X, y + center.Y, 0.0) };
                    return 1;
                }
                else if (Dis < 0.0)
                {
                    // There is no real intersection
                    intersections = null;
                    return 0;
                }
                else
                {
                    // There are two real intersections
                    double sqrt = Math.Sqrt(Dis);
                    double q = -0.5;
                    double Coef_b = 2.0 * m * n;

                    if (Coef_b < 0.0)
                    {
                        q *= (Coef_b - sqrt);
                    }
                    else
                    {
                        q *= (Coef_b + sqrt);
                    }
                    // x1 = q / Coef_a
                    double x1 = q / (1 + mm);
                    double y1 = m * x1 + n;
                    // x2 = Coef_c / q
                    double x2 = (nn - rr) / q;
                    double y2 = m * x2 + n;

                    intersections = new Vector3d[] { 
                        new Vector3d(x1 + center.X, y1 + center.Y, 0.0), 
                        new Vector3d(x2 + center.X, y2 + center.Y, 0.0) };
                    return 2;
                }
            }
        }

        public static int GetIntersectionLineAndEllipse(Vector3d width, Vector3d height, Vector3d center, Vector3d LP1, Vector3d LP2, out Vector3d[] intersections, double tolerance)
        {
            // setup origin of coordinate system = 0/0/0
            Vector2d origin2D      = new Vector2d();
            // setup ellipse's width, height and center 2D vector (skip z-coordinate)
            Vector2d theWidth      = new Vector2d(width.X,  width.Y );
            Vector2d theHeight     = new Vector2d(height.X, height.Y);
            Vector2d center2D      = new Vector2d(center.X, center.Y);
            // setup 2D vector to the line points LP1 and LP2
            Vector2d orig_P1L2D  = new Vector2d( LP1.X, LP1.Y);
            Vector2d orig_P2L2D  = new Vector2d( LP2.X, LP2.Y);

            // transform the line points  to ellipse's system
            // point in ellipse's system = original point2D - center_of_ellipse
            Vector2d p1L2D       = new Vector2d(orig_P1L2D - center2D );
            Vector2d p2L2D       = new Vector2d(orig_P2L2D - center2D );
                        
            // get the length of width vector 2D   a = half of ellipse axis
            double a = Math.Sqrt(theWidth.X * theWidth.X + theWidth.Y * theWidth.Y);

            // get the length of height vector 2D  b = half of the other ellipse axis
            double b = Math.Sqrt(theHeight.X * theHeight.X + theHeight.Y * theHeight.Y);

            // no valid ellipse if both axis are nil
            if (( a < tolerance )&& ( b < tolerance ))
            {
                throw new System.ArgumentOutOfRangeException("The lenght of width and height vector are both less then the specified tolerance value.");
            }
            
            // if it is a circle
            if ( Math.Abs(a-b) < tolerance )
            {
                int foundInt = 0;
                if (a > b)
                {
                    foundInt =  GetIntersectionLineAndCircle(a, center, LP1, LP2, out intersections, tolerance);
                }
                else
                {
                    foundInt = GetIntersectionLineAndCircle(b, center, LP1, LP2, out intersections, tolerance);
                }
                return foundInt;
            }

            // no circle -> ellipse or line

            // calculate the rotation angle of the ellipse
            //
            // It is that angle about the bigger ellipse axis has to be rotated
            // so that it is mapped onto the x-axis
            double angle = 0.0;
            if( a > b ) // take bigger axis for angle calculation
            { 
                angle = Math.Abs(Math.Asin(width.Y / a));
                if (     width.Y>=0 && width.X>=0) // 1. Q
                {
                }
                else if (width.Y>=0 && width.X<0)  // 2. Q
                {
                    angle = Math.PI - angle;
                }
                else if (width.Y<0  && width.X<0)  // 3. Q
                {
                    angle = angle - Math.PI;
                }
                else if (width.Y<0  && width.X>=0) // 4. Q
                {
                    angle = -angle;
                }
            }
            else
            {
                angle = Math.Abs(Math.Asin(height.Y / b));
                if (     height.Y>=0 && height.X>=0)
                {
                }
                else if (height.Y>=0 && height.X<0)
                {
                    angle = Math.PI - angle;
                }
                else if (height.Y<0  && height.X<0)
                {
                    angle = angle - Math.PI;
                }
                else if (height.Y<0  && height.X>=0)
                if (height.Y<0  && height.X>=0)
                {
                    angle = -angle;
                }
            }

            // ajust (rotate) the line points according to the ellipses rotation angle
            Vector2d[] vec_array = new Vector2d[2];
            vec_array = new Vector2d[] { p1L2D, p2L2D };
            origin2D.Rotate(-angle, ref vec_array);
            p1L2D.X = vec_array[0].X;
            p1L2D.Y = vec_array[0].Y;
            p2L2D.X = vec_array[1].X;
            p2L2D.Y = vec_array[1].Y;

            // if it is only a line
            if (( a < tolerance )||( b < tolerance ))
            {
                Vector3d L2P1 = null;
                Vector3d L2P2 = null;

                if (a > b)
                {
                    L2P1 = new Vector3d(center.X - theWidth.X, center.Y - theWidth.Y, 0.0);
                    L2P2 = new Vector3d(center.X + theWidth.X, center.Y + theWidth.Y, 0.0);
                }
                else
                {
                    L2P1 = new Vector3d(center.X - theHeight.X, center.Y - theHeight.Y, 0.0);
                    L2P2 = new Vector3d(center.X + theHeight.X, center.Y + theHeight.Y, 0.0);
                }
                     
                // Calculate the intersection of the two finite lines
                Vector3d lineIntersection = new Vector3d();
                LineStatus lstate = GetIntersectionOf2Lines(LP1, LP2, L2P1, L2P2, lineIntersection, tolerance); 

                switch (lstate)
                {
                    case LineStatus.IntersectionOnBothLines:
                        {
                            intersections = new Vector3d[] { lineIntersection };
                            return 1; // one intersection found
                        }
                    case LineStatus.IsSame:
                        {
                            intersections = new Vector3d[] { L2P1, L2P2 };
                            return 2; // two intersection found
                        }
                    default:
                        {
                            intersections = null;
                            return 0; // no intersection found
                        }
                }
            }

            //  it is an ellipse
            else
            {
                // exception if width and height vector are not orthogonal
                if (!GraphicMath.IsEqual(0.0, theWidth.GetInnerProduct(theHeight), GraphicMath.DEFAULT_TOLERANCE))
                {
                    throw new System.ArgumentOutOfRangeException("width", "The width and the height vector are not orthogonal.");
                }

                Vector3d p1L3D = new Vector3d(p1L2D.X, p1L2D.Y, 0.0);
                Vector3d p2L3D = new Vector3d(p2L2D.X, p2L2D.Y, 0.0);
                int foundInt = 0;
                if (a > b)
                {
                    foundInt = GetIntersectionLineAndOrthogonalEllipse(2.0 * a, 2.0 * b, p1L3D, p2L3D, out intersections, tolerance);
                }
                else
                {
                    foundInt = GetIntersectionLineAndOrthogonalEllipse(2.0 * b, 2.0 * a, p1L3D, p2L3D, out intersections, tolerance);
                }

                if (foundInt == 2)
                {
                    // transform fount intersections back to original system  means
                    // rotate points due to the rotation of ellipse
                    // and intersection point = point in ellipses system + center_of_ellipse
                    Vector2d[] int2D = new Vector2d[] { 
                        new Vector2d(intersections[0].X,intersections[0].Y),
                        new Vector2d(intersections[1].X,intersections[1].Y)};

                    origin2D.Rotate(angle, ref int2D);
                    intersections[0].X = int2D[0].X + center2D.X;
                    intersections[0].Y = int2D[0].Y + center2D.Y;
                    intersections[0].Z = center.Z;
                    intersections[1].X = int2D[1].X + center2D.X;
                    intersections[1].Y = int2D[1].Y + center2D.Y;
                    intersections[1].Z = center.Z;
                }
                else if (foundInt == 1)
                {
                    Vector2d[] int2D = new Vector2d[] { 
                        new Vector2d(intersections[0].X,intersections[0].Y)};

                    origin2D.Rotate(angle, ref int2D);
                    intersections[0].X = int2D[0].X + center2D.X;
                    intersections[0].Y = int2D[0].Y + center2D.Y;
                    intersections[0].Z = center.Z;
                }

                return foundInt;
            }
        }        
        #endregion

        #region Constructors

        private GraphicMath()
        {
        }

        #endregion
    }
}