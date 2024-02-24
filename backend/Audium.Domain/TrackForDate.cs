using Audium.Domain.Common;

namespace Audium.Domain;

public class TrackForDate : BaseEntity
{
    public long TrackId { get; set; }
    public Track Track { get; set; }
    public DateOnly ForDate { get; set; }
}
