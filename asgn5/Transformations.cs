using System;
using System.Collections.Generic;
using System.Text;

namespace asgn5v1
{
    static class Transformations
    {
        // Default constants
        public static double DEFAULT_INCREMENT = 10;
        public static double DEFAULT_THETA     = 0.0174533;  // 0.0174533 radian == 1 degree
        public static double DEFAULT_SCALEUP   = 1.5;
        public static double DEFAULT_SCALEDOWN = 0.5;

        // Matrix that holds the current transformation
        private static double[,] transformMatrix = new double[4, 4];

        // For working with vectors easier and more expressive
        private struct Vector3 {
            public double X, Y, Z;
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
        /// Translates a matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static double[,] Translate(double[,] matrix, double x, double y, double z)
        {
            // Get origin
            origin.X = matrix[0, 0];
            origin.Y = matrix[0, 1];
            origin.Z = matrix[0, 2];

            // Row 1
            transformMatrix[0, 0] = 1;
            transformMatrix[0, 1] = 0;
            transformMatrix[0, 2] = 0;
            transformMatrix[0, 3] = 0;

            // Row 2
            transformMatrix[1, 0] = 0;
            transformMatrix[1, 1] = 1;
            transformMatrix[1, 2] = 0;
            transformMatrix[1, 3] = 0;

            // Row 3
            transformMatrix[2, 0] = 0;
            transformMatrix[2, 1] = 0;
            transformMatrix[2, 2] = 1;
            transformMatrix[2, 3] = 0;

            // Row 4
            transformMatrix[3, 0] = x;
            transformMatrix[3, 1] = y;
            transformMatrix[3, 2] = z;
            transformMatrix[3, 3] = 1;
           
            // Multiply matrix with transformation matrix
            matrix = Multiply(matrix, transformMatrix);

            return matrix;
        }

        /// <summary>
        /// Scales a matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="xFactor"></param>
        /// <param name="yFactor"></param>
        /// <param name="zFactor"></param>
        /// <returns></returns>
        public static double[,] Scale(double[,] matrix, double xFactor, double yFactor, double zFactor)
        {
            // Get origin
            origin.X = matrix[0, 0];
            origin.Y = matrix[0, 1];
            origin.Z = matrix[0, 2];

            // Translate origin to (0, 0)
            matrix = Translate(matrix, -origin.X, -origin.Y, -origin.Z);

            // Row 1
            transformMatrix[0, 0] = xFactor;
            transformMatrix[0, 1] = 0;
            transformMatrix[0, 2] = 0;
            transformMatrix[0, 3] = 0;

            // Row 2
            transformMatrix[1, 0] = 0;
            transformMatrix[1, 1] = yFactor;
            transformMatrix[1, 2] = 0;
            transformMatrix[1, 3] = 0;

            // Row 3
            transformMatrix[2, 0] = 0;
            transformMatrix[2, 1] = 0;
            transformMatrix[2, 2] = zFactor;
            transformMatrix[2, 3] = 0;

            // Row 4
            transformMatrix[3, 0] = 0;
            transformMatrix[3, 1] = 0;
            transformMatrix[3, 2] = 0;
            transformMatrix[3, 3] = 1;

            // Multiply matrix with transformation matrix
            matrix = Multiply(matrix, transformMatrix);

            // Translate back
            matrix = Translate(matrix, origin.X, origin.Y, origin.Z);

            return matrix;
        }

        /// <summary>
        /// Rotates matrix around X-axis
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="theta"></param>
        /// <returns></returns>
        public static double[,] RotateX(double[,] matrix, double theta)
        {
            // Get origin
            origin.X = matrix[0, 0];
            origin.Y = matrix[0, 1];
            origin.Z = matrix[0, 2];

            // Translate origin to (0, 0)
            matrix = Translate(matrix, -origin.X, -origin.Y, -origin.Z);

            // Row 1
            transformMatrix[0, 0] = 1;
            transformMatrix[0, 1] = 0;
            transformMatrix[0, 2] = 0;
            transformMatrix[0, 3] = 0;
            
            // Row 2
            transformMatrix[1, 0] = 0;
            transformMatrix[1, 1] = Math.Cos(theta);
            transformMatrix[1, 2] = -Math.Sin(theta);
            transformMatrix[1, 3] = 0;
            
            // Row 3
            transformMatrix[2, 0] = 0;
            transformMatrix[2, 1] = Math.Sin(theta);
            transformMatrix[2, 2] = Math.Cos(theta);
            transformMatrix[2, 3] = 0;

            // Row 4
            transformMatrix[3, 0] = 0;
            transformMatrix[3, 1] = 0;
            transformMatrix[3, 2] = 0;
            transformMatrix[3, 3] = 1;

            // Multiply matrix with transformation matrix
            matrix = Multiply(matrix, transformMatrix);

            // Translate back
            matrix = Translate(matrix, origin.X, origin.Y, origin.Z);

            return matrix;
        }

