namespace MySpector.Objects
{
    public interface IDownloader
    {
        DownloadResponse Download(IWebTarget target);
    }
}