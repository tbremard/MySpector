namespace MySpector
{
    public class NumberIsEqualChecker : IChecker
    {
        decimal Sample;
        decimal Reference;

        public NumberIsEqualChecker(decimal sample, decimal reference)
        {
            Sample = sample;
            Reference = reference;
        }

        public bool Check()
        {
            bool ret;
            ret = Sample == Reference;
            return ret;
        }
    }

}
