using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            PartOne();

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
    }

    public class BoxScan
    {
        public List<string> GetBoxIds()
        {
            var file = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("AdventOfCode.Day2.Boxes.txt");

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
    }
}
