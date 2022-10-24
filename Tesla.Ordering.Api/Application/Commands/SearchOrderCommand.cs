using MediatR;
using System.Collections.Generic;

namespace Tesla.Ordering.Api.Application.Commands
{
    public class SearchOrderCommand : IRequest<List<string>>
    {
        public string UserName { get; private set; }

        public SearchOrderCommand(string userName)
        {
            UserName = userName;
        }
    }
}
