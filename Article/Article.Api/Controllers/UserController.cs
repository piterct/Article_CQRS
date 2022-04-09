using Article.Domain.Commands.User;
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
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUserHandler _userHandler;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("{user_ID}")]
        public async ValueTask<IActionResult> Get(Guid user_ID)
        {
            try
            {
                var user = await _userRepository.GetUser(user_ID);

                if (user == null)
                    return GetResult(new GenericCommandResult(false, "User Not Found!", user, StatusCodes.Status404NotFound, null));

                return GetResult(new GenericCommandResult(true, "Success", user, StatusCodes.Status200OK, null));
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
        [Route("registerUser")]
        public async ValueTask<IActionResult> Post([FromBody] CreateUserCommand command, [FromServices] UserHandler handler)
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
        [Route("updateUser")]
        public async ValueTask<IActionResult> Patch([FromBody] UpdateUserCommand command, [FromServices] UserHandler handler)
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
        [Route("deleteUser")]
        public async ValueTask<IActionResult> Delete([FromBody] DeleteUserCommand command, [FromServices] UserHandler handler)
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
