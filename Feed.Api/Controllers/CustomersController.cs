using Feed.Api.Kafka.Producers;
using Feed.Api.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Feed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerProducer _customerProducer;
        public CustomersController(CustomerProducer customerProducer) => _customerProducer = customerProducer;


        [HttpPost]
        public async Task New(Customer customer)
        {
            await _customerProducer.AddAsync(customer);
        }
    }
}