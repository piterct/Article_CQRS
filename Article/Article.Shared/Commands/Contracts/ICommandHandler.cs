using Article.Shared.Commands.Contracts;
using System.Threading.Tasks;

namespace Article.Shared.Commands
{
    public interface  ICommandHandler<T> where T: ICommand
    {
        ValueTask<ICommandResult> Handle(T command);
    }
}
