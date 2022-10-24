using MediatR;
using System.Collections.Generic;

namespace Tesla.Pay.Api.Application.Commands
{
    public class QueryPayStatusCommand : IRequest<bool>
    {
        public string OrderId { get; private set; }

        public QueryPayStatusCommand(string orderId)
        {
            OrderId = orderId;
        }
    }
}
