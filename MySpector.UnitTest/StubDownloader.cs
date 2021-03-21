using MySpector.Objects;
using System;

namespace MySpector.UnitTest
{
    public class StubDownloader : IGrabber
    {
        public StubDownloader(IDataTruck data)
        {
            Data = data;
        }

        public IDataTruck Data { get; }

        public GrabResponse Grab(IGrabTarget target)
        {
            var ret = new GrabResponse(Data.GetText(), true, TimeSpan.Zero);
            return ret;
        }
    }
}
