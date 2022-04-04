using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Azure.ServiceBus.Core;
using Microsoft.AspNetCore.Mvc;
using WebApi.Messages;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IBus _bus;

        public HomeController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost("inqueue")]
        public async Task<IActionResult> Post()
        {
            //Sobre o Scheduler do Azure Service Bus
            //https://medium.com/event-driven-utopia/azure-service-bus-essentials-scheduled-messages-98fd542f9d55
            //https://masstransit-project.com/advanced/scheduling/azure-sb-scheduler.html

            // Sobre enviar via "Send" (Queue) ou "Publish" (Topic)
            //https://stackoverflow.com/questions/62713786/masstransit-endpointconvention-azure-service-bus/62714778#62714778
            await _bus.Publish(new MyMessage { Content = DateTime.UtcNow.ToString() },
               context => context.SetScheduledEnqueueTime(DateTime.UtcNow.AddSeconds(10)));

            return Ok();
        }
    }
}
