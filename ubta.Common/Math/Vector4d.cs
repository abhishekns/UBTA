#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : Vector4d.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion
using System;
using System.Windows;

namespace ubta.Common.Maths
{
    public class Vector4d
    {
        #region Constructors

        public Vector4d()
        {
            this.my2DPoint.X= 0.0;
            this.my2DPoint.Y = 0.0;
            this.myZ = 0.0;
            this.myW = 1.0;
        }

        public Vector4d(Vector3d vec3D)
        {
            this.my2DPoint.X= vec3D.my2DPoint.X;
            this.my2DPoint.Y = vec3D.my2DPoint.Y;
            this.myZ = vec3D.Z;
            this.myW = 1.0;
        }

        public Vector4d(Vector3d vec3D, double w)
        {
            this.my2DPoint.X= vec3D.my2DPoint.X;
            this.my2DPoint.Y = vec3D.my2DPoint.Y;
            this.myZ = vec3D.Z;
            this.myW = w;
        }

        public Vector4d(Vector4d vec4D)
        {
            this.my2DPoint.X= vec4D.my2DPoint.X;
            this.my2DPoint.Y = vec4D.my2DPoint.Y;
            this.myZ = vec4D.myZ;
            this.myW = vec4D.myW;
        }

        public Vector4d(double x_value, double y_value, double z_value, double w_value)
        {
            this.my2DPoint.X= x_value;
            this.my2DPoint.Y = y_value;
            this.myZ = z_value;
            this.myW = w_value;
        }
        #endregion

        #region Operators

        public static Vector4d operator +(Vector4d v1, Vector4d v2)
        {
            Vector4d sum = new Vector4d(
                v1.my2DPoint.X+ v2.my2DPoint.X,
                v1.my2DPoint.Y + v2.my2DPoint.Y,
                v1.myZ + v2.myZ,
                v1.myW + v2.myW);
            return (sum);
        }

        public static Vector4d Add(Vector4d v1, Vector4d v2)
        {
            Vector4d sum = new Vector4d(
                v1.my2DPoint.X+ v2.my2DPoint.X,
                v1.my2DPoint.Y + v2.my2DPoint.Y,
                v1.myZ + v2.myZ,
                v1.myW + v2.myW);
            return (sum);
        }

        public static Vector4d operator -(Vector4d v1, Vector4d v2)
        {
            Vector4d diff = new Vector4d(
                v1.my2DPoint.X- v2.my2DPoint.X,
                v1.my2DPoint.Y - v2.my2DPoint.Y,
                v1.myZ - v2.myZ,
                v1.myW - v2.myW);
            return (diff);
        }

        public static Vector4d Subtract(Vector4d v1, Vector4d v2)
        {
            Vector4d diff = new Vector4d(
                v1.my2DPoint.X- v2.my2DPoint.X,
                v1.my2DPoint.Y - v2.my2DPoint.Y,
                v1.myZ - v2.myZ,
                v1.myW - v2.myW);
            return (diff);
        }

        public static Vector4d operator *(Vector4d v1, double mul)
        {
            Vector4d result = new Vector4d(
                v1.my2DPoint.X* mul,
                v1.my2DPoint.Y * mul,
                v1.myZ * mul,
                v1.myW * mul);
            return (result);
        }

        public static Vector4d operator *(double mul, Vector4d v1)
        {
            Vector4d result = new Vector4d(
                v1.my2DPoint.X* mul,
                v1.my2DPoint.Y * mul,
                v1.myZ * mul,
                v1.myW * mul);
            return (result);
        }

        public static Vector4d Multiply(Vector4d v1, double mul)
        {
            Vector4d result = new Vector4d(
                v1.my2DPoint.X* mul,
                v1.my2DPoint.Y * mul,
                v1.myZ * mul,
                v1.myW * mul);
            return (result);
        }

