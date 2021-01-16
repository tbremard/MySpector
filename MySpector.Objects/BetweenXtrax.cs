using MySpector.Objects;

namespace MySpector
{
    public class BetweenXtrax : Xtrax
    {
        private readonly Xtrax _before;
        private readonly Xtrax _after;

        public BetweenXtrax(string prefix, string suffix)
        {
            _after = new AfterXtrax(prefix);
            _before = new BeforeXtrax(suffix);
        }

        protected override IDataTruck GetOutput(IDataTruck data)
        {
            IDataTruck ret;
            var after = _after.GetOutputChained(data);
            if (after.GetText() == XtraxConst.NOT_FOUND)
            {
                return DataTruck.CreateText(XtraxConst.NOT_FOUND);
            }
            ret = _before.GetOutputChained(after);
            return ret;
        }
    }
}