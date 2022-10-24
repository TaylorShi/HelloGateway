using Grpc.Core;
using GrpcServices;
using MediatR;
using System.Threading.Tasks;
using Tesla.Ordering.Api.Application.Commands;

namespace Tesla.Ordering.Api.GrpcServices
{
    public class OrderService : OrderGrpc.OrderGrpcBase
    {
        readonly IMediator _mediator;
        public OrderService(IMediator mediator)
        {
            this._mediator = mediator;
        }

        public override async Task<SearchOrderResponse> SearchOrder(SearchOrderRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new SearchOrderCommand(request.UserName));
            var response = new SearchOrderResponse();
            response.Orders.AddRange(result);
            return await Task.FromResult(response);
        }
    }
}
