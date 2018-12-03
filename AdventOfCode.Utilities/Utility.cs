using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AdventOfCode.Utilities
{
    public static class Utility
    {
        public static List<string> GetDataFromResource(string fileName)
        {
            var path = $"AdventOfCode.Utilities.PuzzleInput.{fileName}";
            var file = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);

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
    }
}
