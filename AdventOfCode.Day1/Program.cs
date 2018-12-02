using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.DayOne
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Day 1 ---");
            Console.WriteLine("Part one");

            var device = new FrequencyDevice();
            device.Calibrate();

            Console.WriteLine("Part two");

            device = new FrequencyDevice();
            device.FindDuplicateFrequency();

            Console.ReadLine();
        }
    }

    class FrequencyDevice
    {
        public int CurrentFrequency { get; set; }

        public void Calibrate()
        {
            var device = new FrequencyDevice();
            Console.WriteLine("Loading new frequency...");
            var frequencies = device.GetFrequencyChanges();
            foreach (var freq in frequencies)
            {
                device.ChangeFrequency(freq);
            }

            Console.WriteLine($"Final frequency: {device.CurrentFrequency}");
        }

        public void FindDuplicateFrequency()
        {
            Console.WriteLine("Loading new frequencies...");
            var frequencies = GetFrequencyChanges();
            var previousFrequencies = new List<int>();
            int loops = 0;
            bool isDuplicateFound = false;

            Console.WriteLine("Searching for duplicate frequencies...");
            while (isDuplicateFound == false)
            {
                Console.WriteLine($"Starting search on loop {loops}");
                foreach (var freq in frequencies)
                {
                    ChangeFrequency(freq);

                    if (previousFrequencies.Contains(CurrentFrequency))
                    {
                        Console.WriteLine($"Duplicate frequency found: {CurrentFrequency} after {loops} loops");
                        isDuplicateFound = true;
                        break;
                    }

                    previousFrequencies.Add(CurrentFrequency);
                }

                loops++;
            }
        }

        private List<int> GetFrequencyChanges()
        {
            var file = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("AdventOfCode.Day1.FrequencyChanges.txt");

            var result = new List<int>();
            using (var stream = new StreamReader(file))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    var parse = int.TryParse(line, out int freq);
                    if (parse)
                    {
                        result.Add(freq);
                    }
                }
            }

            return result;
        }

        private void ChangeFrequency(int change)
        {
            Console.Write($"Current frequency {CurrentFrequency}, change of {change};");
            CurrentFrequency += change;
            Console.Write($" result frequency {CurrentFrequency}. \n");
        }
    }
}
