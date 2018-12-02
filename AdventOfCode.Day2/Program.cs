using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AdventOfCode.Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            PartOne();

            PartTwo();

            Console.ReadLine();
        }

        static void PartOne()
        {
            Console.WriteLine("--- Day 2 ---");
            Console.WriteLine("--- Part 1 ---");

            var scan = new BoxScan();
            Console.WriteLine("Scanning box IDs...");
            var ids = scan.GetBoxIds();

            Console.WriteLine("Counting letters that appears twice...");
            var first = scan.CountMultipleAppearingLetters(ids, 2);

            Console.WriteLine("Counting letters that appears three times...");
            var second = scan.CountMultipleAppearingLetters(ids, 3);

            Console.WriteLine("Generating checksum...");
            var checksum = scan.GenerateBoxCheckSum(first, second);
            Console.WriteLine($"Checksum is {checksum}");
        }

        static void PartTwo()
        {
            Console.WriteLine("--- Part 2 ---");

            var scan = new BoxScan();
            Console.WriteLine("Scanning box IDs...");
            var ids = scan.GetBoxIds();

            Console.WriteLine("Finding similiar IDs...");
            var data = scan.FindCommonLetters(ids);

            Console.WriteLine("Finding common letters...");
            var commonLetters = scan.RemoveDifferentLetters(data.Item1, data.Item2);

            Console.WriteLine($"The common letters are: {commonLetters}");
        }
    }

    public class BoxScan
    {
        public List<string> GetBoxIds()
        {
            var file = Assembly.GetExecutingAssembly().GetManifestResourceStream("AdventOfCode.Day2.Boxes.txt");

            var result = new List<string>();
            using (var stream = new StreamReader(file))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    result.Add(line);
                }
            }

            return result;
        }

        public int CountMultipleAppearingLetters(List<string> ids, int condition)
        {
            int result = 0;
            foreach (var item in ids)
            {
                var query = item.ToCharArray()
                    .GroupBy(c => c)
                    .Where(g => g.Count() == condition)
                    .Select(g => g.Key);

                if (query.Any())
                {
                    result++;
                }
            }

            return result;
        }

        public int GenerateBoxCheckSum(int first, int second)
        {
            return first * second;
        }

        public Tuple<string, string> FindCommonLetters(List<string> ids)
        {
            foreach (var item in ids)
            {
                foreach (var item2 in ids)
                {
                    if (item == item2)
                    {
                        continue;
                    }

                    var dist = ComputeLevenshteinDistance(item, item2);
                    if (dist == 1)
                    {
                        return new Tuple<string, string>(item, item2);
                    }
                }
            }

            return null;
        }

        public string RemoveDifferentLetters(string first, string second)
        {
            var result = "";

            for (int i = 0; i < first.Length; i++)
            {
                if (first[i] == second[i])
                {
                    result += first[i];
                }
            }

            return result;
        }

        public static int ComputeLevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }
    }
}
