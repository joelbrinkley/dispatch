using System.Threading;
using System.Threading.Tasks;

namespace Dispatch.Abstractions
{
    public interface IDispatcher
    {
        Task Dispatch(IRequest request, CancellationToken token);
    }
}
