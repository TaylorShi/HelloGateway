using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tesla.Ordering.Api.Application.Commands
{
    public class SearchOrderCommandHandler : IRequestHandler<SearchOrderCommand, List<string>>
    {
        public async Task<List<string>> Handle(SearchOrderCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new List<string> { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() });
        }
    }
}
