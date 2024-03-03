using Audium.Persistance;
using Audium.WebApi;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Audium.Domain;
using Audium.Application.Helpers;

namespace Audium.Application.Tracks;

public class GetTrackMediaForTodayQuery : IRequest<TrackContentDto>
{
}

public class GetTrackForTodayQueryHandler : IRequestHandler<GetTrackMediaForTodayQuery, TrackContentDto>
{
    private readonly IDbContextFactory<AudiumDbContext> _dbContextFactory;
    private readonly IMediator _mediator;

    public GetTrackForTodayQueryHandler(IDbContextFactory<AudiumDbContext> dbContextFactory, IMediator mediator)
    {
        _dbContextFactory = dbContextFactory;
        _mediator = mediator;
    }

    public async Task<TrackContentDto> Handle(GetTrackMediaForTodayQuery request, CancellationToken cancellationToken)
    {
        var trackForToday = await GetTrackForToday(cancellationToken);

        if (trackForToday == null)
        {
            await _mediator.Send(
                new SetRandomTrackForDateCommand 
                { 
                    ForDate = DateOnly.FromDateTime(DateTime.UtcNow.Date) 
                }, 
                cancellationToken);

            trackForToday = await GetTrackForToday(cancellationToken);
        }

        var response = PrepareResponse(trackForToday);
        return response;

    }

    private async Task<Track?> GetTrackForToday(CancellationToken cancellationToken)
    {

        using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);

        return await context.TrackForDate
                    .Where(x => x.ForDate == today)
                    .Select(x => x.Track)
                    .FirstOrDefaultAsync(cancellationToken);
    }

    private static TrackContentDto PrepareResponse(Track? trackForToday)
    { 
        if (trackForToday == null)
        {
            return new TrackContentDto();
        }

        var content = TrackContentHelper.GetBinaryContentForTrack(trackForToday);

        return new TrackContentDto
        {
            TrackId = trackForToday.Id,
            Content = content
        };
    }
}