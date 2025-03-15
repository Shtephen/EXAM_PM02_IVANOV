using System;
using System.Linq;

public class Program
{
    // Общее количество вершин (точек)
    public const int n = 10;

    public static double[,] CreateGraph()
    {
        double[,] a = new double[n, n];

        // Инициализация матрицы смежности 
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                a[i, j] = i == j ? 0 : double.MaxValue;

        // Заполнение графа расстояниями между точками
        a[0, 1] = 0.94;
        a[1, 0] = 0.94;
        a[1, 2] = 0.66;
        a[2, 1] = 0.66;
        a[2, 3] = 1.04;
        a[3, 2] = 1.04;
        a[3, 5] = 0.77;
        a[5, 3] = 0.77;
        a[4, 5] = 1.92;
        a[5, 4] = 1.92;
        a[2, 5] = 1.7;
        a[5, 2] = 1.7;
        a[0, 6] = 1.88;
        a[6, 0] = 1.88;
        a[1, 6] = 1.2;
        a[6, 1] = 1.2;
        a[5, 9] = 1.52;
        a[9, 5] = 1.52;
        a[6, 7] = 0.53;
        a[7, 6] = 0.53;
        a[7, 8] = 1.54;
        a[8, 7] = 1.54;
        a[8, 9] = 0.86;
        a[9, 8] = 0.86;

        return a;
    }

    public static void Main(string[] args)
    {
        double[,] graph = CreateGraph();

        int startPoint = GetStartPoint();
        double[] distances = Dijkstra(graph, startPoint);

        int[] points = GetInterestedPoints();

        foreach (int point in points)
        {
            Console.WriteLine($"Минимальное расстояние до точки {point + 1}: {distances[point]:F2} км");
        }
    }

    public static int GetStartPoint()
    {
        Console.Write("Введите номер стартовой точки (1-10): ");
        int startPoint;
        while (!int.TryParse(Console.ReadLine(), out startPoint) || startPoint < 1 || startPoint > 10)
        {
            Console.Write("Некорректный ввод. Введите номер стартовой точки (1-10): ");
        }
        return startPoint - 1;
    }

    public static int[] GetInterestedPoints()
    {
        Console.Write("Введите номера интересующих точек (через запятую, например: 2,4,7): ");
        string input;
        var points = new int[n];
        bool validPoints = false;

        while (!validPoints)
        {
            input = Console.ReadLine();

            try
            {
                points = input.Split(',')
                    .Select(x => x.Trim())
                    .Select(num => int.TryParse(num, out int result) && result >= 1 && result <= 10 ? result - 1 : throw new Exception())
                    .Distinct()
                    .ToArray();

                if (points.Length == 0)
                {
                    throw new Exception();
                }

                validPoints = true;
            }
            catch
            {
                Console.Write("Некорректный ввод. Введите номера интересующих точек (через запятую, например: 2,4,7): ");
            }
        }

        return points;
    }

    private static double[] Dijkstra(double[,] a, int v0)
    {
        double[] dist = new double[n];
        bool[] vis = new bool[n];
        int unvis = n;

        for (int i = 0; i < n; i++)
            dist[i] = Double.MaxValue;

        dist[v0] = 0.0;

        while (unvis > 0)
        {
            int v = -1;
            for (int i = 0; i < n; i++)
            {
                if (vis[i]) 
                    continue;
                if (v == -1 || dist[v] > dist[i]) 
                    v = i;
            }

            vis[v] = true;
            unvis--;
            for (int i = 0; i < n; i++)
            {
                if (dist[i] > dist[v] + a[v, i])
                    dist[i] = dist[v] + a[v, i];
            }
        }
        return dist;
    }
}