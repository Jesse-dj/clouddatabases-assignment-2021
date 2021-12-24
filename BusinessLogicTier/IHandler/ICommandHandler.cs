using System.Threading.Tasks;

namespace BusinessTier.IHandler
{
    public interface ICommandHandler<ICommand>
    {
        public Task Handle(ICommand command);
    }
}
