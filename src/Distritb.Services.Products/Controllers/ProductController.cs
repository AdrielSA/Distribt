using Distritb.Services.Products.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Distritb.Services.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet(nameof(GetById))]
        public Task<ProductDto> GetById(Guid productId)
        {
            return Task.FromResult(new ProductDto(productId, "Test Product", "Test Description"));
        }

        [HttpPost(nameof(Add))]
        public Task<Guid> Add(ProductDto product)
        {
            return Task.FromResult(Guid.NewGuid());
        }
    }
}
