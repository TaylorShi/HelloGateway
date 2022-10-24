using GrpcServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tesla.Mobile.ApiAggregator.Models;
using static GrpcServices.OrderGrpc;
using static GrpcServices.PayGrpc;

namespace Tesla.Mobile.ApiAggregator.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        readonly OrderGrpcClient _orderGrpcClient;
        readonly PayGrpcClient _payGrpcClient;

        public OrderController(OrderGrpcClient orderGrpcClient, PayGrpcClient payGrpcClient)
        {
            this._orderGrpcClient = orderGrpcClient;
            this._payGrpcClient = payGrpcClient;
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] SearchOrderRequest searchOrderRequest)
        {
            var infos = new List<SearchOrderInfo>();
            var orderResponse = await _orderGrpcClient.SearchOrderAsync(searchOrderRequest);
            foreach (var o in orderResponse?.Orders?.ToList())
            {
                var payResponse = await _payGrpcClient.QueryPayStatusAsync(new QueryPayStatusRequest() { OrderId = o });
                var orderInfo = new SearchOrderInfo
                {
                    OrderId = o,
                    IsPayed = payResponse?.IsPayed ?? false
                };
                infos.Add(orderInfo);
            }

            return Ok(infos);
        }
    }
}
