using E_Commerce_API.Application.Repository;
using E_Commerce_API.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_API.Application.Features.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IProductReadRepository _repositoryRead;
        private readonly IProductWriteRepository _repositoryWrite;

        public UpdateProductCommandHandler(IProductReadRepository repository, IProductWriteRepository repositoryWrite)
        {
            _repositoryRead = repository;
            _repositoryWrite = repositoryWrite;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Product product = await _repositoryRead.GetByIdAsync(request.Id);
            product.Stock = request.Stock;
            product.Price = request.Price;
            product.Name = request.Name;
            await _repositoryWrite.SaveAsync();
            return new UpdateProductCommandResponse();
        }
    }
}
