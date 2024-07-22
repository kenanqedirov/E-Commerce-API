using E_Commerce_API.Application.Repository;
using E_Commerce_API.Application.RequestParameters;
using E_Commerce_API.Application.ViewModels.Products;
using E_Commerce_API.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace E_Commerce_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository,IWebHostEnvironment webHostEnvironment)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Skip(pagination.Size * pagination.Page).Take(pagination.Size)
                .Select(x => new
            {
                x.Id,
                x.Name,
                x.Price,
                x.Stock,
                x.CreatedDate,
                x.UpdatedDate,
            }).ToList();
            return Ok(new {products,totalCount});
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
           return Ok(await _productReadRepository.GetByIdAsync(id,false));
        }
        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {
                await _productWriteRepository.AddAsync(new()
                {
                    Name = model.Name,
                    Price = model.Price,
                    Stock = model.Stock,
                });
                await _productWriteRepository.SaveAsync();
                return StatusCode((int)HttpStatusCode.Created);
            

            
        }
        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Stock = model.Stock;
            product.Price = model.Price;
            product.Name = model.Name;
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/product-images");
            foreach (IFormFile file in Request.Form.Files)
            {
                string fullPath = Path.Combine(uploadPath,Guid.NewGuid().ToString(),Path.GetExtension(file.FileName));
                using FileStream fileStream = new(fullPath, FileMode.Create,FileAccess.Write,FileShare.None,1024*1024,useAsync:false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }
            return Ok();
        }
    }
}
