using Audium.Persistance;
using Audium.WebApi;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Audium.Domain;
using Audium.Application.Helpers;

namespace Audium.Application.Tracks;

public class GetTrackForTodayQuery : IRequest<TrackContentDto>
{
}

public class GetTrackForTodayQueryHandler : IRequestHandler<GetTrackForTodayQuery, TrackContentDto>
{
    private readonly IDbContextFactory<AudiumDbContext> _dbContextFactory;
    private readonly IMediator _mediator;

    public GetTrackForTodayQueryHandler(IDbContextFactory<AudiumDbContext> dbContextFactory, IMediator mediator)
    {
        _dbContextFactory = dbContextFactory;
        _mediator = mediator;
    }

    public async Task<TrackContentDto> Handle(GetTrackForTodayQuery request, CancellationToken cancellationToken)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);

        var tracks = await context.TrackForDate.ToListAsync(cancellationToken);

        var trackForToday = await context.TrackForDate
            .Where(x => x.ForDate == today)
            .Select(x => x.Track)
            .FirstOrDefaultAsync(cancellationToken);

        var trackId = trackForToday?.Id ?? default;
        var fileName = trackForToday?.StorageFileName;

        var content = TrackContentHelper.GetBinaryContentForTrack(trackForToday);

        return new TrackContentDto
        {
            TrackId = trackId,
            FileName = fileName,
            Content = content
        };
    }
}