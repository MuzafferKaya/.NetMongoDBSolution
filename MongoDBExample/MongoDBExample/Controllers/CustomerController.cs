using Microsoft.AspNetCore.Mvc;
using MongoDBExample.Models;
using MongoDBExample.Services;

namespace MongoDBExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly MongoDBService _mongoDBService;

        public CustomerController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] Customer customer)
        {
            await _mongoDBService.CreateAsync(customer);
            return Created(string.Empty, customer);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var Customers = await _mongoDBService.GetAllAsync();
            return Ok(Customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var Customer = await _mongoDBService.GetByIdAsync(id);
            return Ok(Customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] Customer customer)
        {
            await _mongoDBService.UpdateAsync(id, customer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _mongoDBService.DeleteAsync(id);
            return NoContent();
        }
    }
}
