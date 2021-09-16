using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountriesMap
{
    class Program
    {
        static int N;
        static int M;
        static int[,] coutryMap;

        public class Options
        {

            [Option('N', "rows", Required = true, HelpText = "Number of rows in the matrix")]
            public int Raws { get; set; }

            [Option('M', "columns", Required = true, HelpText = "Number of columns in the matrix")]
            public int Columns { get; set; }

            [Option('A', "matrix", Required = true, HelpText = "Numbers to fill the matrix (Separated by comma)")]
            public string Matrix { get; set; }
        }

        static void Main(string[] args)
        {
            if (args.Length>0)
            {
                Parser.Default.ParseArguments<Options>(args)
                  .WithParsed<Options>(o =>
                  {
                      N = o.Raws;
                      M = o.Columns;

                      string[] inputArray = o.Matrix.Split(new string[] { " ", ",", ", " }, StringSplitOptions.RemoveEmptyEntries);

                      int[] colours = Array.ConvertAll(inputArray, int.Parse);

                      int[,] matrix = new int[N, M];

                      for (int i = 0; i < N; i++)
                      {
                          for (int j = 0; j < M; j++)
                          {
                              matrix[i, j] = colours[i * M + j];
                          }
                      }

                      Console.WriteLine(solution(matrix));

                      for (int i = 0; i < N; i++)
                      {
                          for (int j = 0; j < M; j++)
                          {
                              Console.Write(string.Format("{0} ", coutryMap[i, j]));
                          }
                          Console.Write(Environment.NewLine + Environment.NewLine);
                      }
                      Console.ReadLine();
                  });

            }
            else
            {
                Console.WriteLine("Please eneter number of rows");

                N = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Please eneter number of columns");

                M = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Please enter {0} numbers to fill in the matrix", N * M);

                string input = Console.ReadLine();

                string[] inputArray = input.Split(new string[] { " ", ",", ", " }, StringSplitOptions.RemoveEmptyEntries);

                int[] colours = Array.ConvertAll(inputArray, int.Parse);

                int[,] matrix = new int[N, M];

                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < M; j++)
                    {
                        matrix[i, j] = colours[i * M + j];
                    }
                }

                Console.WriteLine(solution(matrix));

                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < M; j++)
                    {
                        Console.Write(string.Format("{0} ", coutryMap[i, j]));
                    }
                    Console.Write(Environment.NewLine + Environment.NewLine);
                }
                Console.ReadLine();
            }
        }

        private static int solution(int[,] A)
        {
            int countryCode = 0;

            coutryMap = new int[N, M];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (coutryMap[i, j] == 0)
                    {
                        coutryMap[i, j] = ++countryCode;
                    }
                    CheckNeighbors(A, i, j);
                }
            }
            return countryCode;
        }

        private static void CheckNeighbors(int[,] A, int row, int column)
        {
            if (column + 1 < M)
            {
                if (coutryMap[row, column + 1] == 0 && A[row, column + 1] == A[row, column])
                {
                    coutryMap[row, column + 1] = coutryMap[row, column];

                    CheckNeighbors(A, row, column + 1);
                }
            }
            if (column - 1 >= 0)
            {
                if (coutryMap[row, column - 1] == 0 && A[row, column - 1] == A[row, column])
                {
                    coutryMap[row, column - 1] = coutryMap[row, column];

                    CheckNeighbors(A, row, column - 1);
                }
            }
            if (row + 1 < N)
            {
                if (coutryMap[row + 1, column] == 0 && A[row + 1, column] == A[row, column])
                {
                    coutryMap[row + 1, column] = coutryMap[row, column];

                    CheckNeighbors(A, row + 1, column);
                }
            }
            if (row - 1 >= 0)
            {
                if (coutryMap[row - 1, column] == 0 && A[row - 1, column] == A[row, column])
                {
                    coutryMap[row - 1, column] = coutryMap[row, column];

                    CheckNeighbors(A, row - 1, column);
                }
            }
        }
    }
}
