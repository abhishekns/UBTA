#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : Matrix3x3.cs
    Last Author     : Abhishek Sharma
    Language        : C#
    Creation Date   : 18. March 2010
    Description     : 

     
-----------------------------------------------------------------------------*/
/*] END */
#endregion
using System;
using System.Runtime.InteropServices;

namespace ubta.Common.Maths
{
    public class Matrix3x3d
    {
        #region Constructors

        public Matrix3x3d()
        {
            this.myMatrix3x3.myr1c1 = 0.0;
            this.myMatrix3x3.myr1c2 = 0.0;
            this.myMatrix3x3.myr1c3 = 0.0;

            this.myMatrix3x3.myr2c1 = 0.0;
            this.myMatrix3x3.myr2c2 = 0.0;
            this.myMatrix3x3.myr2c3 = 0.0;

            this.myMatrix3x3.myr3c1 = 0.0;
            this.myMatrix3x3.myr3c2 = 0.0;
            this.myMatrix3x3.myr3c3 = 0.0;
        }

        public Matrix3x3d(Matrix3x3d mat3x3)
        {
            this.myMatrix3x3.myr1c1 = mat3x3.myMatrix3x3.myr1c1;
            this.myMatrix3x3.myr1c2 = mat3x3.myMatrix3x3.myr1c2;
            this.myMatrix3x3.myr1c3 = mat3x3.myMatrix3x3.myr1c3;

            this.myMatrix3x3.myr2c1 = mat3x3.myMatrix3x3.myr2c1;
            this.myMatrix3x3.myr2c2 = mat3x3.myMatrix3x3.myr2c2;
            this.myMatrix3x3.myr2c3 = mat3x3.myMatrix3x3.myr2c3;

            this.myMatrix3x3.myr3c1 = mat3x3.myMatrix3x3.myr3c1;
            this.myMatrix3x3.myr3c2 = mat3x3.myMatrix3x3.myr3c2;
            this.myMatrix3x3.myr3c3 = mat3x3.myMatrix3x3.myr3c3;
        }

        public Matrix3x3d(
            double r1c1, double r1c2, double r1c3,
            double r2c1, double r2c2, double r2c3,
            double r3c1, double r3c2, double r3c3)
        {
            this.myMatrix3x3.myr1c1 = r1c1;
            this.myMatrix3x3.myr1c2 = r1c2;
            this.myMatrix3x3.myr1c3 = r1c3;

            this.myMatrix3x3.myr2c1 = r2c1;
            this.myMatrix3x3.myr2c2 = r2c2;
            this.myMatrix3x3.myr2c3 = r2c3;

            this.myMatrix3x3.myr3c1 = r3c1;
            this.myMatrix3x3.myr3c2 = r3c2;
            this.myMatrix3x3.myr3c3 = r3c3;
        }
        #endregion

        #region Operators

        public static Matrix3x3d operator +(Matrix3x3d matrix1, Matrix3x3d matrix2)
        {
            Matrix3x3d sum = new Matrix3x3d
                (
                matrix1.myMatrix3x3.myr1c1 + matrix2.myMatrix3x3.myr1c1, // r1c1
                matrix1.myMatrix3x3.myr1c2 + matrix2.myMatrix3x3.myr1c2, // r1c2
                matrix1.myMatrix3x3.myr1c3 + matrix2.myMatrix3x3.myr1c3, // r1c3

                matrix1.myMatrix3x3.myr2c1 + matrix2.myMatrix3x3.myr2c1, // r2c1   
                matrix1.myMatrix3x3.myr2c2 + matrix2.myMatrix3x3.myr2c2, // r2c2
                matrix1.myMatrix3x3.myr2c3 + matrix2.myMatrix3x3.myr2c3, // r2c3

                matrix1.myMatrix3x3.myr3c1 + matrix2.myMatrix3x3.myr3c1, // r3c1
                matrix1.myMatrix3x3.myr3c2 + matrix2.myMatrix3x3.myr3c2, // r3c2
                matrix1.myMatrix3x3.myr3c3 + matrix2.myMatrix3x3.myr3c3  // r3c3  
                );

            return (sum);
        }

        public static Matrix3x3d Add(Matrix3x3d matrix1, Matrix3x3d matrix2)
        {
            Matrix3x3d sum = new Matrix3x3d
                (
                matrix1.myMatrix3x3.myr1c1 + matrix2.myMatrix3x3.myr1c1, // r1c1
                matrix1.myMatrix3x3.myr1c2 + matrix2.myMatrix3x3.myr1c2, // r1c2
                matrix1.myMatrix3x3.myr1c3 + matrix2.myMatrix3x3.myr1c3, // r1c3

                matrix1.myMatrix3x3.myr2c1 + matrix2.myMatrix3x3.myr2c1, // r2c1   
                matrix1.myMatrix3x3.myr2c2 + matrix2.myMatrix3x3.myr2c2, // r2c2
                matrix1.myMatrix3x3.myr2c3 + matrix2.myMatrix3x3.myr2c3, // r2c3

                matrix1.myMatrix3x3.myr3c1 + matrix2.myMatrix3x3.myr3c1, // r3c1
                matrix1.myMatrix3x3.myr3c2 + matrix2.myMatrix3x3.myr3c2, // r3c2
                matrix1.myMatrix3x3.myr3c3 + matrix2.myMatrix3x3.myr3c3  // r3c3  
                );

            return (sum);
        }

        public static Matrix3x3d operator +(Matrix3x3d matrix, double scalar)
        {

            Matrix3x3d sum = new Matrix3x3d
                (
                matrix.myMatrix3x3.myr1c1 + scalar, // r1c1
                matrix.myMatrix3x3.myr1c2 + scalar, // r1c2
                matrix.myMatrix3x3.myr1c3 + scalar, // r1c3

                matrix.myMatrix3x3.myr2c1 + scalar, // r2c1   
                matrix.myMatrix3x3.myr2c2 + scalar, // r2c2
                matrix.myMatrix3x3.myr2c3 + scalar, // r2c3

                matrix.myMatrix3x3.myr3c1 + scalar, // r3c1
                matrix.myMatrix3x3.myr3c2 + scalar, // r3c2
                matrix.myMatrix3x3.myr3c3 + scalar  // r3c3  
                );

            return (sum);
        }

        public static Matrix3x3d Add(Matrix3x3d matrix, double scalar)
        {
            Matrix3x3d sum = new Matrix3x3d
                (
                matrix.myMatrix3x3.myr1c1 + scalar, // r1c1
                matrix.myMatrix3x3.myr1c2 + scalar, // r1c2
                matrix.myMatrix3x3.myr1c3 + scalar, // r1c3

                matrix.myMatrix3x3.myr2c1 + scalar, // r2c1   
                matrix.myMatrix3x3.myr2c2 + scalar, // r2c2
                matrix.myMatrix3x3.myr2c3 + scalar, // r2c3

                matrix.myMatrix3x3.myr3c1 + scalar, // r3c1
                matrix.myMatrix3x3.myr3c2 + scalar, // r3c2
                matrix.myMatrix3x3.myr3c3 + scalar  // r3c3  
                );

            return (sum);
        }

        public static Matrix3x3d operator -(Matrix3x3d matrix, double scalar)
        {
            Matrix3x3d sum = new Matrix3x3d
                (
                matrix.myMatrix3x3.myr1c1 - scalar, // r1c1
                matrix.myMatrix3x3.myr1c2 - scalar, // r1c2
                matrix.myMatrix3x3.myr1c3 - scalar, // r1c3

                matrix.myMatrix3x3.myr2c1 - scalar, // r2c1   
                matrix.myMatrix3x3.myr2c2 - scalar, // r2c2
                matrix.myMatrix3x3.myr2c3 - scalar, // r2c3

                matrix.myMatrix3x3.myr3c1 - scalar, // r3c1
                matrix.myMatrix3x3.myr3c2 - scalar, // r3c2
                matrix.myMatrix3x3.myr3c3 - scalar  // r3c3  
                );

            return (sum);
        }

        public static Matrix3x3d Subtract(Matrix3x3d matrix, double scalar)
        {
            Matrix3x3d sum = new Matrix3x3d
                (
                matrix.myMatrix3x3.myr1c1 - scalar, // r1c1
                matrix.myMatrix3x3.myr1c2 - scalar, // r1c2
                matrix.myMatrix3x3.myr1c3 - scalar, // r1c3

                matrix.myMatrix3x3.myr2c1 - scalar, // r2c1   
                matrix.myMatrix3x3.myr2c2 - scalar, // r2c2
                matrix.myMatrix3x3.myr2c3 - scalar, // r2c3

                matrix.myMatrix3x3.myr3c1 - scalar, // r3c1
                matrix.myMatrix3x3.myr3c2 - scalar, // r3c2
                matrix.myMatrix3x3.myr3c3 - scalar  // r3c3  
                );

            return (sum);
        }

