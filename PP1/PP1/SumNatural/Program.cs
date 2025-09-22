using System;

class Program
{
    static int SumFor(int n)
    {
        unchecked { return (n * (n + 1)) / 2; }
    }

    static int SumIteSlow(int n)
    {
        int sum = 0;
        unchecked
        {
            for (int i = 1; i <= n; i++) sum += i;
        }
        return sum;
    }

    static int SumIte(int n)
    {
        unchecked
        {
            if ((n & 1) == 0)             
                return (n / 2) * (n + 1);
            else                          
                return n * ((n + 1) / 2);
        }
    }

    static (int n, int sum) SearchAscending(Func<int, int> f)
    {
        int lastN = 0, lastS = 0;
        for (int n = 1; n > 0; n++)
        {
            int s = f(n);
            if (s > 0) { lastN = n; lastS = s; }
            else break;
        }
        return (lastN, lastS);
    }
    static (int n, int sum) SearchDescending(Func<int, int> f)
    {
        for (int n = int.MaxValue; n >= 1; n--)
        {
            int s = f(n);
            if (s > 0) return (n, s);
        }
        return (0, 0);
    }

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var upFor = SearchAscending(SumFor);
        var downFor = SearchDescending(SumFor);

        var upIte = SearchAscending(SumIte);
        var downIte = SearchDescending(SumIte);

        var upIteSlow = SearchAscending(SumIteSlow);

        Console.WriteLine();
        Console.WriteLine("• SumFor:");
        Console.WriteLine($"        ◦ From 1 to Max → n: {upFor.n} → sum: {upFor.sum}");
        Console.WriteLine($"        ◦ From Max to 1 → n: {downFor.n} → sum: {downFor.sum}");
        Console.WriteLine();
        Console.WriteLine("• SumIte (optimizada):");
        Console.WriteLine($"        ◦ From 1 to Max → n: {upIte.n} → sum: {upIte.sum}");
        Console.WriteLine($"        ◦ From Max to 1 → n: {downIte.n} → sum: {downIte.sum}");
        Console.WriteLine();
        Console.WriteLine("• SumIteSlow (iterativa literal):");
        Console.WriteLine($"        ◦ From 1 to Max → n: {upIteSlow.n} → sum: {upIteSlow.sum}");
        Console.WriteLine();
        Console.WriteLine("Presione cualquier tecla para salir…");
        Console.ReadKey();
    }
}

