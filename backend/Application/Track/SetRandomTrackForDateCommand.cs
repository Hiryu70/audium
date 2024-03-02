using Audium.Domain;
using Audium.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audium.Application.Tracks;

public class SetRandomTrackForDateCommand : IRequest<long?>
{
    public DateOnly ForDate { get; set; }
}

public class SetRandomTrackForDateCommandHandler : IRequestHandler<SetRandomTrackForDateCommand, long?>
{
    private readonly IMediator _mediator;
    private readonly ILogger<SetRandomTrackForDateCommandHandler> _logger;
    private readonly IDbContextFactory<AudiumDbContext> _dbContextFactory;

    public SetRandomTrackForDateCommandHandler(IDbContextFactory<AudiumDbContext> dbContextFactory, 
        ILogger<SetRandomTrackForDateCommandHandler> logger, IMediator mediator)
    {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<long?> Handle(SetRandomTrackForDateCommand request, CancellationToken cancellationToken)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var today = request.ForDate;

        var tracksQuery = context.Tracks
            .SelectMany(x => x.TrackForDates.DefaultIfEmpty(), (track, history) => new { track, history})
            .GroupBy(x => x.track.Id)
            .Select(x => new { TrackId = x.Key, Count = x.Count()});

        var minRepeatsForTrack = await tracksQuery.MinAsync(x => x.Count, cancellationToken);
        var tracksWithMinRepeats = await tracksQuery.Where(x => x.Count == minRepeatsForTrack).CountAsync(cancellationToken); 
        var randomTrackPosition = new Random().Next(tracksWithMinRepeats-1);

        var randomTrackForDate = await tracksQuery
            .Skip(randomTrackPosition)
            .Select(x => new TrackForDate { TrackId = x.TrackId, ForDate = today })
            .FirstOrDefaultAsync(cancellationToken);

        if (randomTrackForDate == null)
        {
            _logger.LogError(@"Unable to set track for date. Available tracks to set: {tracksCount}. Generated random position: {position}",
                tracksWithMinRepeats,
                randomTrackPosition);

            return default;
        }

        await context.AddAsync(randomTrackForDate, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return randomTrackForDate?.TrackId;
    }
}