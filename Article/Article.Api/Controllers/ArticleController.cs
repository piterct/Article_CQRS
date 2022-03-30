using Article.Domain.Commands.Article;
using Article.Domain.Handlers;
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
    public class ArticleController : BaseController
    {
        private readonly ILogger<ArticleController> _logger;
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleHandler _articleHandler;

        public ArticleController(ILogger<ArticleController> logger, IArticleRepository articleRepository)
        {
            _logger = logger;
            _articleRepository = articleRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{article_ID}")]
        public async ValueTask<IActionResult> Get(Guid article_ID)
        {
            try
            {
                var article = await _articleRepository.GetArticle(article_ID);

                if (article == null)
                    return GetResult(new GenericCommandResult(false, "Article Not Found!", article, StatusCodes.Status404NotFound, null));

                return GetResult(new GenericCommandResult(true, "Success", article, StatusCodes.Status200OK, null));
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
        [Route("allArticles")]
        public async ValueTask<IActionResult> GetAllArticles()
        {
            try
            {
                var articles = await _articleRepository.GetAllArticles();

                return GetResult(new GenericCommandResult(true, "Success", articles, StatusCodes.Status200OK, null));
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
        [Route("registerArticle")]
        public async ValueTask<IActionResult> Post([FromBody] CreateArticleCommand command, [FromServices] ArticleHandler handler)
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


        [HttpPatch]
        [AllowAnonymous]
        [Route("updateArticle")]
        public async ValueTask<IActionResult> Patch([FromBody] UpdateArticleCommand command, [FromServices] ArticleHandler handler)
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
        [Route("deleteArticle")]
        public async ValueTask<IActionResult> Delete([FromBody] DeleteArticleCommand command, [FromServices] ArticleHandler handler)
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
