using System.Net.NetworkInformation;
using Tekton.Application.Common.Interfaces;
using Tekton.Application.Products.Common;
using Tekton.Application.Products.Dtos;

namespace Tekton.Application.Products.Queries.GetById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStatusProductService _statusProductService;

    public GetProductByIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStatusProductService statusProductService)
    {
        _context = context;
        _mapper = mapper;
        _statusProductService = statusProductService;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var _status = await _statusProductService.GetProductStatus();

        var result = await _context.Products
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync();

        Guard.Against.NotFound(request.Id, result);

        return MapToProductDTO.MapEntityToProductDTO(result!, _status);
    }
}
