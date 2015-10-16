using System;
using System.Collections.Generic;
using System.Text;

namespace asgn5v1
{
    static class Transformations
    {

        /// <summary>
        /// Multiplies two matrixes and returns the result matrix
        /// Throws exception if number of columns in
        /// matrix A aren't the same as number of rows in matrix B.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static double[,] Multiply(double[,] a, double[,] b)
        {
            int rows = a.GetLength(0);
            int cols = b.GetLength(1);

            double[,] resultMatrix = new double[rows, cols];

            // Foreach row in matrix a
            for (int ARow = 0; ARow < a.GetLength(0); ARow++)
            {
                // Number of times for each row that we need to multiply each column
                for (int BCol = 0; BCol < b.GetLength(1); BCol++)
                {
                    // Foreach column in current row of matrix a
                    for (int ACol = 0; ACol < a.GetLength(1); ACol++)
                    {
                        double result = 0;
                        // Foreach row in matrix b
                        for (int BRow = 0; BRow < b.GetLength(0); BRow++)
                        {
                            result += a[ARow, ACol] * b[BRow, BCol];
                            // Increment column in matrix A
                            ACol++;
                        }
                        resultMatrix[ARow, BCol] = result;
                    }
                }
            }

            Console.WriteLine("Result:");
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Console.Write(resultMatrix[r, c] + "\t");
                }
                Console.WriteLine();
            }

            return resultMatrix;
        }

        public static double[,] Translate(double[,] matrix, double x, double y, double z)
        {
            double[,] translationMatrix = new double[4, 4];
            // Row 1
            translationMatrix[0, 0] = 1;
            translationMatrix[0, 1] = 0;
            translationMatrix[0, 2] = 0;
            translationMatrix[0, 3] = 0;
            // Row 2
            translationMatrix[1, 0] = 0;
            translationMatrix[1, 1] = 1;
            translationMatrix[1, 2] = 0;
            translationMatrix[1, 3] = 0;
            // Row 3
            translationMatrix[2, 0] = 0;
            translationMatrix[2, 1] = 0;
            translationMatrix[2, 2] = 1;
            translationMatrix[2, 3] = 0;
            // Row 4
            translationMatrix[3, 0] = x;
            translationMatrix[3, 1] = y;
            translationMatrix[3, 2] = z;
            translationMatrix[3, 3] = 1;

            // Multiply
            return Multiply(matrix, translationMatrix);
        }


        

        

        public static double[,] Scale(double[,] matrix, double x, double y, double z)
        {
            double[,] translationMatrix = new double[4, 4];
            // Row 1
            translationMatrix[0, 0] = x;
            translationMatrix[0, 1] = 0;
            translationMatrix[0, 2] = 0;
            translationMatrix[0, 3] = 0;
            // Row 2
            translationMatrix[1, 0] = 0;
            translationMatrix[1, 1] = y;
            translationMatrix[1, 2] = 0;
            translationMatrix[1, 3] = 0;
            // Row 3
            translationMatrix[2, 0] = 0;
            translationMatrix[2, 1] = 0;
            translationMatrix[2, 2] = z;
            translationMatrix[2, 3] = 0;
            // Row 4
            translationMatrix[3, 0] = 0;
            translationMatrix[3, 1] = 0;
            translationMatrix[3, 2] = 0;
            translationMatrix[3, 3] = 1;

            // Multiply
            return Multiply(matrix, translationMatrix);
        }

        public static double[,] RotateZ(double[,] matrix, double theta)
        {
            double[,] translationMatrix = new double[4, 4];
            // Row 1
            translationMatrix[0, 0] = Math.Cos(theta);
            translationMatrix[0, 1] = -Math.Sin(theta);
            translationMatrix[0, 2] = 0;
            translationMatrix[0, 3] = 0;
            // Row 2
            translationMatrix[1, 0] = Math.Sin(theta);
            translationMatrix[1, 1] = Math.Cos(theta);
            translationMatrix[1, 2] = 0;
            translationMatrix[1, 3] = 0;
            // Row 3
            translationMatrix[2, 0] = 0;
            translationMatrix[2, 1] = 0;
            translationMatrix[2, 2] = 1;
            translationMatrix[2, 3] = 0;
            // Row 4
            translationMatrix[3, 0] = 0;
            translationMatrix[3, 1] = 0;
            translationMatrix[3, 2] = 0;
            translationMatrix[3, 3] = 1;

            // Multiply
            return Multiply(matrix, translationMatrix);
        }
        /*
        public static double[,] Reflect(double[,] matrix, Axis axis)
        {
            double[,] translationMatrix = new double[4, 4];

            if (axis == Axis.X) {
                // Row 1
                translationMatrix[0, 0] = -1;
                translationMatrix[0, 1] = 0;
                translationMatrix[0, 2] = 0;
                translationMatrix[0, 3] = 0;
                // Row 2
                translationMatrix[1, 0] = 0;
                translationMatrix[1, 1] = 1;
                translationMatrix[1, 2] = 0;
                translationMatrix[1, 3] = 0;
                // Row 3
                translationMatrix[2, 0] = 0;
                translationMatrix[2, 1] = 0;
                translationMatrix[2, 2] = 1;
                translationMatrix[2, 3] = 0;
                // Row 4
                translationMatrix[3, 0] = 0;
                translationMatrix[3, 1] = 0;
                translationMatrix[3, 2] = 0;
                translationMatrix[3, 3] = 1;
            }
            if (axis == Axis.Y)
            {
                // Row 1
                translationMatrix[0, 0] = 1;
                translationMatrix[0, 1] = 0;
                translationMatrix[0, 2] = 0;
                translationMatrix[0, 3] = 0;
                // Row 2
                translationMatrix[1, 0] = 0;
                translationMatrix[1, 1] = -1;
                translationMatrix[1, 2] = 0;
                translationMatrix[1, 3] = 0;
                // Row 3
                translationMatrix[2, 0] = 0;
                translationMatrix[2, 1] = 0;
                translationMatrix[2, 2] = 1;
                translationMatrix[2, 3] = 0;
                // Row 4
                translationMatrix[3, 0] = 0;
                translationMatrix[3, 1] = 0;
                translationMatrix[3, 2] = 0;
                translationMatrix[3, 3] = 1;
            }

            // Multiply
            return Multiply(matrix, translationMatrix);
        }*/

        public static void Shear()
        {

        }
    }
}
