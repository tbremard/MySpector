namespace MySpector
{

    public class Rump : IRump
    {
        public string Content { get; }
        public Rump(string content)
        {
            Content = content;
        }

    }
}