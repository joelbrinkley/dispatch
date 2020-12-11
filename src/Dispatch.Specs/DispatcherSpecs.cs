using System;
using System.Collections.Generic;
using System.Threading;
using Dispatch.Abstractions;
using Dispatch.Specs.Infrastructure;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dispatch.Specs
{
    internal class DispatcherSpecs
    {
        [Subject("Dispatch")]
        class when_dispatching_a_request
        {
            static TestRequestHandler _requestHandler;
            static TestRequest _testRequest;
            static Dispatcher _dispatcher;

            Establish context = () =>
            {
                _testRequest = new TestRequest();

                _requestHandler = new TestRequestHandler();

                var dictionary = new Dictionary<Type, IRequestHandler>
                {
                    {typeof(IRequestHandler<TestRequest>), _requestHandler}
                };

                _dispatcher = new Dispatcher(serviceType => dictionary[serviceType]);
            };

            Because of = () => _dispatcher.Dispatch(_testRequest, CancellationToken.None).Await();

            It should_dispatch_to_handler = () => _requestHandler.ShouldHandleRequest(_testRequest);
        }
    }
}