namespace MySpector
{
    public class Trox
    {
        public Data ExtractData(Rump rump, ITroxRule rule)
        {
            string value = rule.GetOutput(rump);
            var ret = new Data(value);
            return ret;
        }
    }
}
