using Grpc.Core;
using GrpcServices;
using MediatR;
using System.Threading.Tasks;
using Tesla.Pay.Api.Application.Commands;

namespace Tesla.Pay.Api.GrpcServices
{
    public class PayService : PayGrpc.PayGrpcBase
    {
        readonly IMediator _mediator;
        public PayService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<QueryPayStatusResponse> QueryPayStatus(QueryPayStatusRequest request, ServerCallContext context)
        {
            var result = await _mediator.Send(new QueryPayStatusCommand(request.OrderId));
            var response = new QueryPayStatusResponse();
            response.IsPayed = result;
            return await Task.FromResult(response);
        }
    }
}
