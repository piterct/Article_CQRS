using Article.Domain.Commands.Like;
using Article.Domain.Handlers;
using Article.Domain.Repositories;
using Article.Shared.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Article.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class LikeController : BaseController
    {
        private readonly ILogger<LikeController> _logger;
        private readonly ILikeRepository _likeRepository;
        public LikeController(ILogger<LikeController> logger, ILikeRepository likeRepository)
        {
            _logger = logger;
            _likeRepository = likeRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("quantityLikesByArticle/{article_ID}")]
        //[ResponseCache(Duration = 300)]
        public async ValueTask<IActionResult> GetQuantityLikesByArticle(Guid article_ID)
        {
            try
            {
                var likes = await _likeRepository.GetQuantityLikesByArticle(article_ID);

                return GetResult(new GenericCommandResult(true, "Success", likes, StatusCodes.Status200OK, null));
            }
            catch (Exception exception)
            {
                _logger.LogError("An exception has occurred at {dateTime}. " +
                 "Exception message: {message}." +
                 "Exception Trace: {trace}", DateTime.UtcNow, exception.Message, exception.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("allLikes")]
        public async ValueTask<IActionResult> GetAllArticles()
        {
            try
            {
                var likes = await _likeRepository.GetAllLikes();

                return GetResult(new GenericCommandResult(true, "Success", likes, StatusCodes.Status200OK, null));
            }
            catch (Exception exception)
            {
                _logger.LogError("An exception has occurred at {dateTime}. " +
                 "Exception message: {message}." +
                 "Exception Trace: {trace}", DateTime.UtcNow, exception.Message, exception.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("registerLike")]
        public async ValueTask<IActionResult> Post([FromBody] AddLikeArticleCommand command, [FromServices] LikeHandler handler)
        {
            try
            {
                var result = await handler.Handle(command);

                return GetResult((GenericCommandResult)result);
            }
            catch (Exception exception)
            {
                _logger.LogError("An exception has occurred at {dateTime}. " +
                 "Exception message: {message}." +
                 "Exception Trace: {trace}", DateTime.UtcNow, exception.Message, exception.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("deleteLike")]
        public async ValueTask<IActionResult> Delete([FromBody] DeleteLikeCommand command, [FromServices] LikeHandler handler)
        {
            try
            {
                var result = await handler.Handle(command);

                return GetResult((GenericCommandResult)result);
            }
            catch (Exception exception)
            {
                _logger.LogError("An exception has occurred at {dateTime}. " +
                 "Exception message: {message}." +
                 "Exception Trace: {trace}", DateTime.UtcNow, exception.Message, exception.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
