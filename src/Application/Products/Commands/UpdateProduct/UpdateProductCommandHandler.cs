using Tekton.Application.Common.Interfaces;
using Tekton.Application.Products.Dtos;

namespace Tekton.Application.Products.Commands.CreateProduct;
internal class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStatusProductService _statusProductService;
    private readonly IDiscountProductService _discountProductService;

    public UpdateProductCommandHandler(
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

    public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products
            .FindAsync(new object[] { request.Id }, cancellationToken);

        var _status = await _statusProductService.GetProductStatus();
        decimal _discount = await _discountProductService.GetProductDiscountAsync(request.Id);
        decimal _finalPrice = request.Price * (100 - _discount) / 100;

        Guard.Against.NotFound(request.Id, entity);

        entity.Description = request.Description;
        entity.Stock = request.Stock;
        entity.Price = request.Price;
        entity.Name = request.Name;
        entity.Status = _status.FirstOrDefault(x => x.Value.ToLower() == request.StatusName.ToLower()).Key;
        entity.FinalPrice = _finalPrice;

        await _context.SaveChangesAsync(cancellationToken);

        return await _context.Products
           .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
           .FirstAsync(x => x.Id == entity.Id);

    }
}
