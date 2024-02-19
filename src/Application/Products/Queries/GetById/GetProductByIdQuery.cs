using Tekton.Application.Products.Dtos;

namespace Tekton.Application.Products.Queries.GetById;
public record GetProductByIdQuery : IRequest<ProductDto>
{
    public int Id { get; init; }
}
