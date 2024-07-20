using Distritb.Services.Subscriptions.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Distritb.Services.Subscriptions.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        [HttpPost(nameof(Subscribe))]
        public Task<bool> Subscribe(SubscriptionDto subscription)
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
