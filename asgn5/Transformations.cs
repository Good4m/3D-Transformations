using System;
using System.Collections.Generic;
using System.Text;

namespace asgn5v1
{
    /// <summary>
    /// Applies transformations to matrices
    /// Author: Jeffrey Schweigler
    /// </summary>
    static class Transformations
    {
        // Default constants
        public static double DEFAULT_INCREMENT = 10;
        public static double DEFAULT_THETA     = 0.0174533;
        public static double DEFAULT_SCALEUP   = 1.1;
        public static double DEFAULT_SCALEDOWN = 0.9;

        // Matrix that holds the current (net) transformation
        private static double[,] transformMatrix = new double[4, 4];

        // For working with vectors easier and more expressive
        private struct Vector3 {
            public double X, Y, Z;
        }

        /// <summary>
        /// Sets a matrix to the identity matrix
        /// </summary>
        /// <param name="A"></param>
        /// <param name="nrow"></param>
        /// <param name="ncol"></param>
        private static void SetIdentity(double[,] A, int nrow, int ncol)
        {
            for (int i = 0; i < nrow; i++)
            {
                for (int j = 0; j < ncol; j++)
                {
                    A[i, j] = 0;
                    A[i, i] = 1;
                }
            }
        }

        /// <summary>
        /// Sets the transformMatrix to the identity matrix
        /// </summary>
        public static void ResetTransform()
        {
            SetIdentity(transformMatrix, 4, 4);
        }

        /// <summary>
        /// Creates and initializes double[,] matrix to the identity matrix
        /// </summary>
        /// <returns></returns>
        private static double[,] CreateMatrix()
        {
            double[,] d = new double[4, 4];
            SetIdentity(d, 4, 4);
            return d;
        }

        // For holding origin of matrix to be transformed.
        // This is so we can remember where to translate back to after a transformation
        // requires us to move the origin to (0,0)
        private static Vector3 origin = new Vector3();

        /// <summary>
        /// Multiplies two matrixes and returns the result matrix
        /// Throws exception if number of columns in
        /// matrix A aren't the same as number of rows in matrix B.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static double[,] Multiply(double[,] a, double[,] b)
        {
            int rowsA = a.GetLength(0);
            int colsA = a.GetLength(1);
            int rowsB = a.GetLength(0);
            int colsB = a.GetLength(1);

            double[,] resultMatrix = new double[rowsA, colsB];

            // Foreach row in matrix a
            for (int rowA = 0; rowA < a.GetLength(0); rowA++)
            {
                // Number of times for each row that we need to multiply each column
                for (int colB = 0; colB < b.GetLength(1); colB++)
                {
                    // Foreach column in current row of matrix a
                    for (int colA = 0; colA < a.GetLength(1); colA++)
                    {
                        double result = 0;
                        // Foreach row in matrix b
                        for (int rowB = 0; rowB < b.GetLength(0); rowB++)
                        {
                            // Multiply currently selected cells and add it to result
                            result += a[rowA, colA] * b[rowB, colB];
                            // Increment column in matrix A
                            colA++;
                        }
                        // Place result into result matrix
                        resultMatrix[rowA, colB] = result;
                    }
                }
            }
            return resultMatrix;
        }
       
        /// <summary>
        /// Initial placement of object in center of screen
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static double[,] CenterObjectAndScaleUp(double[,] matrix, double centerWidth, double centerHeight, double scaleAmount)
        {
            // Initialize transformation matrix
            ResetTransform();

            

            // Translate origin to (0, 0)
            transformMatrix = Translate(transformMatrix, -matrix[0, 0], -matrix[0, 1], -matrix[0, 2]);

            // Scale
            transformMatrix = UniformScale(transformMatrix, scaleAmount, scaleAmount, scaleAmount);

            // Translate to center of screen
            transformMatrix = Translate(transformMatrix, centerWidth, centerHeight, matrix[0, 2]);

            // Multiply matrix with net transformation matrix and return
            return Multiply(matrix, transformMatrix);
        }

        /// <summary>
        /// Translates a matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static double[,] Translate(double[,] matrix, double x, double y, double z)
        {
            double[,] trans = CreateMatrix();
            trans[3, 0] = x;
            trans[3, 1] = y;
            trans[3, 2] = z;
            return Multiply(matrix, trans);
        }

        /// <summary>
        /// Scales a matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="xFactor"></param>
        /// <param name="yFactor"></param>
        /// <param name="zFactor"></param>
        /// <returns></returns>
        public static double[,] UniformScale(double[,] matrix, double xFactor, double yFactor, double zFactor)
        {
            double[,] trans1 = CreateMatrix();
            double[,] scale  = CreateMatrix();
            double[,] trans2 = CreateMatrix();
            double[,] net    = CreateMatrix();

            // Get origin
            origin.X = matrix[0, 0];
            origin.Y = matrix[0, 1];
            origin.Z = matrix[0, 2];

            // Translate origin to (0, 0)
            trans1 = Translate(trans1, -origin.X, -origin.Y, -origin.Z);

            // Scale
            scale[0, 0] = xFactor;
            scale[1, 1] = yFactor;
            scale[2, 2] = zFactor;

            // Translate back
            trans2 = Translate(trans2, origin.X, origin.Y, origin.Z);

            // Create net matrix from transformation matrices
            net = Multiply(net, trans1);
            net = Multiply(net, scale);
            net = Multiply(net, trans2);

            return Multiply(matrix, net);
        }

