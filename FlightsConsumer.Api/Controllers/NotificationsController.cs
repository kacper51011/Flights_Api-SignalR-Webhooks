using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightsConsumer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Notify()
        {

        }
    }
}
