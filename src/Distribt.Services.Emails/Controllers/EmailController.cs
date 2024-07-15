using Distribt.Services.Emails.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Distribt.Services.Emails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpPost(nameof(Send))]
        public Task<bool> Send(EmailDto emailDto)
        {
            return Task.FromResult(true);
        }
    }
}
