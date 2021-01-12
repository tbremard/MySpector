using MySpector.Models;
using System;

namespace MySpector.UnitTest
{
    public class StubDownloader : IDownloader
    {
        public StubDownloader(IDataTruck data)
        {
            Data = data;
        }

        public IDataTruck Data { get; }

        public DownloadResponse Download(Trox item)
        {
            var ret = new DownloadResponse(Data.GetText(), true, TimeSpan.Zero);
            return ret;
        }
    }
}
