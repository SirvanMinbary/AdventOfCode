using AdventOfCode.Day4;
using AdventOfCode.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Tests
{
    [TestFixture]
    class Day4Tests
    {
        [Test]
        public void GetPuzzleInput()
        {
            var data = Utility.GetDataFromResource("GuardShifts.txt");

            Assert.IsTrue(data.Any());
        }

        [Test]
        public void MappingPuzzleInputToShift()
        {
            var data = GetExampleData();
            var day4 = new Day4.Day4();

            var result = day4.MapDataToShifts(data);

            Assert.IsTrue(result[0].Action == Day4.Action.BeginShift);
            Assert.IsTrue(result[0].DateTime == new DateTime(1518, 11, 1, 0, 0, 0));
            Assert.IsTrue(result[0].GuardId == 10);
        }

        [Test]
        public void ShiftDataIsComputedGuardSleepyTime()
        {
            var day4 = new Day4.Day4();
            var data = new List<Shift>
            {
                new Shift
                {
                    Action=Day4.Action.BeginShift,
                    GuardId=1,
                    DateTime=new DateTime(2018,12,05,0,0,0)
                },
                new Shift
                {
                    Action=Day4.Action.FallsAsleep,
                    DateTime= new DateTime(2018,12,5,0,5,0)
                },
                new Shift
                {
                    Action=Day4.Action.WakesUp,
                    DateTime=new DateTime(2018,12,5,0,10,0)
                }
            };

            var actual = day4.ComputeGuardsSleepyTime(data);
            var expected = new List<GuardSleepyTime>
            {
                new GuardSleepyTime{GuardId=1, MinuteAmount= new Dictionary<int, int>()}
            };

            var expectedDict = expected.Single().MinuteAmount;
            expectedDict.Add(5, 1);
            expectedDict.Add(6, 1);
            expectedDict.Add(7, 1);
            expectedDict.Add(8, 1);
            expectedDict.Add(9, 1);
            expectedDict.Add(10, 1);

            Assert.IsTrue(actual.Single().GuardId == expected.Single().GuardId);
            CollectionAssert.AreEqual(expectedDict, actual.Single().MinuteAmount);
        }

        [Test]
        public void GuardAndMostMinutesAsleepMultiplied()
        {
            var day4 = new Day4.Day4();
            var data = day4.MapDataToShifts(GetExampleData());
            var guardsMinutes = day4.ComputeGuardsSleepyTime(data);

            var actual = day4.FindGuardMostAsleepAtMinute(guardsMinutes);
            var expected = 10 * 24;

            Assert.AreEqual(expected, actual);
        }

        private static List<string> GetExampleData()
        {
            return new List<string>
            {
                "[1518 - 11 - 01 00:00] Guard #10 begins shift",
                "[1518 - 11 - 01 00:05] falls asleep",
                "[1518 - 11 - 01 00:25] wakes up",
                "[1518 - 11 - 01 00:30] falls asleep" ,
                "[1518 - 11 - 01 00:55] wakes up",
                "[1518 - 11 - 01 23:58] Guard #99 begins shift",
                "[1518 - 11 - 02 00:40] falls asleep",
                "[1518 - 11 - 02 00:50] wakes up",
                "[1518 - 11 - 03 00:05] Guard #10 begins shift",
                "[1518 - 11 - 03 00:24] falls asleep",
                "[1518 - 11 - 03 00:29] wakes up",
                "[1518 - 11 - 04 00:02] Guard #99 begins shift",
                "[1518 - 11 - 04 00:36] falls asleep",
                "[1518 - 11 - 04 00:46] wakes up",
                "[1518 - 11 - 05 00:03] Guard #99 begins shift",
                "[1518 - 11 - 05 00:45] falls asleep",
                "[1518 - 11 - 05 00:55] wakes up"
            };
        }
    }
}
