namespace MySpector
{
    public class NumberIsEqualChecker : IChecker
    {
        decimal Sample;
        decimal Reference;

        public NumberIsEqualChecker(decimal reference)
        {
            Reference = reference;
        }

        public bool Check(IDataTruck input)
        {
            bool ret;
            if (input == null)
                return false;
            decimal? number = input.GetNumber();
            if (!number.HasValue)
            {
                return false;
            }
            Sample = number.Value;
            ret = Sample == Reference;
            return ret;
        }
    }
}
