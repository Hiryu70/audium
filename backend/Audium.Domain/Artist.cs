using Audium.Domain.Common;

namespace Audium.Domain;

public class Artist : BaseEntity
{
    public string ArtistName { get; set; }
    public virtual ICollection<Track> Tracks { get; set; }
}