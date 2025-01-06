using System.Collections.Generic;

namespace GoldenRaspberryAPI.Model
{
    public class ProducerAward
    {
        public string Producer { get; set; }
        public int Interval { get; set; }
        public int PreviousWin { get; set; }
        public int FollowingWin { get; set; }
    }

    public class AwardIntervalsResponse
    {
        public List<ProducerAward> Min { get; set; }
        public List<ProducerAward> Max { get; set; }
    }
}