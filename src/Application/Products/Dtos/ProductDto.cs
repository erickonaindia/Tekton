using Tekton.Application.Common.Interfaces;
using Tekton.Domain.Entities;

namespace Tekton.Application.Products.Dtos;
public class ProductDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string StatusName { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public int Stock { get; set; }
    public decimal Price { get; init; }
    public decimal Discount { get; init; }
    public decimal FinalPrice { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(x => x.StatusName, opt => opt.MapFrom(x => x.Status == 1 ? "Active" : "Inactive"));
        }
    }
}
