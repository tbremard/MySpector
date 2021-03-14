namespace MySpector.Objects
{
    public class ComparaisonArg
    {
        public decimal Reference { get; set; }
        public bool OrEqual { get; set; }

        public ComparaisonArg()
        {

        }

        public ComparaisonArg(decimal reference, bool orEqual)
        {
            Reference = reference;
            OrEqual = orEqual;
        }
    }

    public class TextDoContainArg
    {
        public string Token { get; set; }
        public bool IgnoreCase { get; set; }
    }
}