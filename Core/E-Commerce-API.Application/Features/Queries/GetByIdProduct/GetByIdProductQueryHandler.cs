using E_Commerce_API.Application.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_API.Application.Features.Queries.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        private readonly IProductReadRepository _repository;

        public GetByIdProductQueryHandler(IProductReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
           var products = await _repository.GetByIdAsync(request.Id, false);
            return new()
            {
                Name = products.Name,
                Price = products.Price,
                Stock = products.Stock, 
            };
        }
    }
}
