using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tesla.Pay.Api.Application.Commands
{
    public class QueryPayStatusCommandHandler : IRequestHandler<QueryPayStatusCommand, bool>
    {
        public async Task<bool> Handle(QueryPayStatusCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(true);
        }
    }
}
