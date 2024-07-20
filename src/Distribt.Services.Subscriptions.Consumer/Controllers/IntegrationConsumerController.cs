using Distribt.Shared.Communication.Consumer.Host;
using Distribt.Shared.Communication.Consumer.Manager.Interfaces;
using Distribt.Shared.Communication.Messages;
using Microsoft.AspNetCore.Mvc;

namespace Distritb.Services.Subscriptions.Consumer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IntegrationConsumerController(IConsumerManager<IntegrationMessage> consumerManager) 
        : ConsumerController<IntegrationMessage>(consumerManager)
    {
    }
}
