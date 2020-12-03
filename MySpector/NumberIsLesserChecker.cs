namespace MySpector
{
    public class NumberIsLesserChecker : IChecker
    {
        decimal Sample;
        decimal Reference;
        bool OrEqual;

        public NumberIsLesserChecker(decimal reference, bool orEqual)
        {
            Reference = reference;
            OrEqual = orEqual;
        }

        public bool Check(IInputData input)
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
            if (OrEqual)
            {
                ret = Sample <= Reference;
            }
            else
            {
                ret = Sample < Reference;
            }
            return ret;
        }
    }
}
