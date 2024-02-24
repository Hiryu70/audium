namespace Audium.WebApi;

public class TrackContentDto
{
    public long TrackId { get; set; }
    public string FileName { get; set; }
    public byte[] Content { get; set; }
}