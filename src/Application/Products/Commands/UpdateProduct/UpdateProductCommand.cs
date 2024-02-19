using System.Text.Json.Serialization;
using Tekton.Application.Products.Dtos;

namespace Tekton.Application.Products.Commands.CreateProduct;
public record UpdateProductCommand : IRequest<ProductDto>
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; init; } = string.Empty;
    public string StatusName { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public int Stock { get; set; }
    public decimal Price { get; init; }
}
