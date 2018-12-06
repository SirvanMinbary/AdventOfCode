using AdventOfCode.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Day4
{
    public class Program
    {
        static void Main(string[] args)
        {
            var program = new Day4();
            var data = Utility.GetDataFromResource("GuardShifts.txt");
            var shifts = program.MapDataToShifts(data);
            var timesAsleep = program.ComputeGuardsSleepyTime(shifts);
            var partOneResult = program.FindGuardMostAsleepAtMinute(timesAsleep);
            Console.WriteLine($"Part one: {partOneResult}");

            var partTwoResult = program.FindMostFrequentlyAsleepAtSameMinute(timesAsleep);
            Console.WriteLine($"Part two: {partTwoResult}");

            Console.ReadKey();
        }

        private static void PartOne()
        {

        }
    }

    public class Day4
    {
        public List<Shift> MapDataToShifts(List<string> data)
        {
            var result = new List<Shift>();

            foreach (var item in data)
            {
                var shift = new Shift();
                var split = item.Split('[', ']');
                shift.DateTime = DateTime.Parse(split[1]);

                shift.Action = FindAction(split[2]);

                if (shift.Action == Action.BeginShift)
                {
                    shift.GuardId = FindGuardId(split[2]);
                }

                result.Add(shift);
            }

            return result.OrderBy(s => s.DateTime).ToList();
        }

        public List<GuardSleepyTime> ComputeGuardsSleepyTime(List<Shift> shifts)
        {
            GuardSleepyTime currentSleepTime = null;
            DateTime? fellAsleepAt = null;

            var result = new List<GuardSleepyTime>();

            foreach (var shift in shifts)
            {
                switch (shift.Action)
                {
                    case Action.BeginShift:
                        currentSleepTime = result.SingleOrDefault(r => r.GuardId == shift.GuardId.Value);

                        if (currentSleepTime == null)
                        {
                            result.Add(new GuardSleepyTime { GuardId = shift.GuardId.Value });

                            currentSleepTime = result.SingleOrDefault(r => r.GuardId == shift.GuardId.Value);
                        }
                        break;
                    case Action.FallsAsleep:
                        fellAsleepAt = shift.DateTime;
                        break;
                    case Action.WakesUp:
                        var wakesUpAt = shift.DateTime;
                        var totalMinutesAsleep = (int)wakesUpAt.Subtract(fellAsleepAt.Value).TotalMinutes;

                        for (int i = 0; i < totalMinutesAsleep + 1; i++)
                        {
                            var minute = fellAsleepAt.Value.Minute + i;
                            if (currentSleepTime.MinuteAmount.ContainsKey(minute))
                            {
                                currentSleepTime.MinuteAmount[minute]++;
                            }
                            else
                            {
                                currentSleepTime.MinuteAmount.Add(minute, 1);
                            }
                        }

                        currentSleepTime.TotalMinutes += totalMinutesAsleep;
                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        public int FindGuardMostAsleepAtMinute(List<GuardSleepyTime> sleepyTimes)
        {
            var guardWithMostMinutesAsleep = sleepyTimes.OrderByDescending(x => x.TotalMinutes).First();

            var mostCommonMinuteAsleep = guardWithMostMinutesAsleep.MinuteAmount.OrderByDescending(x => x.Value).First().Key;

            return guardWithMostMinutesAsleep.GuardId * mostCommonMinuteAsleep;
        }

        public int FindMostFrequentlyAsleepAtSameMinute(List<GuardSleepyTime> sleepyTimes)
        {
            var guardId = 0;
            var timesAsleepOnMinute = 0;
            var minute = 0;
            foreach (var item in sleepyTimes)
            {
                var mostFrequentlyMinuteAsleep = item.MinuteAmount.OrderByDescending(x => x.Value).FirstOrDefault();

                if (mostFrequentlyMinuteAsleep.Value > timesAsleepOnMinute)
                {
                    minute = mostFrequentlyMinuteAsleep.Key;
                    timesAsleepOnMinute = mostFrequentlyMinuteAsleep.Value;
                    guardId = item.GuardId;
                }
            }

            return guardId * minute;
        }

        private int FindGuardId(string s)
        {
            var pattern = @"\d+";
            var match = Regex.Match(s, pattern);

            return int.Parse(match.Value);
        }

        private Action FindAction(string s)
        {
            var substring = s.Substring(1, 5);

            switch (substring)
            {
                case "Guard":
                    return Action.BeginShift;
                case "falls":
                    return Action.FallsAsleep;
                case "wakes":
                    return Action.WakesUp;
                default:
                    break;
            }

            throw new Exception($"Could not find action in string {s}");
        }
    }
}
