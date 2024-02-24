using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Audium.Application.Helpers;
using Audium.Domain;
using Audium.Persistance;
using Audium.WebApi;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Audium.Application.Tracks.GetRandomTrackQueryHandler;

namespace Audium.Application.Tracks;

public class GetRandomTrackQuery : IRequest<TrackContentDto>
{
}

public class GetRandomTrackQueryHandler : IRequestHandler<GetRandomTrackQuery, TrackContentDto>
{
    private readonly IDbContextFactory<AudiumDbContext> _dbContextFactory;

    public GetRandomTrackQueryHandler(IDbContextFactory<AudiumDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<TrackContentDto> Handle(GetRandomTrackQuery request, CancellationToken cancellationToken)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var countTracks = await context.Tracks.CountAsync(cancellationToken);
        var randomTrackPosition = new Random(1).Next(1, countTracks);

        var randomTrack = await context.Tracks
            .Skip(randomTrackPosition - 1)
            .FirstOrDefaultAsync(cancellationToken);

        var trackId = randomTrack.Id;
        var fileName = randomTrack.StorageFileName;

        var content = TrackContentHelper.GetBinaryContentForTrack(randomTrack);

        return new TrackContentDto
        {
            TrackId = trackId,
            FileName = fileName,
            Content = content
        };
    }
}
