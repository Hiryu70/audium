using Audium.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Audium.Application.Tracks;
public class SuggestTracksForInputQuery : IRequest<IEnumerable<AristsTracksTitleDto>>
{
    public string Query { get; set; } = "";

    public int Page { get; set; }

    public int PageSize { get; set; }
}

public class GetTracksOptionsForInputQueryHandler : IRequestHandler<SuggestTracksForInputQuery, IEnumerable<AristsTracksTitleDto>>
{

    private readonly IDbContextFactory<AudiumDbContext> _dbContextFactory;

    public GetTracksOptionsForInputQueryHandler(IDbContextFactory<AudiumDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IEnumerable<AristsTracksTitleDto>> Handle(SuggestTracksForInputQuery request, CancellationToken cancellationToken)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var query = request.Query.ToLower();

        var artistsQuery = context.Artists
            .Where(x => x.ArtistName.ToLower().Contains(query))
            .SelectMany(x => x.Tracks, (artist, track) => new AristsTracksTitleDto
            {
                TrackName = track.TrackName,
                ArtistName = artist.ArtistName
            });

        var tracksQuery = context.Tracks
            .Where(x => x.TrackName.ToLower().Contains(query))
            .Select(x => new AristsTracksTitleDto
            {
                TrackName = x.TrackName,
                ArtistName = x.Artist.ArtistName
            });

        var result = await artistsQuery
            .Union(tracksQuery)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return result ?? Enumerable.Empty<AristsTracksTitleDto>();
    }
}

