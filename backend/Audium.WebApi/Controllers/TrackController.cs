using MediatR;
using Microsoft.AspNetCore.Mvc;
using Audium.Application.Tracks;

namespace Audium.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrackController : ControllerBase
    {
        private readonly ILogger<TrackController> _logger;
        private readonly IMediator _mediator;

        public TrackController(ILogger<TrackController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        /// <summary>
        /// Получить трек для текущего дня
        /// </summary>
        /// <returns>трек для текущего дня</returns>
        [HttpGet("for-today")]
        public async Task<IActionResult> GetTrackForToday()
        {
            var result = await _mediator.Send(new GetTrackForTodayQuery());

            Response.Headers.Add("Content-Disposition", "inline");
            var media = File(result.Content, "audio/mp3", enableRangeProcessing: true);

            return media;

        }

        /// <summary>
        /// Получить случайный трек
        /// </summary>
        /// <returns>случайный трек</returns>
        [HttpGet("random")]
        public async Task<IActionResult> GetRandomTrack()
        {
            var result = await _mediator.Send(new GetRandomTrackQuery());

            Response.Headers.Add("Content-Disposition", "inline");
            var media = File(result.Content, "audio/mp3", enableRangeProcessing: true);

            return media;
        }
    }
}