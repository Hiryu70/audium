using Audium.Domain.Common;

namespace Audium.Domain;

public class Track : BaseEntity
{
    public string TrackName { get; set; }
    public long ArtistId { get; set; }
    public Artist Artist { get; set; }
    public string? ExternalSourceUrl { get; set; }
    public string? StorageFileName { get; set; }
    public virtual ICollection<TrackForDate> TrackForDates { get; set; }
}
