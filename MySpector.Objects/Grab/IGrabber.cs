namespace MySpector.Objects
{
    public interface IGrabber
    {
        GrabResponse Grab(IGrabTarget target);
    }
}