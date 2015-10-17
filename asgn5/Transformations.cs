using System;
using System.Collections.Generic;
using System.Text;

namespace asgn5v1
{
    static class Transformations
    {
        public static double DEFAULT_INCREMENT = 10;
        public static double DEFAULT_THETA     = 0.0174533;  // 0.0174533 radian = 1 degree
        public static double DEFAULT_SCALEUP   = 1.5;
        public static double DEFAULT_SCALEDOWN = 0.5;

        private static double[,] transformMatrix = new double[4, 4];

        private struct Vector3 {
            public double X, Y, Z;
        }
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

        public static double[,] Scale(double[,] matrix, double x, double y, double z)
        {
            // Get origin
            origin.X = matrix[0, 0];
            origin.Y = matrix[0, 1];
            origin.Z = matrix[0, 2];

            // Translate origin to (0,0)
            matrix = Translate(matrix, -matrix[0, 0], -matrix[0, 1], -matrix[0, 2]);

            // Row 1
            transformMatrix[0, 0] = x;
            transformMatrix[0, 1] = 0;
            transformMatrix[0, 2] = 0;
            transformMatrix[0, 3] = 0;

            // Row 2
            transformMatrix[1, 0] = 0;
            transformMatrix[1, 1] = y;
            transformMatrix[1, 2] = 0;
            transformMatrix[1, 3] = 0;

            // Row 3
            transformMatrix[2, 0] = 0;
            transformMatrix[2, 1] = 0;
            transformMatrix[2, 2] = z;
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

        public static double[,] RotateX(double[,] matrix, double theta)
        {
            // Get origin
            origin.X = matrix[0, 0];
            origin.Y = matrix[0, 1];
            origin.Z = matrix[0, 2];

            // Translate origin to (0,0)
            matrix = Translate(matrix, -matrix[0, 0], -matrix[0, 1], -matrix[0, 2]);

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

        public static double[,] RotateY(double[,] matrix, double theta)
        {
            // Get origin
            origin.X = matrix[0, 0];
            origin.Y = matrix[0, 1];
            origin.Z = matrix[0, 2];

            // Translate origin to (0,0)
            matrix = Translate(matrix, -matrix[0, 0], -matrix[0, 1], -matrix[0, 2]);

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

        public static double[,] RotateZ(double[,] matrix, double theta)
        {
            // Get origin
            origin.X = matrix[0, 0];
            origin.Y = matrix[0, 1];
            origin.Z = matrix[0, 2];

            // Translate origin to (0,0)
            matrix = Translate(matrix, -matrix[0, 0], -matrix[0, 1], -matrix[0, 2]);

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

        public static double FindMaxY(double[,] matrix)
        {
            double max = 0;
            for (int i = 1; i < matrix.GetLength(0) - 1; i++)
                if (matrix[i, 1] > max)
                    max = matrix[i, 1];
            return max;
        }

        public static double[,] Sheer(double[,] matrix, double amount)
        {
            // Get origin
            origin.X = matrix[0, 0];
            origin.Y = matrix[0, 1];
            origin.Z = matrix[0, 2];

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
    }
}




/*
            Console.WriteLine("Result:");
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Console.Write(resultMatrix[r, c] + "\t");
                }
                Console.WriteLine();
            }*/