        public static Vector4d operator /(Vector4d v1, double div)
        {
            if ((System.Double.IsNaN(div)) || (div == 0.0))
            {
                throw new System.ArgumentOutOfRangeException("div", "Invalid division by zero. The value is: " + div + " .");
            }

            Vector4d result = new Vector4d(
                v1.my2DPoint.X/ div,
                v1.my2DPoint.Y / div,
                v1.myZ / div,
                v1.myW / div);
            return (result);
        }

        public static Vector4d Divide(Vector4d v1, double div)
        {
            if ((System.Double.IsNaN(div)) || (div == 0.0))
            {
                throw new System.ArgumentOutOfRangeException("div", "Invalid division by zero. The value is: " + div + " .");
            }

            Vector4d result = new Vector4d(
                v1.my2DPoint.X/ div,
                v1.my2DPoint.Y / div,
                v1.myZ / div,
                v1.myW / div);
            return (result);
        }
        #endregion

        #region set / get Methods

        public void Init(double initVal)
        {
            this.my2DPoint.X = initVal;
            this.my2DPoint.Y = initVal;
            this.myZ = initVal;
            this.myW = 1.0;
        }

        public void SetComponents(double xval, double yval, double zval, double wval)
        {
            this.my2DPoint.X = xval;
            this.my2DPoint.Y = yval;
            this.myZ = zval;
            this.myW = wval;
        }

        public void GetComponents(out double xval, out double yval, out double zval, out double wval)
        {
            xval = this.my2DPoint.X;
            yval = this.my2DPoint.Y;
            zval = this.myZ;
            wval = this.myW;
        }

        public void SetFromVector3d(Vector3d vec3D)
        {
            this.my2DPoint.X= vec3D.my2DPoint.X;
            this.my2DPoint.Y = vec3D.my2DPoint.Y;
            this.myZ = vec3D.Z;
            this.myW = 1.0;
        }

        public void SetFromVector4d(Vector4d vec4D)
        {
            this.my2DPoint.X= vec4D.my2DPoint.X;
            this.my2DPoint.Y = vec4D.my2DPoint.Y;
            this.myZ = vec4D.myZ;
            this.myW = vec4D.myW;
        }

        public void SetFromInvertedVector3d(Vector3d vec3D)
        {
            this.my2DPoint.X= -vec3D.my2DPoint.X;
            this.my2DPoint.Y = -vec3D.my2DPoint.Y;
            this.myZ = -vec3D.myZval;
            this.myW = 1.0;
        }

        public void SetHomogenousFromInvertedVector4d(Vector4d vec4D)
        {
            this.my2DPoint.X= -vec4D.my2DPoint.X;
            this.my2DPoint.Y = -vec4D.my2DPoint.Y;
            this.myZ = -vec4D.myZ;
            this.myW = 1.0;
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
                this.my2DPoint.X= value;
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

        public double Z
        {
            get
            {
                return this.myZ;
            }

            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("Z", "invalid set Z component " + value);
                }
                this.myZ = value;
            }
        }

