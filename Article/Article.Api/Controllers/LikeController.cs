using Article.Domain.Commands.Like;
using Article.Domain.Handlers.Interfaces;
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
        private readonly ILikeHandler _likeHandler;
        public LikeController(ILogger<LikeController> logger, ILikeRepository likeRepository, ILikeHandler likeHandler)
        {
            _logger = logger;
            _likeRepository = likeRepository;
            _likeHandler = likeHandler;
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

                return GetResult(new GenericCommandResult(true, "Success !", likes, StatusCodes.Status200OK, null));
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
        public async ValueTask<IActionResult> Post([FromBody] AddLikeArticleCommand command)
        {
            try
            {
                var result = await _likeHandler.Handle(command);

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
        public async ValueTask<IActionResult> Delete([FromBody] DeleteLikeCommand command)
        {
            try
            {
                var result = await _likeHandler.Handle(command);

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