        public static Matrix3x3d operator -(Matrix3x3d matrix)
        {
            Matrix3x3d neg = new Matrix3x3d
                (
                -matrix.myMatrix3x3.myr1c1, // r1c1
                -matrix.myMatrix3x3.myr1c2, // r1c2
                -matrix.myMatrix3x3.myr1c3, // r1c3

                -matrix.myMatrix3x3.myr2c1, // r2c1   
                -matrix.myMatrix3x3.myr2c2, // r2c2
                -matrix.myMatrix3x3.myr2c3, // r2c3

                -matrix.myMatrix3x3.myr3c1, // r3c1
                -matrix.myMatrix3x3.myr3c2, // r3c2
                -matrix.myMatrix3x3.myr3c3  // r3c3  
                );

            return (neg);
        }

        public static Matrix3x3d Negate(Matrix3x3d matrix)
        {
            Matrix3x3d neg = new Matrix3x3d
                (
                -matrix.myMatrix3x3.myr1c1, // r1c1
                -matrix.myMatrix3x3.myr1c2, // r1c2
                -matrix.myMatrix3x3.myr1c3, // r1c3

                -matrix.myMatrix3x3.myr2c1, // r2c1   
                -matrix.myMatrix3x3.myr2c2, // r2c2
                -matrix.myMatrix3x3.myr2c3, // r2c3

                -matrix.myMatrix3x3.myr3c1, // r3c1
                -matrix.myMatrix3x3.myr3c2, // r3c2
                -matrix.myMatrix3x3.myr3c3  // r3c3  
                );

            return (neg);
        }

        public static Matrix3x3d operator *(Matrix3x3d matrix, double scalar)
        {
            Matrix3x3d result = new Matrix3x3d
                (
                matrix.myMatrix3x3.myr1c1 * scalar, // r1c1
                matrix.myMatrix3x3.myr1c2 * scalar, // r1c2
                matrix.myMatrix3x3.myr1c3 * scalar, // r1c3

                matrix.myMatrix3x3.myr2c1 * scalar, // r2c1   
                matrix.myMatrix3x3.myr2c2 * scalar, // r2c2
                matrix.myMatrix3x3.myr2c3 * scalar, // r2c3

                matrix.myMatrix3x3.myr3c1 * scalar, // r3c1
                matrix.myMatrix3x3.myr3c2 * scalar, // r3c2
                matrix.myMatrix3x3.myr3c3 * scalar  // r3c3  
                );

            return (result);
        }

        public static Matrix3x3d Multiply(Matrix3x3d matrix, double scalar)
        {
            Matrix3x3d result = new Matrix3x3d
                (
                matrix.myMatrix3x3.myr1c1 * scalar, // r1c1
                matrix.myMatrix3x3.myr1c2 * scalar, // r1c2
                matrix.myMatrix3x3.myr1c3 * scalar, // r1c3

                matrix.myMatrix3x3.myr2c1 * scalar, // r2c1   
                matrix.myMatrix3x3.myr2c2 * scalar, // r2c2
                matrix.myMatrix3x3.myr2c3 * scalar, // r2c3

                matrix.myMatrix3x3.myr3c1 * scalar, // r3c1
                matrix.myMatrix3x3.myr3c2 * scalar, // r3c2
                matrix.myMatrix3x3.myr3c3 * scalar  // r3c3  
                );

            return (result);
        }

        public static Matrix3x3d operator *(double scalar, Matrix3x3d matrix)
        {
            Matrix3x3d result = new Matrix3x3d
                (
                matrix.myMatrix3x3.myr1c1 * scalar, // r1c1
                matrix.myMatrix3x3.myr1c2 * scalar, // r1c2
                matrix.myMatrix3x3.myr1c3 * scalar, // r1c3

                matrix.myMatrix3x3.myr2c1 * scalar, // r2c1   
                matrix.myMatrix3x3.myr2c2 * scalar, // r2c2
                matrix.myMatrix3x3.myr2c3 * scalar, // r2c3

                matrix.myMatrix3x3.myr3c1 * scalar, // r3c1
                matrix.myMatrix3x3.myr3c2 * scalar, // r3c2
                matrix.myMatrix3x3.myr3c3 * scalar  // r3c3  
                );

            return (result);
        }

        public static Matrix3x3d operator *(Matrix3x3d matrix1, Matrix3x3d matrix2)
        {
            Matrix3x3d result = new Matrix3x3d
                (
                matrix1.myMatrix3x3.myr1c1 * matrix2.myMatrix3x3.myr1c1 + // r1c1 
                matrix1.myMatrix3x3.myr1c2 * matrix2.myMatrix3x3.myr2c1 +
                matrix1.myMatrix3x3.myr1c3 * matrix2.myMatrix3x3.myr3c1,

                matrix1.myMatrix3x3.myr1c1 * matrix2.myMatrix3x3.myr1c2 + // r1c2 
                matrix1.myMatrix3x3.myr1c2 * matrix2.myMatrix3x3.myr2c2 +
                matrix1.myMatrix3x3.myr1c3 * matrix2.myMatrix3x3.myr3c2,

                matrix1.myMatrix3x3.myr1c1 * matrix2.myMatrix3x3.myr1c3 + // r1c3 
                matrix1.myMatrix3x3.myr1c2 * matrix2.myMatrix3x3.myr2c3 +
                matrix1.myMatrix3x3.myr1c3 * matrix2.myMatrix3x3.myr3c3,

                matrix1.myMatrix3x3.myr2c1 * matrix2.myMatrix3x3.myr1c1 + // r2c1
                matrix1.myMatrix3x3.myr2c2 * matrix2.myMatrix3x3.myr2c1 +
                matrix1.myMatrix3x3.myr2c3 * matrix2.myMatrix3x3.myr3c1,

                matrix1.myMatrix3x3.myr2c1 * matrix2.myMatrix3x3.myr1c2 + // r2c2
                matrix1.myMatrix3x3.myr2c2 * matrix2.myMatrix3x3.myr2c2 +
                matrix1.myMatrix3x3.myr2c3 * matrix2.myMatrix3x3.myr3c2,

                matrix1.myMatrix3x3.myr2c1 * matrix2.myMatrix3x3.myr1c3 + // r2c3 
                matrix1.myMatrix3x3.myr2c2 * matrix2.myMatrix3x3.myr2c3 +
                matrix1.myMatrix3x3.myr2c3 * matrix2.myMatrix3x3.myr3c3,

                matrix1.myMatrix3x3.myr3c1 * matrix2.myMatrix3x3.myr1c1 + // r3c1
                matrix1.myMatrix3x3.myr3c2 * matrix2.myMatrix3x3.myr2c1 +
                matrix1.myMatrix3x3.myr3c3 * matrix2.myMatrix3x3.myr3c1,

                matrix1.myMatrix3x3.myr3c1 * matrix2.myMatrix3x3.myr1c2 + // r3c2
                matrix1.myMatrix3x3.myr3c2 * matrix2.myMatrix3x3.myr2c2 +
                matrix1.myMatrix3x3.myr3c3 * matrix2.myMatrix3x3.myr3c2,

                matrix1.myMatrix3x3.myr3c1 * matrix2.myMatrix3x3.myr1c3 + // r3c3
                matrix1.myMatrix3x3.myr3c2 * matrix2.myMatrix3x3.myr2c3 +
                matrix1.myMatrix3x3.myr3c3 * matrix2.myMatrix3x3.myr3c3
                );

            return (result);
        }

