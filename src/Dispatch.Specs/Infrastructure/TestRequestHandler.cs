using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dispatch.Abstractions;
using Machine.Specifications;

namespace Dispatch.Specs.Infrastructure
{
    public class TestRequestHandler : IRequestHandler<TestRequest>
    {
        readonly List<TestRequest> _handledRequests = new List<TestRequest>();

        public Task Handle(TestRequest request, CancellationToken token)
        {
            _handledRequests.Add(request);
            return Task.CompletedTask;
        }

        public void ShouldHandleRequest(TestRequest request)
        {
            _handledRequests.ShouldContain(request);
        }
    }
}
