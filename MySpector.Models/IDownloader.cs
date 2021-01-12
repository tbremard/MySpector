namespace MySpector.Models
{
    public interface IDownloader
    {
        DownloadResponse Download(Trox item);
    }
}