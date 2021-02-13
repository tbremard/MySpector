namespace MySpector.Objects
{
    public interface IChecker
    {
        public bool Check(IDataTruck input);// Legacy
        public int? DbId { get; set; }      //<<<<<<< needed for DB insertion
        public CheckerType Type { get; }    //<<<<<<< needed for DB insertion
        public string JsonArg { get;}       //<<<<<<< needed for DB insertion
    }
}
