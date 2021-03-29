using System.Text;

namespace MySpector.Objects
{
    public class ResultStorage
    {
        public int? TroxId { get; }
        public IDataTruck Result { get; }
        public LocalFile File { get;}
        public StringBuilder MemoryLog { get; }
        public int? DbId { get; set; }
       
        public ResultStorage(int? troxId, IDataTruck result, LocalFile file, StringBuilder memoryLog)
        {
            TroxId = troxId;
            this.Result = result;
            this.File = file;
            MemoryLog = memoryLog;
        }
    }
}