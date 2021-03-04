namespace MySpector.Objects
{
    public class ComparaisonArg
    {
        public decimal Reference { get; set; }
        public bool OrEqual { get; set; }
    }

    public class TextDoContainArg
    {
        public string Token { get; set; }
        public bool IgnoreCase { get; set; }
    }
}