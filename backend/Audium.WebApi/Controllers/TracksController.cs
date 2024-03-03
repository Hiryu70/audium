using MediatR;
using Microsoft.AspNetCore.Mvc;
using Audium.Application.Tracks;

namespace Audium.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TracksController : ControllerBase
    {
        private readonly ILogger<TracksController> _logger;
        private readonly IMediator _mediator;

        public TracksController(ILogger<TracksController> logger, IMediator mediator)
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
            var result = await _mediator.Send(new GetTrackMediaForTodayQuery());

            Response.Headers.Add("Content-Disposition", "inline");
            var media = File(result.Content, "audio/mp3", enableRangeProcessing: true);

            return Ok(media);
        }

        /// <summary>
        /// Получить варианты для предиктивного ввода
        /// </summary>
        /// <param name="query">Пользовательский ввод </param>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Количество элементов на странице</param>
        /// <returns>список треков, подходящих под ввод пользователя</returns>
        [HttpGet("suggest-for-input")]
        public async Task<ActionResult<IEnumerable<AristsTracksTitleDto>>> SuggestTracksForInput(
            [FromQuery] string query,
            [FromQuery] int? page,
            [FromQuery] int? pageSize)
        {
            var result = await _mediator.Send(
                new SuggestTracksForInputQuery 
                { 
                    Query = query,
                    Page = page ?? 1,
                    PageSize = pageSize ?? 10
                });

            return Ok(result);
        }

        /// <summary>
        /// Проверить вариант ответа
        /// </summary>
        /// <param name="hiddenTrackId" >Идентификатор отгадываемого трека</param>
        /// <param name="answerArtistName">Имя исполнителя</param>
        /// <param name="answerTrackName">Название трека</param>
        /// <returns>true - если трек отгадан правильно, false - в остальных случаях</returns>
        [HttpGet("check-user-answer")]
        public async Task<IActionResult> CheckUserAnswer(
            [FromQuery] long hiddenTrackId,
            [FromQuery] string answerArtistName,
            [FromQuery] string answerTrackName)
        {
            var result = await _mediator.Send(
                new CheckUserAnswerQuery
                {
                    HiddenTrackId = hiddenTrackId,
                    AnswerArtistName = answerArtistName,
                    AnswerTrackName = answerTrackName
                });

            return Ok(result);
        }
    }
}