        public static Matrix3x3d Multiply(Matrix3x3d matrix1, Matrix3x3d matrix2)
        {
            Matrix3x3d result = new Matrix3x3d
                (
                matrix1.myMatrix3x3.myr1c1 * matrix2.myMatrix3x3.myr1c1 + // r1c1 
                matrix1.myMatrix3x3.myr1c2 * matrix2.myMatrix3x3.myr2c1 +
                matrix1.myMatrix3x3.myr1c3 * matrix2.myMatrix3x3.myr3c1,

                matrix1.myMatrix3x3.myr1c1 * matrix2.myMatrix3x3.myr1c2 + // r1c2 
                matrix1.myMatrix3x3.myr1c2 * matrix2.myMatrix3x3.myr2c2 +
                matrix1.myMatrix3x3.myr1c3 * matrix2.myMatrix3x3.myr3c2,

                matrix1.myMatrix3x3.myr1c1 * matrix2.myMatrix3x3.myr1c3 + // r1c3 
                matrix1.myMatrix3x3.myr1c2 * matrix2.myMatrix3x3.myr2c3 +
                matrix1.myMatrix3x3.myr1c3 * matrix2.myMatrix3x3.myr3c3,

                matrix1.myMatrix3x3.myr2c1 * matrix2.myMatrix3x3.myr1c1 + // r2c1
                matrix1.myMatrix3x3.myr2c2 * matrix2.myMatrix3x3.myr2c1 +
                matrix1.myMatrix3x3.myr2c3 * matrix2.myMatrix3x3.myr3c1,

                matrix1.myMatrix3x3.myr2c1 * matrix2.myMatrix3x3.myr1c2 + // r2c2
                matrix1.myMatrix3x3.myr2c2 * matrix2.myMatrix3x3.myr2c2 +
                matrix1.myMatrix3x3.myr2c3 * matrix2.myMatrix3x3.myr3c2,

                matrix1.myMatrix3x3.myr2c1 * matrix2.myMatrix3x3.myr1c3 + // r2c3 
                matrix1.myMatrix3x3.myr2c2 * matrix2.myMatrix3x3.myr2c3 +
                matrix1.myMatrix3x3.myr2c3 * matrix2.myMatrix3x3.myr3c3,

                matrix1.myMatrix3x3.myr3c1 * matrix2.myMatrix3x3.myr1c1 + // r3c1
                matrix1.myMatrix3x3.myr3c2 * matrix2.myMatrix3x3.myr2c1 +
                matrix1.myMatrix3x3.myr3c3 * matrix2.myMatrix3x3.myr3c1,

                matrix1.myMatrix3x3.myr3c1 * matrix2.myMatrix3x3.myr1c2 + // r3c2
                matrix1.myMatrix3x3.myr3c2 * matrix2.myMatrix3x3.myr2c2 +
                matrix1.myMatrix3x3.myr3c3 * matrix2.myMatrix3x3.myr3c2,

                matrix1.myMatrix3x3.myr3c1 * matrix2.myMatrix3x3.myr1c3 + // r3c3
                matrix1.myMatrix3x3.myr3c2 * matrix2.myMatrix3x3.myr2c3 +
                matrix1.myMatrix3x3.myr3c3 * matrix2.myMatrix3x3.myr3c3
                );

            return (result);
        }

        public static Vector3d operator *(Vector3d row_vector_in, Matrix3x3d matrix_in)
        {
            Vector3d result = new Vector3d
                (
                matrix_in.myMatrix3x3.myr1c1 * row_vector_in.X +  // X
                matrix_in.myMatrix3x3.myr2c1 * row_vector_in.Y +
                matrix_in.myMatrix3x3.myr3c1 * row_vector_in.Z,

                matrix_in.myMatrix3x3.myr1c2 * row_vector_in.X +  // Y
                matrix_in.myMatrix3x3.myr2c2 * row_vector_in.Y +
                matrix_in.myMatrix3x3.myr3c2 * row_vector_in.Z,

                matrix_in.myMatrix3x3.myr1c3 * row_vector_in.X +  // Z
                matrix_in.myMatrix3x3.myr2c3 * row_vector_in.Y +
                matrix_in.myMatrix3x3.myr3c3 * row_vector_in.Z
                );

            return result;
        }

        public static Vector3d Multiply(Vector3d row_vector_in, Matrix3x3d matrix_in)
        {
            Vector3d result = new Vector3d
                (
                matrix_in.myMatrix3x3.myr1c1 * row_vector_in.X +  // X
                matrix_in.myMatrix3x3.myr2c1 * row_vector_in.Y +
                matrix_in.myMatrix3x3.myr3c1 * row_vector_in.Z,

                matrix_in.myMatrix3x3.myr1c2 * row_vector_in.X +  // Y
                matrix_in.myMatrix3x3.myr2c2 * row_vector_in.Y +
                matrix_in.myMatrix3x3.myr3c2 * row_vector_in.Z,

                matrix_in.myMatrix3x3.myr1c3 * row_vector_in.X +  // Z
                matrix_in.myMatrix3x3.myr2c3 * row_vector_in.Y +
                matrix_in.myMatrix3x3.myr3c3 * row_vector_in.Z
                );

            return result;
        }

        public static Vector3d operator *(Matrix3x3d matrix_in, Vector3d column_vector_in)
        {
            Vector3d result = new Vector3d
                (
                matrix_in.myMatrix3x3.myr1c1 * column_vector_in.X + // X
                matrix_in.myMatrix3x3.myr1c2 * column_vector_in.Y +
                matrix_in.myMatrix3x3.myr1c3 * column_vector_in.Z,

                matrix_in.myMatrix3x3.myr2c1 * column_vector_in.X + // Y
                matrix_in.myMatrix3x3.myr2c2 * column_vector_in.Y +
                matrix_in.myMatrix3x3.myr2c3 * column_vector_in.Z,

                matrix_in.myMatrix3x3.myr3c1 * column_vector_in.X + // Z
                matrix_in.myMatrix3x3.myr3c2 * column_vector_in.Y +
                matrix_in.myMatrix3x3.myr3c3 * column_vector_in.Z
                );

            return result;
        }

        public static Vector3d Multiply(Matrix3x3d matrix_in, Vector3d column_vector_in)
        {
            Vector3d result = new Vector3d
                (
                matrix_in.myMatrix3x3.myr1c1 * column_vector_in.X + // X
                matrix_in.myMatrix3x3.myr1c2 * column_vector_in.Y +
                matrix_in.myMatrix3x3.myr1c3 * column_vector_in.Z,

                matrix_in.myMatrix3x3.myr2c1 * column_vector_in.X + // Y
                matrix_in.myMatrix3x3.myr2c2 * column_vector_in.Y +
                matrix_in.myMatrix3x3.myr2c3 * column_vector_in.Z,

                matrix_in.myMatrix3x3.myr3c1 * column_vector_in.X + // Z
                matrix_in.myMatrix3x3.myr3c2 * column_vector_in.Y +
                matrix_in.myMatrix3x3.myr3c3 * column_vector_in.Z
                );

            return result;
        }
        #endregion

        #region Set / get Methods

        public void Init(double initVal)
        {
            this.myMatrix3x3.myr1c1 = initVal;
            this.myMatrix3x3.myr1c2 = initVal;
            this.myMatrix3x3.myr1c3 = initVal;

            this.myMatrix3x3.myr2c1 = initVal;
            this.myMatrix3x3.myr2c2 = initVal;
            this.myMatrix3x3.myr2c3 = initVal;

            this.myMatrix3x3.myr3c1 = initVal;
            this.myMatrix3x3.myr3c2 = initVal;
            this.myMatrix3x3.myr3c3 = initVal;
        }

        public void SetMatrix(double r1c1, double r1c2, double r1c3,
                              double r2c1, double r2c2, double r2c3,
                              double r3c1, double r3c2, double r3c3)
        {
            this.myMatrix3x3.myr1c1 = r1c1;
            this.myMatrix3x3.myr1c2 = r1c2;
            this.myMatrix3x3.myr1c3 = r1c3;

            this.myMatrix3x3.myr2c1 = r2c1;
            this.myMatrix3x3.myr2c2 = r2c2;
            this.myMatrix3x3.myr2c3 = r2c3;

            this.myMatrix3x3.myr3c1 = r3c1;
            this.myMatrix3x3.myr3c2 = r3c2;
            this.myMatrix3x3.myr3c3 = r3c3;
        }

        public void GetMatrix(out double r1c1, out double r1c2, out double r1c3,
                              out double r2c1, out double r2c2, out double r2c3,
                              out double r3c1, out double r3c2, out double r3c3)
        {
            r1c1 = this.myMatrix3x3.myr1c1;
            r2c1 = this.myMatrix3x3.myr2c1;
            r3c1 = this.myMatrix3x3.myr3c1;

            r1c2 = this.myMatrix3x3.myr1c2;
            r2c2 = this.myMatrix3x3.myr2c2;
            r3c2 = this.myMatrix3x3.myr3c2;

            r1c3 = this.myMatrix3x3.myr1c3;
            r2c3 = this.myMatrix3x3.myr2c3;
            r3c3 = this.myMatrix3x3.myr3c3;
        }

