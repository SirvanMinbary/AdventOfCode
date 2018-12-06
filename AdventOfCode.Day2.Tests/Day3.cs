using AdventOfCode.Day3;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Tests
{
    [TestFixture]
    class Day3
    {
        [Test]
        public void MapDataToClaims()
        {
            var data = GetClaimsRawData();

            var helper = new FabricHelper();

            var actual = helper.MapDataToClaims(data)[0];
            var expected = GetClaims()[0];

            Assert.IsTrue(actual.Id == expected.Id);
            Assert.IsTrue(actual.X == expected.X);
            Assert.IsTrue(actual.Y == expected.Y);
            Assert.IsTrue(actual.Height == expected.Height);
            Assert.IsTrue(actual.Width == expected.Width);
        }

        private List<string> GetClaimsRawData()
        {
            return new List<string>
            {
                "#1 @ 1,3: 4x4",
                "#2 @ 3,1: 4x4",
                "#3 @ 5,5: 2x2"
            };
        }

        private List<Claim> GetClaims()
        {
            return new List<Claim>
            {
                new Claim { Id = 1, X = 1, Y = 3, Height = 4, Width = 4 },
                new Claim { Id = 2, X = 3, Y = 1, Height = 4, Width = 4 },
                new Claim { Id = 3, X = 5, Y = 5, Height = 2, Width = 2 },
            };
        }
    }
}
