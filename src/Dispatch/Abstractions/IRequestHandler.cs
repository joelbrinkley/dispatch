using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dispatch.Abstractions
{
    public interface IRequestHandler
    {

    }
    public interface IRequestHandler<in TRequest>: IRequestHandler
    {
        Task Handle(TRequest request, CancellationToken token);
    }
}