        public void SetFromMatrix3x3(Matrix3x3d mat3x3)
        {
            this.myMatrix3x3.myr1c1 = mat3x3.myMatrix3x3.myr1c1;
            this.myMatrix3x3.myr2c1 = mat3x3.myMatrix3x3.myr2c1;
            this.myMatrix3x3.myr3c1 = mat3x3.myMatrix3x3.myr3c1;

            this.myMatrix3x3.myr1c2 = mat3x3.myMatrix3x3.myr1c2;
            this.myMatrix3x3.myr2c2 = mat3x3.myMatrix3x3.myr2c2;
            this.myMatrix3x3.myr3c2 = mat3x3.myMatrix3x3.myr3c2;

            this.myMatrix3x3.myr1c3 = mat3x3.myMatrix3x3.myr1c3;
            this.myMatrix3x3.myr2c3 = mat3x3.myMatrix3x3.myr2c3;
            this.myMatrix3x3.myr3c3 = mat3x3.myMatrix3x3.myr3c3;
        }

        public void SetSingleElement(short row_index, short column_index, double inp_value)
        {
            switch (row_index)
            {
                case 1:
                    {
                        switch (column_index)
                        {
                            case 1:
                                {
                                    this.myMatrix3x3.myr1c1 = inp_value;
                                    break;
                                }
                            case 2:
                                {
                                    this.myMatrix3x3.myr1c2 = inp_value;
                                    break;
                                }
                            case 3:
                                {
                                    this.myMatrix3x3.myr1c3 = inp_value;
                                    break;
                                }
                            default:
                                {
                                    throw new System.ArgumentOutOfRangeException("column_index", "invalid column index " + column_index);
                                }
                        }
                        break;
                    }
                case 2:
                    {
                        switch (column_index)
                        {
                            case 1:
                                {
                                    this.myMatrix3x3.myr2c1 = inp_value;
                                    break;
                                }
                            case 2:
                                {
                                    this.myMatrix3x3.myr2c2 = inp_value;
                                    break;
                                }
                            case 3:
                                {
                                    this.myMatrix3x3.myr2c3 = inp_value;
                                    break;
                                }
                            default:
                                {
                                    throw new System.ArgumentOutOfRangeException("column_index", "invalid column index " + column_index);
                                }
                        }
                        break;
                    }
                case 3:
                    {
                        switch (column_index)
                        {
                            case 1:
                                {
                                    this.myMatrix3x3.myr3c1 = inp_value;
                                    break;
                                }
                            case 2:
                                {
                                    this.myMatrix3x3.myr3c2 = inp_value;
                                    break;
                                }
                            case 3:
                                {
                                    this.myMatrix3x3.myr3c3 = inp_value;
                                    break;
                                }
                            default:
                                {
                                    throw new System.ArgumentOutOfRangeException("column_index", "invalid column index " + column_index);
                                }
                        }
                        break;
                    }
                default:
                    {
                        throw new System.ArgumentOutOfRangeException("row_index", "invalid row index " + row_index);
                    }
            }
        }

        public double GetSingleElement(short row_index, short column_index)
        {
            switch (row_index)
            {
                case 1:
                    {
                        switch (column_index)
                        {
                            case 1:
                                {
                                    return this.myMatrix3x3.myr1c1;
                                }
                            case 2:
                                {
                                    return this.myMatrix3x3.myr1c2;
                                }
                            case 3:
                                {
                                    return this.myMatrix3x3.myr1c3;
                                }
                            default:
                                {
                                    throw new System.ArgumentOutOfRangeException("column_index", "invalid column index " + column_index);
                                }
                        }
                    }
                case 2:
                    {
                        switch (column_index)
                        {
                            case 1:
                                {
                                    return this.myMatrix3x3.myr2c1;
                                }
                            case 2:
                                {
                                    return this.myMatrix3x3.myr2c2;
                                }
                            case 3:
                                {
                                    return this.myMatrix3x3.myr2c3;
                                }
                            default:
                                {
                                    throw new System.ArgumentOutOfRangeException("column_index", "invalid column index " + column_index);
                                }
                        }
                    }
                case 3:
                    {
                        switch (column_index)
                        {
                            case 1:
                                {
                                    return this.myMatrix3x3.myr3c1;
                                }
                            case 2:
                                {
                                    return this.myMatrix3x3.myr3c2;
                                }
                            case 3:
                                {
                                    return this.myMatrix3x3.myr3c3;
                                }
                            default:
                                {
                                    throw new System.ArgumentOutOfRangeException("column_index", "invalid column index " + column_index);
                                }
                        }
                    }
                default:
                    {
                        throw new System.ArgumentOutOfRangeException("row_index", "invalid row index " + row_index);
                    }
            }
        }

