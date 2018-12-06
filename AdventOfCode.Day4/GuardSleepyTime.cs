using System.Collections.Generic;

namespace AdventOfCode.Day4
{
    public class GuardSleepyTime
    {
        public int GuardId { get; set; }
        public int TotalMinutes { get; set; }
        public Dictionary<int, int> MinuteAmount { get; set; } = new Dictionary<int, int>();
    }
}
