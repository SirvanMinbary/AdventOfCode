using System;

namespace AdventOfCode.Day4
{
    public class Shift
    {
        public DateTime DateTime { get; set; }
        public int? GuardId { get; set; }
        public Action Action { get; set; }
    }

    public enum Action
    {
        BeginShift,
        FallsAsleep,
        WakesUp
    }
}
