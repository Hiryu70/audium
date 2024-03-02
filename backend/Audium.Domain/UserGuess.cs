using Audium.Domain.Common;

namespace Audium.Domain;

public class UserGuess : BaseEntity
{
    public string Cookie { get; set; }
    public long TrackId { get; set; }
    public Track Track { get; set; }
    public DateOnly GuessDate { get; set; }
    public DateTime GuessStart { get; set; }
    public DateTime GuessEnd { get; set; }
    public bool IsCorrect { get; set; }
    public int AttemptsCount { get; set; }
}