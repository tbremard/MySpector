using MySpector.Core;

namespace MySpector
{
    public interface IDownloader
    {
        DownloadResponse Download(WatchItem item);
    }
}