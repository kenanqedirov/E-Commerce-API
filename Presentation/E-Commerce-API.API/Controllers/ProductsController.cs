using E_Commerce_API.Application.Repository;
using E_Commerce_API.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        [HttpGet]
        public async Task Get()
        {
            // await _productWriteRepository.AddRangeAsync(new()
            // {
            //     new() { Id=Guid.NewGuid() , Name = "Product 1", Price=100,CreatedDate=DateTime.Now , Stock=10},
            //     new() { Id=Guid.NewGuid() , Name = "Product 2", Price=200,CreatedDate=DateTime.Now , Stock=20},
            //     new() { Id=Guid.NewGuid() , Name = "Product 3", Price=300,CreatedDate=DateTime.Now , Stock=30},
            // });
            //var count = await _productWriteRepository.SaveAsync();

            Product product = await _productReadRepository.GetByIdAsync("f5fe31f7-389f-4c1d-aafd-1a881d91a85a",false);
            product.Name = "Kenan2";
            await _productWriteRepository.SaveAsync();     // example
        }
 
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Product product = await _productReadRepository.GetByIdAsync(id);
            return Ok(product);
        }
    }
}
