using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.DayOne
{
    class Program
    {
        static void Main(string[] args)
        {
            var device = new FrequencyDevice();
            Console.WriteLine("Loading new frequency...");
            var frequencies = device.GetFrequencyChanges();
            foreach (var freq in frequencies)
            {
                Console.Write($"Current frequency {device.CurrentFrequency}, change of {freq};");
                device.ChangeFrequency(freq);
                Console.Write($" result frequency {device.CurrentFrequency}. \n");
            }

            Console.WriteLine($"Final frequency: {device.CurrentFrequency}");
            Console.ReadLine();
        }
    }

    public class FrequencyDevice
    {
        public int CurrentFrequency { get; set; }

        public void ChangeFrequency(int change)
        {
            CurrentFrequency += change;
        }

        public List<int> GetFrequencyChanges()
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
    }
}
