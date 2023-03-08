using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        // Random object gets created
        static Random r = new Random();

        static void Main(string[] args)
        {   //width,height and n values are taken.
            //weight, height and n values converted to  32 bit integers
            Console.WriteLine("Enter width:");
            int width = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter height:");
            int height = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter n:");
            int n = Convert.ToInt32(Console.ReadLine());

            point[] locations = new point[n]; //an object array which contains the points
            RandomPointGen(width, height, n, r, locations);
            for (int a = 0; a < n; a++)// writes down  the coordinates
            {
                Console.WriteLine("X: {0}, Y: {1}", locations[a].X, locations[a].Y);
            }
            double[,] distances = new double[n, n]; // a matrix that holds distances of each point
            DistanceMatrix(locations, distances);

            Console.Write("       ");
            for (int i = 0; i < n; i++)
            {
                Console.Write(String.Format("{0, 10} |", i));
            }
            Console.WriteLine();
            for (int j = 0; j < n; j++)
            {
                Console.Write(String.Format("{0, 5} |", j));
                for (int b = 0; b < n; b++)
                {
                    // writes down the distances matrix
                    Console.Write(String.Format("{0,10:0.000} |", distances[b, j]));
                }
                Console.WriteLine();
            }
            NearestNeighbor(locations, n, distances);

            static void RandomPointGen(int width, int height, int n, Random r, point[] locations)
            {

                double X;
                double Y;
                for (int i = 0; i < n; i++)
                {
                    X = r.NextDouble() * width;
                    // random object times width axis 
                    Y = r.NextDouble() * height;
                    // random object times height axis          
                    locations[i] = new point(X, Y);
                }
            }
            static void DistanceMatrix(point[] locations, double[,] distances)  //Fills the distances matrix.
            {
                int len = distances.GetLength(0); //length of the distances matrix
                for (int i = 0; i < len; i++)
                {
                    for (int j = 0; j < len; j++)
                    {
                        double dist = Math.Sqrt(Math.Pow((locations[i].X - locations[j].X), 2) + Math.Pow((locations[i].Y - locations[j].Y), 2));
                        distances[i, j] = dist;
                    }
                }
            }
            static int FindShortest(point[] locations, int a, double[,] distances) // Finds the shortest non-visisted next point
            {
                int len = distances.GetLength(0); //length of the distances matrix
                int finali = 0; //Location or name of the point we desire
                double c = double.PositiveInfinity; // We strat with infinity for finding shortest route
                for (int i = 0; i < len; i++)
                {
                    if (distances[a, i] != 0) // point with a 0 distance to point a is itself, and we don't want that
                    {
                        if (locations[i].visited == false)
                        {
                            if (c > distances[a, i])
                            {
                                c = distances[a, i];
                                finali = i;
                            }
                        }
                    }
                }
                return finali;
            }
            static void NearestNeighbor(point[] locations, int n, double[,] distances) // finds the shortest available route acording to nearest neighbor method 10 times starting with a random point.
            {
                List<int> usedList = new List<int>(); //a list that holds  auto generated startingpoints for controlling uniqueness
                int starpoint; // an auto generated starting point
                int otherpoint; // a point that has the shortest distance to any given starting point.
                double distance; // distance between any given 2 points
                double sumOfDisatnces; //total distance of any given path
                int[,] path = new int[n, 1]; // a path that has the shortest route acording to nearest neighbor method
                for (int i = 0; i < 10; i++)
                {
                    starpoint = r.Next(0, n);
                    while (usedList.Contains(starpoint)) // if that startingpoint had been used before creates a new one
                    {
                        starpoint = r.Next(0, n);
                    }
                    usedList.Add(starpoint);
                    sumOfDisatnces = 0;
                    Console.WriteLine(" ");
                    Console.Write("Shortest path starting with " + starpoint + " is: [");
                    for (int a = 0; a < n; a++)
                    {
                        locations[starpoint].visited = true;
                        otherpoint = FindShortest(locations, starpoint, distances);
                        distance = distances[starpoint, a];
                        path[a, 0] = starpoint;
                        starpoint = otherpoint;
                        sumOfDisatnces += distance;
                    }
                    for (int a = 0; a < n; a++) // clears the visited stamps of the previous path and writes down  that same path
                    {
                        locations[a].visited = false;
                        Console.Write(path[a, 0]);
                        Console.Write(" ");
                    }
                    Console.Write("]");
                    Console.WriteLine(" ");
                    Console.Write("And its length is: ");
                    Console.Write(sumOfDisatnces);

                }
            }
        }
    }
    public class point // A class for storing the location of the point and visited stamp
    {
        public double X;
        public double Y;
        public bool visited;
        public point(double tX, double tY)
        {
            X = tX;
            Y = tY;
            visited = false;
        }
    }
}

