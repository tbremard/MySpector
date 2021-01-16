namespace MySpector.Objects
{
    public class EmptyXtrax : Xtrax
    {
        public EmptyXtrax()
        {
        }

        protected override IDataTruck GetOutput(IDataTruck data)
        {
            return data;
        }
    }

}