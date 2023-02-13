using System;

namespace LinearEquationSystemSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the number of unknowns: ");
            int unknowns = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the number of equations: ");
            int equations = int.Parse(Console.ReadLine());

            if (unknowns != equations)
            {
                Console.WriteLine("Error: The number of unknowns must be equal to the number of equations");
                return;
            }

            Console.WriteLine("Enter the augmented matrix ({0} rows, {1} columns): ", equations, unknowns + 1);
            double[,] coefficients = new double[equations, unknowns + 1];
            for (int i = 0; i < equations; i++)
            {
                double[] row = Console.ReadLine().Split().Select(double.Parse).ToArray();
                for (int j = 0; j < unknowns + 1; j++)
                {
                    coefficients[i, j] = row[j];
                }
            }

            Console.WriteLine("The equation system is: ");
            for (int i = 0; i < equations; i++)
            {
                for (int j = 0; j < unknowns; j++)
                {
                    Console.Write("{0}x{1} + ", coefficients[i, j], j + 1);
                }
                Console.Write("= {0}", coefficients[i, unknowns]);
                Console.WriteLine();
            }

            double[] solutions = SolveLinearEquationSystem(equations, unknowns, coefficients);
            Console.WriteLine("The solutions of the system are: ");
            for (int i = 0; i < unknowns; i++)
            {
                Console.WriteLine("x{0} = {1}", i + 1, solutions[i]);
            }
        }

        static double[] SolveLinearEquationSystem(int equations, int unknowns, double[,] coefficients)
        {
            // Forward elimination
            for (int i = 0; i < unknowns - 1; i++)
            {
                for (int j = i + 1; j < unknowns; j++)
                {
                    double factor = coefficients[j, i] / coefficients[i, i];
                    for (int k = i; k < unknowns + 1; k++)
                    {
                        coefficients[j, k] -= factor * coefficients[i, k];
                    }
                }
            }

            // Back substitution
            double[] solutions = new double[unknowns];
            for (int i = unknowns - 1; i >= 0; i--)
            {
                solutions[i] = coefficients[i, unknowns];
                for (int j = i + 1; j < unknowns; j++)
                {
                    solutions[i] -= coefficients[i, j] * solutions[j];
                }
                solutions[i] /= coefficients[i, i];
            }

            return solutions;
        }
    }
}
