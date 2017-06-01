#region File Header
/*[ Compilation unit ----------------------------------------------------------
    Component       : ubta
    Name            : Matrix4x4.cs
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
    public class Matrix4x4d
    {
        #region Constructors

        public Matrix4x4d()
        {
            this.myMatrix4x4.myr1c1 = 0.0;
            this.myMatrix4x4.myr1c2 = 0.0;
            this.myMatrix4x4.myr1c3 = 0.0;
            this.myMatrix4x4.myr1c4 = 0.0;

            this.myMatrix4x4.myr2c1 = 0.0;
            this.myMatrix4x4.myr2c2 = 0.0;
            this.myMatrix4x4.myr2c3 = 0.0;
            this.myMatrix4x4.myr2c4 = 0.0;

            this.myMatrix4x4.myr3c1 = 0.0;
            this.myMatrix4x4.myr3c2 = 0.0;
            this.myMatrix4x4.myr3c3 = 0.0;
            this.myMatrix4x4.myr3c4 = 0.0;

            this.myMatrix4x4.myr4c1 = 0.0;
            this.myMatrix4x4.myr4c2 = 0.0;
            this.myMatrix4x4.myr4c3 = 0.0;
            this.myMatrix4x4.myr4c4 = 0.0;
        }

        public Matrix4x4d(Matrix4x4d mat4x4)
        {
            this.myMatrix4x4.myr1c1 = mat4x4.myMatrix4x4.myr1c1;
            this.myMatrix4x4.myr1c2 = mat4x4.myMatrix4x4.myr1c2;
            this.myMatrix4x4.myr1c3 = mat4x4.myMatrix4x4.myr1c3;
            this.myMatrix4x4.myr1c4 = mat4x4.myMatrix4x4.myr1c4;

            this.myMatrix4x4.myr2c1 = mat4x4.myMatrix4x4.myr2c1;
            this.myMatrix4x4.myr2c2 = mat4x4.myMatrix4x4.myr2c2;
            this.myMatrix4x4.myr2c3 = mat4x4.myMatrix4x4.myr2c3;
            this.myMatrix4x4.myr2c4 = mat4x4.myMatrix4x4.myr2c4;

            this.myMatrix4x4.myr3c1 = mat4x4.myMatrix4x4.myr3c1;
            this.myMatrix4x4.myr3c2 = mat4x4.myMatrix4x4.myr3c2;
            this.myMatrix4x4.myr3c3 = mat4x4.myMatrix4x4.myr3c3;
            this.myMatrix4x4.myr3c4 = mat4x4.myMatrix4x4.myr3c4;

            this.myMatrix4x4.myr4c1 = mat4x4.myMatrix4x4.myr4c1;
            this.myMatrix4x4.myr4c2 = mat4x4.myMatrix4x4.myr4c2;
            this.myMatrix4x4.myr4c3 = mat4x4.myMatrix4x4.myr4c3;
            this.myMatrix4x4.myr4c4 = mat4x4.myMatrix4x4.myr4c4;
        }

        public Matrix4x4d(
            double r1c1, double r1c2, double r1c3, double r1c4,
            double r2c1, double r2c2, double r2c3, double r2c4,
            double r3c1, double r3c2, double r3c3, double r3c4,
            double r4c1, double r4c2, double r4c3, double r4c4)
        {
            this.myMatrix4x4.myr1c1 = r1c1;
            this.myMatrix4x4.myr1c2 = r1c2;
            this.myMatrix4x4.myr1c3 = r1c3;
            this.myMatrix4x4.myr1c4 = r1c4;

            this.myMatrix4x4.myr2c1 = r2c1;
            this.myMatrix4x4.myr2c2 = r2c2;
            this.myMatrix4x4.myr2c3 = r2c3;
            this.myMatrix4x4.myr2c4 = r2c4;

            this.myMatrix4x4.myr3c1 = r3c1;
            this.myMatrix4x4.myr3c2 = r3c2;
            this.myMatrix4x4.myr3c3 = r3c3;
            this.myMatrix4x4.myr3c4 = r3c4;

            this.myMatrix4x4.myr4c1 = r4c1;
            this.myMatrix4x4.myr4c2 = r4c2;
            this.myMatrix4x4.myr4c3 = r4c3;
            this.myMatrix4x4.myr4c4 = r4c4;
        }

        public Matrix4x4d(Matrix3x3d mat3x3, Vector3d vec3d)
        {
            this.myMatrix4x4.myr1c1 = mat3x3.myMatrix3x3.myr1c1;
            this.myMatrix4x4.myr1c2 = mat3x3.myMatrix3x3.myr1c2;
            this.myMatrix4x4.myr1c3 = mat3x3.myMatrix3x3.myr1c3;

            this.myMatrix4x4.myr1c4 = vec3d.X;

            this.myMatrix4x4.myr2c1 = mat3x3.myMatrix3x3.myr2c1;
            this.myMatrix4x4.myr2c2 = mat3x3.myMatrix3x3.myr2c2;
            this.myMatrix4x4.myr2c3 = mat3x3.myMatrix3x3.myr2c3;

            this.myMatrix4x4.myr2c4 = vec3d.Y;

            this.myMatrix4x4.myr3c1 = mat3x3.myMatrix3x3.myr3c1;
            this.myMatrix4x4.myr3c2 = mat3x3.myMatrix3x3.myr3c2;
            this.myMatrix4x4.myr3c3 = mat3x3.myMatrix3x3.myr3c3;

            this.myMatrix4x4.myr3c4 = vec3d.Z;

            this.myMatrix4x4.myr4c1 = 0.0;
            this.myMatrix4x4.myr4c2 = 0.0;
            this.myMatrix4x4.myr4c3 = 0.0;
            this.myMatrix4x4.myr4c4 = 1.0;
        }
        #endregion

        #region Operators

        public static Matrix4x4d operator +(Matrix4x4d matrix1, Matrix4x4d matrix2)
        {

            Matrix4x4d sum = new Matrix4x4d
                (
                matrix1.myMatrix4x4.myr1c1 + matrix2.myMatrix4x4.myr1c1, // r1c1
                matrix1.myMatrix4x4.myr1c2 + matrix2.myMatrix4x4.myr1c2, // r1c2
                matrix1.myMatrix4x4.myr1c3 + matrix2.myMatrix4x4.myr1c3, // r1c3
                matrix1.myMatrix4x4.myr1c4 + matrix2.myMatrix4x4.myr1c4, // r1c4

                matrix1.myMatrix4x4.myr2c1 + matrix2.myMatrix4x4.myr2c1, // r2c1   
                matrix1.myMatrix4x4.myr2c2 + matrix2.myMatrix4x4.myr2c2, // r2c2
                matrix1.myMatrix4x4.myr2c3 + matrix2.myMatrix4x4.myr2c3, // r2c3
                matrix1.myMatrix4x4.myr2c4 + matrix2.myMatrix4x4.myr2c4, // r2c4

                matrix1.myMatrix4x4.myr3c1 + matrix2.myMatrix4x4.myr3c1, // r3c1
                matrix1.myMatrix4x4.myr3c2 + matrix2.myMatrix4x4.myr3c2, // r3c2
                matrix1.myMatrix4x4.myr3c3 + matrix2.myMatrix4x4.myr3c3, // r3c3
                matrix1.myMatrix4x4.myr3c4 + matrix2.myMatrix4x4.myr3c4, // r3c4 

                matrix1.myMatrix4x4.myr4c1 + matrix2.myMatrix4x4.myr4c1, // r4c1
                matrix1.myMatrix4x4.myr4c2 + matrix2.myMatrix4x4.myr4c2, // r4c2
                matrix1.myMatrix4x4.myr4c3 + matrix2.myMatrix4x4.myr4c3, // r4c3
                matrix1.myMatrix4x4.myr4c4 + matrix2.myMatrix4x4.myr4c4   // r4c4  
                );

            return (sum);
        }

        public static Matrix4x4d Add(Matrix4x4d matrix1, Matrix4x4d matrix2)
        {

            Matrix4x4d sum = new Matrix4x4d
                (
                matrix1.myMatrix4x4.myr1c1 + matrix2.myMatrix4x4.myr1c1, // r1c1
                matrix1.myMatrix4x4.myr1c2 + matrix2.myMatrix4x4.myr1c2, // r1c2
                matrix1.myMatrix4x4.myr1c3 + matrix2.myMatrix4x4.myr1c3, // r1c3
                matrix1.myMatrix4x4.myr1c4 + matrix2.myMatrix4x4.myr1c4, // r1c4

                matrix1.myMatrix4x4.myr2c1 + matrix2.myMatrix4x4.myr2c1, // r2c1   
                matrix1.myMatrix4x4.myr2c2 + matrix2.myMatrix4x4.myr2c2, // r2c2
                matrix1.myMatrix4x4.myr2c3 + matrix2.myMatrix4x4.myr2c3, // r2c3
                matrix1.myMatrix4x4.myr2c4 + matrix2.myMatrix4x4.myr2c4, // r2c4

                matrix1.myMatrix4x4.myr3c1 + matrix2.myMatrix4x4.myr3c1, // r3c1
                matrix1.myMatrix4x4.myr3c2 + matrix2.myMatrix4x4.myr3c2, // r3c2
                matrix1.myMatrix4x4.myr3c3 + matrix2.myMatrix4x4.myr3c3, // r3c3
                matrix1.myMatrix4x4.myr3c4 + matrix2.myMatrix4x4.myr3c4, // r3c4 

                matrix1.myMatrix4x4.myr4c1 + matrix2.myMatrix4x4.myr4c1, // r4c1
                matrix1.myMatrix4x4.myr4c2 + matrix2.myMatrix4x4.myr4c2, // r4c2
                matrix1.myMatrix4x4.myr4c3 + matrix2.myMatrix4x4.myr4c3, // r4c3
                matrix1.myMatrix4x4.myr4c4 + matrix2.myMatrix4x4.myr4c4   // r4c4  
                );

            return (sum);
        }

        public static Matrix4x4d operator +(Matrix4x4d matrix, double scalar)
        {
            Matrix4x4d sum = new Matrix4x4d
                (
                matrix.myMatrix4x4.myr1c1 + scalar, // r1c1
                matrix.myMatrix4x4.myr1c2 + scalar, // r1c2
                matrix.myMatrix4x4.myr1c3 + scalar, // r1c3
                matrix.myMatrix4x4.myr1c4 + scalar, // r1c4

                matrix.myMatrix4x4.myr2c1 + scalar, // r2c1   
                matrix.myMatrix4x4.myr2c2 + scalar, // r2c2
                matrix.myMatrix4x4.myr2c3 + scalar, // r2c3
                matrix.myMatrix4x4.myr2c4 + scalar, // r2c4

                matrix.myMatrix4x4.myr3c1 + scalar, // r3c1
                matrix.myMatrix4x4.myr3c2 + scalar, // r3c2
                matrix.myMatrix4x4.myr3c3 + scalar, // r3c3  
                matrix.myMatrix4x4.myr3c4 + scalar, // r2c4

                matrix.myMatrix4x4.myr4c1 + scalar, // r4c1
                matrix.myMatrix4x4.myr4c2 + scalar, // r4c2
                matrix.myMatrix4x4.myr4c3 + scalar, // r4c3  
                matrix.myMatrix4x4.myr4c4 + scalar   // r4c4
                );

            return (sum);
        }

        public static Matrix4x4d Add(Matrix4x4d matrix, double scalar)
        {
            Matrix4x4d sum = new Matrix4x4d
                (
                matrix.myMatrix4x4.myr1c1 + scalar, // r1c1
                matrix.myMatrix4x4.myr1c2 + scalar, // r1c2
                matrix.myMatrix4x4.myr1c3 + scalar, // r1c3
                matrix.myMatrix4x4.myr1c4 + scalar, // r1c4

                matrix.myMatrix4x4.myr2c1 + scalar, // r2c1   
                matrix.myMatrix4x4.myr2c2 + scalar, // r2c2
                matrix.myMatrix4x4.myr2c3 + scalar, // r2c3
                matrix.myMatrix4x4.myr2c4 + scalar, // r2c4

                matrix.myMatrix4x4.myr3c1 + scalar, // r3c1
                matrix.myMatrix4x4.myr3c2 + scalar, // r3c2
                matrix.myMatrix4x4.myr3c3 + scalar, // r3c3  
                matrix.myMatrix4x4.myr3c4 + scalar, // r2c4

                matrix.myMatrix4x4.myr4c1 + scalar, // r4c1
                matrix.myMatrix4x4.myr4c2 + scalar, // r4c2
                matrix.myMatrix4x4.myr4c3 + scalar, // r4c3  
                matrix.myMatrix4x4.myr4c4 + scalar   // r4c4
                );

            return (sum);
        }

        public static Matrix4x4d operator -(Matrix4x4d matrix, double scalar)
        {
            Matrix4x4d sum = new Matrix4x4d
                (
                matrix.myMatrix4x4.myr1c1 - scalar, // r1c1
                matrix.myMatrix4x4.myr1c2 - scalar, // r1c2
                matrix.myMatrix4x4.myr1c3 - scalar, // r1c3
                matrix.myMatrix4x4.myr1c4 - scalar, // r1c4

                matrix.myMatrix4x4.myr2c1 - scalar, // r2c1   
                matrix.myMatrix4x4.myr2c2 - scalar, // r2c2
                matrix.myMatrix4x4.myr2c3 - scalar, // r2c3
                matrix.myMatrix4x4.myr2c4 - scalar, // r2c4

                matrix.myMatrix4x4.myr3c1 - scalar, // r3c1
                matrix.myMatrix4x4.myr3c2 - scalar, // r3c2
                matrix.myMatrix4x4.myr3c3 - scalar, // r3c3  
                matrix.myMatrix4x4.myr3c4 - scalar, // r3c4

                matrix.myMatrix4x4.myr4c1 - scalar, // r4c1
                matrix.myMatrix4x4.myr4c2 - scalar, // r4c2
                matrix.myMatrix4x4.myr4c3 - scalar, // r4c3  
                matrix.myMatrix4x4.myr4c4 - scalar   // r4c4
                );

            return (sum);
        }

        public static Matrix4x4d Subtract(Matrix4x4d matrix, double scalar)
        {
            Matrix4x4d sum = new Matrix4x4d
                (
                matrix.myMatrix4x4.myr1c1 - scalar, // r1c1
                matrix.myMatrix4x4.myr1c2 - scalar, // r1c2
                matrix.myMatrix4x4.myr1c3 - scalar, // r1c3
                matrix.myMatrix4x4.myr1c4 - scalar, // r1c4

                matrix.myMatrix4x4.myr2c1 - scalar, // r2c1   
                matrix.myMatrix4x4.myr2c2 - scalar, // r2c2
                matrix.myMatrix4x4.myr2c3 - scalar, // r2c3
                matrix.myMatrix4x4.myr2c4 - scalar, // r2c4

                matrix.myMatrix4x4.myr3c1 - scalar, // r3c1
                matrix.myMatrix4x4.myr3c2 - scalar, // r3c2
                matrix.myMatrix4x4.myr3c3 - scalar, // r3c3  
                matrix.myMatrix4x4.myr3c4 - scalar, // r3c4

                matrix.myMatrix4x4.myr4c1 - scalar, // r4c1
                matrix.myMatrix4x4.myr4c2 - scalar, // r4c2
                matrix.myMatrix4x4.myr4c3 - scalar, // r4c3  
                matrix.myMatrix4x4.myr4c4 - scalar   // r4c4
                );

            return (sum);
        }

        public static Matrix4x4d operator -(Matrix4x4d matrix)
        {
            Matrix4x4d neg = new Matrix4x4d
                (
                -matrix.myMatrix4x4.myr1c1, // r1c1
                -matrix.myMatrix4x4.myr1c2, // r1c2
                -matrix.myMatrix4x4.myr1c3, // r1c3
                -matrix.myMatrix4x4.myr1c4, // r1c4

                -matrix.myMatrix4x4.myr2c1, // r2c1   
                -matrix.myMatrix4x4.myr2c2, // r2c2
                -matrix.myMatrix4x4.myr2c3, // r2c3
                -matrix.myMatrix4x4.myr2c4, // r2c4

                -matrix.myMatrix4x4.myr3c1, // r3c1
                -matrix.myMatrix4x4.myr3c2, // r3c2
                -matrix.myMatrix4x4.myr3c3, // r3c3  
                -matrix.myMatrix4x4.myr3c4, // r3c4

                -matrix.myMatrix4x4.myr4c1, // r4c1
                -matrix.myMatrix4x4.myr4c2, // r4c2
                -matrix.myMatrix4x4.myr4c3, // r4c3  
                -matrix.myMatrix4x4.myr4c4   // r4c4
                );

            return (neg);
        }

        public static Matrix4x4d Negate(Matrix4x4d matrix)
        {
            Matrix4x4d neg = new Matrix4x4d
                (
                -matrix.myMatrix4x4.myr1c1, // r1c1
                -matrix.myMatrix4x4.myr1c2, // r1c2
                -matrix.myMatrix4x4.myr1c3, // r1c3
                -matrix.myMatrix4x4.myr1c4, // r1c4

                -matrix.myMatrix4x4.myr2c1, // r2c1   
                -matrix.myMatrix4x4.myr2c2, // r2c2
                -matrix.myMatrix4x4.myr2c3, // r2c3
                -matrix.myMatrix4x4.myr2c4, // r2c4

                -matrix.myMatrix4x4.myr3c1, // r3c1
                -matrix.myMatrix4x4.myr3c2, // r3c2
                -matrix.myMatrix4x4.myr3c3, // r3c3  
                -matrix.myMatrix4x4.myr3c4, // r3c4

                -matrix.myMatrix4x4.myr4c1, // r4c1
                -matrix.myMatrix4x4.myr4c2, // r4c2
                -matrix.myMatrix4x4.myr4c3, // r4c3  
                -matrix.myMatrix4x4.myr4c4   // r4c4
                );

            return (neg);
        }

        public static Matrix4x4d operator *(Matrix4x4d matrix, double scalar)
        {
            Matrix4x4d result = new Matrix4x4d
                (
                matrix.myMatrix4x4.myr1c1 * scalar, // r1c1
                matrix.myMatrix4x4.myr1c2 * scalar, // r1c2
                matrix.myMatrix4x4.myr1c3 * scalar, // r1c3
                matrix.myMatrix4x4.myr1c4 * scalar, // r1c4

                matrix.myMatrix4x4.myr2c1 * scalar, // r2c1   
                matrix.myMatrix4x4.myr2c2 * scalar, // r2c2
                matrix.myMatrix4x4.myr2c3 * scalar, // r2c3
                matrix.myMatrix4x4.myr2c4 * scalar, // r2c4

                matrix.myMatrix4x4.myr3c1 * scalar, // r3c1
                matrix.myMatrix4x4.myr3c2 * scalar, // r3c2
                matrix.myMatrix4x4.myr3c3 * scalar, // r3c3
                matrix.myMatrix4x4.myr3c4 * scalar, // r3c4

                matrix.myMatrix4x4.myr4c1 * scalar, // r4c1
                matrix.myMatrix4x4.myr4c2 * scalar, // r4c2
                matrix.myMatrix4x4.myr4c3 * scalar, // r4c3
                matrix.myMatrix4x4.myr4c4 * scalar   // r4c4
                );

            return (result);
        }

        public static Matrix4x4d Multiply(Matrix4x4d matrix, double scalar)
        {
            Matrix4x4d result = new Matrix4x4d
                (
                matrix.myMatrix4x4.myr1c1 * scalar, // r1c1
                matrix.myMatrix4x4.myr1c2 * scalar, // r1c2
                matrix.myMatrix4x4.myr1c3 * scalar, // r1c3
                matrix.myMatrix4x4.myr1c4 * scalar, // r1c4

                matrix.myMatrix4x4.myr2c1 * scalar, // r2c1   
                matrix.myMatrix4x4.myr2c2 * scalar, // r2c2
                matrix.myMatrix4x4.myr2c3 * scalar, // r2c3
                matrix.myMatrix4x4.myr2c4 * scalar, // r2c4

                matrix.myMatrix4x4.myr3c1 * scalar, // r3c1
                matrix.myMatrix4x4.myr3c2 * scalar, // r3c2
                matrix.myMatrix4x4.myr3c3 * scalar, // r3c3
                matrix.myMatrix4x4.myr3c4 * scalar, // r3c4

                matrix.myMatrix4x4.myr4c1 * scalar, // r4c1
                matrix.myMatrix4x4.myr4c2 * scalar, // r4c2
                matrix.myMatrix4x4.myr4c3 * scalar, // r4c3
                matrix.myMatrix4x4.myr4c4 * scalar   // r4c4
                );

            return (result);
        }

        public static Matrix4x4d operator *(double scalar, Matrix4x4d matrix)
        {
            Matrix4x4d result = new Matrix4x4d
                (
                matrix.myMatrix4x4.myr1c1 * scalar, // r1c1
                matrix.myMatrix4x4.myr1c2 * scalar, // r1c2
                matrix.myMatrix4x4.myr1c3 * scalar, // r1c3
                matrix.myMatrix4x4.myr1c4 * scalar, // r1c4

                matrix.myMatrix4x4.myr2c1 * scalar, // r2c1   
                matrix.myMatrix4x4.myr2c2 * scalar, // r2c2
                matrix.myMatrix4x4.myr2c3 * scalar, // r2c3
                matrix.myMatrix4x4.myr2c4 * scalar, // r2c4

                matrix.myMatrix4x4.myr3c1 * scalar, // r3c1
                matrix.myMatrix4x4.myr3c2 * scalar, // r3c2
                matrix.myMatrix4x4.myr3c3 * scalar, // r3c3
                matrix.myMatrix4x4.myr3c4 * scalar, // r3c4

                matrix.myMatrix4x4.myr4c1 * scalar, // r4c1
                matrix.myMatrix4x4.myr4c2 * scalar, // r4c2
                matrix.myMatrix4x4.myr4c3 * scalar, // r4c3
                matrix.myMatrix4x4.myr4c4 * scalar   // r4c4
                );

            return (result);
        }

        public static Matrix4x4d operator *(Matrix4x4d matrix1, Matrix4x4d matrix2)
        {
            Matrix4x4d result = new Matrix4x4d
                (
                matrix1.myMatrix4x4.myr1c1 * matrix2.myMatrix4x4.myr1c1 + // r1c1 
                matrix1.myMatrix4x4.myr1c2 * matrix2.myMatrix4x4.myr2c1 +
                matrix1.myMatrix4x4.myr1c3 * matrix2.myMatrix4x4.myr3c1 +
                matrix1.myMatrix4x4.myr1c4 * matrix2.myMatrix4x4.myr4c1,

                matrix1.myMatrix4x4.myr1c1 * matrix2.myMatrix4x4.myr1c2 + // r1c2 
                matrix1.myMatrix4x4.myr1c2 * matrix2.myMatrix4x4.myr2c2 +
                matrix1.myMatrix4x4.myr1c3 * matrix2.myMatrix4x4.myr3c2 +
                matrix1.myMatrix4x4.myr1c4 * matrix2.myMatrix4x4.myr4c2,

                matrix1.myMatrix4x4.myr1c1 * matrix2.myMatrix4x4.myr1c3 + // r1c3 
                matrix1.myMatrix4x4.myr1c2 * matrix2.myMatrix4x4.myr2c3 +
                matrix1.myMatrix4x4.myr1c3 * matrix2.myMatrix4x4.myr3c3 +
                matrix1.myMatrix4x4.myr1c4 * matrix2.myMatrix4x4.myr4c3,

                matrix1.myMatrix4x4.myr1c1 * matrix2.myMatrix4x4.myr1c4 + // r1c4 
                matrix1.myMatrix4x4.myr1c2 * matrix2.myMatrix4x4.myr2c4 +
                matrix1.myMatrix4x4.myr1c3 * matrix2.myMatrix4x4.myr3c4 +
                matrix1.myMatrix4x4.myr1c4 * matrix2.myMatrix4x4.myr4c4,


                matrix1.myMatrix4x4.myr2c1 * matrix2.myMatrix4x4.myr1c1 + // r2c1 
                matrix1.myMatrix4x4.myr2c2 * matrix2.myMatrix4x4.myr2c1 +
                matrix1.myMatrix4x4.myr2c3 * matrix2.myMatrix4x4.myr3c1 +
                matrix1.myMatrix4x4.myr2c4 * matrix2.myMatrix4x4.myr4c1,

                matrix1.myMatrix4x4.myr2c1 * matrix2.myMatrix4x4.myr1c2 + // r2c2 
                matrix1.myMatrix4x4.myr2c2 * matrix2.myMatrix4x4.myr2c2 +
                matrix1.myMatrix4x4.myr2c3 * matrix2.myMatrix4x4.myr3c2 +
                matrix1.myMatrix4x4.myr2c4 * matrix2.myMatrix4x4.myr4c2,

                matrix1.myMatrix4x4.myr2c1 * matrix2.myMatrix4x4.myr1c3 + // r2c3 
                matrix1.myMatrix4x4.myr2c2 * matrix2.myMatrix4x4.myr2c3 +
                matrix1.myMatrix4x4.myr2c3 * matrix2.myMatrix4x4.myr3c3 +
                matrix1.myMatrix4x4.myr2c4 * matrix2.myMatrix4x4.myr4c3,

                matrix1.myMatrix4x4.myr2c1 * matrix2.myMatrix4x4.myr1c4 + // r2c4 
                matrix1.myMatrix4x4.myr2c2 * matrix2.myMatrix4x4.myr2c4 +
                matrix1.myMatrix4x4.myr2c3 * matrix2.myMatrix4x4.myr3c4 +
                matrix1.myMatrix4x4.myr2c4 * matrix2.myMatrix4x4.myr4c4,


                matrix1.myMatrix4x4.myr3c1 * matrix2.myMatrix4x4.myr1c1 + // r3c1 
                matrix1.myMatrix4x4.myr3c2 * matrix2.myMatrix4x4.myr2c1 +
                matrix1.myMatrix4x4.myr3c3 * matrix2.myMatrix4x4.myr3c1 +
                matrix1.myMatrix4x4.myr3c4 * matrix2.myMatrix4x4.myr4c1,

                matrix1.myMatrix4x4.myr3c1 * matrix2.myMatrix4x4.myr1c2 + // r3c2 
                matrix1.myMatrix4x4.myr3c2 * matrix2.myMatrix4x4.myr2c2 +
                matrix1.myMatrix4x4.myr3c3 * matrix2.myMatrix4x4.myr3c2 +
                matrix1.myMatrix4x4.myr3c4 * matrix2.myMatrix4x4.myr4c2,

                matrix1.myMatrix4x4.myr3c1 * matrix2.myMatrix4x4.myr1c3 + // r3c3 
                matrix1.myMatrix4x4.myr3c2 * matrix2.myMatrix4x4.myr2c3 +
                matrix1.myMatrix4x4.myr3c3 * matrix2.myMatrix4x4.myr3c3 +
                matrix1.myMatrix4x4.myr3c4 * matrix2.myMatrix4x4.myr4c3,

                matrix1.myMatrix4x4.myr3c1 * matrix2.myMatrix4x4.myr1c4 + // r3c4 
                matrix1.myMatrix4x4.myr3c2 * matrix2.myMatrix4x4.myr2c4 +
                matrix1.myMatrix4x4.myr3c3 * matrix2.myMatrix4x4.myr3c4 +
                matrix1.myMatrix4x4.myr3c4 * matrix2.myMatrix4x4.myr4c4,


                matrix1.myMatrix4x4.myr4c1 * matrix2.myMatrix4x4.myr1c1 + // r4c1 
                matrix1.myMatrix4x4.myr4c2 * matrix2.myMatrix4x4.myr2c1 +
                matrix1.myMatrix4x4.myr4c3 * matrix2.myMatrix4x4.myr3c1 +
                matrix1.myMatrix4x4.myr4c4 * matrix2.myMatrix4x4.myr4c1,

                matrix1.myMatrix4x4.myr4c1 * matrix2.myMatrix4x4.myr1c2 + // r4c2 
                matrix1.myMatrix4x4.myr4c2 * matrix2.myMatrix4x4.myr2c2 +
                matrix1.myMatrix4x4.myr4c3 * matrix2.myMatrix4x4.myr3c2 +
                matrix1.myMatrix4x4.myr4c4 * matrix2.myMatrix4x4.myr4c2,

                matrix1.myMatrix4x4.myr4c1 * matrix2.myMatrix4x4.myr1c3 + // r4c3 
                matrix1.myMatrix4x4.myr4c2 * matrix2.myMatrix4x4.myr2c3 +
                matrix1.myMatrix4x4.myr4c3 * matrix2.myMatrix4x4.myr3c3 +
                matrix1.myMatrix4x4.myr4c4 * matrix2.myMatrix4x4.myr4c3,

                matrix1.myMatrix4x4.myr4c1 * matrix2.myMatrix4x4.myr1c4 + // r4c4 
                matrix1.myMatrix4x4.myr4c2 * matrix2.myMatrix4x4.myr2c4 +
                matrix1.myMatrix4x4.myr4c3 * matrix2.myMatrix4x4.myr3c4 +
                matrix1.myMatrix4x4.myr4c4 * matrix2.myMatrix4x4.myr4c4
                );

            return (result);
        }

        public static Matrix4x4d Multiply(Matrix4x4d matrix1, Matrix4x4d matrix2)
        {
            Matrix4x4d result = new Matrix4x4d
                (
                matrix1.myMatrix4x4.myr1c1 * matrix2.myMatrix4x4.myr1c1 + // r1c1 
                matrix1.myMatrix4x4.myr1c2 * matrix2.myMatrix4x4.myr2c1 +
                matrix1.myMatrix4x4.myr1c3 * matrix2.myMatrix4x4.myr3c1 +
                matrix1.myMatrix4x4.myr1c4 * matrix2.myMatrix4x4.myr4c1,

                matrix1.myMatrix4x4.myr1c1 * matrix2.myMatrix4x4.myr1c2 + // r1c2 
                matrix1.myMatrix4x4.myr1c2 * matrix2.myMatrix4x4.myr2c2 +
                matrix1.myMatrix4x4.myr1c3 * matrix2.myMatrix4x4.myr3c2 +
                matrix1.myMatrix4x4.myr1c4 * matrix2.myMatrix4x4.myr4c2,

                matrix1.myMatrix4x4.myr1c1 * matrix2.myMatrix4x4.myr1c3 + // r1c3 
                matrix1.myMatrix4x4.myr1c2 * matrix2.myMatrix4x4.myr2c3 +
                matrix1.myMatrix4x4.myr1c3 * matrix2.myMatrix4x4.myr3c3 +
                matrix1.myMatrix4x4.myr1c4 * matrix2.myMatrix4x4.myr4c3,

                matrix1.myMatrix4x4.myr1c1 * matrix2.myMatrix4x4.myr1c4 + // r1c4 
                matrix1.myMatrix4x4.myr1c2 * matrix2.myMatrix4x4.myr2c4 +
                matrix1.myMatrix4x4.myr1c3 * matrix2.myMatrix4x4.myr3c4 +
                matrix1.myMatrix4x4.myr1c4 * matrix2.myMatrix4x4.myr4c4,


                matrix1.myMatrix4x4.myr2c1 * matrix2.myMatrix4x4.myr1c1 + // r2c1 
                matrix1.myMatrix4x4.myr2c2 * matrix2.myMatrix4x4.myr2c1 +
                matrix1.myMatrix4x4.myr2c3 * matrix2.myMatrix4x4.myr3c1 +
                matrix1.myMatrix4x4.myr2c4 * matrix2.myMatrix4x4.myr4c1,

                matrix1.myMatrix4x4.myr2c1 * matrix2.myMatrix4x4.myr1c2 + // r2c2 
                matrix1.myMatrix4x4.myr2c2 * matrix2.myMatrix4x4.myr2c2 +
                matrix1.myMatrix4x4.myr2c3 * matrix2.myMatrix4x4.myr3c2 +
                matrix1.myMatrix4x4.myr2c4 * matrix2.myMatrix4x4.myr4c2,

                matrix1.myMatrix4x4.myr2c1 * matrix2.myMatrix4x4.myr1c3 + // r2c3 
                matrix1.myMatrix4x4.myr2c2 * matrix2.myMatrix4x4.myr2c3 +
                matrix1.myMatrix4x4.myr2c3 * matrix2.myMatrix4x4.myr3c3 +
                matrix1.myMatrix4x4.myr2c4 * matrix2.myMatrix4x4.myr4c3,

                matrix1.myMatrix4x4.myr2c1 * matrix2.myMatrix4x4.myr1c4 + // r2c4 
                matrix1.myMatrix4x4.myr2c2 * matrix2.myMatrix4x4.myr2c4 +
                matrix1.myMatrix4x4.myr2c3 * matrix2.myMatrix4x4.myr3c4 +
                matrix1.myMatrix4x4.myr2c4 * matrix2.myMatrix4x4.myr4c4,


                matrix1.myMatrix4x4.myr3c1 * matrix2.myMatrix4x4.myr1c1 + // r3c1 
                matrix1.myMatrix4x4.myr3c2 * matrix2.myMatrix4x4.myr2c1 +
                matrix1.myMatrix4x4.myr3c3 * matrix2.myMatrix4x4.myr3c1 +
                matrix1.myMatrix4x4.myr3c4 * matrix2.myMatrix4x4.myr4c1,

                matrix1.myMatrix4x4.myr3c1 * matrix2.myMatrix4x4.myr1c2 + // r3c2 
                matrix1.myMatrix4x4.myr3c2 * matrix2.myMatrix4x4.myr2c2 +
                matrix1.myMatrix4x4.myr3c3 * matrix2.myMatrix4x4.myr3c2 +
                matrix1.myMatrix4x4.myr3c4 * matrix2.myMatrix4x4.myr4c2,

                matrix1.myMatrix4x4.myr3c1 * matrix2.myMatrix4x4.myr1c3 + // r3c3 
                matrix1.myMatrix4x4.myr3c2 * matrix2.myMatrix4x4.myr2c3 +
                matrix1.myMatrix4x4.myr3c3 * matrix2.myMatrix4x4.myr3c3 +
                matrix1.myMatrix4x4.myr3c4 * matrix2.myMatrix4x4.myr4c3,

                matrix1.myMatrix4x4.myr3c1 * matrix2.myMatrix4x4.myr1c4 + // r3c4 
                matrix1.myMatrix4x4.myr3c2 * matrix2.myMatrix4x4.myr2c4 +
                matrix1.myMatrix4x4.myr3c3 * matrix2.myMatrix4x4.myr3c4 +
                matrix1.myMatrix4x4.myr3c4 * matrix2.myMatrix4x4.myr4c4,


                matrix1.myMatrix4x4.myr4c1 * matrix2.myMatrix4x4.myr1c1 + // r4c1 
                matrix1.myMatrix4x4.myr4c2 * matrix2.myMatrix4x4.myr2c1 +
                matrix1.myMatrix4x4.myr4c3 * matrix2.myMatrix4x4.myr3c1 +
                matrix1.myMatrix4x4.myr4c4 * matrix2.myMatrix4x4.myr4c1,

                matrix1.myMatrix4x4.myr4c1 * matrix2.myMatrix4x4.myr1c2 + // r4c2 
                matrix1.myMatrix4x4.myr4c2 * matrix2.myMatrix4x4.myr2c2 +
                matrix1.myMatrix4x4.myr4c3 * matrix2.myMatrix4x4.myr3c2 +
                matrix1.myMatrix4x4.myr4c4 * matrix2.myMatrix4x4.myr4c2,

                matrix1.myMatrix4x4.myr4c1 * matrix2.myMatrix4x4.myr1c3 + // r4c3 
                matrix1.myMatrix4x4.myr4c2 * matrix2.myMatrix4x4.myr2c3 +
                matrix1.myMatrix4x4.myr4c3 * matrix2.myMatrix4x4.myr3c3 +
                matrix1.myMatrix4x4.myr4c4 * matrix2.myMatrix4x4.myr4c3,

                matrix1.myMatrix4x4.myr4c1 * matrix2.myMatrix4x4.myr1c4 + // r4c4 
                matrix1.myMatrix4x4.myr4c2 * matrix2.myMatrix4x4.myr2c4 +
                matrix1.myMatrix4x4.myr4c3 * matrix2.myMatrix4x4.myr3c4 +
                matrix1.myMatrix4x4.myr4c4 * matrix2.myMatrix4x4.myr4c4
                );

            return (result);
        }

        public static Vector4d operator *(Vector4d row_vector_in, Matrix4x4d matrix_in)
        {
            Vector4d result = new Vector4d
                (
                matrix_in.myMatrix4x4.myr1c1 * row_vector_in.X +  // X
                matrix_in.myMatrix4x4.myr2c1 * row_vector_in.Y +
                matrix_in.myMatrix4x4.myr3c1 * row_vector_in.Z +
                matrix_in.myMatrix4x4.myr4c1 * row_vector_in.W,

                matrix_in.myMatrix4x4.myr1c2 * row_vector_in.X +  // Y
                matrix_in.myMatrix4x4.myr2c2 * row_vector_in.Y +
                matrix_in.myMatrix4x4.myr3c2 * row_vector_in.Z +
                matrix_in.myMatrix4x4.myr4c2 * row_vector_in.W,

                matrix_in.myMatrix4x4.myr1c3 * row_vector_in.X +  // Z
                matrix_in.myMatrix4x4.myr2c3 * row_vector_in.Y +
                matrix_in.myMatrix4x4.myr3c3 * row_vector_in.Z +
                matrix_in.myMatrix4x4.myr4c3 * row_vector_in.W,

                matrix_in.myMatrix4x4.myr1c4 * row_vector_in.X +  // W
                matrix_in.myMatrix4x4.myr2c4 * row_vector_in.Y +
                matrix_in.myMatrix4x4.myr3c4 * row_vector_in.Z +
                matrix_in.myMatrix4x4.myr4c4 * row_vector_in.W
                );

            return result;
        }

        public static Vector4d Multiply(Vector4d row_vector_in, Matrix4x4d matrix_in)
        {
            Vector4d result = new Vector4d
                (
                matrix_in.myMatrix4x4.myr1c1 * row_vector_in.X +  // X
                matrix_in.myMatrix4x4.myr2c1 * row_vector_in.Y +
                matrix_in.myMatrix4x4.myr3c1 * row_vector_in.Z +
                matrix_in.myMatrix4x4.myr4c1 * row_vector_in.W,

                matrix_in.myMatrix4x4.myr1c2 * row_vector_in.X +  // Y
                matrix_in.myMatrix4x4.myr2c2 * row_vector_in.Y +
                matrix_in.myMatrix4x4.myr3c2 * row_vector_in.Z +
                matrix_in.myMatrix4x4.myr4c2 * row_vector_in.W,

                matrix_in.myMatrix4x4.myr1c3 * row_vector_in.X +  // Z
                matrix_in.myMatrix4x4.myr2c3 * row_vector_in.Y +
                matrix_in.myMatrix4x4.myr3c3 * row_vector_in.Z +
                matrix_in.myMatrix4x4.myr4c3 * row_vector_in.W,

                matrix_in.myMatrix4x4.myr1c4 * row_vector_in.X +  // W
                matrix_in.myMatrix4x4.myr2c4 * row_vector_in.Y +
                matrix_in.myMatrix4x4.myr3c4 * row_vector_in.Z +
                matrix_in.myMatrix4x4.myr4c4 * row_vector_in.W
                );

            return result;
        }

        public static Vector4d operator *(Matrix4x4d matrix_in, Vector4d column_vector_in)
        {
            Vector4d result = new Vector4d
                (
                matrix_in.myMatrix4x4.myr1c1 * column_vector_in.X +  // X
                matrix_in.myMatrix4x4.myr1c2 * column_vector_in.Y +
                matrix_in.myMatrix4x4.myr1c3 * column_vector_in.Z +
                matrix_in.myMatrix4x4.myr1c4 * column_vector_in.W,

                matrix_in.myMatrix4x4.myr2c1 * column_vector_in.X +  // Y
                matrix_in.myMatrix4x4.myr2c2 * column_vector_in.Y +
                matrix_in.myMatrix4x4.myr2c3 * column_vector_in.Z +
                matrix_in.myMatrix4x4.myr2c4 * column_vector_in.W,

                matrix_in.myMatrix4x4.myr3c1 * column_vector_in.X +  // Z
                matrix_in.myMatrix4x4.myr3c2 * column_vector_in.Y +
                matrix_in.myMatrix4x4.myr3c3 * column_vector_in.Z +
                matrix_in.myMatrix4x4.myr3c4 * column_vector_in.W,

                matrix_in.myMatrix4x4.myr4c1 * column_vector_in.X +  // W
                matrix_in.myMatrix4x4.myr4c2 * column_vector_in.Y +
                matrix_in.myMatrix4x4.myr4c3 * column_vector_in.Z +
                matrix_in.myMatrix4x4.myr4c4 * column_vector_in.W
                );

            return result;
        }

        public static Vector4d Multiply(Matrix4x4d matrix_in, Vector4d column_vector_in)
        {
            Vector4d result = new Vector4d
                (
                matrix_in.myMatrix4x4.myr1c1 * column_vector_in.X +  // X
                matrix_in.myMatrix4x4.myr1c2 * column_vector_in.Y +
                matrix_in.myMatrix4x4.myr1c3 * column_vector_in.Z +
                matrix_in.myMatrix4x4.myr1c4 * column_vector_in.W,

                matrix_in.myMatrix4x4.myr2c1 * column_vector_in.X +  // Y
                matrix_in.myMatrix4x4.myr2c2 * column_vector_in.Y +
                matrix_in.myMatrix4x4.myr2c3 * column_vector_in.Z +
                matrix_in.myMatrix4x4.myr2c4 * column_vector_in.W,

                matrix_in.myMatrix4x4.myr3c1 * column_vector_in.X +  // Z
                matrix_in.myMatrix4x4.myr3c2 * column_vector_in.Y +
                matrix_in.myMatrix4x4.myr3c3 * column_vector_in.Z +
                matrix_in.myMatrix4x4.myr3c4 * column_vector_in.W,

                matrix_in.myMatrix4x4.myr4c1 * column_vector_in.X +  // W
                matrix_in.myMatrix4x4.myr4c2 * column_vector_in.Y +
                matrix_in.myMatrix4x4.myr4c3 * column_vector_in.Z +
                matrix_in.myMatrix4x4.myr4c4 * column_vector_in.W
                );

            return result;
        }
        #endregion

        #region Set / get matrix elements

        public void Init(double initVal)
        {
            this.myMatrix4x4.myr1c1 = initVal;
            this.myMatrix4x4.myr1c2 = initVal;
            this.myMatrix4x4.myr1c3 = initVal;
            this.myMatrix4x4.myr1c4 = initVal;

            this.myMatrix4x4.myr2c1 = initVal;
            this.myMatrix4x4.myr2c2 = initVal;
            this.myMatrix4x4.myr2c3 = initVal;
            this.myMatrix4x4.myr2c4 = initVal;

            this.myMatrix4x4.myr3c1 = initVal;
            this.myMatrix4x4.myr3c2 = initVal;
            this.myMatrix4x4.myr3c3 = initVal;
            this.myMatrix4x4.myr3c4 = initVal;

            this.myMatrix4x4.myr4c1 = initVal;
            this.myMatrix4x4.myr4c2 = initVal;
            this.myMatrix4x4.myr4c3 = initVal;
            this.myMatrix4x4.myr4c4 = initVal;
        }

        public void SetMatrix(double r1c1, double r1c2, double r1c3, double r1c4,
                              double r2c1, double r2c2, double r2c3, double r2c4,
                              double r3c1, double r3c2, double r3c3, double r3c4,
                              double r4c1, double r4c2, double r4c3, double r4c4)
        {
            this.myMatrix4x4.myr1c1 = r1c1;
            this.myMatrix4x4.myr1c2 = r1c2;
            this.myMatrix4x4.myr1c3 = r1c3;
            this.myMatrix4x4.myr1c4 = r1c4;

            this.myMatrix4x4.myr2c1 = r2c1;
            this.myMatrix4x4.myr2c2 = r2c2;
            this.myMatrix4x4.myr2c3 = r2c3;
            this.myMatrix4x4.myr2c4 = r2c4;

            this.myMatrix4x4.myr3c1 = r3c1;
            this.myMatrix4x4.myr3c2 = r3c2;
            this.myMatrix4x4.myr3c3 = r3c3;
            this.myMatrix4x4.myr3c4 = r3c4;

            this.myMatrix4x4.myr4c1 = r4c1;
            this.myMatrix4x4.myr4c2 = r4c2;
            this.myMatrix4x4.myr4c3 = r4c3;
            this.myMatrix4x4.myr4c4 = r4c4;
        }

        public void GetMatrix(out double r1c1, out double r1c2, out double r1c3, out double r1c4,
                              out double r2c1, out double r2c2, out double r2c3, out double r2c4,
                              out double r3c1, out double r3c2, out double r3c3, out double r3c4,
                              out double r4c1, out double r4c2, out double r4c3, out double r4c4)
        {
            r1c1 = this.myMatrix4x4.myr1c1;
            r1c2 = this.myMatrix4x4.myr1c2;
            r1c3 = this.myMatrix4x4.myr1c3;
            r1c4 = this.myMatrix4x4.myr1c4;

            r2c1 = this.myMatrix4x4.myr2c1;
            r2c2 = this.myMatrix4x4.myr2c2;
            r2c3 = this.myMatrix4x4.myr2c3;
            r2c4 = this.myMatrix4x4.myr2c4;

            r3c1 = this.myMatrix4x4.myr3c1;
            r3c2 = this.myMatrix4x4.myr3c2;
            r3c3 = this.myMatrix4x4.myr3c3;
            r3c4 = this.myMatrix4x4.myr3c4;

            r4c1 = this.myMatrix4x4.myr4c1;
            r4c2 = this.myMatrix4x4.myr4c2;
            r4c3 = this.myMatrix4x4.myr4c3;
            r4c4 = this.myMatrix4x4.myr4c4;
        }

        public void SetFromMatrix4x4(Matrix4x4d mat4x4)
        {
            this.myMatrix4x4.myr1c1 = mat4x4.myMatrix4x4.myr1c1;
            this.myMatrix4x4.myr1c2 = mat4x4.myMatrix4x4.myr1c2;
            this.myMatrix4x4.myr1c3 = mat4x4.myMatrix4x4.myr1c3;
            this.myMatrix4x4.myr1c4 = mat4x4.myMatrix4x4.myr1c4;

            this.myMatrix4x4.myr2c1 = mat4x4.myMatrix4x4.myr2c1;
            this.myMatrix4x4.myr2c2 = mat4x4.myMatrix4x4.myr2c2;
            this.myMatrix4x4.myr2c3 = mat4x4.myMatrix4x4.myr2c3;
            this.myMatrix4x4.myr2c4 = mat4x4.myMatrix4x4.myr2c4;

            this.myMatrix4x4.myr3c1 = mat4x4.myMatrix4x4.myr3c1;
            this.myMatrix4x4.myr3c2 = mat4x4.myMatrix4x4.myr3c2;
            this.myMatrix4x4.myr3c3 = mat4x4.myMatrix4x4.myr3c3;
            this.myMatrix4x4.myr3c4 = mat4x4.myMatrix4x4.myr3c4;

            this.myMatrix4x4.myr4c1 = mat4x4.myMatrix4x4.myr4c1;
            this.myMatrix4x4.myr4c2 = mat4x4.myMatrix4x4.myr4c2;
            this.myMatrix4x4.myr4c3 = mat4x4.myMatrix4x4.myr4c3;
            this.myMatrix4x4.myr4c4 = mat4x4.myMatrix4x4.myr4c4;
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
                                    this.myMatrix4x4.myr1c1 = inp_value;
                                    break;
                                }
                            case 2:
                                {
                                    this.myMatrix4x4.myr1c2 = inp_value;
                                    break;
                                }
                            case 3:
                                {
                                    this.myMatrix4x4.myr1c3 = inp_value;
                                    break;
                                }
                            case 4:
                                {
                                    this.myMatrix4x4.myr1c4 = inp_value;
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
                                    this.myMatrix4x4.myr2c1 = inp_value;
                                    break;
                                }
                            case 2:
                                {
                                    this.myMatrix4x4.myr2c2 = inp_value;
                                    break;
                                }
                            case 3:
                                {
                                    this.myMatrix4x4.myr2c3 = inp_value;
                                    break;
                                }
                            case 4:
                                {
                                    this.myMatrix4x4.myr2c4 = inp_value;
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
                                    this.myMatrix4x4.myr3c1 = inp_value;
                                    break;
                                }
                            case 2:
                                {
                                    this.myMatrix4x4.myr3c2 = inp_value;
                                    break;
                                }
                            case 3:
                                {
                                    this.myMatrix4x4.myr3c3 = inp_value;
                                    break;
                                }
                            case 4:
                                {
                                    this.myMatrix4x4.myr3c4 = inp_value;
                                    break;
                                }
                            default:
                                {
                                    throw new System.ArgumentOutOfRangeException("column_index", "invalid column index " + column_index);
                                }
                        }
                        break;
                    }
                case 4:
                    {
                        switch (column_index)
                        {
                            case 1:
                                {
                                    this.myMatrix4x4.myr4c1 = inp_value;
                                    break;
                                }
                            case 2:
                                {
                                    this.myMatrix4x4.myr4c2 = inp_value;
                                    break;
                                }
                            case 3:
                                {
                                    this.myMatrix4x4.myr4c3 = inp_value;
                                    break;
                                }
                            case 4:
                                {
                                    this.myMatrix4x4.myr4c4 = inp_value;
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
                                    return this.myMatrix4x4.myr1c1;
                                }
                            case 2:
                                {
                                    return this.myMatrix4x4.myr1c2;
                                }
                            case 3:
                                {
                                    return this.myMatrix4x4.myr1c3;
                                }
                            case 4:
                                {
                                    return this.myMatrix4x4.myr1c4;
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
                                    return this.myMatrix4x4.myr2c1;
                                }
                            case 2:
                                {
                                    return this.myMatrix4x4.myr2c2;
                                }
                            case 3:
                                {
                                    return this.myMatrix4x4.myr2c3;
                                }
                            case 4:
                                {
                                    return this.myMatrix4x4.myr2c4;
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
                                    return this.myMatrix4x4.myr3c1;
                                }
                            case 2:
                                {
                                    return this.myMatrix4x4.myr3c2;
                                }
                            case 3:
                                {
                                    return this.myMatrix4x4.myr3c3;
                                }
                            case 4:
                                {
                                    return this.myMatrix4x4.myr3c4;
                                }
                            default:
                                {
                                    throw new System.ArgumentOutOfRangeException("column_index", "invalid column index " + column_index);
                                }
                        }
                    }
                case 4:
                    {
                        switch (column_index)
                        {
                            case 1:
                                {
                                    return this.myMatrix4x4.myr4c1;
                                }
                            case 2:
                                {
                                    return this.myMatrix4x4.myr4c2;
                                }
                            case 3:
                                {
                                    return this.myMatrix4x4.myr4c3;
                                }
                            case 4:
                                {
                                    return this.myMatrix4x4.myr4c4;
                                }
                            default:
                                {
                                    throw new System.ArgumentOutOfRangeException("invalid column index " + column_index);
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
                return this.myMatrix4x4.myr1c1;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("R1C1", "invalid set r1c1 " + value);
                }
                this.myMatrix4x4.myr1c1 = value;
            }
        }
        public double R1C2
        {
            get
            {
                return this.myMatrix4x4.myr1c2;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("R1C2", "invalid set r1c2 " + value);
                }
                this.myMatrix4x4.myr1c2 = value;
            }
        }
        public double R1C3
        {
            get
            {
                return this.myMatrix4x4.myr1c3;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("R1C3", "invalid set r1c3 " + value);
                }
                this.myMatrix4x4.myr1c3 = value;
            }
        }
        public double R1C4
        {
            get
            {
                return this.myMatrix4x4.myr1c4;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("R1C4", "invalid set r1c4 " + value);
                }
                this.myMatrix4x4.myr1c4 = value;
            }
        }

        public double R2C1
        {
            get
            {
                return this.myMatrix4x4.myr2c1;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("R2C1", "invalid set r2c1 " + value);
                }
                this.myMatrix4x4.myr2c1 = value;
            }
        }
        public double R2C2
        {
            get
            {
                return this.myMatrix4x4.myr2c2;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("R2C2", "invalid set r2c2 " + value);
                }
                this.myMatrix4x4.myr2c2 = value;
            }
        }
        public double R2C3
        {
            get
            {
                return this.myMatrix4x4.myr2c3;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("R2C3", "invalid set r2c3 " + value);
                }
                this.myMatrix4x4.myr2c3 = value;
            }
        }
        public double R2C4
        {
            get
            {
                return this.myMatrix4x4.myr2c4;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("R2C4", "invalid set r2c4 " + value);
                }
                this.myMatrix4x4.myr2c4 = value;
            }
        }

        public double R3C1
        {
            get
            {
                return this.myMatrix4x4.myr3c1;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("R3C1", "invalid set r3c1 " + value);
                }
                this.myMatrix4x4.myr3c1 = value;
            }
        }
        public double R3C2
        {
            get
            {
                return this.myMatrix4x4.myr3c2;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("R3C2", "invalid set r3c2 " + value);
                }
                this.myMatrix4x4.myr3c2 = value;
            }
        }
        public double R3C3
        {
            get
            {
                return this.myMatrix4x4.myr3c3;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("R3C3", "invalid set r3c3 " + value);
                }
                this.myMatrix4x4.myr3c3 = value;
            }
        }
        public double R3C4
        {
            get
            {
                return this.myMatrix4x4.myr3c4;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("R3C4", "invalid set r3c4 " + value);
                }
                this.myMatrix4x4.myr3c4 = value;
            }
        }

        public double R4C1
        {
            get
            {
                return this.myMatrix4x4.myr4c1;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("R4C1", "invalid set r4c1 " + value);
                }
                this.myMatrix4x4.myr4c1 = value;
            }
        }
        public double R4C2
        {
            get
            {
                return this.myMatrix4x4.myr4c2;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("R4C2", "invalid set r4c2 " + value);
                }
                this.myMatrix4x4.myr4c2 = value;
            }
        }
        public double R4C3
        {
            get
            {
                return this.myMatrix4x4.myr4c3;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("R4C3", "invalid set r4c3 " + value);
                }
                this.myMatrix4x4.myr4c3 = value;
            }
        }
        public double R4C4
        {
            get
            {
                return this.myMatrix4x4.myr4c4;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("R4C4", "invalid set r4c4 " + value);
                }
                this.myMatrix4x4.myr4c4 = value;
            }
        }

        public Vector4d GetColumnVector(short column_index)
        {
            switch (column_index)
            {
                case 1:
                    {
                        Vector4d columnVector = new Vector4d(
                            this.myMatrix4x4.myr1c1,  // x
                            this.myMatrix4x4.myr2c1,  // y
                            this.myMatrix4x4.myr3c1,  // z
                            this.myMatrix4x4.myr4c1); // w
                        return columnVector;
                    }
                case 2:
                    {
                        Vector4d columnVector = new Vector4d(
                            this.myMatrix4x4.myr1c2,  // x
                            this.myMatrix4x4.myr2c2,  // y
                            this.myMatrix4x4.myr3c2,  // z
                            this.myMatrix4x4.myr4c2); // w
                        return columnVector;
                    }
                case 3:
                    {
                        Vector4d columnVector = new Vector4d(
                            this.myMatrix4x4.myr1c3,  // x
                            this.myMatrix4x4.myr2c3,  // y
                            this.myMatrix4x4.myr3c3,  // z
                            this.myMatrix4x4.myr4c3); // w
                        return columnVector;
                    }
                case 4:
                    {
                        Vector4d columnVector = new Vector4d(
                            this.myMatrix4x4.myr1c4,  // x
                            this.myMatrix4x4.myr2c4,  // y
                            this.myMatrix4x4.myr3c4,  // z
                            this.myMatrix4x4.myr4c4); // w
                        return columnVector;
                    }
                default:
                    {
                        throw new System.ArgumentOutOfRangeException("column_index", "illegal column index " + column_index);
                    }
            }
        }

        public void SetColumnVector(short column_index, Vector4d in_vector)
        {
            switch (column_index)
            {
                case 1:
                    {
                        this.myMatrix4x4.myr1c1 = in_vector.X;
                        this.myMatrix4x4.myr2c1 = in_vector.Y;
                        this.myMatrix4x4.myr3c1 = in_vector.Z;
                        this.myMatrix4x4.myr4c1 = in_vector.W;
                        break;
                    }
                case 2:
                    {
                        this.myMatrix4x4.myr1c2 = in_vector.X;
                        this.myMatrix4x4.myr2c2 = in_vector.Y;
                        this.myMatrix4x4.myr3c2 = in_vector.Z;
                        this.myMatrix4x4.myr4c2 = in_vector.W;
                        break;
                    }
                case 3:
                    {
                        this.myMatrix4x4.myr1c3 = in_vector.X;
                        this.myMatrix4x4.myr2c3 = in_vector.Y;
                        this.myMatrix4x4.myr3c3 = in_vector.Z;
                        this.myMatrix4x4.myr4c3 = in_vector.W;
                        break;
                    }
                case 4:
                    {
                        this.myMatrix4x4.myr1c4 = in_vector.X;
                        this.myMatrix4x4.myr2c4 = in_vector.Y;
                        this.myMatrix4x4.myr3c4 = in_vector.Z;
                        this.myMatrix4x4.myr4c4 = in_vector.W;
                        break;
                    }
                default:
                    {
                        throw new System.ArgumentOutOfRangeException("column_index", "illegal column index " + column_index);
                    }
            }
        }

        public Vector4d GetRowVector(short row_index)
        {
            switch (row_index)
            {
                case 1:
                    {
                        Vector4d rowVector = new Vector4d(
                            this.myMatrix4x4.myr1c1,  // x
                            this.myMatrix4x4.myr1c2,  // y
                            this.myMatrix4x4.myr1c3,  // z
                            this.myMatrix4x4.myr1c4); // w
                        return rowVector;
                    }
                case 2:
                    {
                        Vector4d rowVector = new Vector4d(
                            this.myMatrix4x4.myr2c1,  // x
                            this.myMatrix4x4.myr2c2,  // y
                            this.myMatrix4x4.myr2c3,  // z
                            this.myMatrix4x4.myr2c4); // w
                        return rowVector;
                    }
                case 3:
                    {
                        Vector4d rowVector = new Vector4d(
                            this.myMatrix4x4.myr3c1,  // x
                            this.myMatrix4x4.myr3c2,  // y
                            this.myMatrix4x4.myr3c3,  // z
                            this.myMatrix4x4.myr3c4); // w
                        return rowVector;
                    }
                case 4:
                    {
                        Vector4d rowVector = new Vector4d(
                            this.myMatrix4x4.myr4c1,  // x
                            this.myMatrix4x4.myr4c2,  // y
                            this.myMatrix4x4.myr4c3,  // z
                            this.myMatrix4x4.myr4c4); // w
                        return rowVector;
                    }
                default:
                    {
                        throw new System.ArgumentOutOfRangeException("row_index", "illegal row index " + row_index);
                    }
            }
        }

        public void SetRowVector(short row_index, Vector4d in_vector)
        {
            switch (row_index)
            {
                case 1:
                    {
                        this.myMatrix4x4.myr1c1 = in_vector.X;
                        this.myMatrix4x4.myr1c2 = in_vector.Y;
                        this.myMatrix4x4.myr1c3 = in_vector.Z;
                        this.myMatrix4x4.myr1c4 = in_vector.W;
                        break;
                    }
                case 2:
                    {
                        this.myMatrix4x4.myr2c1 = in_vector.X;
                        this.myMatrix4x4.myr2c2 = in_vector.Y;
                        this.myMatrix4x4.myr2c3 = in_vector.Z;
                        this.myMatrix4x4.myr2c4 = in_vector.W;
                        break;
                    }
                case 3:
                    {
                        this.myMatrix4x4.myr3c1 = in_vector.X;
                        this.myMatrix4x4.myr3c2 = in_vector.Y;
                        this.myMatrix4x4.myr3c3 = in_vector.Z;
                        this.myMatrix4x4.myr3c4 = in_vector.W;
                        break;
                    }
                case 4:
                    {
                        this.myMatrix4x4.myr4c1 = in_vector.X;
                        this.myMatrix4x4.myr4c2 = in_vector.Y;
                        this.myMatrix4x4.myr4c3 = in_vector.Z;
                        this.myMatrix4x4.myr4c4 = in_vector.W;
                        break;
                    }
                default:
                    {
                        throw new System.ArgumentOutOfRangeException("row_index", "illegal row index " + row_index);
                    }
            }
        }

        #endregion Set / get matrix elements

        #region ToString / parse

        public override string ToString()
        {
            return "[" + this.myMatrix4x4.myr1c1.ToString("E7") + " "
                       + this.myMatrix4x4.myr1c2.ToString("E7") + " "
                       + this.myMatrix4x4.myr1c3.ToString("E7") + " "
                       + this.myMatrix4x4.myr1c4.ToString("E7") + "]" + Environment.NewLine +

                   "[" + this.myMatrix4x4.myr2c1.ToString("E7") + " "
                       + this.myMatrix4x4.myr2c2.ToString("E7") + " "
                       + this.myMatrix4x4.myr2c3.ToString("E7") + " "
                       + this.myMatrix4x4.myr2c4.ToString("E7") + "]" + Environment.NewLine +

                   "[" + this.myMatrix4x4.myr3c1.ToString("E7") + " "
                       + this.myMatrix4x4.myr3c2.ToString("E7") + " "
                       + this.myMatrix4x4.myr3c3.ToString("E7") + " "
                       + this.myMatrix4x4.myr3c4.ToString("E7") + "]" + Environment.NewLine +

                   "[" + this.myMatrix4x4.myr4c1.ToString("E7") + " "
                       + this.myMatrix4x4.myr4c2.ToString("E7") + " "
                       + this.myMatrix4x4.myr4c3.ToString("E7") + " "
                       + this.myMatrix4x4.myr4c4.ToString("E7") + "]";
        }

        public string ToString(string Format)
        {
            return "[" + this.myMatrix4x4.myr1c1.ToString(Format) + " "
                       + this.myMatrix4x4.myr1c2.ToString(Format) + " "
                       + this.myMatrix4x4.myr1c3.ToString(Format) + " "
                       + this.myMatrix4x4.myr1c4.ToString(Format) + "]" + Environment.NewLine +

                   "[" + this.myMatrix4x4.myr2c1.ToString(Format) + " "
                       + this.myMatrix4x4.myr2c2.ToString(Format) + " "
                       + this.myMatrix4x4.myr2c3.ToString(Format) + " "
                       + this.myMatrix4x4.myr2c4.ToString(Format) + "]" + Environment.NewLine +

                   "[" + this.myMatrix4x4.myr3c1.ToString(Format) + " "
                       + this.myMatrix4x4.myr3c2.ToString(Format) + " "
                       + this.myMatrix4x4.myr3c3.ToString(Format) + " "
                       + this.myMatrix4x4.myr3c4.ToString(Format) + "]" + Environment.NewLine +

                   "[" + this.myMatrix4x4.myr4c1.ToString(Format) + " "
                       + this.myMatrix4x4.myr4c2.ToString(Format) + " "
                       + this.myMatrix4x4.myr4c3.ToString(Format) + " "
                       + this.myMatrix4x4.myr4c4.ToString(Format) + "]";
        }

        public string ToString(string Format, IFormatProvider FormatProvider)
        {
            return "[" + this.myMatrix4x4.myr1c1.ToString(Format, FormatProvider) + " "
                       + this.myMatrix4x4.myr1c2.ToString(Format, FormatProvider) + " "
                       + this.myMatrix4x4.myr1c3.ToString(Format, FormatProvider) + " "
                       + this.myMatrix4x4.myr1c4.ToString(Format, FormatProvider) + "]" + Environment.NewLine +

                   "[" + this.myMatrix4x4.myr2c1.ToString(Format, FormatProvider) + " "
                       + this.myMatrix4x4.myr2c2.ToString(Format, FormatProvider) + " "
                       + this.myMatrix4x4.myr2c3.ToString(Format, FormatProvider) + " "
                       + this.myMatrix4x4.myr2c4.ToString(Format, FormatProvider) + "]" + Environment.NewLine +

                   "[" + this.myMatrix4x4.myr3c1.ToString(Format, FormatProvider) + " "
                       + this.myMatrix4x4.myr3c2.ToString(Format, FormatProvider) + " "
                       + this.myMatrix4x4.myr3c3.ToString(Format, FormatProvider) + " "
                       + this.myMatrix4x4.myr3c4.ToString(Format, FormatProvider) + "]" + Environment.NewLine +

                   "[" + this.myMatrix4x4.myr4c1.ToString(Format, FormatProvider) + " "
                       + this.myMatrix4x4.myr4c2.ToString(Format, FormatProvider) + " "
                       + this.myMatrix4x4.myr4c3.ToString(Format, FormatProvider) + " "
                       + this.myMatrix4x4.myr4c4.ToString(Format, FormatProvider) + "]";
        }

        public void SetFromString(string matrix)
        {
            SetFromString(matrix, null);
        }

        public void SetFromString(string matrix, IFormatProvider FormatProvider)
        {
            // matrix string looks like:   [r1c1 r1c2 r1c3 r1c4]\n[r2c1 r2c2 r2c3 r2c4]\n[r3c1 r3c2 r3c3 r3c4]\n[r4c1 r4c2 r4c3 r4c4]
            int endPos = SetLineFromString(matrix, 0, 1, FormatProvider); // convert row 1
            endPos = SetLineFromString(matrix, endPos, 2, FormatProvider); // convert row 2
            endPos = SetLineFromString(matrix, endPos, 3, FormatProvider); // convert row 3
            endPos = SetLineFromString(matrix, endPos, 4, FormatProvider); // convert row 4
        }

        private int SetLineFromString(string row, int startPos, int row_number, IFormatProvider FormatProvider)
        {
            // row    string looks like:   [-1.0E+000 2.0E+000 3.0E+000 4.0E+000]
            //                             01234567890123456789012345678901234567
            int pos0 = row.IndexOf('[', startPos);          // e.g.  0
            int pos1 = row.IndexOf(' ', pos0 + 7);          // e.g. 10  ( 7 = 1.E+000 )
            int minlength = pos1 - pos0 - 1;                // length - 1 * <sign>
            int pos2 = row.IndexOf(' ', pos1 + minlength);  // e.g. 19
            int pos3 = row.IndexOf(' ', pos2 + minlength);  // e.g. 28
            int pos4 = row.IndexOf(']', pos3 + minlength);  // e.g. 37

            switch (row_number)
            {
                case 2:
                    {
                        if (null == FormatProvider)
                        {
                            this.myMatrix4x4.myr2c1 = Double.Parse(row.Substring(pos0 + 1, pos1 - pos0 - 1));
                            this.myMatrix4x4.myr2c2 = Double.Parse(row.Substring(pos1 + 1, pos2 - pos1 - 1));
                            this.myMatrix4x4.myr2c3 = Double.Parse(row.Substring(pos2 + 1, pos3 - pos2 - 1));
                            this.myMatrix4x4.myr2c4 = Double.Parse(row.Substring(pos3 + 1, pos4 - pos3 - 1));
                        }
                        else
                        {
                            this.myMatrix4x4.myr2c1 = Double.Parse(row.Substring(pos0 + 1, pos1 - pos0 - 1), FormatProvider);
                            this.myMatrix4x4.myr2c2 = Double.Parse(row.Substring(pos1 + 1, pos2 - pos1 - 1), FormatProvider);
                            this.myMatrix4x4.myr2c3 = Double.Parse(row.Substring(pos2 + 1, pos3 - pos2 - 1), FormatProvider);
                            this.myMatrix4x4.myr2c4 = Double.Parse(row.Substring(pos3 + 1, pos4 - pos3 - 1), FormatProvider);
                        }
                        break;
                    }
                case 3:
                    {
                        if (null == FormatProvider)
                        {
                            this.myMatrix4x4.myr3c1 = Double.Parse(row.Substring(pos0 + 1, pos1 - pos0 - 1));
                            this.myMatrix4x4.myr3c2 = Double.Parse(row.Substring(pos1 + 1, pos2 - pos1 - 1));
                            this.myMatrix4x4.myr3c3 = Double.Parse(row.Substring(pos2 + 1, pos3 - pos2 - 1));
                            this.myMatrix4x4.myr3c4 = Double.Parse(row.Substring(pos3 + 1, pos4 - pos3 - 1));
                        }
                        else
                        {
                            this.myMatrix4x4.myr3c1 = Double.Parse(row.Substring(pos0 + 1, pos1 - pos0 - 1), FormatProvider);
                            this.myMatrix4x4.myr3c2 = Double.Parse(row.Substring(pos1 + 1, pos2 - pos1 - 1), FormatProvider);
                            this.myMatrix4x4.myr3c3 = Double.Parse(row.Substring(pos2 + 1, pos3 - pos2 - 1), FormatProvider);
                            this.myMatrix4x4.myr3c4 = Double.Parse(row.Substring(pos3 + 1, pos4 - pos3 - 1), FormatProvider);
                        }
                        break;
                    }
                case 4:
                    {
                        if (null == FormatProvider)
                        {
                            this.myMatrix4x4.myr4c1 = Double.Parse(row.Substring(pos0 + 1, pos1 - pos0 - 1));
                            this.myMatrix4x4.myr4c2 = Double.Parse(row.Substring(pos1 + 1, pos2 - pos1 - 1));
                            this.myMatrix4x4.myr4c3 = Double.Parse(row.Substring(pos2 + 1, pos3 - pos2 - 1));
                            this.myMatrix4x4.myr4c4 = Double.Parse(row.Substring(pos3 + 1, pos4 - pos3 - 1));
                        }
                        else
                        {
                            this.myMatrix4x4.myr4c1 = Double.Parse(row.Substring(pos0 + 1, pos1 - pos0 - 1), FormatProvider);
                            this.myMatrix4x4.myr4c2 = Double.Parse(row.Substring(pos1 + 1, pos2 - pos1 - 1), FormatProvider);
                            this.myMatrix4x4.myr4c3 = Double.Parse(row.Substring(pos2 + 1, pos3 - pos2 - 1), FormatProvider);
                            this.myMatrix4x4.myr4c4 = Double.Parse(row.Substring(pos3 + 1, pos4 - pos3 - 1), FormatProvider);
                        }
                        break;
                    }
                default:
                    {
                        if (null == FormatProvider)
                        {
                            this.myMatrix4x4.myr1c1 = Double.Parse(row.Substring(pos0 + 1, pos1 - pos0 - 1));
                            this.myMatrix4x4.myr1c2 = Double.Parse(row.Substring(pos1 + 1, pos2 - pos1 - 1));
                            this.myMatrix4x4.myr1c3 = Double.Parse(row.Substring(pos2 + 1, pos3 - pos2 - 1));
                            this.myMatrix4x4.myr1c4 = Double.Parse(row.Substring(pos3 + 1, pos4 - pos3 - 1));
                        }
                        else
                        {
                            this.myMatrix4x4.myr1c1 = Double.Parse(row.Substring(pos0 + 1, pos1 - pos0 - 1), FormatProvider);
                            this.myMatrix4x4.myr1c2 = Double.Parse(row.Substring(pos1 + 1, pos2 - pos1 - 1), FormatProvider);
                            this.myMatrix4x4.myr1c3 = Double.Parse(row.Substring(pos2 + 1, pos3 - pos2 - 1), FormatProvider);
                            this.myMatrix4x4.myr1c4 = Double.Parse(row.Substring(pos3 + 1, pos4 - pos3 - 1), FormatProvider);
                        }
                        break;
                    }
            }

            return pos4; // return position of last ']'
        }

        public static Matrix4x4d Parse(string matrix)
        {
            Matrix4x4d result = new Matrix4x4d();
            result.SetFromString(matrix);
            return (result);
        }

        public static Matrix4x4d Parse(string matrix, IFormatProvider FormatProvider)
        {
            Matrix4x4d result = new Matrix4x4d();
            result.SetFromString(matrix, FormatProvider);
            return (result);
        }

        #endregion ToString / parse

        #region Set / get matrix methods

        public void SetIdentityMatrix()
        {
            this.myMatrix4x4.myr1c1 = 1.0;
            this.myMatrix4x4.myr1c2 = 0.0;
            this.myMatrix4x4.myr1c3 = 0.0;
            this.myMatrix4x4.myr1c4 = 0.0;

            this.myMatrix4x4.myr2c1 = 0.0;
            this.myMatrix4x4.myr2c2 = 1.0;
            this.myMatrix4x4.myr2c3 = 0.0;
            this.myMatrix4x4.myr2c4 = 0.0;

            this.myMatrix4x4.myr3c1 = 0.0;
            this.myMatrix4x4.myr3c2 = 0.0;
            this.myMatrix4x4.myr3c3 = 1.0;
            this.myMatrix4x4.myr3c4 = 0.0;

            this.myMatrix4x4.myr4c1 = 0.0;
            this.myMatrix4x4.myr4c2 = 0.0;
            this.myMatrix4x4.myr4c3 = 0.0;
            this.myMatrix4x4.myr4c4 = 1.0;
        }

        public static Matrix4x4d GetIdentityMatrix()
        {
            return new Matrix4x4d(1.0, 0.0, 0.0, 0.0,
                                  0.0, 1.0, 0.0, 0.0,
                                  0.0, 0.0, 1.0, 0.0,
                                  0.0, 0.0, 0.0, 1.0);
        }

        public void SetAffineMatrix(Matrix3x3d mat3x3, Vector3d vec3d)
        {
            this.myMatrix4x4.myr1c1 = mat3x3.myMatrix3x3.myr1c1;
            this.myMatrix4x4.myr1c2 = mat3x3.myMatrix3x3.myr1c2;
            this.myMatrix4x4.myr1c3 = mat3x3.myMatrix3x3.myr1c3;

            this.myMatrix4x4.myr1c4 = vec3d.X;

            this.myMatrix4x4.myr2c1 = mat3x3.myMatrix3x3.myr2c1;
            this.myMatrix4x4.myr2c2 = mat3x3.myMatrix3x3.myr2c2;
            this.myMatrix4x4.myr2c3 = mat3x3.myMatrix3x3.myr2c3;

            this.myMatrix4x4.myr2c4 = vec3d.Y;

            this.myMatrix4x4.myr3c1 = mat3x3.myMatrix3x3.myr3c1;
            this.myMatrix4x4.myr3c2 = mat3x3.myMatrix3x3.myr3c2;
            this.myMatrix4x4.myr3c3 = mat3x3.myMatrix3x3.myr3c3;

            this.myMatrix4x4.myr3c4 = vec3d.Z;

            this.myMatrix4x4.myr4c1 = 0.0;
            this.myMatrix4x4.myr4c2 = 0.0;
            this.myMatrix4x4.myr4c3 = 0.0;
            this.myMatrix4x4.myr4c4 = 1.0;
        }

        public static Matrix4x4d GetAffineMatrix(Matrix3x3d mat3x3, Vector3d vec3d)
        {
            Matrix4x4d mat = new Matrix4x4d();
            mat.SetAffineMatrix(mat3x3, vec3d);
            return mat;
        }

        public Matrix3x3d GetAffineSubMatrix()
        {
            return new Matrix3x3d(
                this.myMatrix4x4.myr1c1, this.myMatrix4x4.myr1c2, this.myMatrix4x4.myr1c3,
                this.myMatrix4x4.myr2c1, this.myMatrix4x4.myr2c2, this.myMatrix4x4.myr2c3,
                this.myMatrix4x4.myr3c1, this.myMatrix4x4.myr3c2, this.myMatrix4x4.myr3c3);
        }

        public Vector3d GetAffineTranslation()
        {
            return new Vector3d(this.myMatrix4x4.myr1c4, this.myMatrix4x4.myr2c4, this.myMatrix4x4.myr3c4);
        }

        public void SetScalingMatrix(double scaleX_in, double scaleY_in, double scaleZ_in)
        {
            this.myMatrix4x4.myr1c1 = scaleX_in;
            this.myMatrix4x4.myr1c2 = 0.0;
            this.myMatrix4x4.myr1c3 = 0.0;
            this.myMatrix4x4.myr1c4 = 0.0;

            this.myMatrix4x4.myr2c1 = 0.0;
            this.myMatrix4x4.myr2c2 = scaleY_in;
            this.myMatrix4x4.myr2c3 = 0.0;
            this.myMatrix4x4.myr2c4 = 0.0;

            this.myMatrix4x4.myr3c1 = 0.0;
            this.myMatrix4x4.myr3c2 = 0.0;
            this.myMatrix4x4.myr3c3 = scaleZ_in;
            this.myMatrix4x4.myr3c4 = 0.0;

            this.myMatrix4x4.myr4c1 = 0.0;
            this.myMatrix4x4.myr4c2 = 0.0;
            this.myMatrix4x4.myr4c3 = 0.0;
            this.myMatrix4x4.myr4c4 = 1.0;
        }

        public static Matrix4x4d GetScalingMatrix(double scaleX_in, double scaleY_in, double scaleZ_in)
        {
            Matrix4x4d mat = new Matrix4x4d();
            mat.SetScalingMatrix(scaleX_in, scaleY_in, scaleZ_in);
            return mat;
        }

        public void SetTranslationMatrix(double translationX_in, double translationY_in, double translationZ_in)
        {
            this.myMatrix4x4.myr1c1 = 1.0;
            this.myMatrix4x4.myr1c2 = 0.0;
            this.myMatrix4x4.myr1c3 = 0.0;
            this.myMatrix4x4.myr1c4 = translationX_in;

            this.myMatrix4x4.myr2c1 = 0.0;
            this.myMatrix4x4.myr2c2 = 1.0;
            this.myMatrix4x4.myr2c3 = 0.0;
            this.myMatrix4x4.myr2c4 = translationY_in;

            this.myMatrix4x4.myr3c1 = 0.0;
            this.myMatrix4x4.myr3c2 = 0.0;
            this.myMatrix4x4.myr3c3 = 1.0;
            this.myMatrix4x4.myr3c4 = translationZ_in;

            this.myMatrix4x4.myr4c1 = 0.0;
            this.myMatrix4x4.myr4c2 = 0.0;
            this.myMatrix4x4.myr4c3 = 0.0;
            this.myMatrix4x4.myr4c4 = 1.0;
        }

        public static Matrix4x4d GetTranslationMatrix(double translationX_in, double translationY_in, double translationZ_in)
        {
            Matrix4x4d mat = new Matrix4x4d();
            mat.SetTranslationMatrix(translationX_in, translationY_in, translationZ_in);
            return mat;
        }

        public void SetShearMatrix(double shearXY_in, double shearXZ_in, double shearYZ_in)
        {
            this.myMatrix4x4.myr1c1 = 1;
            this.myMatrix4x4.myr1c2 = shearXY_in;
            this.myMatrix4x4.myr1c3 = shearXZ_in;
            this.myMatrix4x4.myr1c4 = 0.0;

            this.myMatrix4x4.myr2c1 = 0.0;
            this.myMatrix4x4.myr2c2 = 1;
            this.myMatrix4x4.myr2c3 = shearYZ_in;
            this.myMatrix4x4.myr2c4 = 0.0;

            this.myMatrix4x4.myr3c1 = 0.0;
            this.myMatrix4x4.myr3c2 = 0.0;
            this.myMatrix4x4.myr3c3 = 1;
            this.myMatrix4x4.myr3c4 = 0.0;

            this.myMatrix4x4.myr4c1 = 0.0;
            this.myMatrix4x4.myr4c2 = 0.0;
            this.myMatrix4x4.myr4c3 = 0.0;
            this.myMatrix4x4.myr4c4 = 1.0;
        }

        public static Matrix4x4d GetShearMatrix(double shearXY_in, double shearXZ_in, double shearYZ_in)
        {
            Matrix4x4d mat = new Matrix4x4d();
            mat.SetShearMatrix(shearXY_in, shearXZ_in, shearYZ_in);
            return mat;
        }

       
        public void SetTransformationMatrix(Vector4d vectorV_in, Vector4d vectorW_in, double tolerance)
        {
            Vector4d normalizedV = new Vector4d(vectorV_in);
            Vector4d normalizedW = new Vector4d(vectorW_in);
            Matrix4x4d matrix = new Matrix4x4d();

            normalizedV.W = 0.0;
            normalizedW.W = 0.0;

            if (normalizedV.IsNil(tolerance))
            {
                throw new System.ArgumentOutOfRangeException("vectorV_in", "The specified source vector is NIL. Its length is: " + normalizedV.GetLength() + ".");
            }
            if (normalizedW.IsNil(tolerance))
            {
                throw new System.ArgumentOutOfRangeException("vectorW_in", "The specified target vector is NIL. Its length is: " + normalizedW.GetLength() + ".");
            }

            //the transformation matrix consists of a multiplication of 6 basic matrices

            double lengthV = normalizedV.GetLength();
            double lengthW = normalizedW.GetLength();
            normalizedV.Normalize(tolerance);
            normalizedW.Normalize(tolerance);

            this.SetScalingMatrix(lengthW, lengthW, lengthW);             //matrix 1

            if (normalizedW.X != 0.0 || normalizedW.Z != 0.0)
            {
                matrix.SetRotateOnYZPlane(normalizedW);
                matrix.Transpose();                                            //matrix 2
                this.MultiplyWithMatrixOnRight(matrix);
            }

            matrix.SetRotateFromYZPlaneOnZAxis(normalizedW);
            matrix.Transpose();                                                //matrix 3
            this.MultiplyWithMatrixOnRight(matrix);

            matrix.SetRotateFromYZPlaneOnZAxis(normalizedV);                 //matrix 4
            this.MultiplyWithMatrixOnRight(matrix);

            if (normalizedV.X != 0.0 || normalizedV.Z != 0.0)
            {
                matrix.SetRotateOnYZPlane(normalizedV);                      //matrix 5
                this.MultiplyWithMatrixOnRight(matrix);
            }

            double reciLV = 1 / lengthV;

            matrix.SetScalingMatrix(reciLV, reciLV, reciLV);                 //matrix 6
            this.MultiplyWithMatrixOnRight(matrix);
        }

        public static Matrix4x4d GetTransformationMatrix(Vector4d vectorV_in, Vector4d vectorW_in, double tolerance)
        {
            Matrix4x4d mat = new Matrix4x4d();
            mat.SetTransformationMatrix(vectorV_in, vectorW_in, tolerance);
            return mat;
        }

        public void SetRotationMatrixXaxis(double angle_in)
        {
            double sinOfAngle = Math.Sin(angle_in);
            double cosOfAngle = Math.Cos(angle_in);

            this.myMatrix4x4.myr1c1 = 1.0;
            this.myMatrix4x4.myr1c2 = 0.0;
            this.myMatrix4x4.myr1c3 = 0.0;
            this.myMatrix4x4.myr1c4 = 0.0;

            this.myMatrix4x4.myr2c1 = 0.0;
            this.myMatrix4x4.myr2c2 = cosOfAngle;
            this.myMatrix4x4.myr2c3 = -sinOfAngle;
            this.myMatrix4x4.myr2c4 = 0.0;

            this.myMatrix4x4.myr3c1 = 0.0;
            this.myMatrix4x4.myr3c2 = sinOfAngle;
            this.myMatrix4x4.myr3c3 = cosOfAngle;
            this.myMatrix4x4.myr3c4 = 0.0;

            this.myMatrix4x4.myr4c1 = 0.0;
            this.myMatrix4x4.myr4c2 = 0.0;
            this.myMatrix4x4.myr4c3 = 0.0;
            this.myMatrix4x4.myr4c4 = 1.0;
        }

        public void SetRotationMatrixYaxis(double angle_in)
        {
            double sinOfAngle = Math.Sin(angle_in);
            double cosOfAngle = Math.Cos(angle_in);

            this.myMatrix4x4.myr1c1 = cosOfAngle;
            this.myMatrix4x4.myr1c2 = 0.0;
            this.myMatrix4x4.myr1c3 = sinOfAngle;
            this.myMatrix4x4.myr1c4 = 0.0;

            this.myMatrix4x4.myr2c1 = 0.0;
            this.myMatrix4x4.myr2c2 = 1.0;
            this.myMatrix4x4.myr2c3 = 0.0;
            this.myMatrix4x4.myr2c4 = 0.0;

            this.myMatrix4x4.myr3c1 = -sinOfAngle;
            this.myMatrix4x4.myr3c2 = 0.0;
            this.myMatrix4x4.myr3c3 = cosOfAngle;
            this.myMatrix4x4.myr3c4 = 0.0;

            this.myMatrix4x4.myr4c1 = 0.0;
            this.myMatrix4x4.myr4c2 = 0.0;
            this.myMatrix4x4.myr4c3 = 0.0;
            this.myMatrix4x4.myr4c4 = 1.0;
        }

        public void SetRotationMatrixZaxis(double angle_in)
        {
            double sinOfAngle = Math.Sin(angle_in);
            double cosOfAngle = Math.Cos(angle_in);

            this.myMatrix4x4.myr1c1 = cosOfAngle;
            this.myMatrix4x4.myr1c2 = -sinOfAngle;
            this.myMatrix4x4.myr1c3 = 0.0;
            this.myMatrix4x4.myr1c4 = 0.0;

            this.myMatrix4x4.myr2c1 = sinOfAngle;
            this.myMatrix4x4.myr2c2 = cosOfAngle;
            this.myMatrix4x4.myr2c3 = 0.0;
            this.myMatrix4x4.myr2c4 = 0.0;

            this.myMatrix4x4.myr3c1 = 0.0;
            this.myMatrix4x4.myr3c2 = 0.0;
            this.myMatrix4x4.myr3c3 = 1.0;
            this.myMatrix4x4.myr3c4 = 0.0;

            this.myMatrix4x4.myr4c1 = 0.0;
            this.myMatrix4x4.myr4c2 = 0.0;
            this.myMatrix4x4.myr4c3 = 0.0;
            this.myMatrix4x4.myr4c4 = 1.0;
        }

        public static Matrix4x4d GetRotationMatrixXaxis(double angle)
        {
            Matrix4x4d mat = new Matrix4x4d();
            mat.SetRotationMatrixXaxis(angle);
            return mat;
        }

        public static Matrix4x4d GetRotationMatrixYaxis(double angle)
        {
            Matrix4x4d mat = new Matrix4x4d();
            mat.SetRotationMatrixYaxis(angle);
            return mat;
        }

        public static Matrix4x4d GetRotationMatrixZaxis(double angle)
        {
            Matrix4x4d mat = new Matrix4x4d();
            mat.SetRotationMatrixZaxis(angle);
            return mat;
        }

        public void SetRotationMatrix(double angleX_in, double angleY_in, double angleZ_in, bool static_axes)
        {
            if (static_axes)
            {
                // Static axes!
                SetRotationMatrix(angleX_in, angleY_in, angleZ_in);
            }
            else
            {
                // Moving axes!
                double sinOfAngleX = Math.Sin(angleX_in);
                double sinOfAngleY = Math.Sin(angleY_in);
                double sinOfAngleZ = Math.Sin(angleZ_in);
                double cosOfAngleX = Math.Cos(angleX_in);
                double cosOfAngleY = Math.Cos(angleY_in);
                double cosOfAngleZ = Math.Cos(angleZ_in);

                this.myMatrix4x4.myr1c1 = cosOfAngleY * cosOfAngleZ;
                this.myMatrix4x4.myr1c2 = -cosOfAngleY * sinOfAngleZ;
                this.myMatrix4x4.myr1c3 = sinOfAngleY;
                this.myMatrix4x4.myr1c4 = 0.0;

                this.myMatrix4x4.myr2c1 = sinOfAngleX * sinOfAngleY * cosOfAngleZ + cosOfAngleX * sinOfAngleZ;
                this.myMatrix4x4.myr2c2 = -sinOfAngleX * sinOfAngleY * sinOfAngleZ + cosOfAngleX * cosOfAngleZ;
                this.myMatrix4x4.myr2c3 = -sinOfAngleX * cosOfAngleY;
                this.myMatrix4x4.myr2c4 = 0.0;

                this.myMatrix4x4.myr3c1 = -cosOfAngleX * sinOfAngleY * cosOfAngleZ + sinOfAngleX * sinOfAngleZ;
                this.myMatrix4x4.myr3c2 = cosOfAngleX * sinOfAngleY * sinOfAngleZ + sinOfAngleX * cosOfAngleZ;
                this.myMatrix4x4.myr3c3 = cosOfAngleX * cosOfAngleY;
                this.myMatrix4x4.myr3c4 = 0.0;

                this.myMatrix4x4.myr4c1 = 0.0;
                this.myMatrix4x4.myr4c2 = 0.0;
                this.myMatrix4x4.myr4c3 = 0.0;
                this.myMatrix4x4.myr4c4 = 1.0;
            }
        }

        public void SetRotationMatrix(double angleX_in, double angleY_in, double angleZ_in)
        {
            double sinOfAngleX = Math.Sin(angleX_in);
            double sinOfAngleY = Math.Sin(angleY_in);
            double sinOfAngleZ = Math.Sin(angleZ_in);
            double cosOfAngleX = Math.Cos(angleX_in);
            double cosOfAngleY = Math.Cos(angleY_in);
            double cosOfAngleZ = Math.Cos(angleZ_in);

            this.myMatrix4x4.myr1c1 = cosOfAngleY * cosOfAngleZ;
            this.myMatrix4x4.myr1c2 = sinOfAngleX * sinOfAngleY * cosOfAngleZ - cosOfAngleX * sinOfAngleZ;
            this.myMatrix4x4.myr1c3 = cosOfAngleX * sinOfAngleY * cosOfAngleZ + sinOfAngleX * sinOfAngleZ;
            this.myMatrix4x4.myr1c4 = 0.0;

            this.myMatrix4x4.myr2c1 = cosOfAngleY * sinOfAngleZ;
            this.myMatrix4x4.myr2c2 = sinOfAngleX * sinOfAngleY * sinOfAngleZ + cosOfAngleX * cosOfAngleZ;
            this.myMatrix4x4.myr2c3 = cosOfAngleX * sinOfAngleY * sinOfAngleZ - sinOfAngleX * cosOfAngleZ;
            this.myMatrix4x4.myr2c4 = 0.0;

            this.myMatrix4x4.myr3c1 = -sinOfAngleY;
            this.myMatrix4x4.myr3c2 = sinOfAngleX * cosOfAngleY;
            this.myMatrix4x4.myr3c3 = cosOfAngleX * cosOfAngleY;
            this.myMatrix4x4.myr3c4 = 0.0;

            this.myMatrix4x4.myr4c1 = 0.0;
            this.myMatrix4x4.myr4c2 = 0.0;
            this.myMatrix4x4.myr4c3 = 0.0;
            this.myMatrix4x4.myr4c4 = 1.0;
        }


        public static Matrix4x4d GetRotationMatrix(double angleX_in, double angleY_in, double angleZ_in, bool static_axes)
        {
            Matrix4x4d mat = new Matrix4x4d();
            if (static_axes)
            {
                mat.SetRotationMatrix(angleX_in, angleY_in, angleZ_in);
            }
            else
            {
                mat.SetRotationMatrix(angleX_in, angleY_in, angleZ_in, false);
            }
            return mat;
        }

        public static Matrix4x4d GetRotationMatrix(double angleX_in, double angleY_in, double angleZ_in)
        {
            Matrix4x4d mat = new Matrix4x4d();
            mat.SetRotationMatrix(angleX_in, angleY_in, angleZ_in);
            return mat;
        }

        public void SetRotationMatrix(Vector4d axis, double angle, double tolerance)
        {
            // Arbitrary axis rotation matrix
            // | ax*ax + cos*(1-ax*ax)  ax*ay*(1-cos)-az*sin  ax*az*(1-cos)+ay*sin   0 |
            // | ax*ay*(1-cos)+az*sin   ay*ay+cos*(1-ay*ay)   ay*az(1-cos)-ax*sin    0 |
            // | ax*az*(1-cos)-ay*sin   ay*az*(1-cos)+ax*sin  az*az+cos*(1-az*az)    0 |
            // | 0                      0                     0                      1 |       

            Vector3d naxis = new Vector3d(axis);
            naxis.Normalize(tolerance);

            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);

            double oMcos = (1 - cos);

            double ax2 = naxis.X * naxis.X;
            double ay2 = naxis.Y * naxis.Y;
            double az2 = naxis.Z * naxis.Z;

            double axayoMcos = naxis.X * naxis.Y * oMcos;
            double axazoMcos = naxis.X * naxis.Z * oMcos;
            double ayazoMcos = naxis.Y * naxis.Z * oMcos;

            double axsin = naxis.X * sin;
            double aysin = naxis.Y * sin;
            double azsin = naxis.Z * sin;

            this.myMatrix4x4.myr1c1 = ax2 + cos * (1 - ax2);
            this.myMatrix4x4.myr1c2 = axayoMcos - azsin;
            this.myMatrix4x4.myr1c3 = axazoMcos + aysin;
            this.myMatrix4x4.myr1c4 = 0.0;

            this.myMatrix4x4.myr2c1 = axayoMcos + azsin;
            this.myMatrix4x4.myr2c2 = ay2 + cos * (1 - ay2);
            this.myMatrix4x4.myr2c3 = ayazoMcos - axsin;
            this.myMatrix4x4.myr2c4 = 0.0;

            this.myMatrix4x4.myr3c1 = axazoMcos - aysin;
            this.myMatrix4x4.myr3c2 = ayazoMcos + axsin;
            this.myMatrix4x4.myr3c3 = az2 + cos * (1 - az2);
            this.myMatrix4x4.myr3c4 = 0.0;

            this.myMatrix4x4.myr4c1 = 0.0;
            this.myMatrix4x4.myr4c2 = 0.0;
            this.myMatrix4x4.myr4c3 = 0.0;
            this.myMatrix4x4.myr4c4 = 1.0;
        }

        public static Matrix4x4d GetRotationMatrix(Vector4d axis, double angle, double tolerance)
        {
            Matrix4x4d mat = new Matrix4x4d();
            mat.SetRotationMatrix(axis, angle, tolerance);
            return mat;
        }

        public void SetRotationMatrix(Vector4d rotationPoint_in,
                                       Vector4d rotationAxis_in,
                                       double rotationAngle_in, double tolerance)
        {

            Vector4d normalizedAxis = new Vector4d(rotationAxis_in);
            Matrix4x4d matrix = new Matrix4x4d();

            normalizedAxis.W = 0.0;

            if (normalizedAxis.IsNil(tolerance))
            {
                throw new System.ArgumentOutOfRangeException("rotationAxis_in", "The specified rotation axis is NIL. Its length is: " + normalizedAxis.GetLength() + ".");
            }
            else    //compute rotationMatrix 
            {
                normalizedAxis.Normalize(tolerance);

                // Inverse translation of rotation point onto the origin
                this.SetTranslationMatrix(rotationPoint_in.X, rotationPoint_in.Y, rotationPoint_in.Z);

                // Inverse rotation of the rotation axis about the y-axis onto the yz-plane
                matrix.SetRotateOnYZPlane(normalizedAxis);
                matrix.Transpose();
                this.MultiplyWithMatrixOnRight(matrix);

                // Inverse rotation of the rotation axis about the x-axis onto the z-axis 
                matrix.SetRotateFromYZPlaneOnZAxis(normalizedAxis);
                matrix.Transpose();
                this.MultiplyWithMatrixOnRight(matrix);

                double rotCos = Math.Cos(rotationAngle_in);
                double rotSin = Math.Sin(rotationAngle_in);

                // Rotate about the z-axis
                matrix.myMatrix4x4.myr1c1 = rotCos;
                matrix.myMatrix4x4.myr1c2 = -rotSin;
                matrix.myMatrix4x4.myr1c3 = 0.0;
                matrix.myMatrix4x4.myr1c4 = 0.0;

                matrix.myMatrix4x4.myr2c1 = rotSin;
                matrix.myMatrix4x4.myr2c2 = rotCos;
                matrix.myMatrix4x4.myr2c3 = 0.0;
                matrix.myMatrix4x4.myr2c4 = 0.0;

                matrix.myMatrix4x4.myr3c1 = 0.0;
                matrix.myMatrix4x4.myr3c2 = 0.0;
                matrix.myMatrix4x4.myr3c3 = 1.0;
                matrix.myMatrix4x4.myr3c4 = 0.0;

                matrix.myMatrix4x4.myr4c1 = 0.0;
                matrix.myMatrix4x4.myr4c2 = 0.0;
                matrix.myMatrix4x4.myr4c3 = 0.0;
                matrix.myMatrix4x4.myr4c4 = 1.0;

                this.MultiplyWithMatrixOnRight(matrix);

                // Rotate the rotation axis about the x-axis onto the z-axis
                matrix.SetRotateFromYZPlaneOnZAxis(normalizedAxis);
                this.MultiplyWithMatrixOnRight(matrix);

                // Rotate the rotation axis about the y-axis onto the yz-plane
                matrix.SetRotateOnYZPlane(normalizedAxis);
                this.MultiplyWithMatrixOnRight(matrix);

                // Translation of rotation point onto the origin
                matrix.SetTranslationMatrix(-rotationPoint_in.X, -rotationPoint_in.Y, -rotationPoint_in.Z);

                // calculate this result matrix
                this.MultiplyWithMatrixOnRight(matrix);
            }
        }

        public static Matrix4x4d GetRotationMatrix(Vector4d rotationPoint_in,
                                       Vector4d rotationAxis_in,
                                       double rotationAngle_in, double tolerance)
        {
            Matrix4x4d mat = new Matrix4x4d();
            mat.SetRotationMatrix(rotationPoint_in, rotationAxis_in, rotationAngle_in, tolerance);
            return mat;
        }

        public void SetRotateOnYZPlane(Vector4d vector_in)
        {
            // return identity matrix if  sqrt(x*x + y*y) = 0
            // i.e. it is a Nil vector which has to be rotated;
            double zcomp = 1.0;
            double xcomp = 0.0;

            double sqrtV = Math.Sqrt(vector_in.X * vector_in.X + vector_in.Z * vector_in.Z);
            if (sqrtV != 0.0)
            {
                double reciSqrt = 1 / sqrtV;
                zcomp = vector_in.Z * reciSqrt;
                xcomp = vector_in.X * reciSqrt;
            }

            this.myMatrix4x4.myr1c1 = zcomp;
            this.myMatrix4x4.myr1c2 = 0.0;
            this.myMatrix4x4.myr1c3 = -xcomp;
            this.myMatrix4x4.myr1c4 = 0.0;

            this.myMatrix4x4.myr2c1 = 0.0;
            this.myMatrix4x4.myr2c2 = 1.0;
            this.myMatrix4x4.myr2c3 = 0.0;
            this.myMatrix4x4.myr2c4 = 0.0;

            this.myMatrix4x4.myr3c1 = xcomp;
            this.myMatrix4x4.myr3c2 = 0.0;
            this.myMatrix4x4.myr3c3 = zcomp;
            this.myMatrix4x4.myr3c4 = 0.0;

            this.myMatrix4x4.myr4c1 = 0.0;
            this.myMatrix4x4.myr4c2 = 0.0;
            this.myMatrix4x4.myr4c3 = 0.0;
            this.myMatrix4x4.myr4c4 = 1.0;
        }

        public static Matrix4x4d GetRotateOnYZPlane(Vector4d vector_in)
        {
            Matrix4x4d mat = new Matrix4x4d();
            mat.SetRotateOnYZPlane(vector_in);
            return mat;
        }

        public void SetRotateFromYZPlaneOnZAxis(Vector4d vector_in)
        {
            double sqrtV = Math.Sqrt(vector_in.X * vector_in.X + vector_in.Z * vector_in.Z);

            this.myMatrix4x4.myr1c1 = 1.0;
            this.myMatrix4x4.myr1c2 = 0.0;
            this.myMatrix4x4.myr1c3 = 0.0;
            this.myMatrix4x4.myr1c4 = 0.0;

            this.myMatrix4x4.myr2c1 = 0.0;
            this.myMatrix4x4.myr2c2 = sqrtV;
            this.myMatrix4x4.myr2c3 = -vector_in.Y;
            this.myMatrix4x4.myr2c4 = 0.0;

            this.myMatrix4x4.myr3c1 = 0.0;
            this.myMatrix4x4.myr3c2 = vector_in.Y;
            this.myMatrix4x4.myr3c3 = sqrtV;
            this.myMatrix4x4.myr3c4 = 0.0;

            this.myMatrix4x4.myr4c1 = 0.0;
            this.myMatrix4x4.myr4c2 = 0.0;
            this.myMatrix4x4.myr4c3 = 0.0;
            this.myMatrix4x4.myr4c4 = 1.0;
        }

        public static Matrix4x4d GetRotateFromYZPlaneOnZAxis(Vector4d vector_in)
        {
            Matrix4x4d mat = new Matrix4x4d();
            mat.SetRotateFromYZPlaneOnZAxis(vector_in);
            return mat;
        }

        #endregion Set / get matrix methods

        #region Arithmetic matrix methods

        public double CalculateDeterminant()
        {
            return
                 (
                //this.myMatrix4x4.myr1c1 * this.SubDeterminant(1,1) -
                        this.myMatrix4x4.myr1c1 *
                                (
                                this.myMatrix4x4.myr2c2 * this.myMatrix4x4.myr3c3 * this.myMatrix4x4.myr4c4 -
                                this.myMatrix4x4.myr2c2 * this.myMatrix4x4.myr3c4 * this.myMatrix4x4.myr4c3 +

                                this.myMatrix4x4.myr2c3 * this.myMatrix4x4.myr3c4 * this.myMatrix4x4.myr4c2 -
                                this.myMatrix4x4.myr2c3 * this.myMatrix4x4.myr3c2 * this.myMatrix4x4.myr4c4 +

                                this.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr3c2 * this.myMatrix4x4.myr4c3 -
                                this.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr3c3 * this.myMatrix4x4.myr4c2
                                ) -

                      //this.myMatrix4x4.myr1c2 * this.SubDeterminant(1,2) + 
                        this.myMatrix4x4.myr1c2 *
                                (
                                this.myMatrix4x4.myr2c1 * this.myMatrix4x4.myr3c3 * this.myMatrix4x4.myr4c4 -
                                this.myMatrix4x4.myr2c1 * this.myMatrix4x4.myr3c4 * this.myMatrix4x4.myr4c3 +

                                this.myMatrix4x4.myr2c3 * this.myMatrix4x4.myr3c4 * this.myMatrix4x4.myr4c1 -
                                this.myMatrix4x4.myr2c3 * this.myMatrix4x4.myr3c1 * this.myMatrix4x4.myr4c4 +

                                this.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr3c1 * this.myMatrix4x4.myr4c3 -
                                this.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr3c3 * this.myMatrix4x4.myr4c1
                                ) +

                      //this.myMatrix4x4.myr1c3 * this.SubDeterminant(1,3) - 
                        this.myMatrix4x4.myr1c3 *
                                (
                                this.myMatrix4x4.myr2c1 * this.myMatrix4x4.myr3c2 * this.myMatrix4x4.myr4c4 -
                                this.myMatrix4x4.myr2c1 * this.myMatrix4x4.myr3c4 * this.myMatrix4x4.myr4c2 +

                                this.myMatrix4x4.myr2c2 * this.myMatrix4x4.myr3c4 * this.myMatrix4x4.myr4c1 -
                                this.myMatrix4x4.myr2c2 * this.myMatrix4x4.myr3c1 * this.myMatrix4x4.myr4c4 +

                                this.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr3c1 * this.myMatrix4x4.myr4c2 -
                                this.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr3c2 * this.myMatrix4x4.myr4c1
                                ) -

                      //this.myMatrix4x4.myr1c4 * this.SubDeterminant(1,4) 
                        this.myMatrix4x4.myr1c4 *
                                (
                                this.myMatrix4x4.myr2c1 * this.myMatrix4x4.myr3c2 * this.myMatrix4x4.myr4c3 -
                                this.myMatrix4x4.myr2c1 * this.myMatrix4x4.myr3c3 * this.myMatrix4x4.myr4c2 +

                                this.myMatrix4x4.myr2c2 * this.myMatrix4x4.myr3c3 * this.myMatrix4x4.myr4c1 -
                                this.myMatrix4x4.myr2c2 * this.myMatrix4x4.myr3c1 * this.myMatrix4x4.myr4c3 +

                                this.myMatrix4x4.myr2c3 * this.myMatrix4x4.myr3c1 * this.myMatrix4x4.myr4c2 -
                                this.myMatrix4x4.myr2c3 * this.myMatrix4x4.myr3c2 * this.myMatrix4x4.myr4c1
                                )
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
                                        this.myMatrix4x4.myr2c2 * this.myMatrix4x4.myr3c3 * this.myMatrix4x4.myr4c4 -
                                        this.myMatrix4x4.myr2c2 * this.myMatrix4x4.myr3c4 * this.myMatrix4x4.myr4c3 +

                                        this.myMatrix4x4.myr2c3 * this.myMatrix4x4.myr3c4 * this.myMatrix4x4.myr4c2 -
                                        this.myMatrix4x4.myr2c3 * this.myMatrix4x4.myr3c2 * this.myMatrix4x4.myr4c4 +

                                        this.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr3c2 * this.myMatrix4x4.myr4c3 -
                                        this.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr3c3 * this.myMatrix4x4.myr4c2
                                        );
                                }
                            case 2:
                                {
                                    return
                                        (
                                        this.myMatrix4x4.myr2c1 * this.myMatrix4x4.myr3c3 * this.myMatrix4x4.myr4c4 -
                                        this.myMatrix4x4.myr2c1 * this.myMatrix4x4.myr3c4 * this.myMatrix4x4.myr4c3 +

                                        this.myMatrix4x4.myr2c3 * this.myMatrix4x4.myr3c4 * this.myMatrix4x4.myr4c1 -
                                        this.myMatrix4x4.myr2c3 * this.myMatrix4x4.myr3c1 * this.myMatrix4x4.myr4c4 +

                                        this.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr3c1 * this.myMatrix4x4.myr4c3 -
                                        this.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr3c3 * this.myMatrix4x4.myr4c1
                                        );
                                }
                            case 3:
                                {
                                    return
                                        (
                                        this.myMatrix4x4.myr2c1 * this.myMatrix4x4.myr3c2 * this.myMatrix4x4.myr4c4 -
                                        this.myMatrix4x4.myr2c1 * this.myMatrix4x4.myr3c4 * this.myMatrix4x4.myr4c2 +

                                        this.myMatrix4x4.myr2c2 * this.myMatrix4x4.myr3c4 * this.myMatrix4x4.myr4c1 -
                                        this.myMatrix4x4.myr2c2 * this.myMatrix4x4.myr3c1 * this.myMatrix4x4.myr4c4 +

                                        this.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr3c1 * this.myMatrix4x4.myr4c2 -
                                        this.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr3c2 * this.myMatrix4x4.myr4c1
                                        );
                                }
                            case 4:
                                {
                                    return
                                        (
                                        this.myMatrix4x4.myr2c1 * this.myMatrix4x4.myr3c2 * this.myMatrix4x4.myr4c3 -
                                        this.myMatrix4x4.myr2c1 * this.myMatrix4x4.myr3c3 * this.myMatrix4x4.myr4c2 +

                                        this.myMatrix4x4.myr2c2 * this.myMatrix4x4.myr3c3 * this.myMatrix4x4.myr4c1 -
                                        this.myMatrix4x4.myr2c2 * this.myMatrix4x4.myr3c1 * this.myMatrix4x4.myr4c3 +

                                        this.myMatrix4x4.myr2c3 * this.myMatrix4x4.myr3c1 * this.myMatrix4x4.myr4c2 -
                                        this.myMatrix4x4.myr2c3 * this.myMatrix4x4.myr3c2 * this.myMatrix4x4.myr4c1
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
                                        this.myMatrix4x4.myr1c2 * this.myMatrix4x4.myr3c3 * this.myMatrix4x4.myr4c4 -
                                        this.myMatrix4x4.myr1c2 * this.myMatrix4x4.myr3c4 * this.myMatrix4x4.myr4c3 +

                                        this.myMatrix4x4.myr1c3 * this.myMatrix4x4.myr3c4 * this.myMatrix4x4.myr4c2 -
                                        this.myMatrix4x4.myr1c3 * this.myMatrix4x4.myr3c2 * this.myMatrix4x4.myr4c4 +

                                        this.myMatrix4x4.myr1c4 * this.myMatrix4x4.myr3c2 * this.myMatrix4x4.myr4c3 -
                                        this.myMatrix4x4.myr1c4 * this.myMatrix4x4.myr3c3 * this.myMatrix4x4.myr4c2
                                        );
                                }
                            case 2:
                                {
                                    return
                                        (
                                        this.myMatrix4x4.myr1c1 * this.myMatrix4x4.myr3c3 * this.myMatrix4x4.myr4c4 -
                                        this.myMatrix4x4.myr1c1 * this.myMatrix4x4.myr3c4 * this.myMatrix4x4.myr4c3 +

                                        this.myMatrix4x4.myr1c3 * this.myMatrix4x4.myr3c4 * this.myMatrix4x4.myr4c1 -
                                        this.myMatrix4x4.myr1c3 * this.myMatrix4x4.myr3c1 * this.myMatrix4x4.myr4c4 +

                                        this.myMatrix4x4.myr1c4 * this.myMatrix4x4.myr3c1 * this.myMatrix4x4.myr4c3 -
                                        this.myMatrix4x4.myr1c4 * this.myMatrix4x4.myr3c3 * this.myMatrix4x4.myr4c1
                                        );
                                }
                            case 3:
                                {
                                    return
                                        (
                                        this.myMatrix4x4.myr1c1 * this.myMatrix4x4.myr3c2 * this.myMatrix4x4.myr4c4 -
                                        this.myMatrix4x4.myr1c1 * this.myMatrix4x4.myr3c4 * this.myMatrix4x4.myr4c2 +

                                        this.myMatrix4x4.myr1c2 * this.myMatrix4x4.myr3c4 * this.myMatrix4x4.myr4c1 -
                                        this.myMatrix4x4.myr1c2 * this.myMatrix4x4.myr3c1 * this.myMatrix4x4.myr4c4 +

                                        this.myMatrix4x4.myr1c4 * this.myMatrix4x4.myr3c1 * this.myMatrix4x4.myr4c2 -
                                        this.myMatrix4x4.myr1c4 * this.myMatrix4x4.myr3c2 * this.myMatrix4x4.myr4c1
                                        );
                                }
                            case 4:
                                {
                                    return
                                        (
                                        this.myMatrix4x4.myr1c1 * this.myMatrix4x4.myr3c2 * this.myMatrix4x4.myr4c3 -
                                        this.myMatrix4x4.myr1c1 * this.myMatrix4x4.myr3c3 * this.myMatrix4x4.myr4c2 +

                                        this.myMatrix4x4.myr1c2 * this.myMatrix4x4.myr3c3 * this.myMatrix4x4.myr4c1 -
                                        this.myMatrix4x4.myr1c2 * this.myMatrix4x4.myr3c1 * this.myMatrix4x4.myr4c3 +

                                        this.myMatrix4x4.myr1c3 * this.myMatrix4x4.myr3c1 * this.myMatrix4x4.myr4c2 -
                                        this.myMatrix4x4.myr1c3 * this.myMatrix4x4.myr3c2 * this.myMatrix4x4.myr4c1
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
                                        this.myMatrix4x4.myr1c2 * this.myMatrix4x4.myr2c3 * this.myMatrix4x4.myr4c4 -
                                        this.myMatrix4x4.myr1c2 * this.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr4c3 +

                                        this.myMatrix4x4.myr1c3 * this.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr4c2 -
                                        this.myMatrix4x4.myr1c3 * this.myMatrix4x4.myr2c2 * this.myMatrix4x4.myr4c4 +

                                        this.myMatrix4x4.myr1c4 * this.myMatrix4x4.myr2c2 * this.myMatrix4x4.myr4c3 -
                                        this.myMatrix4x4.myr1c4 * this.myMatrix4x4.myr2c3 * this.myMatrix4x4.myr4c2
                                        );
                                }
                            case 2:
                                {
                                    return
                                        (
                                        this.myMatrix4x4.myr1c1 * this.myMatrix4x4.myr2c3 * this.myMatrix4x4.myr4c4 -
                                        this.myMatrix4x4.myr1c1 * this.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr4c3 +

                                        this.myMatrix4x4.myr1c3 * this.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr4c1 -
                                        this.myMatrix4x4.myr1c3 * this.myMatrix4x4.myr2c1 * this.myMatrix4x4.myr4c4 +

                                        this.myMatrix4x4.myr1c4 * this.myMatrix4x4.myr2c1 * this.myMatrix4x4.myr4c3 -
                                        this.myMatrix4x4.myr1c4 * this.myMatrix4x4.myr2c3 * this.myMatrix4x4.myr4c1
                                        );
                                }
                            case 3:
                                {
                                    return
                                        (
                                        this.myMatrix4x4.myr1c1 * this.myMatrix4x4.myr2c2 * this.myMatrix4x4.myr4c4 -
                                        this.myMatrix4x4.myr1c1 * this.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr4c2 +

                                        this.myMatrix4x4.myr1c2 * this.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr4c1 -
                                        this.myMatrix4x4.myr1c2 * this.myMatrix4x4.myr2c1 * this.myMatrix4x4.myr4c4 +

                                        this.myMatrix4x4.myr1c4 * this.myMatrix4x4.myr2c1 * this.myMatrix4x4.myr4c2 -
                                        this.myMatrix4x4.myr1c4 * this.myMatrix4x4.myr2c2 * this.myMatrix4x4.myr4c1
                                        );
                                }
                            case 4:
                                {
                                    return
                                        (
                                        this.myMatrix4x4.myr1c1 * this.myMatrix4x4.myr2c2 * this.myMatrix4x4.myr4c3 -
                                        this.myMatrix4x4.myr1c1 * this.myMatrix4x4.myr2c3 * this.myMatrix4x4.myr4c2 +

                                        this.myMatrix4x4.myr1c2 * this.myMatrix4x4.myr2c3 * this.myMatrix4x4.myr4c1 -
                                        this.myMatrix4x4.myr1c2 * this.myMatrix4x4.myr2c1 * this.myMatrix4x4.myr4c3 +

                                        this.myMatrix4x4.myr1c3 * this.myMatrix4x4.myr2c1 * this.myMatrix4x4.myr4c2 -
                                        this.myMatrix4x4.myr1c3 * this.myMatrix4x4.myr2c2 * this.myMatrix4x4.myr4c1
                                        );
                                }
                            default:
                                {
                                    throw new System.ArgumentOutOfRangeException("column_index", "illegal column index " + column_index);
                                }
                        }
                    }

                case 4:
                    {
                        switch (column_index)
                        {
                            case 1:
                                {
                                    return
                                        (
                                        this.myMatrix4x4.myr1c2 * this.myMatrix4x4.myr2c3 * this.myMatrix4x4.myr3c4 -
                                        this.myMatrix4x4.myr1c2 * this.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr3c3 +

                                        this.myMatrix4x4.myr1c3 * this.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr3c2 -
                                        this.myMatrix4x4.myr1c3 * this.myMatrix4x4.myr2c2 * this.myMatrix4x4.myr3c4 +

                                        this.myMatrix4x4.myr1c4 * this.myMatrix4x4.myr2c2 * this.myMatrix4x4.myr3c3 -
                                        this.myMatrix4x4.myr1c4 * this.myMatrix4x4.myr2c3 * this.myMatrix4x4.myr3c2
                                        );
                                }
                            case 2:
                                {
                                    return
                                        (
                                        this.myMatrix4x4.myr1c1 * this.myMatrix4x4.myr2c3 * this.myMatrix4x4.myr3c4 -
                                        this.myMatrix4x4.myr1c1 * this.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr3c3 +

                                        this.myMatrix4x4.myr1c3 * this.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr3c1 -
                                        this.myMatrix4x4.myr1c3 * this.myMatrix4x4.myr2c1 * this.myMatrix4x4.myr3c4 +

                                        this.myMatrix4x4.myr1c4 * this.myMatrix4x4.myr2c1 * this.myMatrix4x4.myr3c3 -
                                        this.myMatrix4x4.myr1c4 * this.myMatrix4x4.myr2c3 * this.myMatrix4x4.myr3c1
                                        );
                                }
                            case 3:
                                {
                                    return
                                        (
                                        this.myMatrix4x4.myr1c1 * this.myMatrix4x4.myr2c2 * this.myMatrix4x4.myr3c4 -
                                        this.myMatrix4x4.myr1c1 * this.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr3c2 +

                                        this.myMatrix4x4.myr1c2 * this.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr3c1 -
                                        this.myMatrix4x4.myr1c2 * this.myMatrix4x4.myr2c1 * this.myMatrix4x4.myr3c4 +

                                        this.myMatrix4x4.myr1c4 * this.myMatrix4x4.myr2c1 * this.myMatrix4x4.myr3c2 -
                                        this.myMatrix4x4.myr1c4 * this.myMatrix4x4.myr2c2 * this.myMatrix4x4.myr3c1
                                        );
                                }
                            case 4:
                                {
                                    return
                                        (
                                        this.myMatrix4x4.myr1c1 * this.myMatrix4x4.myr2c2 * this.myMatrix4x4.myr3c3 -
                                        this.myMatrix4x4.myr1c1 * this.myMatrix4x4.myr2c3 * this.myMatrix4x4.myr3c2 +

                                        this.myMatrix4x4.myr1c2 * this.myMatrix4x4.myr2c3 * this.myMatrix4x4.myr3c1 -
                                        this.myMatrix4x4.myr1c2 * this.myMatrix4x4.myr2c1 * this.myMatrix4x4.myr3c3 +

                                        this.myMatrix4x4.myr1c3 * this.myMatrix4x4.myr2c1 * this.myMatrix4x4.myr3c2 -
                                        this.myMatrix4x4.myr1c3 * this.myMatrix4x4.myr2c2 * this.myMatrix4x4.myr3c1
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

        public void Inverse(Matrix4x4d matrix_in)
        {
            //calculateDeterminant
            // double det = matrix_in.CalculateDeterminant();
            double det =
            (
                //matrix_in.myMatrix4x4.myr1c1 * matrix_in.SubDeterminant(1,1) -
                matrix_in.myMatrix4x4.myr1c1 *
                (
                matrix_in.myMatrix4x4.myr2c2 * matrix_in.myMatrix4x4.myr3c3 * matrix_in.myMatrix4x4.myr4c4 -
                matrix_in.myMatrix4x4.myr2c2 * matrix_in.myMatrix4x4.myr3c4 * matrix_in.myMatrix4x4.myr4c3 +

                matrix_in.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr3c4 * matrix_in.myMatrix4x4.myr4c2 -
                matrix_in.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr3c2 * matrix_in.myMatrix4x4.myr4c4 +

                matrix_in.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr3c2 * matrix_in.myMatrix4x4.myr4c3 -
                matrix_in.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr3c3 * matrix_in.myMatrix4x4.myr4c2
                ) -

                //matrix_in.myMatrix4x4.myr1c2 * matrix_in.SubDeterminant(1,2) + 
                matrix_in.myMatrix4x4.myr1c2 *
                (
                matrix_in.myMatrix4x4.myr2c1 * matrix_in.myMatrix4x4.myr3c3 * matrix_in.myMatrix4x4.myr4c4 -
                matrix_in.myMatrix4x4.myr2c1 * matrix_in.myMatrix4x4.myr3c4 * matrix_in.myMatrix4x4.myr4c3 +

                matrix_in.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr3c4 * matrix_in.myMatrix4x4.myr4c1 -
                matrix_in.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr3c1 * matrix_in.myMatrix4x4.myr4c4 +

                matrix_in.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr3c1 * matrix_in.myMatrix4x4.myr4c3 -
                matrix_in.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr3c3 * matrix_in.myMatrix4x4.myr4c1
                ) +

                //matrix_in.myMatrix4x4.myr1c3 * matrix_in.SubDeterminant(1,3) - 
                matrix_in.myMatrix4x4.myr1c3 *
                (
                matrix_in.myMatrix4x4.myr2c1 * matrix_in.myMatrix4x4.myr3c2 * matrix_in.myMatrix4x4.myr4c4 -
                matrix_in.myMatrix4x4.myr2c1 * matrix_in.myMatrix4x4.myr3c4 * matrix_in.myMatrix4x4.myr4c2 +

                matrix_in.myMatrix4x4.myr2c2 * matrix_in.myMatrix4x4.myr3c4 * matrix_in.myMatrix4x4.myr4c1 -
                matrix_in.myMatrix4x4.myr2c2 * matrix_in.myMatrix4x4.myr3c1 * matrix_in.myMatrix4x4.myr4c4 +

                matrix_in.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr3c1 * matrix_in.myMatrix4x4.myr4c2 -
                matrix_in.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr3c2 * matrix_in.myMatrix4x4.myr4c1
                ) -

                //matrix_in.myMatrix4x4.myr1c4 * matrix_in.SubDeterminant(1,4) 
                matrix_in.myMatrix4x4.myr1c4 *
                (
                matrix_in.myMatrix4x4.myr2c1 * matrix_in.myMatrix4x4.myr3c2 * matrix_in.myMatrix4x4.myr4c3 -
                matrix_in.myMatrix4x4.myr2c1 * matrix_in.myMatrix4x4.myr3c3 * matrix_in.myMatrix4x4.myr4c2 +

                matrix_in.myMatrix4x4.myr2c2 * matrix_in.myMatrix4x4.myr3c3 * matrix_in.myMatrix4x4.myr4c1 -
                matrix_in.myMatrix4x4.myr2c2 * matrix_in.myMatrix4x4.myr3c1 * matrix_in.myMatrix4x4.myr4c3 +

                matrix_in.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr3c1 * matrix_in.myMatrix4x4.myr4c2 -
                matrix_in.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr3c2 * matrix_in.myMatrix4x4.myr4c1
                )
                );

            // exception if matrix is singular
            if (det == 0.0)
            {
                throw new System.ArgumentOutOfRangeException("matrix_in", "The specified Matrix4x4 is singular. The value of determinant is: " + det + ".");
            }
            else
            {
                //this.myMatrix4x4.myr1c1 =  matrix_in.SubDeterminant(1,1) / det;
                this.myMatrix4x4.myr1c1 =
                    (
                    matrix_in.myMatrix4x4.myr2c2 * matrix_in.myMatrix4x4.myr3c3 * matrix_in.myMatrix4x4.myr4c4 -
                    matrix_in.myMatrix4x4.myr2c2 * matrix_in.myMatrix4x4.myr3c4 * matrix_in.myMatrix4x4.myr4c3 +

                    matrix_in.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr3c4 * matrix_in.myMatrix4x4.myr4c2 -
                    matrix_in.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr3c2 * matrix_in.myMatrix4x4.myr4c4 +

                    matrix_in.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr3c2 * matrix_in.myMatrix4x4.myr4c3 -
                    matrix_in.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr3c3 * matrix_in.myMatrix4x4.myr4c2
                    )
              / det;

                //this.myMatrix4x4.myr2c1 = -matrix_in.SubDeterminant(1,2) / det;
                this.myMatrix4x4.myr2c1 =
                    -(
                    matrix_in.myMatrix4x4.myr2c1 * matrix_in.myMatrix4x4.myr3c3 * matrix_in.myMatrix4x4.myr4c4 -
                    matrix_in.myMatrix4x4.myr2c1 * matrix_in.myMatrix4x4.myr3c4 * matrix_in.myMatrix4x4.myr4c3 +

                    matrix_in.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr3c4 * matrix_in.myMatrix4x4.myr4c1 -
                    matrix_in.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr3c1 * matrix_in.myMatrix4x4.myr4c4 +

                    matrix_in.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr3c1 * matrix_in.myMatrix4x4.myr4c3 -
                    matrix_in.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr3c3 * matrix_in.myMatrix4x4.myr4c1
                    )
              / det;

                //this.myMatrix4x4.myr3c1 =  matrix_in.SubDeterminant(1,3) / det;
                this.myMatrix4x4.myr3c1 =
                    (
                    matrix_in.myMatrix4x4.myr2c1 * matrix_in.myMatrix4x4.myr3c2 * matrix_in.myMatrix4x4.myr4c4 -
                    matrix_in.myMatrix4x4.myr2c1 * matrix_in.myMatrix4x4.myr3c4 * matrix_in.myMatrix4x4.myr4c2 +

                    matrix_in.myMatrix4x4.myr2c2 * matrix_in.myMatrix4x4.myr3c4 * matrix_in.myMatrix4x4.myr4c1 -
                    matrix_in.myMatrix4x4.myr2c2 * matrix_in.myMatrix4x4.myr3c1 * matrix_in.myMatrix4x4.myr4c4 +

                    matrix_in.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr3c1 * matrix_in.myMatrix4x4.myr4c2 -
                    matrix_in.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr3c2 * matrix_in.myMatrix4x4.myr4c1
                    )
              / det;

                //this.myMatrix4x4.myr4c1 = -matrix_in.SubDeterminant(1,4) / det;
                this.myMatrix4x4.myr4c1 =
                    -(
                    matrix_in.myMatrix4x4.myr2c1 * matrix_in.myMatrix4x4.myr3c2 * matrix_in.myMatrix4x4.myr4c3 -
                    matrix_in.myMatrix4x4.myr2c1 * matrix_in.myMatrix4x4.myr3c3 * matrix_in.myMatrix4x4.myr4c2 +

                    matrix_in.myMatrix4x4.myr2c2 * matrix_in.myMatrix4x4.myr3c3 * matrix_in.myMatrix4x4.myr4c1 -
                    matrix_in.myMatrix4x4.myr2c2 * matrix_in.myMatrix4x4.myr3c1 * matrix_in.myMatrix4x4.myr4c3 +

                    matrix_in.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr3c1 * matrix_in.myMatrix4x4.myr4c2 -
                    matrix_in.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr3c2 * matrix_in.myMatrix4x4.myr4c1
                    )
              / det;


                //this.myMatrix4x4.myr1c2 = -matrix_in.SubDeterminant(2,1) / det;
                this.myMatrix4x4.myr1c2 =
                    -(
                    matrix_in.myMatrix4x4.myr1c2 * matrix_in.myMatrix4x4.myr3c3 * matrix_in.myMatrix4x4.myr4c4 -
                    matrix_in.myMatrix4x4.myr1c2 * matrix_in.myMatrix4x4.myr3c4 * matrix_in.myMatrix4x4.myr4c3 +

                    matrix_in.myMatrix4x4.myr1c3 * matrix_in.myMatrix4x4.myr3c4 * matrix_in.myMatrix4x4.myr4c2 -
                    matrix_in.myMatrix4x4.myr1c3 * matrix_in.myMatrix4x4.myr3c2 * matrix_in.myMatrix4x4.myr4c4 +

                    matrix_in.myMatrix4x4.myr1c4 * matrix_in.myMatrix4x4.myr3c2 * matrix_in.myMatrix4x4.myr4c3 -
                    matrix_in.myMatrix4x4.myr1c4 * matrix_in.myMatrix4x4.myr3c3 * matrix_in.myMatrix4x4.myr4c2
                    )
              / det;


                //this.myMatrix4x4.myr2c2 =  matrix_in.SubDeterminant(2,2) / det;
                this.myMatrix4x4.myr2c2 =
                    (
                    matrix_in.myMatrix4x4.myr1c1 * matrix_in.myMatrix4x4.myr3c3 * matrix_in.myMatrix4x4.myr4c4 -
                    matrix_in.myMatrix4x4.myr1c1 * matrix_in.myMatrix4x4.myr3c4 * matrix_in.myMatrix4x4.myr4c3 +

                    matrix_in.myMatrix4x4.myr1c3 * matrix_in.myMatrix4x4.myr3c4 * matrix_in.myMatrix4x4.myr4c1 -
                    matrix_in.myMatrix4x4.myr1c3 * matrix_in.myMatrix4x4.myr3c1 * matrix_in.myMatrix4x4.myr4c4 +

                    matrix_in.myMatrix4x4.myr1c4 * matrix_in.myMatrix4x4.myr3c1 * matrix_in.myMatrix4x4.myr4c3 -
                    matrix_in.myMatrix4x4.myr1c4 * matrix_in.myMatrix4x4.myr3c3 * matrix_in.myMatrix4x4.myr4c1
                    )
              / det;

                //this.myMatrix4x4.myr3c2 = -matrix_in.SubDeterminant(2,3) / det;
                this.myMatrix4x4.myr3c2 =
                    -(
                    matrix_in.myMatrix4x4.myr1c1 * matrix_in.myMatrix4x4.myr3c2 * matrix_in.myMatrix4x4.myr4c4 -
                    matrix_in.myMatrix4x4.myr1c1 * matrix_in.myMatrix4x4.myr3c4 * matrix_in.myMatrix4x4.myr4c2 +

                    matrix_in.myMatrix4x4.myr1c2 * matrix_in.myMatrix4x4.myr3c4 * matrix_in.myMatrix4x4.myr4c1 -
                    matrix_in.myMatrix4x4.myr1c2 * matrix_in.myMatrix4x4.myr3c1 * matrix_in.myMatrix4x4.myr4c4 +

                    matrix_in.myMatrix4x4.myr1c4 * matrix_in.myMatrix4x4.myr3c1 * matrix_in.myMatrix4x4.myr4c2 -
                    matrix_in.myMatrix4x4.myr1c4 * matrix_in.myMatrix4x4.myr3c2 * matrix_in.myMatrix4x4.myr4c1
                    )
              / det;

                //this.myMatrix4x4.myr4c2 =  matrix_in.SubDeterminant(2,4) / det;
                this.myMatrix4x4.myr4c2 =
                    (
                    matrix_in.myMatrix4x4.myr1c1 * matrix_in.myMatrix4x4.myr3c2 * matrix_in.myMatrix4x4.myr4c3 -
                    matrix_in.myMatrix4x4.myr1c1 * matrix_in.myMatrix4x4.myr3c3 * matrix_in.myMatrix4x4.myr4c2 +

                    matrix_in.myMatrix4x4.myr1c2 * matrix_in.myMatrix4x4.myr3c3 * matrix_in.myMatrix4x4.myr4c1 -
                    matrix_in.myMatrix4x4.myr1c2 * matrix_in.myMatrix4x4.myr3c1 * matrix_in.myMatrix4x4.myr4c3 +

                    matrix_in.myMatrix4x4.myr1c3 * matrix_in.myMatrix4x4.myr3c1 * matrix_in.myMatrix4x4.myr4c2 -
                    matrix_in.myMatrix4x4.myr1c3 * matrix_in.myMatrix4x4.myr3c2 * matrix_in.myMatrix4x4.myr4c1
                    )
              / det;


                //this.myMatrix4x4.myr1c3 =  matrix_in.SubDeterminant(3,1) / det;
                this.myMatrix4x4.myr1c3 =
                    (
                    matrix_in.myMatrix4x4.myr1c2 * matrix_in.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr4c4 -
                    matrix_in.myMatrix4x4.myr1c2 * matrix_in.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr4c3 +

                    matrix_in.myMatrix4x4.myr1c3 * matrix_in.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr4c2 -
                    matrix_in.myMatrix4x4.myr1c3 * matrix_in.myMatrix4x4.myr2c2 * matrix_in.myMatrix4x4.myr4c4 +

                    matrix_in.myMatrix4x4.myr1c4 * matrix_in.myMatrix4x4.myr2c2 * matrix_in.myMatrix4x4.myr4c3 -
                    matrix_in.myMatrix4x4.myr1c4 * matrix_in.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr4c2
                    )
              / det;

                //this.myMatrix4x4.myr2c3 = -matrix_in.SubDeterminant(3,2) / det;
                this.myMatrix4x4.myr2c3 =
                    -(
                    matrix_in.myMatrix4x4.myr1c1 * matrix_in.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr4c4 -
                    matrix_in.myMatrix4x4.myr1c1 * matrix_in.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr4c3 +

                    matrix_in.myMatrix4x4.myr1c3 * matrix_in.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr4c1 -
                    matrix_in.myMatrix4x4.myr1c3 * matrix_in.myMatrix4x4.myr2c1 * matrix_in.myMatrix4x4.myr4c4 +

                    matrix_in.myMatrix4x4.myr1c4 * matrix_in.myMatrix4x4.myr2c1 * matrix_in.myMatrix4x4.myr4c3 -
                    matrix_in.myMatrix4x4.myr1c4 * matrix_in.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr4c1
                    )
              / det;

                //this.myMatrix4x4.myr3c3 =  matrix_in.SubDeterminant(3,3) / det;
                this.myMatrix4x4.myr3c3 =
                    (
                    matrix_in.myMatrix4x4.myr1c1 * matrix_in.myMatrix4x4.myr2c2 * matrix_in.myMatrix4x4.myr4c4 -
                    matrix_in.myMatrix4x4.myr1c1 * matrix_in.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr4c2 +

                    matrix_in.myMatrix4x4.myr1c2 * matrix_in.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr4c1 -
                    matrix_in.myMatrix4x4.myr1c2 * matrix_in.myMatrix4x4.myr2c1 * matrix_in.myMatrix4x4.myr4c4 +

                    matrix_in.myMatrix4x4.myr1c4 * matrix_in.myMatrix4x4.myr2c1 * matrix_in.myMatrix4x4.myr4c2 -
                    matrix_in.myMatrix4x4.myr1c4 * matrix_in.myMatrix4x4.myr2c2 * matrix_in.myMatrix4x4.myr4c1
                    )

                    / det;

                //this.myMatrix4x4.myr4c3 = -matrix_in.SubDeterminant(3,4) / det;
                this.myMatrix4x4.myr4c3 =
                    -(
                    matrix_in.myMatrix4x4.myr1c1 * matrix_in.myMatrix4x4.myr2c2 * matrix_in.myMatrix4x4.myr4c3 -
                    matrix_in.myMatrix4x4.myr1c1 * matrix_in.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr4c2 +

                    matrix_in.myMatrix4x4.myr1c2 * matrix_in.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr4c1 -
                    matrix_in.myMatrix4x4.myr1c2 * matrix_in.myMatrix4x4.myr2c1 * matrix_in.myMatrix4x4.myr4c3 +

                    matrix_in.myMatrix4x4.myr1c3 * matrix_in.myMatrix4x4.myr2c1 * matrix_in.myMatrix4x4.myr4c2 -
                    matrix_in.myMatrix4x4.myr1c3 * matrix_in.myMatrix4x4.myr2c2 * matrix_in.myMatrix4x4.myr4c1
                    )
              / det;


                //this.myMatrix4x4.myr1c4 = -matrix_in.SubDeterminant(4,1) / det;
                this.myMatrix4x4.myr1c4 =
                    -(
                    matrix_in.myMatrix4x4.myr1c2 * matrix_in.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr3c4 -
                    matrix_in.myMatrix4x4.myr1c2 * matrix_in.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr3c3 +

                    matrix_in.myMatrix4x4.myr1c3 * matrix_in.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr3c2 -
                    matrix_in.myMatrix4x4.myr1c3 * matrix_in.myMatrix4x4.myr2c2 * matrix_in.myMatrix4x4.myr3c4 +

                    matrix_in.myMatrix4x4.myr1c4 * matrix_in.myMatrix4x4.myr2c2 * matrix_in.myMatrix4x4.myr3c3 -
                    matrix_in.myMatrix4x4.myr1c4 * matrix_in.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr3c2
                    )
              / det;

                //this.myMatrix4x4.myr2c4 =  matrix_in.SubDeterminant(4,2) / det;
                this.myMatrix4x4.myr2c4 =
                    (
                    matrix_in.myMatrix4x4.myr1c1 * matrix_in.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr3c4 -
                    matrix_in.myMatrix4x4.myr1c1 * matrix_in.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr3c3 +

                    matrix_in.myMatrix4x4.myr1c3 * matrix_in.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr3c1 -
                    matrix_in.myMatrix4x4.myr1c3 * matrix_in.myMatrix4x4.myr2c1 * matrix_in.myMatrix4x4.myr3c4 +

                    matrix_in.myMatrix4x4.myr1c4 * matrix_in.myMatrix4x4.myr2c1 * matrix_in.myMatrix4x4.myr3c3 -
                    matrix_in.myMatrix4x4.myr1c4 * matrix_in.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr3c1
                    )
              / det;

                //this.myMatrix4x4.myr3c4 = -matrix_in.SubDeterminant(4,3) / det;
                this.myMatrix4x4.myr3c4 =
                    -(
                    matrix_in.myMatrix4x4.myr1c1 * matrix_in.myMatrix4x4.myr2c2 * matrix_in.myMatrix4x4.myr3c4 -
                    matrix_in.myMatrix4x4.myr1c1 * matrix_in.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr3c2 +

                    matrix_in.myMatrix4x4.myr1c2 * matrix_in.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr3c1 -
                    matrix_in.myMatrix4x4.myr1c2 * matrix_in.myMatrix4x4.myr2c1 * matrix_in.myMatrix4x4.myr3c4 +

                    matrix_in.myMatrix4x4.myr1c4 * matrix_in.myMatrix4x4.myr2c1 * matrix_in.myMatrix4x4.myr3c2 -
                    matrix_in.myMatrix4x4.myr1c4 * matrix_in.myMatrix4x4.myr2c2 * matrix_in.myMatrix4x4.myr3c1
                    )
              / det;

                //this.myMatrix4x4.myr4c4 =  matrix_in.SubDeterminant(4,4) / det;
                this.myMatrix4x4.myr4c4 =
                    (
                    matrix_in.myMatrix4x4.myr1c1 * matrix_in.myMatrix4x4.myr2c2 * matrix_in.myMatrix4x4.myr3c3 -
                    matrix_in.myMatrix4x4.myr1c1 * matrix_in.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr3c2 +

                    matrix_in.myMatrix4x4.myr1c2 * matrix_in.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr3c1 -
                    matrix_in.myMatrix4x4.myr1c2 * matrix_in.myMatrix4x4.myr2c1 * matrix_in.myMatrix4x4.myr3c3 +

                    matrix_in.myMatrix4x4.myr1c3 * matrix_in.myMatrix4x4.myr2c1 * matrix_in.myMatrix4x4.myr3c2 -
                    matrix_in.myMatrix4x4.myr1c3 * matrix_in.myMatrix4x4.myr2c2 * matrix_in.myMatrix4x4.myr3c1
                    )
              / det;

            }
        }

        public void Inverse()
        {
            Matrix4x4d matrix = new Matrix4x4d(this);
            this.Inverse(matrix);
        }

        public static Matrix4x4d GetInverseMatrix(Matrix4x4d mat_in)
        {
            Matrix4x4d mat = new Matrix4x4d();
            mat.Inverse(mat_in);
            return mat;
        }

        public void Transpose(Matrix4x4d mat_in)
        {
            this.myMatrix4x4.myr1c1 = mat_in.myMatrix4x4.myr1c1;
            this.myMatrix4x4.myr1c2 = mat_in.myMatrix4x4.myr2c1;
            this.myMatrix4x4.myr1c3 = mat_in.myMatrix4x4.myr3c1;
            this.myMatrix4x4.myr1c4 = mat_in.myMatrix4x4.myr4c1;

            this.myMatrix4x4.myr2c1 = mat_in.myMatrix4x4.myr1c2;
            this.myMatrix4x4.myr2c2 = mat_in.myMatrix4x4.myr2c2;
            this.myMatrix4x4.myr2c3 = mat_in.myMatrix4x4.myr3c2;
            this.myMatrix4x4.myr2c4 = mat_in.myMatrix4x4.myr4c2;

            this.myMatrix4x4.myr3c1 = mat_in.myMatrix4x4.myr1c3;
            this.myMatrix4x4.myr3c2 = mat_in.myMatrix4x4.myr2c3;
            this.myMatrix4x4.myr3c3 = mat_in.myMatrix4x4.myr3c3;
            this.myMatrix4x4.myr3c4 = mat_in.myMatrix4x4.myr4c3;

            this.myMatrix4x4.myr4c1 = mat_in.myMatrix4x4.myr1c4;
            this.myMatrix4x4.myr4c2 = mat_in.myMatrix4x4.myr2c4;
            this.myMatrix4x4.myr4c3 = mat_in.myMatrix4x4.myr3c4;
            this.myMatrix4x4.myr4c4 = mat_in.myMatrix4x4.myr4c4;
        }

        public void Transpose()
        {
            double temp;

            temp = this.myMatrix4x4.myr1c2;
            this.myMatrix4x4.myr1c2 = this.myMatrix4x4.myr2c1;
            this.myMatrix4x4.myr2c1 = temp;

            temp = this.myMatrix4x4.myr1c3;
            this.myMatrix4x4.myr1c3 = this.myMatrix4x4.myr3c1;
            this.myMatrix4x4.myr3c1 = temp;

            temp = this.myMatrix4x4.myr1c4;
            this.myMatrix4x4.myr1c4 = this.myMatrix4x4.myr4c1;
            this.myMatrix4x4.myr4c1 = temp;


            temp = this.myMatrix4x4.myr2c3;
            this.myMatrix4x4.myr2c3 = this.myMatrix4x4.myr3c2;
            this.myMatrix4x4.myr3c2 = temp;

            temp = this.myMatrix4x4.myr2c4;
            this.myMatrix4x4.myr2c4 = this.myMatrix4x4.myr4c2;
            this.myMatrix4x4.myr4c2 = temp;


            temp = this.myMatrix4x4.myr3c4;
            this.myMatrix4x4.myr3c4 = this.myMatrix4x4.myr4c3;
            this.myMatrix4x4.myr4c3 = temp;

            // r1c1, r2c2, r3c3, r4c4 keep unchanged
        }

        public static Matrix4x4d GetTransposedMatrix(Matrix4x4d mat_in)
        {
            Matrix4x4d mat = new Matrix4x4d();
            mat.Transpose(mat_in);
            return mat;
        }

        public Vector4d MultiplyWithColumnVector(Vector4d column_vector)
        {

            Vector4d result_vector = new Vector4d();

            result_vector.X =
                this.myMatrix4x4.myr1c1 * column_vector.X +
                this.myMatrix4x4.myr1c2 * column_vector.Y +
                this.myMatrix4x4.myr1c3 * column_vector.Z +
                this.myMatrix4x4.myr1c4 * column_vector.W;

            result_vector.Y =
                this.myMatrix4x4.myr2c1 * column_vector.X +
                this.myMatrix4x4.myr2c2 * column_vector.Y +
                this.myMatrix4x4.myr2c3 * column_vector.Z +
                this.myMatrix4x4.myr2c4 * column_vector.W;

            result_vector.Z =
                this.myMatrix4x4.myr3c1 * column_vector.X +
                this.myMatrix4x4.myr3c2 * column_vector.Y +
                this.myMatrix4x4.myr3c3 * column_vector.Z +
                this.myMatrix4x4.myr3c4 * column_vector.W;

            result_vector.W =
                this.myMatrix4x4.myr4c1 * column_vector.X +
                this.myMatrix4x4.myr4c2 * column_vector.Y +
                this.myMatrix4x4.myr4c3 * column_vector.Z +
                this.myMatrix4x4.myr4c4 * column_vector.W;

            return result_vector;
        }

        public Vector4d MultiplyWithRowVector(Vector4d row_vector)
        {

            Vector4d result_vector = new Vector4d();

            result_vector.X =
                this.myMatrix4x4.myr1c1 * row_vector.X +
                this.myMatrix4x4.myr2c1 * row_vector.Y +
                this.myMatrix4x4.myr3c1 * row_vector.Z +
                this.myMatrix4x4.myr4c1 * row_vector.W;

            result_vector.Y =
                this.myMatrix4x4.myr1c2 * row_vector.X +
                this.myMatrix4x4.myr2c2 * row_vector.Y +
                this.myMatrix4x4.myr3c2 * row_vector.Z +
                this.myMatrix4x4.myr4c2 * row_vector.W;

            result_vector.Z =
                this.myMatrix4x4.myr1c3 * row_vector.X +
                this.myMatrix4x4.myr2c3 * row_vector.Y +
                this.myMatrix4x4.myr3c3 * row_vector.Z +
                this.myMatrix4x4.myr4c3 * row_vector.W;

            result_vector.W =
                this.myMatrix4x4.myr1c4 * row_vector.X +
                this.myMatrix4x4.myr2c4 * row_vector.Y +
                this.myMatrix4x4.myr3c4 * row_vector.Z +
                this.myMatrix4x4.myr4c4 * row_vector.W;

            return result_vector;
        }

        public void MultiplyWithMatrixOnRight(Matrix4x4d matrix_in)
        {
            double r1c1 = this.myMatrix4x4.myr1c1;
            double r1c2 = this.myMatrix4x4.myr1c2;
            double r1c3 = this.myMatrix4x4.myr1c3;
            //double r1c4 = this.myMatrix4x4.myr1c4

            double r2c1 = this.myMatrix4x4.myr2c1;
            double r2c2 = this.myMatrix4x4.myr2c2;
            double r2c3 = this.myMatrix4x4.myr2c3;
            //double r2c4 = this.myMatrix4x4.myr2c4;

            double r3c1 = this.myMatrix4x4.myr3c1;
            double r3c2 = this.myMatrix4x4.myr3c2;
            double r3c3 = this.myMatrix4x4.myr3c3;
            //double r3c4 = this.myMatrix4x4.myr3c4;

            double r4c1 = this.myMatrix4x4.myr4c1;
            double r4c2 = this.myMatrix4x4.myr4c2;
            double r4c3 = this.myMatrix4x4.myr4c3;
            //double r4c4 = this.myMatrix4x4.myr4c4;

            // calculate row 1
            this.myMatrix4x4.myr1c1 =

                                   r1c1 * matrix_in.myMatrix4x4.myr1c1 +
                this.myMatrix4x4.myr1c2 * matrix_in.myMatrix4x4.myr2c1 +
                this.myMatrix4x4.myr1c3 * matrix_in.myMatrix4x4.myr3c1 +
                this.myMatrix4x4.myr1c4 * matrix_in.myMatrix4x4.myr4c1;

            this.myMatrix4x4.myr1c2 =

                                   r1c1 * matrix_in.myMatrix4x4.myr1c2 +
                                   r1c2 * matrix_in.myMatrix4x4.myr2c2 +
                this.myMatrix4x4.myr1c3 * matrix_in.myMatrix4x4.myr3c2 +
                this.myMatrix4x4.myr1c4 * matrix_in.myMatrix4x4.myr4c2;

            this.myMatrix4x4.myr1c3 =

                                   r1c1 * matrix_in.myMatrix4x4.myr1c3 +
                                   r1c2 * matrix_in.myMatrix4x4.myr2c3 +
                                   r1c3 * matrix_in.myMatrix4x4.myr3c3 +
                this.myMatrix4x4.myr1c4 * matrix_in.myMatrix4x4.myr4c3;

            this.myMatrix4x4.myr1c4 =

                                   r1c1 * matrix_in.myMatrix4x4.myr1c4 +
                                   r1c2 * matrix_in.myMatrix4x4.myr2c4 +
                                   r1c3 * matrix_in.myMatrix4x4.myr3c4 +
                this.myMatrix4x4.myr1c4 * matrix_in.myMatrix4x4.myr4c4;

            // calculate row 2
            this.myMatrix4x4.myr2c1 =

                                   r2c1 * matrix_in.myMatrix4x4.myr1c1 +
                this.myMatrix4x4.myr2c2 * matrix_in.myMatrix4x4.myr2c1 +
                this.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr3c1 +
                this.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr4c1;

            this.myMatrix4x4.myr2c2 =

                                   r2c1 * matrix_in.myMatrix4x4.myr1c2 +
                                   r2c2 * matrix_in.myMatrix4x4.myr2c2 +
                this.myMatrix4x4.myr2c3 * matrix_in.myMatrix4x4.myr3c2 +
                this.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr4c2;

            this.myMatrix4x4.myr2c3 =

                                   r2c1 * matrix_in.myMatrix4x4.myr1c3 +
                                   r2c2 * matrix_in.myMatrix4x4.myr2c3 +
                                   r2c3 * matrix_in.myMatrix4x4.myr3c3 +
                this.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr4c3;

            this.myMatrix4x4.myr2c4 =

                                   r2c1 * matrix_in.myMatrix4x4.myr1c4 +
                                   r2c2 * matrix_in.myMatrix4x4.myr2c4 +
                                   r2c3 * matrix_in.myMatrix4x4.myr3c4 +
                this.myMatrix4x4.myr2c4 * matrix_in.myMatrix4x4.myr4c4;

            // calculate row 3
            this.myMatrix4x4.myr3c1 =

                                   r3c1 * matrix_in.myMatrix4x4.myr1c1 +
                this.myMatrix4x4.myr3c2 * matrix_in.myMatrix4x4.myr2c1 +
                this.myMatrix4x4.myr3c3 * matrix_in.myMatrix4x4.myr3c1 +
                this.myMatrix4x4.myr3c4 * matrix_in.myMatrix4x4.myr4c1;

            this.myMatrix4x4.myr3c2 =

                                   r3c1 * matrix_in.myMatrix4x4.myr1c2 +
                                   r3c2 * matrix_in.myMatrix4x4.myr2c2 +
                this.myMatrix4x4.myr3c3 * matrix_in.myMatrix4x4.myr3c2 +
                this.myMatrix4x4.myr3c4 * matrix_in.myMatrix4x4.myr4c2;

            this.myMatrix4x4.myr3c3 =

                                   r3c1 * matrix_in.myMatrix4x4.myr1c3 +
                                   r3c2 * matrix_in.myMatrix4x4.myr2c3 +
                                   r3c3 * matrix_in.myMatrix4x4.myr3c3 +
                this.myMatrix4x4.myr3c4 * matrix_in.myMatrix4x4.myr4c3;

            this.myMatrix4x4.myr3c4 =

                                   r3c1 * matrix_in.myMatrix4x4.myr1c4 +
                                   r3c2 * matrix_in.myMatrix4x4.myr2c4 +
                                   r3c3 * matrix_in.myMatrix4x4.myr3c4 +
                this.myMatrix4x4.myr3c4 * matrix_in.myMatrix4x4.myr4c4;

            // calculate row 4
            this.myMatrix4x4.myr4c1 =

                                   r4c1 * matrix_in.myMatrix4x4.myr1c1 +
                this.myMatrix4x4.myr4c2 * matrix_in.myMatrix4x4.myr2c1 +
                this.myMatrix4x4.myr4c3 * matrix_in.myMatrix4x4.myr3c1 +
                this.myMatrix4x4.myr4c4 * matrix_in.myMatrix4x4.myr4c1;

            this.myMatrix4x4.myr4c2 =

                                   r4c1 * matrix_in.myMatrix4x4.myr1c2 +
                                   r4c2 * matrix_in.myMatrix4x4.myr2c2 +
                this.myMatrix4x4.myr4c3 * matrix_in.myMatrix4x4.myr3c2 +
                this.myMatrix4x4.myr4c4 * matrix_in.myMatrix4x4.myr4c2;

            this.myMatrix4x4.myr4c3 =

                                   r4c1 * matrix_in.myMatrix4x4.myr1c3 +
                                   r4c2 * matrix_in.myMatrix4x4.myr2c3 +
                                   r4c3 * matrix_in.myMatrix4x4.myr3c3 +
                this.myMatrix4x4.myr4c4 * matrix_in.myMatrix4x4.myr4c3;

            this.myMatrix4x4.myr4c4 =

                                   r4c1 * matrix_in.myMatrix4x4.myr1c4 +
                                   r4c2 * matrix_in.myMatrix4x4.myr2c4 +
                                   r4c3 * matrix_in.myMatrix4x4.myr3c4 +
                this.myMatrix4x4.myr4c4 * matrix_in.myMatrix4x4.myr4c4;
        }

        public void MultiplyWithMatrixOnLeft(Matrix4x4d matrix_in)
        {
            double r1c1 = this.myMatrix4x4.myr1c1;
            double r1c2 = this.myMatrix4x4.myr1c2;
            double r1c3 = this.myMatrix4x4.myr1c3;
            double r1c4 = this.myMatrix4x4.myr1c4;

            double r2c1 = this.myMatrix4x4.myr2c1;
            double r2c2 = this.myMatrix4x4.myr2c2;
            double r2c3 = this.myMatrix4x4.myr2c3;
            double r2c4 = this.myMatrix4x4.myr2c4;

            double r3c1 = this.myMatrix4x4.myr3c1;
            double r3c2 = this.myMatrix4x4.myr3c2;
            double r3c3 = this.myMatrix4x4.myr3c3;
            double r3c4 = this.myMatrix4x4.myr3c4;

            //double r4c1 = this.myMatrix4x4.myr4c1;
            //double r4c2 = this.myMatrix4x4.myr4c2;
            //double r4c3 = this.myMatrix4x4.myr4c3;
            //double r4c4 = this.myMatrix4x4.myr4c4;

            // calculate row 1
            this.myMatrix4x4.myr1c1 =

                matrix_in.myMatrix4x4.myr1c1 * r1c1 +
                matrix_in.myMatrix4x4.myr1c2 * r2c1 +
                matrix_in.myMatrix4x4.myr1c3 * r3c1 +
                matrix_in.myMatrix4x4.myr1c4 * this.myMatrix4x4.myr4c1;

            this.myMatrix4x4.myr1c2 =

                matrix_in.myMatrix4x4.myr1c1 * r1c2 +
                matrix_in.myMatrix4x4.myr1c2 * r2c2 +
                matrix_in.myMatrix4x4.myr1c3 * r3c2 +
                matrix_in.myMatrix4x4.myr1c4 * this.myMatrix4x4.myr4c2;

            this.myMatrix4x4.myr1c3 =

                matrix_in.myMatrix4x4.myr1c1 * r1c3 +
                matrix_in.myMatrix4x4.myr1c2 * r2c3 +
                matrix_in.myMatrix4x4.myr1c3 * r3c3 +
                matrix_in.myMatrix4x4.myr1c4 * this.myMatrix4x4.myr4c3;

            this.myMatrix4x4.myr1c4 =

                matrix_in.myMatrix4x4.myr1c1 * r1c4 +
                matrix_in.myMatrix4x4.myr1c2 * r2c4 +
                matrix_in.myMatrix4x4.myr1c3 * r3c4 +
                matrix_in.myMatrix4x4.myr1c4 * this.myMatrix4x4.myr4c4;

            // calculate row 2
            this.myMatrix4x4.myr2c1 =

                matrix_in.myMatrix4x4.myr2c1 * r1c1 +
                matrix_in.myMatrix4x4.myr2c2 * r2c1 +
                matrix_in.myMatrix4x4.myr2c3 * r3c1 +
                matrix_in.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr4c1;

            this.myMatrix4x4.myr2c2 =

                matrix_in.myMatrix4x4.myr2c1 * r1c2 +
                matrix_in.myMatrix4x4.myr2c2 * r2c2 +
                matrix_in.myMatrix4x4.myr2c3 * r3c2 +
                matrix_in.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr4c2;

            this.myMatrix4x4.myr2c3 =

                matrix_in.myMatrix4x4.myr2c1 * r1c3 +
                matrix_in.myMatrix4x4.myr2c2 * r2c3 +
                matrix_in.myMatrix4x4.myr2c3 * r3c3 +
                matrix_in.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr4c3;

            this.myMatrix4x4.myr2c4 =

                matrix_in.myMatrix4x4.myr2c1 * r1c4 +
                matrix_in.myMatrix4x4.myr2c2 * r2c4 +
                matrix_in.myMatrix4x4.myr2c3 * r3c4 +
                matrix_in.myMatrix4x4.myr2c4 * this.myMatrix4x4.myr4c4;

            // calculate row 3
            this.myMatrix4x4.myr3c1 =

                matrix_in.myMatrix4x4.myr3c1 * r1c1 +
                matrix_in.myMatrix4x4.myr3c2 * r2c1 +
                matrix_in.myMatrix4x4.myr3c3 * r3c1 +
                matrix_in.myMatrix4x4.myr3c4 * this.myMatrix4x4.myr4c1;

            this.myMatrix4x4.myr3c2 =

                matrix_in.myMatrix4x4.myr3c1 * r1c2 +
                matrix_in.myMatrix4x4.myr3c2 * r2c2 +
                matrix_in.myMatrix4x4.myr3c3 * r3c2 +
                matrix_in.myMatrix4x4.myr3c4 * this.myMatrix4x4.myr4c2;

            this.myMatrix4x4.myr3c3 =

                matrix_in.myMatrix4x4.myr3c1 * r1c3 +
                matrix_in.myMatrix4x4.myr3c2 * r2c3 +
                matrix_in.myMatrix4x4.myr3c3 * r3c3 +
                matrix_in.myMatrix4x4.myr3c4 * this.myMatrix4x4.myr4c3;

            this.myMatrix4x4.myr3c4 =

                matrix_in.myMatrix4x4.myr3c1 * r1c4 +
                matrix_in.myMatrix4x4.myr3c2 * r2c4 +
                matrix_in.myMatrix4x4.myr3c3 * r3c4 +
                matrix_in.myMatrix4x4.myr3c4 * this.myMatrix4x4.myr4c4;

            // calculate row 4
            this.myMatrix4x4.myr4c1 =

                matrix_in.myMatrix4x4.myr4c1 * r1c1 +
                matrix_in.myMatrix4x4.myr4c2 * r2c1 +
                matrix_in.myMatrix4x4.myr4c3 * r3c1 +
                matrix_in.myMatrix4x4.myr4c4 * this.myMatrix4x4.myr4c1;

            this.myMatrix4x4.myr4c2 =

                matrix_in.myMatrix4x4.myr4c1 * r1c2 +
                matrix_in.myMatrix4x4.myr4c2 * r2c2 +
                matrix_in.myMatrix4x4.myr4c3 * r3c2 +
                matrix_in.myMatrix4x4.myr4c4 * this.myMatrix4x4.myr4c2;

            this.myMatrix4x4.myr4c3 =

                matrix_in.myMatrix4x4.myr4c1 * r1c3 +
                matrix_in.myMatrix4x4.myr4c2 * r2c3 +
                matrix_in.myMatrix4x4.myr4c3 * r3c3 +
                matrix_in.myMatrix4x4.myr4c4 * this.myMatrix4x4.myr4c3;

            this.myMatrix4x4.myr4c4 =

                matrix_in.myMatrix4x4.myr4c1 * r1c4 +
                matrix_in.myMatrix4x4.myr4c2 * r2c4 +
                matrix_in.myMatrix4x4.myr4c3 * r3c4 +
                matrix_in.myMatrix4x4.myr4c4 * this.myMatrix4x4.myr4c4;
        }

        public bool IsAlmostEqual(Matrix4x4d matrix_in, double tolerance)
        {
            if (Math.Abs(this.myMatrix4x4.myr1c1 - matrix_in.myMatrix4x4.myr1c1) > tolerance) return false;
            if (Math.Abs(this.myMatrix4x4.myr1c2 - matrix_in.myMatrix4x4.myr1c2) > tolerance) return false;
            if (Math.Abs(this.myMatrix4x4.myr1c3 - matrix_in.myMatrix4x4.myr1c3) > tolerance) return false;
            if (Math.Abs(this.myMatrix4x4.myr1c4 - matrix_in.myMatrix4x4.myr1c4) > tolerance) return false;

            if (Math.Abs(this.myMatrix4x4.myr2c1 - matrix_in.myMatrix4x4.myr2c1) > tolerance) return false;
            if (Math.Abs(this.myMatrix4x4.myr2c2 - matrix_in.myMatrix4x4.myr2c2) > tolerance) return false;
            if (Math.Abs(this.myMatrix4x4.myr2c3 - matrix_in.myMatrix4x4.myr2c3) > tolerance) return false;
            if (Math.Abs(this.myMatrix4x4.myr2c4 - matrix_in.myMatrix4x4.myr2c4) > tolerance) return false;

            if (Math.Abs(this.myMatrix4x4.myr3c1 - matrix_in.myMatrix4x4.myr3c1) > tolerance) return false;
            if (Math.Abs(this.myMatrix4x4.myr3c2 - matrix_in.myMatrix4x4.myr3c2) > tolerance) return false;
            if (Math.Abs(this.myMatrix4x4.myr3c3 - matrix_in.myMatrix4x4.myr3c3) > tolerance) return false;
            if (Math.Abs(this.myMatrix4x4.myr3c4 - matrix_in.myMatrix4x4.myr3c4) > tolerance) return false;

            if (Math.Abs(this.myMatrix4x4.myr4c1 - matrix_in.myMatrix4x4.myr4c1) > tolerance) return false;
            if (Math.Abs(this.myMatrix4x4.myr4c2 - matrix_in.myMatrix4x4.myr4c2) > tolerance) return false;
            if (Math.Abs(this.myMatrix4x4.myr4c3 - matrix_in.myMatrix4x4.myr4c3) > tolerance) return false;
            if (Math.Abs(this.myMatrix4x4.myr4c4 - matrix_in.myMatrix4x4.myr4c4) > tolerance) return false;

            return true;
        }

        public bool IsAffine(double tolerance)
        {
            if ((Math.Abs(this.myMatrix4x4.myr4c1 - 0.0) > tolerance) ||
                (Math.Abs(this.myMatrix4x4.myr4c2 - 0.0) > tolerance) ||
                (Math.Abs(this.myMatrix4x4.myr4c3 - 0.0) > tolerance) ||
                (Math.Abs(this.myMatrix4x4.myr4c4 - 1.0) > tolerance))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool IsIdentity(double tolerance)
        {
            if ((Math.Abs(this.myMatrix4x4.myr1c1 - 1.0) > tolerance) ||
                (Math.Abs(this.myMatrix4x4.myr1c2 - 0.0) > tolerance) ||
                (Math.Abs(this.myMatrix4x4.myr1c3 - 0.0) > tolerance) ||
                (Math.Abs(this.myMatrix4x4.myr1c4 - 0.0) > tolerance) ||
                (Math.Abs(this.myMatrix4x4.myr2c1 - 0.0) > tolerance) ||
                (Math.Abs(this.myMatrix4x4.myr2c2 - 1.0) > tolerance) ||
                (Math.Abs(this.myMatrix4x4.myr2c3 - 0.0) > tolerance) ||
                (Math.Abs(this.myMatrix4x4.myr2c4 - 0.0) > tolerance) ||
                (Math.Abs(this.myMatrix4x4.myr3c1 - 0.0) > tolerance) ||
                (Math.Abs(this.myMatrix4x4.myr3c2 - 0.0) > tolerance) ||
                (Math.Abs(this.myMatrix4x4.myr3c3 - 1.0) > tolerance) ||
                (Math.Abs(this.myMatrix4x4.myr3c4 - 0.0) > tolerance) ||
                (Math.Abs(this.myMatrix4x4.myr4c1 - 0.0) > tolerance) ||
                (Math.Abs(this.myMatrix4x4.myr4c2 - 0.0) > tolerance) ||
                (Math.Abs(this.myMatrix4x4.myr4c3 - 0.0) > tolerance) ||
                (Math.Abs(this.myMatrix4x4.myr4c4 - 1.0) > tolerance))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion Arithmetic matrix methods

        #region Compose / Decompose methods

        public static Matrix4x4d GetComposedMatrix(
            Matrix4x4d rotMatrix,
            double scaleX, double scaleY, double scaleZ,
            double transX, double transY, double transZ,
            double shearXY, double shearXZ, double shearYZ)
        {
            //Matrix4x4d transMatrix = Matrix4x4d.GetTranslationMatrix(transX, transY, transZ);
            //Matrix4x4d shearMatrix = Matrix4x4d.GetShearMatrix(shearXY, shearXZ, shearYZ);
            //Matrix4x4d scaleMatrix = Matrix4x4d.GetScalingMatrix(scaleX, scaleY, scaleZ);
            //Matrix4x4d result = transMatrix * rotMatrix * shearMatrix * scaleMatrix;

            // | 1 0 0 xp | | xu xv xw 0 | |  1 sxy  sxz  0 | | sx  0  0  0 | 
            // | 0 1 0 yp | | yu yv yw 0 | |  0   1  syz  0 | |  0 sy  0  0 | 
            // | 0 0 1 zp | | zu zv zw 0 | |  0   0    1  0 | |  0  0 sz  0 |
            // | 0 0 0  1 | | 0  0  0  1 | |  0   0    0  1 | |  0  0  0  1 |
            //
            //                   |
            //                   V
            //
            // | xu xv xw xp | |  1 sxy  sxz  0 | | sx  0  0  0 | 
            // | yu yv yw yp | |  0   1  syz  0 | |  0 sy  0  0 | 
            // | zu zv zw zp | |  0   0    1  0 | |  0  0 sz  0 |
            // |  0  0  0  1 | |  0   0    0  1 | |  0  0  0  1 |
            //
            //                   |
            //                   V
            //
            // | xu  xu*sxy+xv  xu*sxz+xv*syz+xw  xp | | sx  0  0  0 | 
            // | yu  yu*sxy+yv  yu*sxz+yv*syz+yw  yp | |  0 sy  0  0 | 
            // | zu  zu*sxy+zv  zu*sxz+zv*syz+zw  zp | |  0  0 sz  0 |
            // |  0          0                 0   1 | |  0  0  0  1 |
            //
            //                   |
            //                   V
            //
            // | xu*sx  (xu*sxy+xv)*sy  (xu*sxz+xv*syz+xw)*sz  xp |
            // | yu*sx  (yu*sxy+yv)*sy  (yu*sxz+yv*syz+yw)*sz  yp |
            // | zu*sx  (zu*sxy+zv)*sy  (zu*sxz+zv*syz+zw)*sz  zp |
            // |     0               0                      0   1 |

            Matrix4x4d result = new Matrix4x4d(
                (rotMatrix.myMatrix4x4.myr1c1 * scaleX),
                (rotMatrix.myMatrix4x4.myr1c1 * shearXY + rotMatrix.myMatrix4x4.myr1c2) * scaleY,
                (rotMatrix.myMatrix4x4.myr1c1 * shearXZ + rotMatrix.myMatrix4x4.myr1c2 * shearYZ + rotMatrix.myMatrix4x4.myr1c3) * scaleZ, transX,

                (rotMatrix.myMatrix4x4.myr2c1 * scaleX),
                (rotMatrix.myMatrix4x4.myr2c1 * shearXY + rotMatrix.myMatrix4x4.myr2c2) * scaleY,
                (rotMatrix.myMatrix4x4.myr2c1 * shearXZ + rotMatrix.myMatrix4x4.myr2c2 * shearYZ + rotMatrix.myMatrix4x4.myr2c3) * scaleZ, transY,

                (rotMatrix.myMatrix4x4.myr3c1 * scaleX),
                (rotMatrix.myMatrix4x4.myr3c1 * shearXY + rotMatrix.myMatrix4x4.myr3c2) * scaleY,
                (rotMatrix.myMatrix4x4.myr3c1 * shearXZ + rotMatrix.myMatrix4x4.myr3c2 * shearYZ + rotMatrix.myMatrix4x4.myr3c3) * scaleZ, transZ,

                (rotMatrix.myMatrix4x4.myr4c1 * scaleX),
                (rotMatrix.myMatrix4x4.myr4c1 * shearXY + rotMatrix.myMatrix4x4.myr4c2) * scaleY,
                (rotMatrix.myMatrix4x4.myr4c1 * shearXZ + rotMatrix.myMatrix4x4.myr4c2 * shearYZ + rotMatrix.myMatrix4x4.myr4c3) * scaleZ, 1.0);

            return result;
        }

        public Matrix4x4d GetDecomposedMatrix(
            out double scaleX, out double scaleY, out double scaleZ,
            out double transX, out double transY, out double transZ,
            out double shearXY, out double shearXZ, out double shearYZ,
            double tolerance)
        {
            // get the relevant column vectors
            Vector3d u1 = new Vector3d(this.myMatrix4x4.myr1c1, this.myMatrix4x4.myr2c1, this.myMatrix4x4.myr3c1);
            scaleX = u1.GetLength();
            if (GraphicMath.IsEqual(scaleX, 0.0, tolerance))
            {
                throw new System.ArithmeticException("This matrix cannot be decomposed because column vector 1 isNil!");
            }
            Vector3d e1 = u1 / scaleX;

            Vector3d a2 = new Vector3d(this.myMatrix4x4.myr1c2, this.myMatrix4x4.myr2c2, this.myMatrix4x4.myr3c2);
            double e1a2 = e1.GetInnerProduct(a2);
            Vector3d u2 = a2 - e1a2 * e1;
            scaleY = u2.GetLength();
            if (GraphicMath.IsEqual(scaleY, 0.0, tolerance))
            {
                throw new System.ArithmeticException("This matrix cannot be decomposed because column vector 2 isNil or parallel to 1!");
            }
            Vector3d e2 = u2 / scaleY;

            Vector3d a3 = new Vector3d(this.myMatrix4x4.myr1c3, this.myMatrix4x4.myr2c3, this.myMatrix4x4.myr3c3);
            double e1a3 = e1.GetInnerProduct(a3);
            double e2a3 = e2.GetInnerProduct(a3);
            Vector3d u3 = a3 - e1a3 * e1 - e2a3 * e2;
            scaleZ = u3.GetLength();
            if (GraphicMath.IsEqual(scaleZ, 0.0, tolerance))
            {
                throw new System.ArithmeticException("This matrix cannot be decomposed because column vector 3 isNil or parallel to 1 or 2!");
            }
            Vector3d e3 = u3 / scaleZ;

            // return translation coefficients
            transX = this.myMatrix4x4.myr1c4;
            transY = this.myMatrix4x4.myr2c4;
            transZ = this.myMatrix4x4.myr3c4;

            // return shear coefficients
            shearXY = e1a2 / scaleY;
            shearXZ = e1a3 / scaleZ;
            shearYZ = e2a3 / scaleZ;

            // setup and return the Q matrix, i.e. the rotation matrix
            Matrix4x4d qMatrix = new Matrix4x4d(
                e1.X, e2.X, e3.X, 0.0,
                e1.Y, e2.Y, e3.Y, 0.0,
                e1.Z, e2.Z, e3.Z, 0.0,
                0.0, 0.0, 0.0, 1.0);

            return qMatrix;
        }

        public void GetQRdecomposition(ref Matrix4x4d qMatrix, ref Matrix4x4d rMatrix, double tolerance)
        {
            // get the relevant column vectors
            Vector3d u1 = new Vector3d(this.myMatrix4x4.myr1c1, this.myMatrix4x4.myr2c1, this.myMatrix4x4.myr3c1);
            double l1 = u1.GetLength();
            if (GraphicMath.IsEqual(l1, 0.0, tolerance))
            {
                throw new System.ArithmeticException("This matrix cannot be decomposed to QR because column vector 1 isNil!");
            }
            Vector3d e1 = u1 / l1;

            Vector3d a2 = new Vector3d(this.myMatrix4x4.myr1c2, this.myMatrix4x4.myr2c2, this.myMatrix4x4.myr3c2);
            double e1a2 = e1.GetInnerProduct(a2);
            Vector3d u2 = a2 - e1a2 * e1;
            double l2 = u2.GetLength();
            if (GraphicMath.IsEqual(l2, 0.0, tolerance))
            {
                throw new System.ArithmeticException("This matrix cannot be decomposed to QR because column vector 2 isNil or parallel to 1!");
            }
            Vector3d e2 = u2 / l2;

            Vector3d a3 = new Vector3d(this.myMatrix4x4.myr1c3, this.myMatrix4x4.myr2c3, this.myMatrix4x4.myr3c3);
            double e1a3 = e1.GetInnerProduct(a3);
            double e2a3 = e2.GetInnerProduct(a3);
            Vector3d u3 = a3 - e1a3 * e1 - e2a3 * e2;
            double l3 = u3.GetLength();
            if (GraphicMath.IsEqual(l3, 0.0, tolerance))
            {
                throw new System.ArithmeticException("This matrix cannot be decomposed to QR because column vector 3 isNil or parallel to 1 or 2!");
            }
            Vector3d e3 = u3 / l3;

            // setup the Q matrix
            qMatrix.SetMatrix(
                e1.X, e2.X, e3.X, 0.0,
                e1.Y, e2.Y, e3.Y, 0.0,
                e1.Z, e2.Z, e3.Z, 0.0,
                0.0, 0.0, 0.0, 1.0);

            // setup the R matrix
            rMatrix.SetMatrix(
                l1, e1a2, e1a3, 0.0,
                0.0, l2, e2a3, 0.0,
                0.0, 0.0, l3, 0.0,
                0.0, 0.0, 0.0, 1.0);
        }
        #endregion Decompose methods

        #region Data

        public StructMatrix4x4d myMatrix4x4;

        #endregion
    }

    #region struct Matrix4x4

    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    public struct StructMatrix4x4d
    {
        public double myr1c1;  // row 1 column 1
        public double myr1c2;  // row 1 column 2
        public double myr1c3;  // row 1 column 3
        public double myr1c4;  // row 1 column 4

        public double myr2c1;  // row 2 column 1
        public double myr2c2;  // row 2 column 2
        public double myr2c3;  // row 2 column 3
        public double myr2c4;  // row 2 column 4

        public double myr3c1;  // row 3 column 1
        public double myr3c2;  // row 3 column 2
        public double myr3c3;  // row 3 column 3
        public double myr3c4;  // row 3 column 4

        public double myr4c1;  // row 4 column 1
        public double myr4c2;  // row 4 column 2
        public double myr4c3;  // row 4 column 3
        public double myr4c4;  // row 4 column 3

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
        public double r1c4
        {
            get
            {
                return this.myr1c4;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("r1c4", "invalid set r1c4 " + value);
                }
                this.myr1c4 = value;
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
        public double r2c4
        {
            get
            {
                return this.myr2c4;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("r2c4", "invalid set r2c4 " + value);
                }
                this.myr2c4 = value;
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
        public double r3c4
        {
            get
            {
                return this.myr3c4;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("r3c4", "invalid set r3c4 " + value);
                }
                this.myr3c4 = value;
            }
        }

        public double r4c1
        {
            get
            {
                return this.myr4c1;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("r4c1", "invalid set r4c1 " + value);
                }
                this.myr4c1 = value;
            }
        }
        public double r4c2
        {
            get
            {
                return this.myr4c2;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("r4c2", "invalid set r4c2 " + value);
                }
                this.myr4c2 = value;
            }
        }
        public double r4c3
        {
            get
            {
                return this.myr4c3;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("r4c3", "invalid set r4c3 " + value);
                }
                this.myr4c3 = value;
            }
        }
        public double r4c4
        {
            get
            {
                return this.myr4c4;
            }
            set
            {
                if (System.Double.IsNaN(value))
                {
                    throw new System.ArgumentOutOfRangeException("r4c4", "invalid set r4c4 " + value);
                }
                this.myr4c4 = value;
            }
        }
    }
    #endregion
}