        /// <summary>
        /// Rotates matrix around X-axis
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="theta"></param>
        /// <returns></returns>
        public static double[,] RotateX(double[,] matrix, double theta)
        {
            double[,] trans1 = CreateMatrix();
            double[,] rotate = CreateMatrix();
            double[,] trans2 = CreateMatrix();
            double[,] net    = CreateMatrix();

            // Get origin
            origin.X = matrix[0, 0];
            origin.Y = matrix[0, 1];
            origin.Z = matrix[0, 2];

            // Translate origin to (0, 0)
            trans1 = Translate(trans1, -origin.X, -origin.Y, -origin.Z);

            // Set variables
            rotate[1, 1] = Math.Cos(theta);
            rotate[1, 2] = -Math.Sin(theta);
            rotate[2, 1] = Math.Sin(theta);
            rotate[2, 2] = Math.Cos(theta);

            // Translate back
            trans2 = Translate(trans2, origin.X, origin.Y, origin.Z);

            // Create net matrix from transformation matrices
            net = Multiply(net, trans1);
            net = Multiply(net, rotate);
            net = Multiply(net, trans2);

            return Multiply(matrix, net);
        }

        /// <summary>
        /// Rotates matrix around Y-axis
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="theta"></param>
        /// <returns></returns>
        public static double[,] RotateY(double[,] matrix, double theta)
        {
            double[,] trans1 = CreateMatrix();
            double[,] rotate = CreateMatrix();
            double[,] trans2 = CreateMatrix();
            double[,] net    = CreateMatrix();

            // Get origin
            origin.X = matrix[0, 0];
            origin.Y = matrix[0, 1];
            origin.Z = matrix[0, 2];

            // Translate origin to (0, 0)
            trans1 = Translate(trans1, -origin.X, -origin.Y, -origin.Z);

            // Set variables
            rotate[0, 0] = Math.Cos(theta);
            rotate[0, 2] = Math.Sin(theta);
            rotate[2, 0] = -Math.Sin(theta);
            rotate[2, 2] = Math.Cos(theta);

            // Translate back
            trans2 = Translate(trans2, origin.X, origin.Y, origin.Z);

            // Create net matrix from transformation matrices
            net = Multiply(net, trans1);
            net = Multiply(net, rotate);
            net = Multiply(net, trans2);

            return Multiply(matrix, net);
        }

        /// <summary>
        /// Rotates matrix around Z-axis
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="theta"></param>
        /// <returns></returns>
        public static double[,] RotateZ(double[,] matrix, double theta)
        {
            double[,] trans1 = CreateMatrix();
            double[,] rotate = CreateMatrix();
            double[,] trans2 = CreateMatrix();
            double[,] net    = CreateMatrix();

            // Get origin
            origin.X = matrix[0, 0];
            origin.Y = matrix[0, 1];
            origin.Z = matrix[0, 2];

            // Translate origin to (0, 0)
            trans1 = Translate(trans1, -origin.X, -origin.Y, -origin.Z);

            // Set variables
            rotate[0, 0] = Math.Cos(theta);
            rotate[0, 1] = -Math.Sin(theta);
            rotate[1, 0] = Math.Sin(theta);
            rotate[1, 1] = Math.Cos(theta);

            // Translate back
            trans2 = Translate(trans2, origin.X, origin.Y, origin.Z);

            // Create net matrix from transformation matrices
            net = Multiply(net, trans1);
            net = Multiply(net, rotate);
            net = Multiply(net, trans2);

            return Multiply(matrix, net);
        }

        /// <summary>
        /// Shears matrix with respect to Y-axis
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static double[,] ShearX(double[,] matrix, double amount)
        {
            double[,] trans1 = CreateMatrix();
            double[,] shear  = CreateMatrix();
            double[,] trans2 = CreateMatrix();
            double[,] net    = CreateMatrix();

            // Get origin
            origin.X = matrix[0, 0];
            origin.Y = matrix[0, 1];
            origin.Z = matrix[0, 2];

            // Find maximum Y coordinate (the bottom of the object in this case)
            double maxY = FindMaxY(matrix);

            // Translate origin to (0, 0)
            trans1 = Translate(trans1, -origin.X, -maxY, -origin.Z);

            // Set variables
            shear[1, 0] = amount;
            shear[1, 2] = amount;

            // Translate back
            trans2 = Translate(trans2, origin.X, maxY, origin.Z);

            // Create net matrix from transformation matrices
            net = Multiply(net, trans1);
            net = Multiply(net, shear);
            net = Multiply(net, trans2);

            return Multiply(matrix, net);
        }

        /// <summary>
        /// Finds minimum Y coordinate of a matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static double FindMinY(double[,] matrix)
        {
            double min = double.MaxValue;
            for (int i = 1; i < matrix.GetLength(0) - 1; i++)
                if (matrix[i, 1] < min)
                    min = matrix[i, 1];
            return min;
        }

        /// <summary>
        /// Finds maximum Y coordinate of a matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static double FindMaxY(double[,] matrix)
        {
            double max = 0;
            for (int i = 1; i < matrix.GetLength(0) - 1; i++)
                if (matrix[i, 1] > max)
                    max = matrix[i, 1];
            return max;
        }
    }
}