        /// <summary>
        /// Rotates matrix around Y-axis
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="theta"></param>
        /// <returns></returns>
        public static double[,] RotateY(double[,] matrix, double theta)
        {
            // Get origin
            origin.X = matrix[0, 0];
            origin.Y = matrix[0, 1];
            origin.Z = matrix[0, 2];

            // Translate origin to (0, 0)
            matrix = Translate(matrix, -origin.X, -origin.Y, -origin.Z);

            // Row 1
            transformMatrix[0, 0] = Math.Cos(theta);
            transformMatrix[0, 1] = 0;
            transformMatrix[0, 2] = Math.Sin(theta);
            transformMatrix[0, 3] = 0;

            // Row 2
            transformMatrix[1, 0] = 0;
            transformMatrix[1, 1] = 1;
            transformMatrix[1, 2] = 0;
            transformMatrix[1, 3] = 0;

            // Row 3
            transformMatrix[2, 0] = -Math.Sin(theta);
            transformMatrix[2, 1] = 0;
            transformMatrix[2, 2] = Math.Cos(theta);
            transformMatrix[2, 3] = 0;

            // Row 4
            transformMatrix[3, 0] = 0;
            transformMatrix[3, 1] = 0;
            transformMatrix[3, 2] = 0;
            transformMatrix[3, 3] = 1;

            // Multiply matrix with transformation matrix
            matrix = Multiply(matrix, transformMatrix);

            // Translate back
            matrix = Translate(matrix, origin.X, origin.Y, origin.Z);

            return matrix;
        }

        /// <summary>
        /// Rotates matrix around Z-axis
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="theta"></param>
        /// <returns></returns>
        public static double[,] RotateZ(double[,] matrix, double theta)
        {
            // Get origin
            origin.X = matrix[0, 0];
            origin.Y = matrix[0, 1];
            origin.Z = matrix[0, 2];

            // Translate origin to (0, 0)
            matrix = Translate(matrix, -origin.X, -origin.Y, -origin.Z);

            // Row 1
            transformMatrix[0, 0] = Math.Cos(theta);
            transformMatrix[0, 1] = -Math.Sin(theta);
            transformMatrix[0, 2] = 0;
            transformMatrix[0, 3] = 0;

            // Row 2
            transformMatrix[1, 0] = Math.Sin(theta);
            transformMatrix[1, 1] = Math.Cos(theta);
            transformMatrix[1, 2] = 0;
            transformMatrix[1, 3] = 0;

            // Row 3
            transformMatrix[2, 0] = 0;
            transformMatrix[2, 1] = 0;
            transformMatrix[2, 2] = 1;
            transformMatrix[2, 3] = 0;

            // Row 4
            transformMatrix[3, 0] = 0;
            transformMatrix[3, 1] = 0;
            transformMatrix[3, 2] = 0;
            transformMatrix[3, 3] = 1;

            // Multiply matrix with transformation matrix
            matrix = Multiply(matrix, transformMatrix);

            // Translate back
            matrix = Translate(matrix, origin.X, origin.Y, origin.Z);

            return matrix;
        }

        /// <summary>
        /// Sheers matrix with respect to Y-axis
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static double[,] Sheer(double[,] matrix, double amount)
        {
            // Get origin
            origin.X = matrix[0, 0];
            origin.Y = matrix[0, 1];
            origin.Z = matrix[0, 2];

            // Find maximum Y coordinate (the bottom of the object in this case)
            double maxY = FindMaxY(matrix);

            // Translate origin to (0,0)
            matrix = Translate(matrix, 0, -maxY, 0);
            // Row 1
            transformMatrix[0, 0] = 1;
            transformMatrix[0, 1] = 0;
            transformMatrix[0, 2] = 0;
            transformMatrix[0, 3] = 0;

            // Row 2
            transformMatrix[1, 0] = amount;
            transformMatrix[1, 1] = 1;
            transformMatrix[1, 2] = amount;
            transformMatrix[1, 3] = 0;

            // Row 3
            transformMatrix[2, 0] = 0;
            transformMatrix[2, 1] = 0;
            transformMatrix[2, 2] = 1;
            transformMatrix[2, 3] = 0;

            // Row 4
            transformMatrix[3, 0] = 0;
            transformMatrix[3, 1] = 0;
            transformMatrix[3, 2] = 0;
            transformMatrix[3, 3] = 1;

            // Multiply matrix with transformation matrix
            matrix = Multiply(matrix, transformMatrix);

            // Translate back
            matrix = Translate(matrix, 0, maxY, 0);

            return matrix;
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
