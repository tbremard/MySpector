namespace MySpector.Core
{
    public class EmptyXtraxRule : XtraxRule
    {
        public EmptyXtraxRule()
        {
        }

        protected override IDataTruck GetOutput(IDataTruck data)
        {
            return data;
        }
    }

}