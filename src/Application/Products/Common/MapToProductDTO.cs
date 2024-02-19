using Tekton.Application.Products.Dtos;
using Tekton.Domain.Entities;

namespace Tekton.Application.Products.Common;
public static class MapToProductDTO
{
    public static ProductDto MapEntityToProductDTO(
        Product entity,
        Dictionary<int, string> status)
    {
        ProductDto productDto = new ProductDto()
        {
            Id = entity.Id,
            Description = entity.Description,
            Discount = entity.Discount,
            FinalPrice = entity.FinalPrice,
            Name = entity.Name,
            Price = entity.Price,
            StatusName = status.FirstOrDefault(x => x.Key == entity.Status).Value,
            Stock = entity.Stock,
        };

        return productDto;
    }
}
