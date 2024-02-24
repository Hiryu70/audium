using MediatR;
using Microsoft.AspNetCore.Mvc;
using Audium.Application.Tracks;

namespace Audium.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AudiumController : ControllerBase
    {
        private readonly ILogger<AudiumController> _logger;
        private readonly IMediator _mediator;

        public AudiumController(ILogger<AudiumController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        /// <summary>
        /// Получить трек для текущего дня
        /// </summary>
        /// <returns>Получить медиа-файл на сегодня</returns>
        [HttpGet("GetTrackForToday")]
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
        /// <returns>Получить случайный трек</returns>
        [HttpGet("GetRandomTrack")]
        public async Task<IActionResult> GetRandomTrack()
        {
            var result = await _mediator.Send(new GetRandomTrackQuery());

            Response.Headers.Add("Content-Disposition", "inline");
            var media = File(result.Content, "audio/mp3", enableRangeProcessing: true);

            return media;
        }
    }
}