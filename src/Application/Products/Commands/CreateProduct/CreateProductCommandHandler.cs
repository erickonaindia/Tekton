using Tekton.Application.Common.Interfaces;
using Tekton.Application.Products.Common;
using Tekton.Application.Products.Dtos;
using Tekton.Domain.Entities;
using Tekton.Domain.Events;

namespace Tekton.Application.Products.Commands.CreateProduct;
internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStatusProductService _statusProductService;
    private readonly IDiscountProductService _discountProductService;
    public CreateProductCommandHandler(
        IApplicationDbContext context, 
        IMapper mapper,
        IStatusProductService statusProductService,
        IDiscountProductService discountProductService)
    {
        _context = context;
        _mapper = mapper;
        _statusProductService = statusProductService;
        _discountProductService = discountProductService;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        //1 is by default for all products
        decimal _discount = await _discountProductService.GetProductDiscountAsync(1);
        decimal _finalPrice = request.Price * (100 - _discount) / 100;
        var _status = await _statusProductService.GetProductStatus();

        var entity = new Product
        {
            Description = request.Description,
            Name = request.Name,
            Price = request.Price,
            Status = _status.FirstOrDefault(x => string.Equals(x.Value, request.StatusName, StringComparison.OrdinalIgnoreCase)).Key,
            Stock = request.Stock,
            Discount = _discount,
            FinalPrice = _finalPrice,
        };

        entity.AddDomainEvent(new ProductCreatedEvent(entity));

        _context.Products.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return MapToProductDTO.MapEntityToProductDTO(entity, _status);

    }
}
