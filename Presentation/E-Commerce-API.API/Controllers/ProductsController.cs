using E_Commerce_API.Application.Abstraction.Storage;
using E_Commerce_API.Application.Features.Commands.CreateProduct;
using E_Commerce_API.Application.Features.Commands.RemoveProduct;
using E_Commerce_API.Application.Features.Commands.UpdateProduct;
using E_Commerce_API.Application.Features.Queries.GetAllProduct;
using E_Commerce_API.Application.Features.Queries.GetByIdProduct;
using E_Commerce_API.Application.Repository;
using E_Commerce_API.Application.RequestParameters;
using E_Commerce_API.Application.ViewModels.Products;
using E_Commerce_API.Domain.Entities;
using E_Commerce_API.Persistence.Repository;
using E_Commerce_API.Persistence.Repository.File;
using MediatR;
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
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        private readonly IProductImageFileReadRepository _productImageFileReadRepository;
        private readonly IFileWriteRepository _fileWriteRepository;
        private readonly IFileReadRepository _fileReadRepository;
        private readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;
        private readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        private readonly IStorageService _storageService;

        private readonly IMediator _mediator;
        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment, IProductImageFileWriteRepository productImageFileWriteRepository, IProductImageFileReadRepository productImageFileReadRepository, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IInvoiceFileWriteRepository invoiceFileWriteRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IStorageService storageService, IMediator mediator)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _fileWriteRepository = fileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _storageService = storageService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse products = await _mediator.Send(getAllProductQueryRequest);
            return Ok(products);
        }

        [HttpGet("{id}")]   // fromRoute doesnt workking ! Fix there 
        public async Task<IActionResult> GetById([FromRoute]GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            GetByIdProductQueryResponse products = await _mediator.Send(getByIdProductQueryRequest);
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateProductCommandRequest updateProductCommandRequest)
        {
            var response = await _mediator.Send(updateProductCommandRequest);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]RemoveProductCommandRequest removeProductCommandRequest)
        {
            var response = await _mediator.Send(removeProductCommandRequest);
            return Ok();
        }

        //[HttpPost("[action]")]
        //public async Task<IActionResult> Upload(IFormFileCollection files)
        //{
        //    var datas = await _storageService.UploadAsync("resource/files", files);

        //    await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
        //    {
        //        FileName = d.fileName,
        //        Path = d.pathOrContainer,
        //        Storage = _storageService.StorageName,
        //    }).ToList());
        //    await _productImageFileWriteRepository.SaveAsync();

        //    return Ok();
        //}
    }
}
