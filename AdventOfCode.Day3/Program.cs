using AdventOfCode.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Day3
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    public class FabricHelper
    {
        public List<string> GetClaimsData()
        {
            return Utility.GetDataFromResource("Claims.txt");
        }

        public List<Claim> MapDataToClaims(List<string> data)
        {
            var result = new List<Claim>();

            foreach (var item in data)
            {
                var claim = new Claim();

                var split = item.Split('@', ':');

                claim.Id = int.Parse(split[0].Substring(1));

                var coords = split[1].Split(',');
                claim.X = int.Parse(coords[0]);
                claim.Y = int.Parse(coords[1]);

                var dimensions = split[2].Split('x');
                claim.Height = int.Parse(dimensions[0]);
                claim.Width = int.Parse(dimensions[1]);

                result.Add(claim);
            }

            return result;
        }

        public int CalculateOverlappingArea(List<Claim> claims)
        {
            throw new NotImplementedException();
        }
    }
}
