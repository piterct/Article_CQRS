using Article.Shared.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Article.Api.Controllers
{
    public class BaseController: Controller
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult GetResult(GenericCommandResult result) => StatusCode(result.StatusCode, result);
    }
}
