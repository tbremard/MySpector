using System;

namespace MySpector.Objects
{
    public class Result
    {
        public int? TroxId { get; }
        public IDataTruck Truck { get; }
        public TimeSpan Latency { get; set; }
        public int? DbId { get; set; }

        public Result(int? troxId, IDataTruck truck, TimeSpan latency)
        {
            TroxId = troxId;
            Truck = truck;
            Latency = latency;
        }
    }
}