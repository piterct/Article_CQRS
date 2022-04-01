using Article.Domain.Commands.Like;
using Article.Shared.Commands.Contracts;
using System.Threading.Tasks;

namespace Article.Domain.Handlers.Interfaces
{
    public interface  ILikeHandler
    {
        ValueTask<ICommandResult> Handle(AddLikeArticleCommand command)
    }
}
