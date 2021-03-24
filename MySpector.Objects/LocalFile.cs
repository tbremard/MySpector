using System;
using System.Text;

namespace MySpector.Objects
{
    public class LocalFile
    {
        public string FilePath;
        public IDataTruck Truck;
        public TimeSpan Latency;
        public bool GrabSuccess = true;
        public bool XtraxSuccess = true;
        public StringBuilder ErrorMessage { get; }
        public bool IsSignaled = false;

        public LocalFile()
        {
            ErrorMessage = new StringBuilder();
        }
    }
}
