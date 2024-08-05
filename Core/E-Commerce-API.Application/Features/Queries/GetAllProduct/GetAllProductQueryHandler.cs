using E_Commerce_API.Application.Repository;
using E_Commerce_API.Application.RequestParameters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_API.Application.Features.Queries.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetAllProductQueryHandler(IProductReadRepository repository)
        {
            _productReadRepository = repository;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Skip(request.Size * (request.Page)).Take(request.Size)
            .Select(x => new
            {
                x.Id,
                x.Name,
                x.Price,
                x.Stock,
                x.CreatedDate,
                x.UpdatedDate,
            }).ToList();

            return new GetAllProductQueryResponse()
            {
                Products = products,
                TotalCount = totalCount
            };
        }
    }
}
