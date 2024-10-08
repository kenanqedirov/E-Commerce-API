﻿using E_Commerce_API.Application.Features.Commands.CreateProduct;
using E_Commerce_API.Application.Features.Commands.RemoveProduct;
using E_Commerce_API.Application.Features.Commands.UpdateProduct;
using E_Commerce_API.Application.Features.Queries.GetAllProduct;
using E_Commerce_API.Application.Features.Queries.GetByIdProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace E_Commerce_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {           
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse products = await _mediator.Send(getAllProductQueryRequest);
            return Ok(products);
        }

        //[HttpGet("{id}")]   // fromRoute doesnt workking ! Fix there 
        //public async Task<IActionResult> GetById([FromRoute]GetByIdProductQueryRequest getByIdProductQueryRequest)
        //{
        //    GetByIdProductQueryResponse products = await _mediator.Send(getByIdProductQueryRequest);
        //    return Ok(products);
        //}

        [HttpGet("{id}")]   // fromRoute doesnt workking ! Fix there 
        public async Task<IActionResult> GetById(string id)
        {
            GetByIdProductQueryResponse products = await _mediator.Send(new GetByIdProductQueryRequest { Id = id});
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
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _mediator.Send(new RemoveProductCommandRequest { Id = id});
            return Ok();
        }


        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete([FromRoute]RemoveProductCommandRequest removeProductCommandRequest)
        //{
        //    var response = await _mediator.Send(removeProductCommandRequest);
        //    return Ok();
        //}



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
