using Article.Domain.Commands.Article;
using Article.Shared.Commands.Contracts;
using System.Threading.Tasks;

namespace Article.Domain.Handlers.Interfaces
{
    public  interface IArticleHandler
    {
        ValueTask<ICommandResult> Handle(CreateArticleCommand command);
    }
}
