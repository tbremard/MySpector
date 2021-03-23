namespace MySpector.Objects
{
    public class ResultStorage
    {
        public int? TroxId { get; }
        public IDataTruck Result { get; }
        public LocalFile File { get;}
        public int? DbId { get; set; }
       
        public ResultStorage(int? troxId, IDataTruck result, LocalFile file)
        {
            TroxId = troxId;
            this.Result = result;
            this.File = file;
        }
    }
}