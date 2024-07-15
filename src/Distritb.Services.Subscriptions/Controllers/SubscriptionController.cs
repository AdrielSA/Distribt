using Distritb.Services.Subscriptions.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Distritb.Services.Subscriptions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        [HttpPost(nameof(Suscribe))]
        public Task<bool> Suscribe(SubscriptionDto subscription)
        {
            return Task.FromResult(true);
        }

        [HttpDelete(nameof(Unsubscribe))]
        public Task<bool> Unsubscribe(SubscriptionDto subscription)
        {
            return Task.FromResult(true);
        }
    }
}