        public double R1C1
        {
            get
            {
                return this.myMatrix3x3.myr1c1;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("R1C1", "invalid set r1c1 " + value);
                }
                this.myMatrix3x3.myr1c1 = value;
            }
        }
        public double R2C1
        {
            get
            {
                return this.myMatrix3x3.myr2c1;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("R2C1", "invalid set r2c1 " + value);
                }
                this.myMatrix3x3.myr2c1 = value;
            }
        }
        public double R3C1
        {
            get
            {
                return this.myMatrix3x3.myr3c1;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("R3C1", "invalid set r3c1 " + value);
                }
                this.myMatrix3x3.myr3c1 = value;
            }
        }

        public double R1C2
        {
            get
            {
                return this.myMatrix3x3.myr1c2;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("R1C2", "invalid set r1c2 " + value);
                }
                this.myMatrix3x3.myr1c2 = value;
            }
        }
        public double R2C2
        {
            get
            {
                return this.myMatrix3x3.myr2c2;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("R2C2", "invalid set r2c2 " + value);
                }
                this.myMatrix3x3.myr2c2 = value;
            }
        }
        public double R3C2
        {
            get
            {
                return this.myMatrix3x3.myr3c2;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("R3C2", "invalid set r3c2 " + value);
                }
                this.myMatrix3x3.myr3c2 = value;
            }
        }

        public double R1C3
        {
            get
            {
                return this.myMatrix3x3.myr1c3;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("R3C1", "invalid set r1c3 " + value);
                }
                this.myMatrix3x3.myr1c3 = value;
            }
        }
        public double R2C3
        {
            get
            {
                return this.myMatrix3x3.myr2c3;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("R2C3", "invalid set r2c3 " + value);
                }
                this.myMatrix3x3.myr2c3 = value;
            }
        }
        public double R3C3
        {
            get
            {
                return this.myMatrix3x3.myr3c3;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("R3C3", "invalid set r3c3 " + value);
                }
                this.myMatrix3x3.myr3c3 = value;
            }
        }

        public Vector3d GetColumnVector(int column_index)
        {
            switch (column_index)
            {
                case 1:
                    {
                        Vector3d columnVector = new Vector3d(
                            this.myMatrix3x3.myr1c1,  // x
                            this.myMatrix3x3.myr2c1,  // y
                            this.myMatrix3x3.myr3c1); // z
                        return columnVector;
                    }
                case 2:
                    {
                        Vector3d columnVector = new Vector3d(
                            this.myMatrix3x3.myr1c2,  // x
                            this.myMatrix3x3.myr2c2,  // y
                            this.myMatrix3x3.myr3c2); // z
                        return columnVector;
                    }
                case 3:
                    {
                        Vector3d columnVector = new Vector3d(
                            this.myMatrix3x3.myr1c3,  // x
                            this.myMatrix3x3.myr2c3,  // y
                            this.myMatrix3x3.myr3c3); // z
                        return columnVector;
                    }
                default:
                    {
                        throw new System.ArgumentOutOfRangeException("column_index", "illegal column index " + column_index);
                    }
            }
        }

        public void SetColumnVector(int column_index, Vector3d in_vector)
        {
            switch (column_index)
            {
                case 1:
                    {
                        this.myMatrix3x3.myr1c1 = in_vector.X;
                        this.myMatrix3x3.myr2c1 = in_vector.Y;
                        this.myMatrix3x3.myr3c1 = in_vector.Z;
                        break;
                    }
                case 2:
                    {
                        this.myMatrix3x3.myr1c2 = in_vector.X;
                        this.myMatrix3x3.myr2c2 = in_vector.Y;
                        this.myMatrix3x3.myr3c2 = in_vector.Z;
                        break;
                    }
                case 3:
                    {
                        this.myMatrix3x3.myr1c3 = in_vector.X;
                        this.myMatrix3x3.myr2c3 = in_vector.Y;
                        this.myMatrix3x3.myr3c3 = in_vector.Z;
                        break;
                    }
                default:
                    {
                        throw new System.ArgumentOutOfRangeException("column_index", "illegal column index " + column_index);
                    }
            }
        }

        public Vector3d GetRowVector(int row_index)
        {
            switch (row_index)
            {
                case 1:
                    {
                        Vector3d rowVector = new Vector3d(
                            this.myMatrix3x3.myr1c1,  // x
                            this.myMatrix3x3.myr1c2,  // y
                            this.myMatrix3x3.myr1c3); // z
                        return rowVector;
                    }
                case 2:
                    {
                        Vector3d rowVector = new Vector3d(
                            this.myMatrix3x3.myr2c1,  // x
                            this.myMatrix3x3.myr2c2,  // y
                            this.myMatrix3x3.myr2c3); // z
                        return rowVector;
                    }
                case 3:
                    {
                        Vector3d rowVector = new Vector3d(
                            this.myMatrix3x3.myr3c1,  // x
                            this.myMatrix3x3.myr3c2,  // y
                            this.myMatrix3x3.myr3c3); // z
                        return rowVector;
                    }
                default:
                    {
                        throw new System.ArgumentOutOfRangeException("row_index", "illegal row index " + row_index);
                    }
            }
        }

        public void SetRowVector(int row_index, Vector3d in_vector)
        {
            switch (row_index)
            {
                case 1:
                    {
                        this.myMatrix3x3.myr1c1 = in_vector.X;
                        this.myMatrix3x3.myr1c2 = in_vector.Y;
                        this.myMatrix3x3.myr1c3 = in_vector.Z;
                        break;
                    }
                case 2:
                    {
                        this.myMatrix3x3.myr2c1 = in_vector.X;
                        this.myMatrix3x3.myr2c2 = in_vector.Y;
                        this.myMatrix3x3.myr2c3 = in_vector.Z;
                        break;
                    }
                case 3:
                    {
                        this.myMatrix3x3.myr3c1 = in_vector.X;
                        this.myMatrix3x3.myr3c2 = in_vector.Y;
                        this.myMatrix3x3.myr3c3 = in_vector.Z;
                        break;
                    }
                default:
                    {
                        throw new System.ArgumentOutOfRangeException("row_index", "illegal row index " + row_index);
                    }
            }
        }

        public override string ToString()
        {
            return "[" + this.myMatrix3x3.myr1c1.ToString("E7") + " "
                       + this.myMatrix3x3.myr1c2.ToString("E7") + " "
                       + this.myMatrix3x3.myr1c3.ToString("E7") + "]" + Environment.NewLine +

                   "[" + this.myMatrix3x3.myr2c1.ToString("E7") + " "
                       + this.myMatrix3x3.myr2c2.ToString("E7") + " "
                       + this.myMatrix3x3.myr2c3.ToString("E7") + "]" + Environment.NewLine +

                   "[" + this.myMatrix3x3.myr3c1.ToString("E7") + " "
                       + this.myMatrix3x3.myr3c2.ToString("E7") + " "
                       + this.myMatrix3x3.myr3c3.ToString("E7") + "]";
        }

        public string ToString(string Format)
        {
            return "[" + this.myMatrix3x3.myr1c1.ToString(Format) + " "
                       + this.myMatrix3x3.myr1c2.ToString(Format) + " "
                       + this.myMatrix3x3.myr1c3.ToString(Format) + "]" + Environment.NewLine +

                   "[" + this.myMatrix3x3.myr2c1.ToString(Format) + " "
                       + this.myMatrix3x3.myr2c2.ToString(Format) + " "
                       + this.myMatrix3x3.myr2c3.ToString(Format) + "]" + Environment.NewLine +

                   "[" + this.myMatrix3x3.myr3c1.ToString(Format) + " "
                       + this.myMatrix3x3.myr3c2.ToString(Format) + " "
                       + this.myMatrix3x3.myr3c3.ToString(Format) + "]";
        }

        public string ToString(string Format, IFormatProvider FormatProvider)
        {
            return "[" + this.myMatrix3x3.myr1c1.ToString(Format, FormatProvider) + " "
                       + this.myMatrix3x3.myr1c2.ToString(Format, FormatProvider) + " "
                       + this.myMatrix3x3.myr1c3.ToString(Format, FormatProvider) + "]" + Environment.NewLine +

                   "[" + this.myMatrix3x3.myr2c1.ToString(Format, FormatProvider) + " "
                       + this.myMatrix3x3.myr2c2.ToString(Format, FormatProvider) + " "
                       + this.myMatrix3x3.myr2c3.ToString(Format, FormatProvider) + "]" + Environment.NewLine +

                   "[" + this.myMatrix3x3.myr3c1.ToString(Format, FormatProvider) + " "
                       + this.myMatrix3x3.myr3c2.ToString(Format, FormatProvider) + " "
                       + this.myMatrix3x3.myr3c3.ToString(Format, FormatProvider) + "]";
        }

        public void SetFromString(string matrix)
        {
            SetFromString(matrix, null);
        }

        public void SetFromString(string matrix, IFormatProvider FormatProvider)
        {
            // matrix string looks like:   [r1c1 r1c2 r1c3]\n[r2c1 r2c2 r2c3]\n[r3c1 r3c2 r3c3]
            int endPos = SetLineFromString(matrix, 0, 1, FormatProvider); // convert row 1
            endPos = SetLineFromString(matrix, endPos, 2, FormatProvider); // convert row 2
            endPos = SetLineFromString(matrix, endPos, 3, FormatProvider); // convert row 3
        }

        private int SetLineFromString(string row, int startPos, int row_number, IFormatProvider FormatProvider)
        {
            // string looks like:   [-1.0E+000 2.0E+000 3.0E+000]
            //                      01234567890123456789012345678
            int pos0 = row.IndexOf('[', startPos);          // e.g.  0
            int pos1 = row.IndexOf(' ', pos0 + 7);          // e.g. 10  ( 7 = 1.E+000 )
            int minlength = pos1 - pos0 - 1;                // length - 1 * <sign>
            int pos2 = row.IndexOf(' ', pos1 + minlength);  // e.g. 19
            int pos3 = row.IndexOf(']', pos2 + minlength);  // e.g. 28

            switch (row_number)
            {
                case 2:
                    {
                        if (null == FormatProvider)
                        {
                            this.myMatrix3x3.myr2c1 = Double.Parse(row.Substring(pos0 + 1, pos1 - pos0 - 1));
                            this.myMatrix3x3.myr2c2 = Double.Parse(row.Substring(pos1 + 1, pos2 - pos1 - 1));
                            this.myMatrix3x3.myr2c3 = Double.Parse(row.Substring(pos2 + 1, pos3 - pos2 - 1));
                        }
                        else
                        {
                            this.myMatrix3x3.myr2c1 = Double.Parse(row.Substring(pos0 + 1, pos1 - pos0 - 1), FormatProvider);
                            this.myMatrix3x3.myr2c2 = Double.Parse(row.Substring(pos1 + 1, pos2 - pos1 - 1), FormatProvider);
                            this.myMatrix3x3.myr2c3 = Double.Parse(row.Substring(pos2 + 1, pos3 - pos2 - 1), FormatProvider);
                        }
                        break;
                    }
                case 3:
                    {
                        if (null == FormatProvider)
                        {
                            this.myMatrix3x3.myr3c1 = Double.Parse(row.Substring(pos0 + 1, pos1 - pos0 - 1));
                            this.myMatrix3x3.myr3c2 = Double.Parse(row.Substring(pos1 + 1, pos2 - pos1 - 1));
                            this.myMatrix3x3.myr3c3 = Double.Parse(row.Substring(pos2 + 1, pos3 - pos2 - 1));
                        }
                        else
                        {
                            this.myMatrix3x3.myr3c1 = Double.Parse(row.Substring(pos0 + 1, pos1 - pos0 - 1), FormatProvider);
                            this.myMatrix3x3.myr3c2 = Double.Parse(row.Substring(pos1 + 1, pos2 - pos1 - 1), FormatProvider);
                            this.myMatrix3x3.myr3c3 = Double.Parse(row.Substring(pos2 + 1, pos3 - pos2 - 1), FormatProvider);
                        }
                        break;
                    }
                default:
                    {
                        if (null == FormatProvider)
                        {
                            this.myMatrix3x3.myr1c1 = Double.Parse(row.Substring(pos0 + 1, pos1 - pos0 - 1));
                            this.myMatrix3x3.myr1c2 = Double.Parse(row.Substring(pos1 + 1, pos2 - pos1 - 1));
                            this.myMatrix3x3.myr1c3 = Double.Parse(row.Substring(pos2 + 1, pos3 - pos2 - 1));
                        }
                        else
                        {
                            this.myMatrix3x3.myr1c1 = Double.Parse(row.Substring(pos0 + 1, pos1 - pos0 - 1), FormatProvider);
                            this.myMatrix3x3.myr1c2 = Double.Parse(row.Substring(pos1 + 1, pos2 - pos1 - 1), FormatProvider);
                            this.myMatrix3x3.myr1c3 = Double.Parse(row.Substring(pos2 + 1, pos3 - pos2 - 1), FormatProvider);
                        }
                        break;
                    }
            }

            return pos3; // return position of last ']'
        }

        public static Matrix3x3d Parse(string matrix)
        {
            Matrix3x3d result = new Matrix3x3d();
            result.SetFromString(matrix);
            return (result);
        }

        public static Matrix3x3d Parse(string matrix, IFormatProvider FormatProvider)
        {
            Matrix3x3d result = new Matrix3x3d();
            result.SetFromString(matrix, FormatProvider);
            return (result);
        }

        #endregion

        #region arithmetic Methods
        public void SetIdentityMatrix()
        {
            this.myMatrix3x3.myr1c1 = 1.0;
            this.myMatrix3x3.myr1c2 = 0.0;
            this.myMatrix3x3.myr1c3 = 0.0;

            this.myMatrix3x3.myr2c1 = 0.0;
            this.myMatrix3x3.myr2c2 = 1.0;
            this.myMatrix3x3.myr2c3 = 0.0;

            this.myMatrix3x3.myr3c1 = 0.0;
            this.myMatrix3x3.myr3c2 = 0.0;
            this.myMatrix3x3.myr3c3 = 1.0;
        }

        public static Matrix3x3d GetIdentityMatrix()
        {
            Matrix3x3d mat = new Matrix3x3d();
            mat.SetIdentityMatrix();
            return mat;
        }

        public double CalculateDeterminant()
        {
            return
                (
                  this.myMatrix3x3.myr1c1 * this.myMatrix3x3.myr2c2 * this.myMatrix3x3.myr3c3 -
                  this.myMatrix3x3.myr1c1 * this.myMatrix3x3.myr2c3 * this.myMatrix3x3.myr3c2 +

                  this.myMatrix3x3.myr1c2 * this.myMatrix3x3.myr2c3 * this.myMatrix3x3.myr3c1 -
                  this.myMatrix3x3.myr1c2 * this.myMatrix3x3.myr2c1 * this.myMatrix3x3.myr3c3 +

                  this.myMatrix3x3.myr1c3 * this.myMatrix3x3.myr2c1 * this.myMatrix3x3.myr3c2 -
                  this.myMatrix3x3.myr1c3 * this.myMatrix3x3.myr2c2 * this.myMatrix3x3.myr3c1
                );
        }

        public double SubDeterminant(short row_index, short column_index)
        {
            switch (row_index)
            {
                case 1:
                    {
                        switch (column_index)
                        {
                            case 1:
                                {
                                    return
                                        (
                                        this.myMatrix3x3.myr2c2 * this.myMatrix3x3.myr3c3 -
                                        this.myMatrix3x3.myr2c3 * this.myMatrix3x3.myr3c2
                                        );
                                }
                            case 2:
                                {
                                    return
                                        (
                                        this.myMatrix3x3.myr2c1 * this.myMatrix3x3.myr3c3 -
                                        this.myMatrix3x3.myr2c3 * this.myMatrix3x3.myr3c1
                                        );
                                }
                            case 3:
                                {
                                    return
                                        (
                                        this.myMatrix3x3.myr2c1 * this.myMatrix3x3.myr3c2 -
                                        this.myMatrix3x3.myr2c2 * this.myMatrix3x3.myr3c1
                                        );
                                }
                            default:
                                {
                                    throw new System.ArgumentOutOfRangeException("column_index", "illegal column index " + column_index);
                                }
                        }
                    }

                case 2:
                    {
                        switch (column_index)
                        {
                            case 1:
                                {
                                    return
                                        (
                                        this.myMatrix3x3.myr1c2 * this.myMatrix3x3.myr3c3 -
                                        this.myMatrix3x3.myr1c3 * this.myMatrix3x3.myr3c2
                                        );
                                }
                            case 2:
                                {
                                    return
                                        (
                                        this.myMatrix3x3.myr1c1 * this.myMatrix3x3.myr3c3 -
                                        this.myMatrix3x3.myr1c3 * this.myMatrix3x3.myr3c1
                                        );
                                }
                            case 3:
                                {
                                    return
                                        (
                                        this.myMatrix3x3.myr1c1 * this.myMatrix3x3.myr3c2 -
                                        this.myMatrix3x3.myr1c2 * this.myMatrix3x3.myr3c1
                                        );
                                }
                            default:
                                {
                                    throw new System.ArgumentOutOfRangeException("column_index", "illegal column index " + column_index);
                                }
                        }
                    }

                case 3:
                    {
                        switch (column_index)
                        {
                            case 1:
                                {
                                    return
                                        (
                                        this.myMatrix3x3.myr1c2 * this.myMatrix3x3.myr2c3 -
                                        this.myMatrix3x3.myr1c3 * this.myMatrix3x3.myr2c2
                                        );
                                }
                            case 2:
                                {
                                    return
                                        (
                                        this.myMatrix3x3.myr1c1 * this.myMatrix3x3.myr2c3 -
                                        this.myMatrix3x3.myr1c3 * this.myMatrix3x3.myr2c1
                                        );
                                }
                            case 3:
                                {
                                    return
                                        (
                                        this.myMatrix3x3.myr1c1 * this.myMatrix3x3.myr2c2 -
                                        this.myMatrix3x3.myr1c2 * this.myMatrix3x3.myr2c1
                                        );
                                }
                            default:
                                {
                                    throw new System.ArgumentOutOfRangeException("column_index", "illegal column index " + column_index);
                                }
                        }
                    }
                default:
                    {
                        throw new System.ArgumentOutOfRangeException("row_index", "illegal row index " + row_index);
                    }
            }
        }

        public void Inverse(Matrix3x3d in_matrix)
        {
            //calculateDeterminant
            double det =
                in_matrix.myMatrix3x3.myr1c1 * in_matrix.myMatrix3x3.myr2c2 * in_matrix.myMatrix3x3.myr3c3 -
                in_matrix.myMatrix3x3.myr1c1 * in_matrix.myMatrix3x3.myr2c3 * in_matrix.myMatrix3x3.myr3c2 +

                in_matrix.myMatrix3x3.myr1c2 * in_matrix.myMatrix3x3.myr2c3 * in_matrix.myMatrix3x3.myr3c1 -
                in_matrix.myMatrix3x3.myr1c2 * in_matrix.myMatrix3x3.myr2c1 * in_matrix.myMatrix3x3.myr3c3 +

                in_matrix.myMatrix3x3.myr1c3 * in_matrix.myMatrix3x3.myr2c1 * in_matrix.myMatrix3x3.myr3c2 -
                in_matrix.myMatrix3x3.myr1c3 * in_matrix.myMatrix3x3.myr2c2 * in_matrix.myMatrix3x3.myr3c1;

            // exception if matrix is singular
            if (det == 0.0)
            {
                throw new System.ArgumentOutOfRangeException("in_matrix", "The specified Matrix3x3 is singular. The value of the determinant is: " + det + ".");
            }
            else
            {
                this.myMatrix3x3.myr1c1 = (in_matrix.myMatrix3x3.myr2c2 * in_matrix.myMatrix3x3.myr3c3 - in_matrix.myMatrix3x3.myr3c2 * in_matrix.myMatrix3x3.myr2c3) / det; // subdet(1,1)
                this.myMatrix3x3.myr2c1 = -(in_matrix.myMatrix3x3.myr2c1 * in_matrix.myMatrix3x3.myr3c3 - in_matrix.myMatrix3x3.myr3c1 * in_matrix.myMatrix3x3.myr2c3) / det; // subdet(1,2)
                this.myMatrix3x3.myr3c1 = (in_matrix.myMatrix3x3.myr2c1 * in_matrix.myMatrix3x3.myr3c2 - in_matrix.myMatrix3x3.myr3c1 * in_matrix.myMatrix3x3.myr2c2) / det; // subdet(1,3)

                this.myMatrix3x3.myr1c2 = -(in_matrix.myMatrix3x3.myr1c2 * in_matrix.myMatrix3x3.myr3c3 - in_matrix.myMatrix3x3.myr3c2 * in_matrix.myMatrix3x3.myr1c3) / det; // subdet(2,1)
                this.myMatrix3x3.myr2c2 = (in_matrix.myMatrix3x3.myr1c1 * in_matrix.myMatrix3x3.myr3c3 - in_matrix.myMatrix3x3.myr3c1 * in_matrix.myMatrix3x3.myr1c3) / det; // subdet(2,2)
                this.myMatrix3x3.myr3c2 = -(in_matrix.myMatrix3x3.myr1c1 * in_matrix.myMatrix3x3.myr3c2 - in_matrix.myMatrix3x3.myr3c1 * in_matrix.myMatrix3x3.myr1c2) / det; // subdet(2,3)

                this.myMatrix3x3.myr1c3 = (in_matrix.myMatrix3x3.myr1c2 * in_matrix.myMatrix3x3.myr2c3 - in_matrix.myMatrix3x3.myr2c2 * in_matrix.myMatrix3x3.myr1c3) / det; // subdet(3,1)
                this.myMatrix3x3.myr2c3 = -(in_matrix.myMatrix3x3.myr1c1 * in_matrix.myMatrix3x3.myr2c3 - in_matrix.myMatrix3x3.myr2c1 * in_matrix.myMatrix3x3.myr1c3) / det; // subdet(3,2)
                this.myMatrix3x3.myr3c3 = (in_matrix.myMatrix3x3.myr1c1 * in_matrix.myMatrix3x3.myr2c2 - in_matrix.myMatrix3x3.myr2c1 * in_matrix.myMatrix3x3.myr1c2) / det; // subdet(3,3)
            }
        }

        public void Inverse()
        {
            Matrix3x3d matrix = new Matrix3x3d(this);
            this.Inverse(matrix);
        }

        public static Matrix3x3d GetInverseMatrix(Matrix3x3d mat_in)
        {
            Matrix3x3d mat = new Matrix3x3d();
            mat.Inverse(mat_in);
            return mat;
        }

        public void Transpose(Matrix3x3d mat_in)
        {
            this.myMatrix3x3.myr1c1 = mat_in.myMatrix3x3.myr1c1;
            this.myMatrix3x3.myr1c2 = mat_in.myMatrix3x3.myr2c1;
            this.myMatrix3x3.myr1c3 = mat_in.myMatrix3x3.myr3c1;

            this.myMatrix3x3.myr2c1 = mat_in.myMatrix3x3.myr1c2;
            this.myMatrix3x3.myr2c2 = mat_in.myMatrix3x3.myr2c2;
            this.myMatrix3x3.myr2c3 = mat_in.myMatrix3x3.myr3c2;

            this.myMatrix3x3.myr3c1 = mat_in.myMatrix3x3.myr1c3;
            this.myMatrix3x3.myr3c2 = mat_in.myMatrix3x3.myr2c3;
            this.myMatrix3x3.myr3c3 = mat_in.myMatrix3x3.myr3c3;
        }

        public void Transpose()
        {
            double temp;

            temp = this.myMatrix3x3.myr1c2;
            this.myMatrix3x3.myr1c2 = this.myMatrix3x3.myr2c1;
            this.myMatrix3x3.myr2c1 = temp;

            temp = this.myMatrix3x3.myr1c3;
            this.myMatrix3x3.myr1c3 = this.myMatrix3x3.myr3c1;
            this.myMatrix3x3.myr3c1 = temp;

            temp = this.myMatrix3x3.myr2c3;
            this.myMatrix3x3.myr2c3 = this.myMatrix3x3.myr3c2;
            this.myMatrix3x3.myr3c2 = temp;

            // r1c1, r2c2, r3c3 keep unchanged
        }

        public static Matrix3x3d GetTransposedMatrix(Matrix3x3d mat_in)
        {
            Matrix3x3d mat = new Matrix3x3d();
            mat.Transpose(mat_in);
            return mat;
        }

        public Vector3d MultiplyWithColumnVector(Vector3d column_vector)
        {

            Vector3d result_vector = new Vector3d
                (
                this.myMatrix3x3.myr1c1 * column_vector.X + // x 
                this.myMatrix3x3.myr1c2 * column_vector.Y +
                this.myMatrix3x3.myr1c3 * column_vector.Z,

                this.myMatrix3x3.myr2c1 * column_vector.X + // y
                this.myMatrix3x3.myr2c2 * column_vector.Y +
                this.myMatrix3x3.myr2c3 * column_vector.Z,

                this.myMatrix3x3.myr3c1 * column_vector.X + // z
                this.myMatrix3x3.myr3c2 * column_vector.Y +
                this.myMatrix3x3.myr3c3 * column_vector.Z
                );

            return result_vector;
        }

        public Vector3d MultiplyWithRowVector(Vector3d row_vector)
        {

            Vector3d result_vector = new Vector3d
                (
                this.myMatrix3x3.myr1c1 * row_vector.X + // x
                this.myMatrix3x3.myr2c1 * row_vector.Y +
                this.myMatrix3x3.myr3c1 * row_vector.Z,

                this.myMatrix3x3.myr1c2 * row_vector.X + // y
                this.myMatrix3x3.myr2c2 * row_vector.Y +
                this.myMatrix3x3.myr3c2 * row_vector.Z,

                this.myMatrix3x3.myr1c3 * row_vector.X + // z 
                this.myMatrix3x3.myr2c3 * row_vector.Y +
                this.myMatrix3x3.myr3c3 * row_vector.Z
                );

            return result_vector;
        }

        public void MultiplyWithMatrixOnRight(Matrix3x3d matrix_in)
        {

            double r1c1 = this.myMatrix3x3.myr1c1;
            double r1c2 = this.myMatrix3x3.myr1c2;
            //double r1c3 = this.myMatrix3x3.myr1c3;

            double r2c1 = this.myMatrix3x3.myr2c1;
            double r2c2 = this.myMatrix3x3.myr2c2;
            //double r2c3 = this.myMatrix3x3.myr2c3;

            double r3c1 = this.myMatrix3x3.myr3c1;
            double r3c2 = this.myMatrix3x3.myr3c2;
            //double r3c3 = this.myMatrix3x3.myr3c3;

            // calculate row 1
            this.myMatrix3x3.myr1c1 =

                                   r1c1 * matrix_in.myMatrix3x3.myr1c1 +
                this.myMatrix3x3.myr1c2 * matrix_in.myMatrix3x3.myr2c1 +
                this.myMatrix3x3.myr1c3 * matrix_in.myMatrix3x3.myr3c1;

            this.myMatrix3x3.myr1c2 =

                                   r1c1 * matrix_in.myMatrix3x3.myr1c2 +
                                   r1c2 * matrix_in.myMatrix3x3.myr2c2 +
                this.myMatrix3x3.myr1c3 * matrix_in.myMatrix3x3.myr3c2;

            this.myMatrix3x3.myr1c3 =

                                   r1c1 * matrix_in.myMatrix3x3.myr1c3 +
                                   r1c2 * matrix_in.myMatrix3x3.myr2c3 +
                this.myMatrix3x3.myr1c3 * matrix_in.myMatrix3x3.myr3c3;

            // calculate row 2
            this.myMatrix3x3.myr2c1 =

                                   r2c1 * matrix_in.myMatrix3x3.myr1c1 +
                this.myMatrix3x3.myr2c2 * matrix_in.myMatrix3x3.myr2c1 +
                this.myMatrix3x3.myr2c3 * matrix_in.myMatrix3x3.myr3c1;

            this.myMatrix3x3.myr2c2 =

                                   r2c1 * matrix_in.myMatrix3x3.myr1c2 +
                                   r2c2 * matrix_in.myMatrix3x3.myr2c2 +
                this.myMatrix3x3.myr2c3 * matrix_in.myMatrix3x3.myr3c2;

            this.myMatrix3x3.myr2c3 =

                                   r2c1 * matrix_in.myMatrix3x3.myr1c3 +
                                   r2c2 * matrix_in.myMatrix3x3.myr2c3 +
                this.myMatrix3x3.myr2c3 * matrix_in.myMatrix3x3.myr3c3;

            // calculate row 3
            this.myMatrix3x3.myr3c1 =

                                   r3c1 * matrix_in.myMatrix3x3.myr1c1 +
                this.myMatrix3x3.myr3c2 * matrix_in.myMatrix3x3.myr2c1 +
                this.myMatrix3x3.myr3c3 * matrix_in.myMatrix3x3.myr3c1;

            this.myMatrix3x3.myr3c2 =

                                   r3c1 * matrix_in.myMatrix3x3.myr1c2 +
                                   r3c2 * matrix_in.myMatrix3x3.myr2c2 +
                this.myMatrix3x3.myr3c3 * matrix_in.myMatrix3x3.myr3c2;

            this.myMatrix3x3.myr3c3 =

                                   r3c1 * matrix_in.myMatrix3x3.myr1c3 +
                                   r3c2 * matrix_in.myMatrix3x3.myr2c3 +
                this.myMatrix3x3.myr3c3 * matrix_in.myMatrix3x3.myr3c3;


        }


        public void MultiplyWithMatrixOnLeft(Matrix3x3d matrix_in)
        {
            double r1c1 = this.myMatrix3x3.myr1c1;
            double r1c2 = this.myMatrix3x3.myr1c2;
            double r1c3 = this.myMatrix3x3.myr1c3;

            double r2c1 = this.myMatrix3x3.myr2c1;
            double r2c2 = this.myMatrix3x3.myr2c2;
            double r2c3 = this.myMatrix3x3.myr2c3;

            //double r3c1 = this.myMatrix3x3.myr3c1;
            //double r3c2 = this.myMatrix3x3.myr3c2;
            //double r3c3 = this.myMatrix3x3.myr3c3;

            // calculate row 1
            this.myMatrix3x3.myr1c1 =

                matrix_in.myMatrix3x3.myr1c1 * r1c1 +
                matrix_in.myMatrix3x3.myr1c2 * r2c1 +
                matrix_in.myMatrix3x3.myr1c3 * this.myMatrix3x3.myr3c1;

            this.myMatrix3x3.myr1c2 =

                matrix_in.myMatrix3x3.myr1c1 * r1c2 +
                matrix_in.myMatrix3x3.myr1c2 * r2c2 +
                matrix_in.myMatrix3x3.myr1c3 * this.myMatrix3x3.myr3c2;

            this.myMatrix3x3.myr1c3 =

                matrix_in.myMatrix3x3.myr1c1 * r1c3 +
                matrix_in.myMatrix3x3.myr1c2 * r2c3 +
                matrix_in.myMatrix3x3.myr1c3 * this.myMatrix3x3.myr3c3;

            // calculate row 2
            this.myMatrix3x3.myr2c1 =

                matrix_in.myMatrix3x3.myr2c1 * r1c1 +
                matrix_in.myMatrix3x3.myr2c2 * r2c1 +
                matrix_in.myMatrix3x3.myr2c3 * this.myMatrix3x3.myr3c1;

            this.myMatrix3x3.myr2c2 =

                matrix_in.myMatrix3x3.myr2c1 * r1c2 +
                matrix_in.myMatrix3x3.myr2c2 * r2c2 +
                matrix_in.myMatrix3x3.myr2c3 * this.myMatrix3x3.myr3c2;

            this.myMatrix3x3.myr2c3 =

                matrix_in.myMatrix3x3.myr2c1 * r1c3 +
                matrix_in.myMatrix3x3.myr2c2 * r2c3 +
                matrix_in.myMatrix3x3.myr2c3 * this.myMatrix3x3.myr3c3;

            // calculate row 3
            this.myMatrix3x3.myr3c1 =

                matrix_in.myMatrix3x3.myr3c1 * r1c1 +
                matrix_in.myMatrix3x3.myr3c2 * r2c1 +
                matrix_in.myMatrix3x3.myr3c3 * this.myMatrix3x3.myr3c1;

            this.myMatrix3x3.myr3c2 =

                matrix_in.myMatrix3x3.myr3c1 * r1c2 +
                matrix_in.myMatrix3x3.myr3c2 * r2c2 +
                matrix_in.myMatrix3x3.myr3c3 * this.myMatrix3x3.myr3c2;

            this.myMatrix3x3.myr3c3 =

                matrix_in.myMatrix3x3.myr3c1 * r1c3 +
                matrix_in.myMatrix3x3.myr3c2 * r2c3 +
                matrix_in.myMatrix3x3.myr3c3 * this.myMatrix3x3.myr3c3;
        }


        public bool IsAlmostEqual(Matrix3x3d matrix_in, double tolerance)
        {
            if (Math.Abs(this.myMatrix3x3.myr1c1 - matrix_in.myMatrix3x3.myr1c1) > tolerance) return false;
            if (Math.Abs(this.myMatrix3x3.myr1c2 - matrix_in.myMatrix3x3.myr1c2) > tolerance) return false;
            if (Math.Abs(this.myMatrix3x3.myr1c3 - matrix_in.myMatrix3x3.myr1c3) > tolerance) return false;

            if (Math.Abs(this.myMatrix3x3.myr2c1 - matrix_in.myMatrix3x3.myr2c1) > tolerance) return false;
            if (Math.Abs(this.myMatrix3x3.myr2c2 - matrix_in.myMatrix3x3.myr2c2) > tolerance) return false;
            if (Math.Abs(this.myMatrix3x3.myr2c3 - matrix_in.myMatrix3x3.myr2c3) > tolerance) return false;

            if (Math.Abs(this.myMatrix3x3.myr3c1 - matrix_in.myMatrix3x3.myr3c1) > tolerance) return false;
            if (Math.Abs(this.myMatrix3x3.myr3c2 - matrix_in.myMatrix3x3.myr3c2) > tolerance) return false;
            if (Math.Abs(this.myMatrix3x3.myr3c3 - matrix_in.myMatrix3x3.myr3c3) > tolerance) return false;

            return true;
        }

        public bool IsAffine(double tolerance)
        {
            if ((Math.Abs(this.myMatrix3x3.myr3c1 - 0.0) > tolerance) ||
                (Math.Abs(this.myMatrix3x3.myr3c2 - 0.0) > tolerance) ||
                (Math.Abs(this.myMatrix3x3.myr3c3 - 1.0) > tolerance))
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

        public StructMatrix3x3d myMatrix3x3;

        #endregion
    }

    #region struct Matrix3x3

    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    public struct StructMatrix3x3d
    {
        public double myr1c1;  // row 1 column 1
        public double myr1c2;  // row 1 column 2
        public double myr1c3;  // row 1 column 3

        public double myr2c1;  // row 2 column 1
        public double myr2c2;  // row 2 column 2
        public double myr2c3;  // row 2 column 3

        public double myr3c1;  // row 3 column 1
        public double myr3c2;  // row 3 column 2
        public double myr3c3;  // row 3 column 3

        public double r1c1
        {
            get
            {
                return this.myr1c1;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("r1c1", "invalid set r1c1 " + value);
                }
                this.myr1c1 = value;
            }
        }
        public double r1c2
        {
            get
            {
                return this.myr1c2;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("r1c2", "invalid set r1c2 " + value);
                }
                this.myr1c2 = value;
            }
        }
        public double r1c3
        {
            get
            {
                return this.myr1c3;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("r1c3", "invalid set r1c3 " + value);
                }
                this.myr1c3 = value;
            }
        }

        public double r2c1
        {
            get
            {
                return this.myr2c1;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("r2c1", "invalid set r2c1 " + value);
                }
                this.myr2c1 = value;
            }
        }
        public double r2c2
        {
            get
            {
                return this.myr2c2;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("r2c2", "invalid set r2c2 " + value);
                }
                this.myr2c2 = value;
            }
        }
        public double r2c3
        {
            get
            {
                return this.myr2c3;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("r2c3", "invalid set r2c3 " + value);
                }
                this.myr2c3 = value;
            }
        }

        public double r3c1
        {
            get
            {
                return this.myr3c1;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("r3c1", "invalid set r3c1 " + value);
                }
                this.myr3c1 = value;
            }
        }
        public double r3c2
        {
            get
            {
                return this.myr3c2;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("r3c2", "invalid set r3c2 " + value);
                }
                this.myr3c2 = value;
            }
        }
        public double r3c3
        {
            get
            {
                return this.myr3c3;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("r3c3", "invalid set r3c3 " + value);
                }
                this.myr3c3 = value;
            }
        }
    }
    #endregion
}