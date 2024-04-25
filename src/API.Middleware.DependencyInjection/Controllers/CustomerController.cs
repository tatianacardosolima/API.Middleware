using Bogus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Bogus.DataSets.Name.Gender;

namespace API.Cache.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        

        private readonly ILogger<CustomerController> _logger;
   

        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
            
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(GetCustomers());
        }
        
        [HttpPost]
        public ActionResult Save()
        {
            return Ok();
        }

        private List<Customers> GetCustomers()
        { 
           
            var list = new List<Customers>();
            var faker = new Faker();

            for (var x = 1; x <= 1000; x++)
            {
                list.Add(new Customers() { Id = x, Name = faker.Name.FirstName(x%2==0 ? Female: Male) });
            }

            return list;
        }


    }
    public class Customers
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
