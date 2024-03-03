using Audium.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audium.Application.Tracks;

public class CheckUserAnswerQuery : IRequest<bool>
{
    public long HiddenTrackId { get; set; }
    public string AnswerArtistName { get; set; }
    public string AnswerTrackName { get; set; }
}

public class CheckUserAnswerQueryHandler : IRequestHandler<CheckUserAnswerQuery, bool>
{
    private readonly IDbContextFactory<AudiumDbContext> _dbContextFactory;

    public CheckUserAnswerQueryHandler(IDbContextFactory<AudiumDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<bool> Handle(CheckUserAnswerQuery request, CancellationToken cancellationToken)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var track = await context.Tracks
            .AsNoTracking()
            .Where(x => x.Id == request.HiddenTrackId)
            .Include(x => x.Artist)
            .FirstOrDefaultAsync(cancellationToken);

        return track?.Artist.ArtistName == request.AnswerArtistName 
            && track?.TrackName == request.AnswerTrackName;
    }
}

