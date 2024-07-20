using Distribt.Services.Orders.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Distribt.Services.Orders.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet(nameof(GetById))]
        public Task<OrderDto> GetById(Guid orderId)
        {
            return Task.FromResult(new OrderDto(orderId));
        }

        [HttpPost(nameof(Add))]
        public Task<Guid> Add(OrderDto orderDto)
        {
            return Task.FromResult(Guid.NewGuid());
        }
    }
}
