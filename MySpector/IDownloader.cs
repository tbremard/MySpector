using MySpector.Core;

namespace MySpector
{
    public interface IDownloader
    {
        DownloadResponse Download(Trox item);
    }
}