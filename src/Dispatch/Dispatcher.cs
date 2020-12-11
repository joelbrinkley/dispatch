using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Dispatch.Abstractions;

namespace Dispatch
{
    public delegate object RequestHandlerLocator(Type serviceType);

    public class Dispatcher : IDispatcher
    {
        readonly RequestHandlerLocator _requestHandlerLocator;

        public Dispatcher(RequestHandlerLocator requestHandlerLocator)
        {
            _requestHandlerLocator = requestHandlerLocator;
        }
        
        public async Task Dispatch(IRequest request, CancellationToken token)
        {
            try
            {
                var requestType = request.GetType();

                var requestInterfaceType =
                    requestType.GetInterfaces()
                        .FirstOrDefault(x => x == typeof(IRequest));

                if (requestInterfaceType == null) throw new Exception("Unable to resolve request interface type.");

                var handlerType = typeof(IRequestHandler<>).MakeGenericType(request.GetType());

                var requestHandler = _requestHandlerLocator(handlerType);

                var flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod;

                var task = (Task) requestHandler.GetType()
                    .InvokeMember("Handle", flags, null, requestHandler, new object[] {request, token});

                await task;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