        public double W
        {
            get
            {
                return this.myW;
            }

            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("W", "invalid set W component " + value);
                }
                this.myW = value;
            }
        }

        #endregion

        #region arithmetic Methods

        public static Vector4d AddHomogenous(Vector4d v1, Vector4d v2)
        {
            Vector4d sum = new Vector4d(
            v1.my2DPoint.X+ v2.my2DPoint.X,
            v1.my2DPoint.Y + v2.my2DPoint.Y,
            v1.myZ + v2.myZ,
            1.0);

            return (sum);
        }

        public static Vector4d SubtractHomogenous(Vector4d v1, Vector4d v2)
        {
            Vector4d dif = new Vector4d(
            v1.my2DPoint.X- v2.my2DPoint.X,
            v1.my2DPoint.Y - v2.my2DPoint.Y,
            v1.myZ - v2.myZ,
            1.0);

            return (dif);
        }

        public void Invert()
        {
            this.my2DPoint.X= -this.my2DPoint.X;
            this.my2DPoint.Y = -this.my2DPoint.Y;
            this.myZ = -this.myZ;
            this.myW = -this.myW;
            return;
        }

        public void InvertHomogenous()
        {
            this.my2DPoint.X= -this.my2DPoint.X;
            this.my2DPoint.Y = -this.my2DPoint.Y;
            this.myZ = -this.myZ;
            this.myW = 1.0;
            return;
        }

        public double GetLength()
        {
            return Math.Sqrt((this.my2DPoint.X* this.my2DPoint.X) +
                             (this.my2DPoint.Y * this.my2DPoint.Y) +
                             (this.myZ * this.myZ) +
                             (this.myW * this.myW));
        }

        public void SetLength(double lenght, double tolerance)
        {
            this.Normalize(tolerance);
            this.my2DPoint.X*= lenght;
            this.my2DPoint.Y *= lenght;
            this.myZ *= lenght;
            this.myW *= lenght;
        }

        public Vector4d GetNormalized(double tolerance)
        {
            double length;
            length = Math.Sqrt((this.my2DPoint.X* this.my2DPoint.X) +
                               (this.my2DPoint.Y * this.my2DPoint.Y) +
                               (this.myZ * this.myZ) +
                               (this.myW * this.myW));

            if (length < tolerance)
            {
                throw new System.ArithmeticException("The vector IsNil. Its length is: " + length + " .");
            }
            else
            {
                Vector4d vec_out = new Vector4d(
                this.my2DPoint.X/ length,
                this.my2DPoint.Y / length,
                this.myZ / length,
                this.myW / length);
                return vec_out;
            }
        }

        public void Normalize(double tolerance)
        {
            double length;
            length = Math.Sqrt((this.my2DPoint.X* this.my2DPoint.X) +
                               (this.my2DPoint.Y * this.my2DPoint.Y) +
                               (this.myZ * this.myZ) +
                               (this.myW * this.myW));

            if (length < tolerance)
            {
                throw new System.ArithmeticException("The vector IsNil. Its length is: " + length + " .");
            }
            else
            {
                this.my2DPoint.X/= length;
                this.my2DPoint.Y /= length;
                this.myZ /= length;
                this.myW /= length;
                return;
            }
        }

        public bool IsNormalized(double tolerance)
        {
            double length2 = ((this.my2DPoint.X* this.my2DPoint.X) +
                              (this.my2DPoint.Y * this.my2DPoint.Y) +
                              (this.myZ * this.myZ) +
                              (this.myW * this.myW));

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
            double length2 = ((this.my2DPoint.X* this.my2DPoint.X) +
                              (this.my2DPoint.Y * this.my2DPoint.Y) +
                              (this.myZ * this.myZ) +
                              (this.myW * this.myW));

            if (length2 < (tolerance * tolerance))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsAlmostEqual(Vector4d v1, double tolerance)
        {
            return (((v1.my2DPoint.X- this.my2DPoint.X) * (v1.my2DPoint.X- this.my2DPoint.X) +
                     (v1.my2DPoint.Y - this.my2DPoint.Y) * (v1.my2DPoint.Y - this.my2DPoint.Y) +
                     (v1.myZ - this.myZ) * (v1.myZ - this.myZ) +
                     (v1.myW - this.myW) * (v1.myW - this.myW)) <
                     (tolerance * tolerance));
        }

        public double GetInnerProduct(Vector4d v1)
        {
            return ((v1.my2DPoint.X* this.my2DPoint.X) + (v1.my2DPoint.Y * this.my2DPoint.Y) +
                    (v1.myZ * this.myZ) + (v1.myW * this.myW));
        }

        public bool IsAffine(double tolerance)
        {
            if (Math.Abs(this.myW - 1.0) > tolerance)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region Data

        public Point my2DPoint;
        public double myZ;
        public double myW;

        #endregion
    }
}