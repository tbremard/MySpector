namespace MySpector.Objects
{
    public class EmptyXtrax : Xtrax
    {
        public override XtraxType Type => XtraxType.Empty;

        public EmptyXtrax()
        {
        }

        protected override IDataTruck GetOutput(IDataTruck data)
        {
            return data;
        }
    }

}