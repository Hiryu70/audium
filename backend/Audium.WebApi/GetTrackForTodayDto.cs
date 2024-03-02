namespace Audium.WebApi
{
    public class GetTrackForTodayDto
    {
        public DateOnly Date { get; set; }

        public string Path { get; set; }
    }
}